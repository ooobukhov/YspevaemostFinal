using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Diplom_Yspevaemost
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
            password_textBox.PasswordChar = '*'; // Скрываем пароль по умолчанию
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            Glavnaya frm = new Glavnaya();
            frm.Show();
            this.Hide();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private string connectionString = "Data Source=WIN-RGCFO3057QU;Initial Catalog=Diplom_Yspevaemost;Integrated Security=True";

        private void vhod_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open(); // Открываем соединение

                    string query = "SELECT * FROM [Authorization] WHERE Login = @login AND Password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, sqlcon))
                    {
                        // Добавляем параметры для предотвращения SQL-инъекций
                        cmd.Parameters.AddWithValue("@login", login_textBox.Text);
                        cmd.Parameters.AddWithValue("@password", password_textBox.Text);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dtbl = new DataTable();
                        sda.Fill(dtbl);

                        if (dtbl.Rows.Count == 1)
                        {
                            // Переход в зависимости от роли
                            Form frm;
                            switch (login_textBox.Text)
                            {
                                case "admin":
                                    frm = new Admin_Tables();
                                    break;

                                case "teacher":
                                    frm = new Teacher_Tables();
                                    break;

                                case "student":
                                    frm = new Student_Tables();
                                    break;

                                default:
                                    MessageBox.Show("Неизвестная роль.");
                                    return;
                            }
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Не верно введен логин или пароль");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных, убедитесь что данные подключения верны, ошибка - " + ex.Message);
                connectionString = PromptForNewConnectionString(); // Запрос новой строки подключения
            }
        }

        private string PromptForNewConnectionString()
        {
            using (Connection connectionForm = new Connection())
            {
                while (true)
                {
                    if (connectionForm.ShowDialog() == DialogResult.OK)
                    {
                        return connectionForm.ConnectionString; // Возвращает строку подключения
                    }
                    if (connectionForm.DialogResult == DialogResult.Cancel)
                    {
                        return null; // Если пользователь отменил, возвращаем null
                    }
                }
            }
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            // Управление видимостью пароля
            if (password_textBox.PasswordChar == '*')
            {
                password_textBox.PasswordChar = '\0'; // Показываем пароль
                showPassword.Text = "Скрыть пароль"; // Меняем текст кнопки
            }
            else
            {
                password_textBox.PasswordChar = '*'; // Скрываем пароль
                showPassword.Text = "Показать пароль"; // Меняем текст кнопки
            }
        }
    }
}