using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR32
{
    public static class BackupService
    {
        private static readonly string[] Tables = {
            "Role", "User", "Unit", "Sallery", "Category", "Product", "Status", "PickupPoint", "Order", "OrderProduct"
        };

        public static string ConnStr => $"Server={Properties.Settings.Default.host};" +
                                        $"Uid={Properties.Settings.Default.uid};" +
                                        $"Pwd={Properties.Settings.Default.pwd};" +
                                        $"Database=db29;";

        public static void CreateBackup(string pathToFolder = null)
        {
            try
            {
                bool isManual = !string.IsNullOrEmpty(pathToFolder);
                if (!isManual)
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    pathToFolder = Path.Combine(Application.StartupPath, "Backups", timestamp);
                }

                if (!Directory.Exists(pathToFolder))
                    Directory.CreateDirectory(pathToFolder);

                using (var conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();

                    foreach (var tableName in Tables)
                    {
                        SaveTableToCsv(conn, tableName, pathToFolder);
                    }

                    SaveDatabaseToSql(conn, Path.Combine(pathToFolder, "database_backup.sql"));
                }

                if (isManual)
                {
                    MessageBox.Show($"Резервная копия (CSV + SQL) успешно создана:\n{pathToFolder}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании резервной копии: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SaveTableToCsv(MySqlConnection conn, string tableName, string folderPath)
        {
            string filePath = Path.Combine(folderPath, tableName + ".csv");

            var cmd = new MySqlCommand($"SELECT * FROM `{tableName}`;", conn);
            using (var reader = cmd.ExecuteReader())
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                var headers = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                    headers[i] = reader.GetName(i);

                writer.WriteLine(string.Join(";", headers));

                while (reader.Read())
                {
                    var values = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.IsDBNull(i))
                        {
                            values[i] = "";
                        }
                        else
                        {
                            string val = reader.GetValue(i).ToString()
                                .Replace("\"", "\"\"")
                                .Replace("\r", " ")
                                .Replace("\n", " ");

                            if (val.Contains(";") || val.Contains("\""))
                                val = $"\"{val}\"";

                            values[i] = val;
                        }
                    }
                    writer.WriteLine(string.Join(";", values));
                }
            }
        }

        private static void SaveDatabaseToSql(MySqlConnection conn, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("SET FOREIGN_KEY_CHECKS = 0;"); 
                writer.WriteLine();

                foreach (var tableName in Tables)
                {
                    var cmdCreate = new MySqlCommand($"SHOW CREATE TABLE `{tableName}`;", conn);
                    using (var reader = cmdCreate.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            writer.WriteLine($"DROP TABLE IF EXISTS `{tableName}`;");
                            writer.WriteLine(reader.GetString(1) + ";");
                            writer.WriteLine();
                        }
                    }

                    var cmdData = new MySqlCommand($"SELECT * FROM `{tableName}`;", conn);
                    using (var reader = cmdData.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var values = new string[reader.FieldCount];
                            var cols = new string[reader.FieldCount];

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cols[i] = $"`{reader.GetName(i)}`";

                                if (reader.IsDBNull(i))
                                    values[i] = "NULL";
                                else
                                {
                                    string val = reader.GetValue(i).ToString()
                                        .Replace("\\", "\\\\")
                                        .Replace("'", "\\'")
                                        .Replace("\r", "\\r")
                                        .Replace("\n", "\\n");

                                    if (reader.GetFieldType(i) == typeof(int) || reader.GetFieldType(i) == typeof(decimal) || reader.GetFieldType(i) == typeof(byte))
                                        values[i] = val;
                                    else
                                        values[i] = $"'{val}'";
                                }
                            }
                            writer.WriteLine($"INSERT INTO `{tableName}` ({string.Join(", ", cols)}) VALUES ({string.Join(", ", values)});");
                        }
                    }
                    writer.WriteLine();
                }

                writer.WriteLine("SET FOREIGN_KEY_CHECKS = 1;");
            }
        }
    }
}