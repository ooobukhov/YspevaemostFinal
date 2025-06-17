using System;
using System.Windows.Forms;

namespace Diplom_Yspevaemost
{
    public partial class Connection : Form
    {
        public string ConnectionString { get; private set; }

        public Connection()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string input = txtConnectionString.Text;

            // Проверяем корректность введенной строки подключения
            if (IsValidConnectionString(input))
            {
                ConnectionString = input; // Сохраняем строку подключения
                this.DialogResult = DialogResult.OK; // Закрываем форму с результатом OK
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректная строка подключения. Пожалуйста, попробуйте снова.", "Ошибка ввода", MessageBoxButtons.OK);
                txtConnectionString.Clear(); // Очищаем поле ввода
                txtConnectionString.Focus(); // Устанавливаем фокус на поле ввода
            }
        }

        private bool IsValidConnectionString(string connectionString)
        {
            // Проверка на пустую строку
            return !string.IsNullOrWhiteSpace(connectionString);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Закрываем форму с результатом Cancel
            this.Close();
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            // Здесь можно добавить инициализацию, если требуется
        }
    }
}