using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PO_DLYA_DANILI
{
    /// <summary>
    /// Логика взаимодействия для AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
            {
                con.Open();

                string name = txtName.Text;
                string age = txtAge.Text;
                string gender = txtGender.Text;
                string pogonyalo = txtPogonyalo.Text;

                // Создаем команду SQL для вставки данных
                string query = "INSERT INTO People (Name, Age, Gender, Pogonyalo) VALUES (@Name, @Age, @Gender, @Pogonyalo)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Pogonyalo", pogonyalo);

                // Выполняем команду
                cmd.ExecuteNonQuery();

                con.Close();
            }

            this.Close(); // Закрываем окно после сохранения данных

            // Обновляем данные в DataGrid
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.RefreshDataGrid();
            }
        }
    }
}
