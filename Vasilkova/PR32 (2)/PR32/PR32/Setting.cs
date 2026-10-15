using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR32
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string server = host.Text.Trim();
            string user = uid.Text.Trim();
            string password = pwd.Text;

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Заполните сервер и логин.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Строка подключения БЕЗ указания базы — только сервер
            string connStr = $"Server={server};Uid={user};Pwd={password};";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // Создаём БД db29, если её ещё нет
                    using (MySqlCommand cmd = new MySqlCommand(
                        "CREATE DATABASE IF NOT EXISTS `db29` " +
                        "CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // Сохраняем настройки — только после успешной проверки
                Properties.Settings.Default.host = server;
                Properties.Settings.Default.uid = user;
                Properties.Settings.Default.pwd = password;
                Properties.Settings.Default.Save();

                MessageBox.Show(
                    $"Подключение успешно!\n\n" +
                    $"Строка подключения:\nServer={server};Uid={user};Pwd=***;",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Закрываем форму настроек и открываем авторизацию
                Hide();
                new Avtoriz().ShowDialog();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            host.Text = Properties.Settings.Default.host;
            uid.Text = Properties.Settings.Default.uid;
            pwd.Text = Properties.Settings.Default.pwd;
        }
    }
}
