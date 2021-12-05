using Microsoft.Win32;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Denis_osma
{
    class Shop
    {
        public int OrderCount { get; set; } // счетчик заказов 
        private Order[] orderArray; // массива заказов 
        public int ArraySize { get; set; } // размер массива
        public Shop(int arraysize) // конструктор
        {
            OrderCount = 0;
            ArraySize = arraysize;
            SetOrderArray(new Order[ArraySize]); // создание массива
        }
        public Order[] GetOrderArray() // получить массив
        {
            return orderArray;
        }

        public void SetOrderArray(Order[] value) // изменить массив
        {
            orderArray = value;
        }
        public bool AddOrder(string lastName) // добавление заказа 
        {
            Order order = new Order(lastName); // создание заказа
            orderArray[OrderCount] = order; // добавление заказа в конец
            OrderCount++; // увеличение счетчика
            if (OrderCount == ArraySize) // если массив заполнен
            {
                ArraySize *= 2;
                Array.Resize(ref orderArray, ArraySize); // увеличиваем его размер на 2 
                return true;
            }
            return false;
        }
        public bool AddOrder(Order order) // добавление заказа 
        {
            orderArray[OrderCount] = order; // добавление заказа в конец
            OrderCount++; // увеличение счетчика
            if (OrderCount == ArraySize) // если массив заполнен
            {
                ArraySize *= 2;
                Array.Resize(ref orderArray, ArraySize); // увеличиваем его размер на 2 
                return true;
            }
            return false;
        }
        public bool DeleteOrder() // удаление заказа
        {
            if (OrderCount != 0) // если массив не пуст
            {
                orderArray[0] = null; // удаление заказа из начала
                for (int i = 1; i < OrderCount; i++) // сдвиг
                    orderArray[i - 1] = orderArray[i];
                OrderCount--; // уменьшение счетчика
                return true;
            }
            else return false;
        }
        public int FindOrder(string lastName) // Поиск заказа
        {
            for (int i = 0; i < OrderCount; i++) // проход по массиву
            {
                if (orderArray[i].LastName == lastName)// если имена совпали 
                {
                    return i; // вовращаем индекс 
                }
            }
            return -1;
        }
        public Order FindOrder(Order order) // Поиск заказа
        {
            for (int i = 0; i < OrderCount; i++) // проход по массиву
            {
                if (orderArray[i].LastName == order.LastName)// если имена совпали 
                {
                    return orderArray[i]; // вовращаем 
                }
            }
            return null;
        }
        public int ShopSum() // Подсчет суммы 
        {
            int shopSum = 0;
            for (int i = 0; i < OrderCount; i++) // проход по массиву с подсчетом суммы
            {
                shopSum += orderArray[i].OrderSum();
            }
            return shopSum;
        }
        public string AllData()  // вывод информации о магазине 
        {
            string data = null;
            for (int i = 0; i < OrderCount; i++) // проход по массиву с выводом информации
            {
                data += orderArray[i].LastName + "    " + orderArray[i].Date + "    " + orderArray[i].ProductCount + "\n";
            }
            return data;
        }
        public bool Save(FileStream fs)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //if ((bool)saveFileDialog.ShowDialog())
            //    using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
            //    {
                    JsonSerializer.Serialize<Order[]>(new Utf8JsonWriter(fs), orderArray);
                    return true;
            //    }
            //return false;
        }
        public async Task Load(FileStream fs)
        {
            Order[] orders;
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if ((bool)openFileDialog.ShowDialog())
            //    using (FileStream fs = (FileStream)openFileDialog.OpenFile())
            //    {
                    orders = await JsonSerializer.DeserializeAsync<Order[]>(fs);
                    OrderCount = 0;
                    ArraySize = orderArray.Length;
                    SetOrderArray(new Order[ArraySize]);
                    foreach (var i in orders)
                    {
                        if (i != null)
                        {
                            AddOrder(i);
                        }
                        else
                        {
                            break;
                        }
                    }
                //}
        }
    }
}
