using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmployeeData.ViewModel;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using EmployeeData.Model;

namespace EmployeeData
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewModelMain Data;
        public MainWindow()
        {
            InitializeComponent();
            Data = new ViewModelMain();
            DataContext = Data;

            Binding myBinding = new Binding("Ss");
            myBinding.Source = Data.Departments;
            myBinding.Path = new PropertyPath(".");
            myBinding.Mode = BindingMode.TwoWay;
            
            BindingOperations.SetBinding(DepartmentsData, DataGrid.ItemsSourceProperty, myBinding);

            DepartmentsData.CanUserAddRows = false;
        }

        //Вызов sourceuptdated события для.. при изменении одной ячейки
        public void OnChecked(object sender, RoutedEventArgs e)
        {
            DepartmentsData.CommitEdit(DataGridEditingUnit.Row,true);
        }

    }
}
