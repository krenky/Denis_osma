using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;

namespace Denis_osma

{
    class Order : INotifyPropertyChanged
    {
        string lastName;
        DateTime date;
        int productCount;
        Product head;
        Product last;
        Product prev;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        } // фамилия заказчика
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        } // дата заказа 
        public int ProductCount
        {
            get { return productCount; }
            set
            {
                productCount = value;
                OnPropertyChanged("ProductCount");
            }
        } // счетчик товаров 
        public Product Head
        {
            get { return head; }
            set
            {
                head = value;
                OnPropertyChanged("Head");
            }
        } // заголовок 
        public Order(string lastName) // конструктор 
        {
            LastName = lastName;
            Date = DateTime.Now; // установка текущей даты и времени 
            ProductCount = 0;
            Head = new Product("", 0); // создание заголовка
            last = null;
            prev = Head;
        }
        public void AddProduct(string name, int price) // добавление товара
        {
            bool check = true; // нужно ли добавлять в конец? 
            Product prev = Head;
            Product current = Head.Next; // записываем в переменную первый товар
            Product product = new Product(name, price); // создаем товар 
            if (ProductCount == 0) // если заказ пуст
            { // добавляем в начало
                Head.Next = product;
                last = product;
            }
            else // иначе
            {
                while (current != null) // проходим по товарам
                {
                    int comp = string.Compare(name, current.Name); // сравниваем строки
                    if (comp < 0) //если меньше
                    {//добавляем, с изменением указателей Next и Prev
                        check = false;
                        product.Next = current;
                        prev.Next = product;
                        break;
                    }
                    else // иначе, идем дальше
                    {
                        prev = current;
                        current = current.Next;
                    }
                }

                if (check) // если нужно добавлять в конец
                {//добавляем
                    last.Next = product;
                    last = product;
                }
            }
            ProductCount++; // увеличиваем счетчик 
        }
        public bool DeleteProduct(string name) // удаление товара 
        {
            if (ProductCount != 0) // если заказ не пуст 
            {
                Product current = FindProduct(name);
                if (current == null) // если такого товара нет
                {
                    return false; // возвращаем ложь
                }
                else if (current != last) // если есть
                {//удаляем с изменением указателей
                    prev.Next = current.Next;
                    current = null;
                    ProductCount--; // уменьшаем счетчик
                    return true;
                }
                else // если есть и это последний товар
                { //удаляем с изменением указателей на Last 
                    last = prev;
                    last.Next = null;
                    current = null;
                    ProductCount--;// уменьшаем счетчик
                    return true;
                }
            }
            else return false;
        }
        public Product FindProduct(string name) // Поиск товара 
        {
            Product current = Head.Next; // записываем в переменную первый товар
            while (current != null) // проходим по товарам 
            {
                if (current.Name == name) // если названия совпали
                    return current; // возвращаем
                else // иначе, идем дальше
                {
                    prev = current;
                    current = current.Next;
                }
            }
            return null;
        }
        public int OrderSum() // подсчет суммы
        {
            Product current = Head.Next; // записываем в переменную первый товар
            int orderSum = 0;
            while (current != null) // проход по заказу с подсчетом суммы
            {
                orderSum += current.Price;
                current = current.Next;
            }
            return orderSum;
        }
        public string OrderData() // вывод информации о заказе 
        {
            Product current = Head.Next; // записываем в переменную первый товар
            string orderData = "";
            while (current != null)// проход по заказу с выводом информации
            {
                orderData += current.Name + "   " + current.Price + "\n";
                current = current.Next;
            }
            return orderData;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)//обработчик изменения коллекции
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<Product> GetProduct()
        {
            Product products = head.Next;
            ObservableCollection<Product> productsCollection = new ObservableCollection<Product>();
            while(products != null)
            {
                productsCollection.Add(products);
                products = products.Next;
            }
            return productsCollection;
        }
    }
}
