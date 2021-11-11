using System.Collections;
using System.ComponentModel;

namespace Denis_osma
{
    class Product : INotifyPropertyChanged
    {
        string name;
        int price;
        Product next;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public Product Next
        {
            get => next;
            set
            {
                next = value;
                OnPropertyChanged("Next");
            }
        }

        public Product(string name, int price) // конструктор 
        {
            Name = name;
            Price = price;
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

        //public IEnumerator GetEnumerator()
        //{
        //    Product product = this;
        //    while(product != null)
        //    {
        //        yield return product;
        //        product = product.Next;
        //    }
        //}
    }
}
