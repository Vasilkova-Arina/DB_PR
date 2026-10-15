using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR32
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Строка подключения БЕЗ указания базы
            string connStr = $"Server={Properties.Settings.Default.host};" +
                             $"Uid={Properties.Settings.Default.uid};" +
                             $"Pwd={Properties.Settings.Default.pwd};";

            bool ok = false;
            string error = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // Есть ли БД db29?
                    string checkQuery =
                        "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA " +
                        "WHERE SCHEMA_NAME = 'db29';";
                    bool exists;
                    using (MySqlCommand cmd = new MySqlCommand(checkQuery, conn))
                    {
                        exists = cmd.ExecuteScalar() != null;
                    }

                    // Нет — создаём
                    if (!exists)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(
                            "CREATE DATABASE `db29` " +
                            "CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ok = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            if (ok)
            {
                MessageBox.Show(
                    $"Подключение к СУБД успешно!\n\n" +
                    $"Строка подключения:\n" +
                    $"Server={Properties.Settings.Default.host};" +
                    $"Uid={Properties.Settings.Default.uid};Pwd=***;",
                    "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Run(new Avtoriz());
            }
            else
            {
                MessageBox.Show(
                    $"Не удалось подключиться к СУБД.\n\n{error}\n\n" +
                    "Откроется форма настроек.",
                    "Сбой подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);

                using (var set = new Setting())
                {
                    if (set.ShowDialog() == DialogResult.OK)
                        Application.Run(new Avtoriz());
                    else
                        Application.Exit();
                }
            }
        }
    }
}
