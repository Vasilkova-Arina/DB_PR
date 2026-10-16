using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;

namespace PR32
{
    public partial class CartForm : Form
    {
        private User _user;

        private string ConnStr = $"Server={Properties.Settings.Default.host};" +
                                 $"Uid={Properties.Settings.Default.uid};" +
                                 $"Pwd={Properties.Settings.Default.pwd};" +
                                 $"Database=db29;";
        public CartForm(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            if (_user != null)
            {
                txtFIO.Text = _user.Fio;
                txtFIO.ReadOnly = true;
            }

            LoadPickupPoints();
            BuildGrid();
            RefreshGrid();
        }
        private void LoadPickupPoints()
        {
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                var dt = new DataTable();
                new MySqlDataAdapter(
                    "SELECT PickupPointID, " +
                    "CONCAT(PickupPointCity, ', ', PickupPointStreet, ', ', PickupPointHome) AS Addr " +
                    "FROM PickupPoint ORDER BY PickupPointCity;", conn).Fill(dt);

                comboBoxPickup.DataSource = dt;
                comboBoxPickup.DisplayMember = "Addr";
                comboBoxPickup.ValueMember = "PickupPointID";
            }
        }

        private void BuildGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 30;

            dataGridView1.Columns.Add("Article", "Артикул");
            dataGridView1.Columns.Add("Name", "Наименование");
            dataGridView1.Columns.Add("Price", "Цена");
            dataGridView1.Columns.Add("Discount", "Скидка %");
            dataGridView1.Columns.Add("FinalPrice", "Со скидкой");
            dataGridView1.Columns.Add("Quantity", "Кол-во");

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Удаление",
                Text = "Удалить",
                UseColumnTextForButtonValue = true,
                Width = 90
            });

            foreach (DataGridViewColumn c in dataGridView1.Columns)
                if (c.Name != "Quantity" && c.Name != "Удаление")
                    c.ReadOnly = true;

            dataGridView1.CellContentClick += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                if (dataGridView1.Columns[ev.ColumnIndex].Name != "Удаление") return;

                string art = dataGridView1.Rows[ev.RowIndex].Cells["Article"].Value?.ToString();
                Cart.Remove(art);
                RefreshGrid();
            };

            dataGridView1.CellValueChanged += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                if (dataGridView1.Columns[ev.ColumnIndex].Name != "Quantity") return;

                string art = dataGridView1.Rows[ev.RowIndex].Cells["Артикул"].Value?.ToString();
                int.TryParse(dataGridView1.Rows[ev.RowIndex].Cells["Quantity"].Value?.ToString(), out int q);
                Cart.SetQty(art, q);
                RefreshGrid();
            };

            dataGridView1.CurrentCellDirtyStateChanged += (s, ev) =>
            {
                if (dataGridView1.IsCurrentCellDirty)
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void RefreshGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var it in Cart.Items)
                dataGridView1.Rows.Add(
                    it.Article,
                    it.Name,
                    it.Price.ToString("0.00"),
                    it.Discount.ToString("0.##"),
                    it.FinalPrice.ToString("0.00"),
                    it.Quantity);

            lblTotal.Text = "Итого: " + Cart.Total.ToString("0.00") + " руб.";
            lblDiscount.Text = "Скидка: " + Cart.DiscountSum.ToString("0.00") + " руб.";
        }




        private int GetDeliveryDays()
        {
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                foreach (var it in Cart.Items)
                {
                    var cmd = new MySqlCommand(
                        "SELECT ProductQuantityInStock FROM Product WHERE ProductArticleNumber=@a;", conn);
                    cmd.Parameters.AddWithValue("@a", it.Article);

                    object res = cmd.ExecuteScalar();
                    int stock = (res == null || res == DBNull.Value) ? 0 : Convert.ToInt32(res);
                    if (stock <= 3) return 6;
                }
                return 3;
            }
        }

        private int SaveOrder(string fio, string pickup, string code, int days)
        {
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var cmd = new MySqlCommand(@"
                        INSERT INTO `Order`
                            (OrderDate, OrderDeliveryDate, OrderPickupPoint,
                             OrderClient, OrderCode, OrderStatus)
                        VALUES
                            (NOW(), DATE_ADD(NOW(), INTERVAL @d DAY), @pp,
                             @cl, @code,
                             (SELECT StatusID FROM Status ORDER BY StatusID LIMIT 1));
                        SELECT LAST_INSERT_ID();", conn, tx);

                    cmd.Parameters.AddWithValue("@d", days);
                    cmd.Parameters.AddWithValue("@pp", pickup);
                    cmd.Parameters.AddWithValue("@cl", fio);
                    cmd.Parameters.AddWithValue("@code", int.Parse(code));

                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (var it in Cart.Items)
                    {
                        var ci = new MySqlCommand(
                            "INSERT INTO OrderProduct (OrderID, ProductArticleNumber, ProductCount) " +
                            "VALUES (@o, @a, @c);", conn, tx);
                        ci.Parameters.AddWithValue("@o", id);
                        ci.Parameters.AddWithValue("@a", it.Article);
                        ci.Parameters.AddWithValue("@c", it.Quantity);
                        ci.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return id;
                }
            }
        }

        private void MakeReceipt(int number, string code, int days, string pickup)
        {
            string path = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        $"Талон_{number}.docx");

            Word.Application word = null;
            Word.Document doc = null;

            try
            {
                word = new Word.Application();
                word.Visible = false;
                word.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;   // <-- глушим диалоги

                doc = word.Documents.Add();

                Action<string> line = text =>
                {
                    var p = doc.Content.Paragraphs.Add();
                    p.Range.Text = text;
                    p.Range.InsertParagraphAfter();
                };

                int ticketWidth = 32;

                string lineSeparator = new string('-', ticketWidth);
                string doubleSeparator = new string('=', ticketWidth);

                string FormatRow(string left, string right)
                {
                    int spacesCount = ticketWidth - left.Length - right.Length;
                    if (spacesCount < 1) spacesCount = 1; 
                    return left + new string(' ', spacesCount) + right;
                }

                line(doubleSeparator);
                line("    ТАЛОН НА ПОЛУЧЕНИЕ ЗАКАЗА   ");
                line(doubleSeparator);

                line("Дата: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                line("Номер заказа: " + number);
                line("Пункт выдачи: " + pickup);
                line(lineSeparator);

                line("Состав заказа:");
                foreach (var it in Cart.Items)
                {
                    string leftSide = $" {it.Name} x{it.Quantity}";
                    string rightSide = $"{it.Sum:0.00} руб.";
                    line(FormatRow(leftSide, rightSide));
                }
                line(lineSeparator);

                line(FormatRow("Сумма заказа:", $"{Cart.Total:0.00} руб."));
                line(FormatRow("Сумма скидки:", $"{Cart.DiscountSum:0.00} руб."));
                line(doubleSeparator);

                doc.SaveAs(path,
                    Word.WdSaveFormat.wdFormatXMLDocument);

                doc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            }
            finally
            {
                if (word != null)
                {
                    word.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(word);
                }
            }

            System.Diagnostics.Process.Start(path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Cart.IsEmpty)
            {
                MessageBox.Show("Заказ пуст.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFIO.Text))
            {
                MessageBox.Show("Введите ФИО.");
                return;
            }
            if (comboBoxPickup.SelectedValue == null)
            {
                MessageBox.Show("Выберите пункт выдачи.");
                return;
            }

            try
            {
                string code = new Random().Next(100, 1000).ToString();
                int days = GetDeliveryDays();
                string pickupText = comboBoxPickup.Text;

                int orderId = SaveOrder(txtFIO.Text, pickupText, code, days);
                MakeReceipt(orderId, code, days, pickupText);

                MessageBox.Show($"Заказ №{orderId} оформлен.\nКод получения: {code}",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Cart.Clear();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка оформления: " + ex.Message);
            }
        }
    }
}
