using Langulag.Windows;
using System;
using System.Collections.Generic;
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

namespace Langulag
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<EF.Client> listClient = new List<EF.Client>();
        List<EF.Gender> listGender = new List<EF.Gender>();
        List<string> listFilter = new List<string>(){
            "Все",
            "Мужчины",
            "Женщины"
        };
        List<string> listSort = new List<string>(){
            "По умолчанию",
            "По фамилии",
            "По дате последнего посещения",
            "По количеству посещений"
        };
        List<string> listPage = new List<string>(){
            "10",
            "25",
            "50",
            "100"
        };
        int currentPage = 1;
        public MainWindow()
        {
            InitializeComponent();
            Clear();
        }

        private void Filter()
        {
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.Where(i => i.ClientGender == "мужской").ToList();
                    break;
                case 2:
                    listClient = listClient.Where(i => i.ClientGender == "женский").ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
            }
        }
        private void Sort()
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.OrderBy(i => i.SecondName).ToList();
                    break;
                case 2:
                    listClient = listClient.OrderBy(i => i.LastVisit).ToList();
                    break;
                case 3:
                    listClient = listClient.OrderBy(i => i.qtyOfVisits).ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
            }
        }
        private void Search()
        {
            listClient = Classes.AppData.Context.Client.ToList();
            listClient = listClient.Where(i =>
            i.Email.ToLower().Contains(tbSearch.Text.ToLower())
            || (i.SecondName.ToLower() + " " + i.FirstName.ToLower() + " " + i.Patronymic.ToLower()).Contains(tbSearch.Text.ToLower())
            || i.Phone.ToLower().Contains(tbSearch.Text.ToLower())
            ).ToList();
            if (cbBirthday.IsChecked == true)
            {
                listClient = listClient.Where(i => i.DateOfBirth == DateTime.Now).ToList();
            }

        }
        private void Pagination()
        {
            if (cbPage.SelectedIndex != -1 && currentPage <= Math.Ceiling(Convert.ToDouble(Classes.AppData.Context.Client.ToList().Count) / Convert.ToDouble(cbPage.SelectedItem.ToString())))
            {
                string listClientBefore = listClient.Count.ToString();
                if (currentPage <= 1)
                {
                    listClient = listClient.Take(Convert.ToInt32(cbPage.SelectedItem.ToString())).ToList();
                }
                else if (Convert.ToInt32(cbPage.SelectedItem.ToString()) * (currentPage - 1) + Convert.ToInt32(cbPage.SelectedItem.ToString()) <= Classes.AppData.Context.Client.ToList().Count)
                {
                    listClient = listClient.Skip(Convert.ToInt32(cbPage.SelectedItem.ToString()) * (currentPage - 1)).Take(Convert.ToInt32(cbPage.SelectedItem.ToString())).ToList();
                }
                else
                {
                    listClient = listClient.Skip(Convert.ToInt32(cbPage.SelectedItem.ToString()) * (currentPage - 1)).ToList();
                }
                tbCount.Text = listClient.Count.ToString() + " из " + listClientBefore;
            }
        }
        private void UpdateData()
        {
            Search();
            Filter();
            Sort();
            Pagination();
            tbPageNumber.Text = "страница: " + currentPage;
            lvClient.ItemsSource = listClient;
        }
        private void Clear()
        {
            cbFilter.ItemsSource = listFilter;
            cbSort.ItemsSource = listSort;
            cbPage.ItemsSource = listPage;
            cbFilter.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            cbPage.SelectedIndex = 0;
            cbBirthday.IsChecked = false;
            tbSearch.Text = "";
            currentPage = 1;
            lvClient.ItemsSource = listClient;
            UpdateData();
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentPage = 1;
            UpdateData();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            UpdateData();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            UpdateData();
        }

        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            UpdateData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (currentPage < Math.Ceiling(Convert.ToDouble(Classes.AppData.Context.Client.ToList().Count) / Convert.ToDouble(cbPage.SelectedItem.ToString())) && listClient.Count == Convert.ToInt32(cbPage.SelectedItem.ToString()))
            {
                currentPage++;
                UpdateData();
            }
        }

        private void cbBirthday_Checked(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            UpdateData();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (lvClient.SelectedItem != null)
            {
                // передать выбраный элемент листа в удаление
                MessageBox.Show(lvClient.SelectedItem.ToString());
            }
        }

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvClient.SelectedItem != null)
            {
                // передать выбраный элемент листа в окно
                UpdateClientWindow updateClientWindow = new UpdateClientWindow();
                updateClientWindow.ShowDialog();
            }
        }
    }
}
