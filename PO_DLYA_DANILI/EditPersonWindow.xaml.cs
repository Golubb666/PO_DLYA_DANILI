using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PO_DLYA_DANILI
{
    /// <summary>
    /// Логика взаимодействия для EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        private DataRowView selectedDataRow;

        public EditPersonWindow()
        {
            InitializeComponent();
        }


        public EditPersonWindow(DataRowView selectedDataRow)
        {
            InitializeComponent();
            this.selectedDataRow = selectedDataRow;

            // Заполнение полей TextBox данными из выбранной строки
            txtName.Text = selectedDataRow["Name"].ToString();
            txtAge.Text = selectedDataRow["Age"].ToString();
            txtGender.Text = selectedDataRow["Gender"].ToString();
            txtPogonyalo.Text = selectedDataRow["Pogonyalo"].ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Обновление данных в выбранной строке
            selectedDataRow.BeginEdit();
            selectedDataRow["Name"] = txtName.Text;
            selectedDataRow["Age"] = txtAge.Text;
            selectedDataRow["Gender"] = txtGender.Text;
            selectedDataRow["Pogonyalo"] = txtPogonyalo.Text;
            selectedDataRow.EndEdit();

            DialogResult = true;
        }
    }
}
