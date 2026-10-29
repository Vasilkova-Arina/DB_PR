using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR32
{
    public partial class Order : Form
    {
        private User _user;
        private Timer timerSon;
        private string ConnStr = $"Server={Properties.Settings.Default.host};" +
                                 $"Uid={Properties.Settings.Default.uid};" +
                                 $"Pwd={Properties.Settings.Default.pwd};" +
                                 $"Database=db29;";
        private const int PageSize = 10;
        private List<OrderRow> _filteredOrders = new List<OrderRow>();
        private int _currentPage;
        public Order(User user)
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

        private void Order_Load(object sender, EventArgs e)
        {
            if (_user == null)
                label1.Text = "Гостевой режим " + DateTime.Now.ToString("dd.MM.yyyy");
            else
                label1.Text = "Добро пожаловать, " + _user.Fio;

            ActiveForm(this);
            timerSon.Start();

            cbDiscount.Items.AddRange(new object[] { "Все заказы", "0-10%", "11-14%", "15% и более" });
            cbDiscount.SelectedIndex = 0;

            cbSort.Items.AddRange(new object[] { "Без сортировки", "По возрастанию стоимости", "По убыванию стоимости" });
            cbSort.SelectedIndex = 0;

            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                var dt = new DataTable();
                new MySqlDataAdapter("SELECT StatusID, StatusName FROM Status;", conn).Fill(dt);
                cbStatus.DataSource = dt;
                cbStatus.DisplayMember = "StatusName";
                cbStatus.ValueMember = "StatusID";
            }

            tbPoick.TextChanged += (s, ev) => ApplyFilters();
            cbDiscount.SelectedIndexChanged += (s, ev) => ApplyFilters();
            cbSort.SelectedIndexChanged += (s, ev) => ApplyFilters();

            ApplyFilters();
            //Pagination();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ApplyFilters()
        {

            string strCmd = @"
                SELECT o.OrderID, o.OrderDate, o.OrderDeliveryDate, o.OrderClient, o.OrderPickupPoint, 
                s.StatusName, p.ProductCost, p.ProductDiscountAmount, p.ProductQuantityInStock, op.ProductCount
                FROM `Order` o
                LEFT JOIN OrderProduct op ON o.OrderID = op.OrderID
                LEFT JOIN Product p ON op.ProductArticleNumber = p.ProductArticleNumber
                LEFT JOIN Status s ON o.OrderStatus = s.StatusID";

            FillDataGrid(strCmd);
            UpdateCounter();
            //Pagination();
        }

        public void FillDataGrid(string strCmd)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DataTable rawData = new DataTable();
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                new MySqlDataAdapter(strCmd, conn).Fill(rawData);
            }

            var orders = new List<OrderRow>();

            foreach (var group in rawData.AsEnumerable().GroupBy(r => Convert.ToInt32(r["OrderID"])))
            {
                var first = group.First();

                decimal totalCost = 0;
                decimal totalDiscount = 0;
                bool allInStock = true;

                foreach (var row in group)
                {
                    if (row["ProductCost"] == DBNull.Value ||
                        row["ProductCount"] == DBNull.Value)
                    {
                        allInStock = false;
                        continue;
                    }

                    decimal cost = Convert.ToDecimal(row["ProductCost"]);
                    int count = Convert.ToInt32(row["ProductCount"]);
                    decimal disc = row["ProductDiscountAmount"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(row["ProductDiscountAmount"]);
                    int stock = row["ProductQuantityInStock"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(row["ProductQuantityInStock"]);

                    totalCost += cost * count;
                    totalDiscount += cost * count * (disc / 100m);

                    if (stock <= 3)
                        allInStock = false;
                }

                orders.Add(new OrderRow
                {
                    OrderID = Convert.ToInt32(first["OrderID"]),
                    Date = Convert.ToDateTime(first["OrderDate"]).ToString("dd.MM.yyyy"),
                    Delivery = Convert.ToDateTime(first["OrderDeliveryDate"]).ToString("dd.MM.yyyy"),
                    Client = first["OrderClient"].ToString(),
                    Pickup = first["OrderPickupPoint"].ToString(),
                    Status = first["StatusName"].ToString(),
                    Total = totalCost,
                    DiscountPercent = totalCost == 0 ? 0 : (totalDiscount / totalCost) * 100,
                    AllInStock = allInStock
                });
            }

            if (!string.IsNullOrWhiteSpace(tbPoick.Text))
            {
                string search = tbPoick.Text.ToLower();
                orders = orders.Where(o => o.OrderID.ToString().Contains(search) || o.Client.ToLower().Contains(search)).ToList();
            }

            if (cbDiscount.SelectedIndex == 1) orders = orders.Where(o => o.DiscountPercent <= 10).ToList();
            else if (cbDiscount.SelectedIndex == 2) orders = orders.Where(o => o.DiscountPercent >= 11 && o.DiscountPercent <= 14).ToList();
            else if (cbDiscount.SelectedIndex == 3) orders = orders.Where(o => o.DiscountPercent >= 15).ToList();

            if (cbSort.SelectedIndex == 1) orders = orders.OrderBy(o => o.Total).ToList();
            else if (cbSort.SelectedIndex == 2) orders = orders.OrderByDescending(o => o.Total).ToList();


            _filteredOrders = orders;
            _currentPage = 0;
            ShowPage(_currentPage);

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
        private void ShowPage(int page)
        {
            int pageCount = Math.Max(1, (_filteredOrders.Count + PageSize - 1) / PageSize);

            _currentPage = Math.Max(0, Math.Min(page, pageCount - 1));

            var pageRows = _filteredOrders
                .Skip(_currentPage * PageSize)
                .Take(PageSize)
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pageRows;

            string[] hiddenColumns = { "OrderID", "Total", "DiscountPercent", "AllInStock" };
            foreach (string name in hiddenColumns)
            {
                if (dataGridView1.Columns.Contains(name))
                    dataGridView1.Columns[name].Visible = false;
            }

            SetHeader("Date", "Дата заказа");
            SetHeader("Delivery", "Дата доставки");
            SetHeader("Client", "ФИО клиента");
            SetHeader("Pickup", "Пункт выдачи");
            SetHeader("Status", "Статус");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is OrderRow order)
                {
                    row.DefaultCellStyle.BackColor = order.AllInStock
                        ? Color.FromArgb(0x20, 0xB2, 0xAA)
                        : Color.FromArgb(0xFF, 0x8C, 0x00);
                }
            }

            CreatePageLinks(pageCount);
        }

        private void SetHeader(string columnName, string header)
        {
            if (dataGridView1.Columns.Contains(columnName))
                dataGridView1.Columns[columnName].HeaderText = header;
        }

        private void CreatePageLinks(int pageCount)
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i].Name.StartsWith("page"))
                    Controls.RemoveAt(i);
            }

            int x = 10;
            int y = 650;
            int step = 25;

            for (int i = 0; i < pageCount; i++)
            {
                var link = new LinkLabel
                {
                    Text = (i + 1).ToString(),
                    Name = "page" + i,
                    Tag = i,
                    AutoSize = true,
                    Location = new Point(x, y),
                    LinkColor = Color.FromArgb(102, 255, 102),
                    Font = new Font("Comic Sans MS", 14f, FontStyle.Bold),
                    LinkBehavior = i == _currentPage
                        ? LinkBehavior.NeverUnderline
                        : LinkBehavior.AlwaysUnderline
                };

                link.Click += LinkLabel_Click;
                Controls.Add(link);
                link.BringToFront();

                x += step;
            }
        }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            if (sender is LinkLabel link && link.Tag is int page)
                ShowPage(page);
        }

        private void tbPoick_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is OrderRow order)
            {
                dtpDelivery.Value = DateTime.ParseExact(order.Delivery, "dd.MM.yyyy", null);
                cbStatus.Text = order.Status;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbPoick.Clear();
            cbDiscount.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            ApplyFilters();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is OrderRow order)
            {
                using (var conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("UPDATE `Order` SET OrderDeliveryDate=@dd, OrderStatus=@st WHERE OrderID=@id;", conn);
                    cmd.Parameters.AddWithValue("@dd", dtpDelivery.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@st", cbStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", order.OrderID);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Сохранено!");
                ApplyFilters();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timerSon.Stop();
            this.Hide();
            new Menu2(_user).ShowDialog();
            this.Show();
            timerSon.Start();
        }
    }
}