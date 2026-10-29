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

namespace PR32
{
    public partial class Menu2 : Form
    {
        private Timer timerSon;
        private User _user;
        public Menu2(User user)
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

        private void button3_Click(object sender, EventArgs e)
        {
            timerSon.Stop();
            this.Hide();
            new Avtoriz().ShowDialog();
            this.Show();
            timerSon.Start();
        }

        private void Menu2_Load(object sender, EventArgs e)
        {
            if (_user == null)
                label2.Text = "Гостевой режим " + DateTime.Now.ToString("dd.MM.yyyy");
            else
                label2.Text = "Добро пожаловать, " + _user.Fio;


            button5.Visible = (_user != null && _user.UserRole == 1);

            ActiveForm(this);
            timerSon.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // --- выбор файлов ---
            string sqlFile;
            using (var dlg = new OpenFileDialog { Filter = "SQL (*.sql)|*.sql" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                sqlFile = dlg.FileName;
            }

            string[] csvFiles;
            using (var dlg = new OpenFileDialog
            { Filter = "CSV (*.csv)|*.csv", Multiselect = true })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                csvFiles = dlg.FileNames;
            }

            // --- строка подключения ---
            string connStr =
                $"Server={Properties.Settings.Default.host};" +
                $"Uid={Properties.Settings.Default.uid};" +
                $"Pwd={Properties.Settings.Default.pwd};" +
                $"Database=db29;AllowLoadLocalInfile=true;";

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Структура БД
                    var script = new MySqlScript(conn, File.ReadAllText(sqlFile, Encoding.UTF8));
                    script.Delimiter = ";";
                    script.Execute();

                    // 2. Отключаем FK на время загрузки
                    using (var cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0;", conn))
                        cmd.ExecuteNonQuery();

                    int totalRows = 0;

                    // 3. Грузим каждый CSV через LOAD DATA
                    foreach (var csv in csvFiles)
                    {
                        string table = Path.GetFileNameWithoutExtension(csv);
                        string path = csv.Replace("\\", "/");

                        // Считаем строки ДО загрузки
                        long before = Convert.ToInt64(new MySqlCommand(
                            $"SELECT COUNT(*) FROM `{table}`;", conn).ExecuteScalar());

                        string sql =
                            $"LOAD DATA LOCAL INFILE '{path}' INTO TABLE `{table}` " +
                            "CHARACTER SET cp1251 " +
                            "FIELDS TERMINATED BY ';' " +
                            "LINES TERMINATED BY '\\r\\n' " +
                            "IGNORE 1 LINES;";

                        using (var cmd = new MySqlCommand(sql, conn))
                            cmd.ExecuteNonQuery();

                        // Считаем строки ПОСЛЕ загрузки
                        long after = Convert.ToInt64(new MySqlCommand(
                            $"SELECT COUNT(*) FROM `{table}`;", conn).ExecuteScalar());

                        totalRows += (int)(after - before);
                    }

                    // 4. Возвращаем FK
                    using (var cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 1;", conn))
                        cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        $"Импорт завершён.\n" +
                        $"CSV-файлов: {csvFiles.Length}\n" +
                        $"Вставлено строк: {totalRows}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timerSon.Stop();

            this.Hide();
            ProductForm product = new ProductForm(_user);
            product.ShowDialog();
            this.Show();
            timerSon.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timerSon.Stop();

            this.Hide();
            Order order = new Order(_user);
            order.ShowDialog();
            this.Show();
            timerSon.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Выберите папку для сохранения резервной копии";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    BackupService.CreateBackup(fbd.SelectedPath);
                }
            }
        }
    }
}
