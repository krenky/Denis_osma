using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Denis_osma
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataOrders.ItemsSource = Orders;     // привязка коллекций к таблицам
            DataProducts.ItemsSource = products; // привязка коллекций к таблицам
        }
        Shop shop = new Shop(10);                                                     //объявление коллекций
        ObservableCollection<Order> Orders = new ObservableCollection<Order>();       //объявление коллекций
        ObservableCollection<Product> products = new ObservableCollection<Product>(); //объявление коллекций

        private void AddOrder_Click(object sender, RoutedEventArgs e)//обработчик добавления заказа
        {
            Order order = new Order(TextBoxName.Text);
            shop.AddOrder(order);
            Orders.Add(order);
        }

        private void DelOrder_Click(object sender, RoutedEventArgs e)//обработчик удаления заказа
        {
            Order order = DataOrders.SelectedItem as Order;
            Orders.Remove(order);
        }

        private void DataOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)//обработчик смены фокуса в таблицы
        {
            if (DataOrders.SelectedItem != null)
            {
                Order order = shop.FindOrder(DataOrders.SelectedItem as Order);
                DataProducts.ItemsSource = order?.GetProduct();
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)//обработчик добавления продуктов
        {
            Order order = shop.FindOrder(DataOrders.SelectedItem as Order);
            order.AddProduct(TextBoxProduct.Text, Convert.ToInt32(TextBoxPriceProduct.Text));
            DataProducts.ItemsSource = order?.GetProduct();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)//обработчик удаления продуктов
        {
            Product product = DataProducts.SelectedItem as Product;
            Order order = shop.FindOrder(DataOrders.SelectedItem as Order);
            order.DeleteProduct(product.Name);
            DataProducts.ItemsSource = order?.GetProduct();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)//обработчик сохранения
        {
            shop.Save();
        }

        private async void LoadFile_ClickAsync(object sender, RoutedEventArgs e)//обработчик загрузки
        {
            await shop.Load();
            foreach (var i in shop.GetOrderArray()) // добавления заказов в коллекцию
            {
                if (i != null)
                {
                    Orders.Add(i);
                }
            }
        }

        private void TextBoxName_PreviewTextInput(object sender, TextCompositionEventArgs e)//обработчик
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }

        private void TextBoxName_PreviewKeyDown(object sender, KeyEventArgs e)//обработчик
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void TextBoxProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)//обработчик
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }

        private void TextBoxProduct_PreviewKeyDown(object sender, KeyEventArgs e)//обработчик
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void TextBoxPriceProduct_PreviewKeyDown(object sender, KeyEventArgs e)//обработчик
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void TextBoxPriceProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)//обработчик
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
