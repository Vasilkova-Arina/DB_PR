using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR32
{
    public partial class ProductForm : Form
    {
        private User _user;
        private Timer timerSon;
        private string _editingArticle = null;
        private bool _photoChanged = false;

        private string ConnStr = $"Server={Properties.Settings.Default.host};" +
                                 $"Uid={Properties.Settings.Default.uid};" +
                                 $"Pwd={Properties.Settings.Default.pwd};" +
                                 $"Database=db29;";

        public ProductForm(User user)
        {
            InitializeComponent();
            _user = user;
            timerSon = new Timer { Interval = 30000 };
            timerSon.Tick += timerSon_Tick;
        }

        private void ActiveForm(Control control)
        {
            control.MouseMove += OnActivity;
            control.MouseDown += OnActivity;
            control.KeyDown += OnActivity;
            control.KeyPress += OnActivity;

            foreach (Control c in control.Controls)
                ActiveForm(c);
        }

        private void OnActivity(object sender, EventArgs e)
        {
            timerSon.Stop();
            timerSon.Start();
        }

        private void timerSon_Tick(object sender, EventArgs e)
        {
            timerSon.Stop();
            this.Hide();
            new Avtoriz().ShowDialog();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            if (_user == null)
                label1.Text = "Гостевой режим " + DateTime.Now.ToString("dd.MM.yyyy");
            else
                label1.Text = "Пользователь: " + _user.Fio;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[]
            {
                "Без сортировки",
                "По возрастанию цены",
                "По убыванию цены"
            });

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[]
            {
                "Все диапазоны",
                "0-9,99%",
                "10-14,99%",
                "15% и более"
            });
            comboBox2.SelectedIndex = 0;

            comboBox1.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            textBox1.TextChanged += textBox1_TextChanged;

            LoadComboBoxes();

            unitProduct.Items.Clear();
            unitProduct.Items.Add("шт.");
            unitProduct.Items.Add("уп.");
            unitProduct.SelectedIndex = 0;

            ApplyFilters();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellPainting += dataGridView1_CellPainting;


            btnViewOrder.Visible = !Cart.IsEmpty;

            btnViewOrder.Click += (s, ev) =>
            {
                new CartForm(_user).ShowDialog();
                btnViewOrder.Visible = !Cart.IsEmpty;
            };

            dataGridView1.CellMouseClick += (s, ev) =>
            {
                if (ev.Button != MouseButtons.Right) return;
                if (ev.RowIndex < 0) return;

                dataGridView1.ClearSelection();
                dataGridView1.Rows[ev.RowIndex].Selected = true;

                AddToCart(dataGridView1.Rows[ev.RowIndex]);
            };
        }

        private void AddToCart(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow) return;

            string art = row.Cells["ProductArticleNumber"].Value?.ToString();
            string name = row.Cells["ProductName"].Value?.ToString();
            decimal pr = Convert.ToDecimal(row.Cells["ProductCost"].Value);

            decimal disc = 0;
            if (row.Cells["ProductDiscountAmount"].Value != null &&
                row.Cells["ProductDiscountAmount"].Value != DBNull.Value)
                disc = Convert.ToDecimal(row.Cells["ProductDiscountAmount"].Value);

            Cart.Add(art, name, pr, disc);
            btnViewOrder.Visible = true;

            MessageBox.Show($"«{name}» добавлен в заказ.");
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var conditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                string search = textBox1.Text.Replace("'", "''");
                conditions.Add($"p.ProductName LIKE '%{search}%'");
            }

            switch (comboBox2.SelectedIndex)
            {
                case 1: conditions.Add("p.ProductDiscountAmount >= 0 AND p.ProductDiscountAmount < 10"); break;
                case 2: conditions.Add("p.ProductDiscountAmount >= 10 AND p.ProductDiscountAmount < 15"); break;
                case 3: conditions.Add("p.ProductDiscountAmount >= 15"); break;
            }

            string where = conditions.Count > 0
                ? " WHERE " + string.Join(" AND ", conditions)
                : "";

            string orderBy = "";
            switch (comboBox1.SelectedIndex)
            {
                case 1: orderBy = " ORDER BY p.ProductCost ASC"; break;
                case 2: orderBy = " ORDER BY p.ProductCost DESC"; break;
            }

            string strCmd = $@"SELECT 
                        p.ProductArticleNumber,
                        p.ProductName,
                        p.ProductUnit,
                        p.ProductCost,
                        p.ProductManufacturer,
                        p.ProductDiscountAmountMax,
                        p.ProductDiscountAmount,
                        p.ProductDescription,
                        p.ProductQuantityInStock,
                        c.CategoryName,
                        s.SalleryName,
                        p.ProductPhoto AS 'Фото',
                        CONCAT(c.CategoryName, ' | ', p.ProductName,
                                '\nАртикул: ', p.ProductArticleNumber,
                                '\nПроизводитель: ', p.ProductManufacturer,
                                '\nПоставщик: ', s.SalleryName,
                                '\nОписание: ', p.ProductDescription,
                                '\nЦена: ', p.ProductCost)   AS 'Информация',
                        p.ProductDiscountAmount             AS 'Скидка'
                    FROM Product p
                    LEFT JOIN Sallery  s ON s.SalleryID  = p.ProductSallery
                    LEFT JOIN Category c ON c.CategoryID = p.ProductCategory"
            + where + orderBy;

            FillDataGrid(strCmd);
            Pagination();
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            int visible = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (row.Visible && !row.IsNewRow) visible++;

            int total;
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                total = Convert.ToInt32(new MySqlCommand(
                    "SELECT COUNT(*) FROM Product;", conn).ExecuteScalar());
            }

            labelCount.Text = visible + " из " + total;
        }

        public void FillDataGrid(string strCmd)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strCmd, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "ProductPhoto";
            imageColumn.HeaderText = "Фото";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageColumn.Width = 150;
            dataGridView1.Columns.Insert(0, imageColumn);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string photoDir = Path.Combine(baseDir, "Resources");

            Image defaultImage = null;
            string defImPath = Path.Combine(photoDir, "glysh.jpg");
            if (File.Exists(defImPath))
            {
                using (var fs = new FileStream(defImPath, FileMode.Open, FileAccess.Read))
                using (var tmp = Image.FromStream(fs))
                    defaultImage = new Bitmap(tmp);
            }

            if (dataGridView1.Columns.Contains("Информация"))
            {
                dataGridView1.Columns["Информация"].Width = 350;
                dataGridView1.Columns["Информация"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            if (dataGridView1.Columns.Contains("Скидка"))
                dataGridView1.Columns["Скидка"].Width = 150;
            if (dataGridView1.Columns.Contains("Фото"))
                dataGridView1.Columns["Фото"].Visible = false;

            dataGridView1.AllowUserToAddRows = false;

            string[] hideCols = {
                "ProductArticleNumber", "ProductName", "ProductUnit", "ProductCost",
                "ProductManufacturer", "ProductDiscountAmountMax", "ProductDiscountAmount",
                "ProductDescription", "ProductQuantityInStock",
                "CategoryName", "SalleryName"
            };
            foreach (var col in hideCols)
                if (dataGridView1.Columns.Contains(col))
                    dataGridView1.Columns[col].Visible = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string name = row.Cells["Фото"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(name))
                {
                    row.Cells["ProductPhoto"].Value = defaultImage;
                    continue;
                }

                name = Path.GetFileName(name);
                string path = Path.Combine(photoDir, name);

                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (var tmp = Image.FromStream(fs))
                        row.Cells["ProductPhoto"].Value = new Bitmap(tmp);
                }
                else
                {
                    row.Cells["ProductPhoto"].Value = defaultImage;
                }
            }
        }

        void Pagination()
        {
            for (int j = this.Controls.Count - 1; j >= 0; --j)
            {
                if (this.Controls[j].Name.StartsWith("page"))
                    this.Controls.RemoveAt(j);
            }

            int rowsCount = dataGridView1.Rows.Count;
            int size = rowsCount / 20;
            if (rowsCount % 20 > 0) size += 1;
            if (size <= 1) return;

            LinkLabel[] ll = new LinkLabel[size];
            int x = 10, y = 480, step = 15;

            for (int i = 0; i < size; ++i)
            {
                ll[i] = new LinkLabel();
                ll[i].Text = Convert.ToString(i + 1);
                ll[i].Name = "page" + i;
                ll[i].AutoSize = true;
                ll[i].Location = new Point(x, y);
                ll[i].Click += new EventHandler(LinkLabel_Click);
                ll[i].LinkColor = Color.FromArgb(102, 255, 102);
                ll[i].Font = new Font("Comic Sans MS", 14f, FontStyle.Bold);

                this.Controls.Add(ll[i]);
                ll[i].BringToFront();

                x += step;
            }

            if (ll.Length > 0)
                LinkLabel_Click(ll[0], EventArgs.Empty);
        }

        void LinkLabel_Click(object sender, EventArgs e)
        {
            LinkLabel l = sender as LinkLabel;
            if (l == null) return;

            foreach (Control ctrl in this.Controls)
                if (ctrl is LinkLabel && ctrl.Name.StartsWith("page"))
                    ((LinkLabel)ctrl).LinkBehavior = LinkBehavior.AlwaysUnderline;

            l.LinkBehavior = LinkBehavior.NeverUnderline;

            int numPage = Convert.ToInt32(l.Text) - 1;
            int countRows = dataGridView1.Rows.Count;
            int sizePage = 20;
            int start = numPage * sizePage;
            int stop = start + sizePage;

            if (dataGridView1.DataSource != null)
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                cm.SuspendBinding();

                for (int j = 0; j < countRows; ++j)
                    dataGridView1.Rows[j].Visible = (j >= start && j < stop);

                cm.ResumeBinding();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timerSon.Stop();
            this.Hide();
            new Menu(_user).ShowDialog();
            this.Show();
            timerSon.Start();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            _editingArticle = row.Cells["ProductArticleNumber"].Value?.ToString();

            nameProduct.Text = row.Cells["ProductName"].Value?.ToString();

            int unitFromDb = 1;
            int.TryParse(row.Cells["ProductUnit"].Value?.ToString(), out unitFromDb);
            unitProduct.SelectedIndex = (unitFromDb >= 1 && unitFromDb <= 2) ? unitFromDb - 1 : 0;

            priceProduct.Text = row.Cells["ProductCost"].Value?.ToString();
            categoryProduct.Text = row.Cells["CategoryName"].Value?.ToString();
            manufProduct.Text = row.Cells["ProductManufacturer"].Value?.ToString();
            salleryProduct.Text = row.Cells["SalleryName"].Value?.ToString();
            maxAmount.Text = row.Cells["ProductDiscountAmountMax"].Value?.ToString();
            amountProduct.Text = row.Cells["ProductDiscountAmount"].Value?.ToString();
            decriptionProduct.Text = row.Cells["ProductDescription"].Value?.ToString();
            countProduct.Text = row.Cells["ProductQuantityInStock"].Value?.ToString();

            imageProduct.Image?.Dispose();
            imageProduct.Image = null;
            _photoChanged = false;

            string photoName = row.Cells["Фото"].Value?.ToString();
            if (!string.IsNullOrWhiteSpace(photoName) && photoName != "glysh.jpg")
            {
                string path = Path.Combine(Application.StartupPath, "Resources", Path.GetFileName(photoName));
                if (File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (var tmp = Image.FromStream(fs))
                        imageProduct.Image = new Bitmap(tmp);
                }
            }
        }

        private string GenerateArticul()
        {
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                int n = 1;
                while (true)
                {
                    string art = "А" + n.ToString("D5");
                    var cmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM Product WHERE ProductArticleNumber=@a;", conn);
                    cmd.Parameters.AddWithValue("@a", art);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0) return art;
                    n++;
                }
            }
        }

        private object NullIfEmpty(string s, bool isInt = false)
        {
            if (string.IsNullOrWhiteSpace(s)) return DBNull.Value;
            return isInt ? (object)int.Parse(s) : (object)decimal.Parse(s);
        }

        private object SavePhoto()
        {
            if (imageProduct.Image == null)
                return "glysh.jpg";

            string dir = Path.Combine(Application.StartupPath, "Resources");
            Directory.CreateDirectory(dir);
            string name = Guid.NewGuid().ToString("N") + ".jpg";

            try
            {
                imageProduct.Image.Save(Path.Combine(dir, name),
                                        System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения фото: " + ex.Message);
                return "glysh.jpg";
            }

            return name;
        }

        private void ClearFields()
        {
            nameProduct.Clear();
            priceProduct.Clear();
            manufProduct.Clear();
            maxAmount.Clear();
            amountProduct.Clear();
            decriptionProduct.Clear();
            countProduct.Clear();

            unitProduct.SelectedIndex = 0;      

            categoryProduct.SelectedIndex = -1;
            salleryProduct.SelectedIndex = -1;

            imageProduct.Image?.Dispose();
            imageProduct.Image = null;
            _editingArticle = null;
            _photoChanged = false;
        }

        private void LoadComboBoxes()
        {
            try
            {
                using (var conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();

                    void Fill(ComboBox cb, string sql, string field)
                    {
                        var dt = new DataTable();
                        new MySqlDataAdapter(sql, conn).Fill(dt);
                        cb.DataSource = dt;
                        cb.DisplayMember = field;
                    }

                    Fill(categoryProduct, "SELECT CategoryName FROM Category ORDER BY CategoryName;", "CategoryName");
                    Fill(salleryProduct, "SELECT SalleryName FROM Sallery ORDER BY SalleryName;", "SalleryName");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка справочников: " + ex.Message);
            }
        }

        private void SaveProduct(bool isNew)
        {
            if (string.IsNullOrWhiteSpace(nameProduct.Text))
            {
                MessageBox.Show("Введите название.");
                return;
            }
            if (!decimal.TryParse(priceProduct.Text, out decimal cost))
            {
                MessageBox.Show("Некорректная цена.");
                return;
            }

            string art = isNew ? GenerateArticul() : _editingArticle;
            if (art == null)
            {
                MessageBox.Show("Выберите товар.");
                return;
            }

            int unitId = unitProduct.SelectedIndex + 1;

            string sql = isNew
                ? @"INSERT INTO Product
                    (ProductArticleNumber, ProductName, ProductUnit, ProductCost,
                     ProductCategory, ProductManufacturer, ProductSallery,
                     ProductDiscountAmountMax, ProductDiscountAmount,
                     ProductDescription, ProductQuantityInStock, ProductPhoto)
                    VALUES
                    (@art, @name, @unit, @cost,
                     (SELECT CategoryID FROM Category WHERE CategoryName=@cat LIMIT 1),
                     @man,
                     (SELECT SalleryID FROM Sallery WHERE SalleryName=@sal LIMIT 1),
                     @maxd, @disc, @descr, @qty, @photo);"
                : @"UPDATE Product SET
                    ProductName=@name, ProductUnit=@unit, ProductCost=@cost,
                    ProductCategory=(SELECT CategoryID FROM Category WHERE CategoryName=@cat LIMIT 1),
                    ProductManufacturer=@man,
                    ProductSallery=(SELECT SalleryID FROM Sallery WHERE SalleryName=@sal LIMIT 1),
                    ProductDiscountAmountMax=@maxd,
                    ProductDiscountAmount=@disc,
                    ProductDescription=@descr,
                    ProductQuantityInStock=@qty"
                   + (_photoChanged ? ", ProductPhoto=@photo" : "")
                   + " WHERE ProductArticleNumber=@art;";

            try
            {
                using (var conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@art", art);
                    cmd.Parameters.AddWithValue("@name", nameProduct.Text);
                    cmd.Parameters.AddWithValue("@unit", unitId);
                    cmd.Parameters.AddWithValue("@cost", cost);
                    cmd.Parameters.AddWithValue("@cat", categoryProduct.Text);
                    cmd.Parameters.AddWithValue("@man", manufProduct.Text);
                    cmd.Parameters.AddWithValue("@sal", salleryProduct.Text);
                    cmd.Parameters.AddWithValue("@maxd", NullIfEmpty(maxAmount.Text));
                    cmd.Parameters.AddWithValue("@disc", NullIfEmpty(amountProduct.Text));
                    cmd.Parameters.AddWithValue("@descr", decriptionProduct.Text);
                    cmd.Parameters.AddWithValue("@qty", NullIfEmpty(countProduct.Text, true));

                    if (isNew || _photoChanged)
                        cmd.Parameters.AddWithValue("@photo", SavePhoto());

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(isNew ? "Товар добавлен: " + art : "Товар обновлён.");
                ApplyFilters();
                ClearFields();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Такой артикул уже есть.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void imageProduct_Click_1(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Изображения|*.jpg;*.jpeg;*.png" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                imageProduct.Image?.Dispose();

                using (var fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                using (var tmp = Image.FromStream(fs))
                    imageProduct.Image = new Bitmap(tmp);

                imageProduct.SizeMode = PictureBoxSizeMode.Zoom;
                _photoChanged = true;
            }
        }

        private void addButton_Click(object sender, EventArgs e) => SaveProduct(true);

        private void updateButton_Click(object sender, EventArgs e) => SaveProduct(false);

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            decimal discount = 0;
            decimal.TryParse(row.Cells["ProductDiscountAmount"].Value?.ToString(), out discount);

            row.DefaultCellStyle.BackColor =
                discount > 15 ? Color.FromArgb(0x7F, 0xFF, 0x00) : Color.White;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!dataGridView1.Columns.Contains("Информация")) return;
            if (e.ColumnIndex != dataGridView1.Columns["Информация"].Index) return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            decimal discount = 0;
            decimal.TryParse(row.Cells["ProductDiscountAmount"].Value?.ToString(), out discount);

            if (discount <= 0) return;

            decimal cost = 0;
            decimal.TryParse(row.Cells["ProductCost"].Value?.ToString(), out cost);
            decimal final = cost - (cost * discount / 100m);

            string name = row.Cells["ProductName"].Value?.ToString() ?? "";
            string article = row.Cells["ProductArticleNumber"].Value?.ToString() ?? "";
            string manuf = row.Cells["ProductManufacturer"].Value?.ToString() ?? "";
            string sallery = row.Cells["SalleryName"].Value?.ToString() ?? "";
            string descr = row.Cells["ProductDescription"].Value?.ToString() ?? "";
            string category = row.Cells["CategoryName"].Value?.ToString() ?? "";

            e.PaintBackground(e.CellBounds, true);

            string line1 = $"{category} | {name}";
            string line2 = $"Артикул: {article}";
            string line3 = $"Производитель: {manuf}";
            string line4 = $"Поставщик: {sallery}";
            string line5 = $"Описание: {descr}";
            string prefix = "Цена: ";
            string oldPrice = cost.ToString("0.00");
            string arrow = " → ";
            string newPrice = final.ToString("0.00") + " руб.";

            var g = e.Graphics;
            var fore = e.CellStyle.ForeColor;
            var font = e.CellStyle.Font;

            float x = e.CellBounds.Left + 3;
            float y = e.CellBounds.Top + 3;
            float lh = font.GetHeight(g) + 1;

            using (var brush = new SolidBrush(fore))
            {
                g.DrawString(line1, font, brush, x, y); y += lh;
                g.DrawString(line2, font, brush, x, y); y += lh;
                g.DrawString(line3, font, brush, x, y); y += lh;
                g.DrawString(line4, font, brush, x, y); y += lh;
                g.DrawString(line5, font, brush, x, y); y += lh;

                g.DrawString(prefix, font, brush, x, y);
                float wPrefix = g.MeasureString(prefix, font).Width;

                g.DrawString(oldPrice, font, brush, x + wPrefix, y);
                float wOld = g.MeasureString(oldPrice, font).Width;

                using (var strike = new Pen(fore, 1.5f))
                    g.DrawLine(strike,
                               x + wPrefix, y + lh / 2,
                               x + wPrefix + wOld, y + lh / 2);

                g.DrawString(arrow, font, brush, x + wPrefix + wOld, y);
                float wArrow = g.MeasureString(arrow, font).Width;

                using (var bold = new Font(font, FontStyle.Bold))
                    g.DrawString(newPrice, bold, brush, x + wPrefix + wOld + wArrow, y);
            }

            e.Handled = true;
        }

        private void btnViewOrder_Click(object sender, EventArgs e)
        {

        }

        private void priceProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void priceProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = separator;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if (e.KeyChar == separator && !((TextBox)sender).Text.Contains(separator.ToString()))
            {
                return;
            }

            e.Handled = true;
        }

        private void maxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = separator;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if (e.KeyChar == separator && !((TextBox)sender).Text.Contains(separator.ToString()))
            {
                return;
            }

            e.Handled = true;
        }

        private void amountProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = separator;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if (e.KeyChar == separator && !((TextBox)sender).Text.Contains(separator.ToString()))
            {
                return;
            }

            e.Handled = true;
        }

        private void countProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void countProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = separator;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if (e.KeyChar == separator && !((TextBox)sender).Text.Contains(separator.ToString()))
            {
                return;
            }

            e.Handled = true;
        }
    }
}