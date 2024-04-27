using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PO_DLYA_DANILI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void loaded_spisok(object sender, RoutedEventArgs e)
        {
            using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM [People]", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("People");
                sda.Fill(dt);
                bomzi.ItemsSource = dt.DefaultView;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow AddPersonWindow = new AddPersonWindow();
            AddPersonWindow.Show();
        }

        public void RefreshDataGrid()
        {
            using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
            {
                con.Open();

                var cmd = new SqlCommand("SELECT * FROM [People]", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("People");
                sda.Fill(dt);

                bomzi.ItemsSource = dt.DefaultView;

                con.Close();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = bomzi.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditPersonWindow editWindow = new EditPersonWindow(selectedRow);
                if (editWindow.ShowDialog() == true) // Ожидаем закрытия окна редактирования
                {
                    // Обновление данных в базе данных при редактировании
                    using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
                    {
                        con.Open();
                        var cmd = new SqlCommand("UPDATE [People] SET Name = @Name, Age = @Age, Gender = @Gender, Pogonyalo = @Pogonyalo WHERE Id = @Id", con);
                        cmd.Parameters.AddWithValue("@Name", editWindow.txtName.Text);
                        cmd.Parameters.AddWithValue("@Age", editWindow.txtAge.Text);
                        cmd.Parameters.AddWithValue("@Gender", editWindow.txtGender.Text);
                        cmd.Parameters.AddWithValue("@Pogonyalo", editWindow.txtPogonyalo.Text);
                        cmd.Parameters.AddWithValue("@Id", selectedRow["Id"]);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    RefreshDataGrid(); // Обновление DataGrid
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = bomzi.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                // Удаление данных из базы данных
                using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
                {
                    con.Open();
                    var cmd = new SqlCommand("DELETE FROM [People] WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", selectedRow["Id"]); // Предположим, что у вас есть столбец Id
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                RefreshDataGrid(); // Обновление DataGrid
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }





        /*private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Место для логики добавления новой строки в базу данных
            // Пример:
            DataRowView newRow = bomzi.Items[bomzi.Items.Count - 1] as DataRowView;
            string name = newRow["Name"].ToString();
            string age = newRow["Age"].ToString();
            string gender = newRow["Gender"].ToString();
            string pogonyalo = newRow["Pogonyalo"].ToString();

            using (var con = new SqlConnection("Data Source=PLABSQLw19s1,49172;Initial Catalog=PerekupKV;Integrated Security=True"))
            {
                con.Open();
                var cmd = new SqlCommand("INSERT INTO People (Name, Age, Gender, Pogonyalo) VALUES (@Name, @Age, @Gender, @Pogonyalo)", con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Pogonyalo", pogonyalo);
                cmd.ExecuteNonQuery();
                // Обновите DataGrid после добавления данных в базу данных, если это необходимо
            }
        }*/



    }
}
