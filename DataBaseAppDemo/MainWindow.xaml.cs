using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace DataBaseAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FernandoDBEntities dataEntities = new FernandoDBEntities();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DbSet<Degree> degrees = dataEntities.Degrees;

            //var query = from degree in degrees
            //            where degree.Salary > 70000
            //            orderby degree.Name
            //            select degree;
            var query = from degree in degrees
                        where degree.IsCool == true
                        orderby degree.Heads
                        select degree;

            //DegreeDataBase.ItemsSource = degrees.ToList();
            DegreeDataBase.ItemsSource = query.ToList();
        }

        private void DegreeDataBase_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement element1 = DegreeDataBase.Columns[0].GetCellContent(e.Row);
                if (element1.GetType() == typeof(TextBox))
                {
                    newDegree.Name = ((TextBox)element1).Text;
                }
                FrameworkElement element2 = DegreeDataBase.Columns[1].GetCellContent(e.Row);
                if (element2.GetType() == typeof(TextBox))
                {
                    newDegree.Heads = ((TextBox)element2).Text;
                }
                FrameworkElement element3 = DegreeDataBase.Columns[2].GetCellContent(e.Row);
                if (element3.GetType() == typeof(TextBox))
                {
                    newDegree.StudentCount = Convert.ToInt32(((TextBox)element3).Text);
                }
                FrameworkElement element4 = DegreeDataBase.Columns[3].GetCellContent(e.Row);
                if (element4.GetType() == typeof(TextBox))
                {
                    newDegree.Salary = Convert.ToDecimal(((TextBox)element4).Text);
                }
                FrameworkElement element5 = DegreeDataBase.Columns[4].GetCellContent(e.Row);
                if (element5.GetType() == typeof(TextBox))
                {
                    newDegree.IsCool = Convert.ToBoolean(((TextBox)element5).Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void DegreeDataBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newDegree = DegreeDataBase.SelectedItem as Degree;
        }

        private void DegreeDataBase_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            bool exists = false;
            foreach (Degree degree in dataEntities.Degrees)
            {
                if (newDegree == degree || newDegree.Name == degree.Name)
                {
                    exists = true;
                    break;

                }


            }
        }
    }
}
