using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PR32
{
    public partial class Avtoriz : Form
    {
        private int count = 0;
        private string captchaOtv = "";
        private bool viewCaotcha = false;
        private readonly Random random = new Random();
       
        public Avtoriz()
        {
            InitializeComponent();
            lblCaptcha.Visible = pictCaptcha.Visible = txtCaptcha.Visible = false;
            time.Visible = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Avtoriz_Load(object sender, EventArgs e)
        {
            log.Focus();
            if (!TestConnection(out string err))
            {
                var res = MessageBox.Show(
                    "Не удалось подключиться к БД:\n" + err +
                    "\n\nОткрыть настройки подключения?",
                    "Ошибка подключения",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    using (var set = new Setting())
                        set.ShowDialog();

                    if (!TestConnection(out string err2))
                    {
                        MessageBox.Show(
                            "Подключение по-прежнему недоступно:\n" + err2,
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Setting set = new Setting();
            set.ShowDialog();
            this.Close();
        }

        private string ConnStr =>
            $"Server={Properties.Settings.Default.host};" +
            $"Uid={Properties.Settings.Default.uid};" +
            $"Pwd={Properties.Settings.Default.pwd};" +
            $"Database=db29;";

        private User CheckUser(string login, string password)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();

                    var cmd = new MySqlCommand(
                        @"SELECT UserID, UserSurname, UserName, UserPatronymic,
                                 UserLogin, UserPassword, UserRole
                          FROM user
                          WHERE UserLogin=@l AND UserPassword=@p LIMIT 1;", conn);
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", password);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return null;
                        return new User
                        {
                            UserID = Convert.ToInt32(r["UserID"]),
                            UserSurname = r["UserSurname"].ToString(),
                            UserName = r["UserName"].ToString(),
                            UserPatronymic = r["UserPatronymic"].ToString(),
                            UserLogin = r["UserLogin"].ToString(),
                            UserPassword = r["UserPassword"].ToString(),
                            UserRole = Convert.ToInt32(r["UserRole"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private bool TestConnection(out string error)
        {
            error = null;
            try
            {
                using (var conn = new MySqlConnection(ConnStr))
                    conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private void SetFormEnabled(bool enabled)
        {
            foreach (Control c in this.Controls)
                SetControlEnabled(c, enabled);
        }
        private void SetControlEnabled(Control c, bool enabled)
        {
            c.Enabled = enabled;
            foreach (Control child in c.Controls)
                SetControlEnabled(child, enabled);
        }

        private void GenerationCaptcha()
        {
            int n = random.Next(1, 5);
            string path = System.IO.Path.Combine(
                Application.StartupPath,
                "Resources", "Captcha" + n + ".png");
            try
            {
                pictCaptcha.Image = Image.FromFile(path);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки капчи: " + ex.Message);
                return;
            }

            pictCaptcha.SizeMode = PictureBoxSizeMode.StretchImage;
            captchaOtv = new[] { "", "hsmhn", "vzpk2z", "mwxe2", "263s2v" }[n];
            txtCaptcha.Clear();
        }
        private async void BlockForm10sec()
        {
            SetFormEnabled(false);

            time.Visible = true;
            for ( int i = 10; i > 0; i--)
            {
                time.Text = "Подождите " + i + " сек";
                await Task.Delay(1000);
            }

            time.Text = "";
            time.Visible = false;
            SetFormEnabled(true);
            txtCaptcha.Clear();
            log.Focus();
        }
        private void Fail()
        {
            count++;
            MessageBox.Show("Неверные данные для входа!");

            if(count == 1 && !viewCaotcha)
            {
                viewCaotcha = true;
                lblCaptcha.Visible = pictCaptcha.Visible = txtCaptcha.Visible = true;
                GenerationCaptcha();
                return;
            }
            if (count > 1)
            {
                GenerationCaptcha();
                BlockForm10sec();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string login = log.Text.Trim();
            string password = pwd.Text;

            if (viewCaotcha && !string.Equals(txtCaptcha.Text.Trim(), captchaOtv, StringComparison.OrdinalIgnoreCase))
            {
                Fail();
                return;
            }

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = CheckUser(login, password);
            if (user == null)
            {
                Fail();
                return;
            }

            count = 0;
            viewCaotcha = false;

            this.Hide();

            if (user.UserRole == 1 || user.UserRole == 2)
            {
                Menu2 menu2 = new Menu2(user);
                menu2.ShowDialog();
            }
            else
            {
                Menu menu = new Menu(user);
                menu.ShowDialog();
            }

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu(null);  
            menu.ShowDialog();
            this.Close();
        }
    }
}
