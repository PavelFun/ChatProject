using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    class Program
    {
        /// <summary>
        /// Имя пользователя, или логин.
        /// </summary>
        static string userName; 
        /// <summary>
        /// Адрес для подключения.
        /// </summary>
        private const string host = "127.0.0.1";
        /// <summary>
        /// Порт для подключения
        /// </summary>
        private const int port = 8888;
        /// <summary>
        /// Объект класса "Клиент".
        /// </summary>
        static TcpClient client;
        /// <summary>
        /// Объект класса "Сетевой поток".
        /// </summary>
        static NetworkStream stream;

        static void Main(string[] args)
        {
            Console.Write("Введите свое имя: ");
            userName = Console.ReadLine();
            client = new TcpClient(); // Создание нового объекта "Клиент".
            try
            {
                client.Connect(host, port); //подключение клиента.
                stream = client.GetStream(); // получаем поток.

                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // Поток получения данных (исполняет операцию получения данных).
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //Запуск потока получения данных.
                Console.WriteLine("Добро пожаловать, {0}", userName);
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Иначе выводим сообщение о возникшей ошибке.
            }
            finally
            {
                Disconnect();
            }
        }
        /// <summary>
        /// Операция отправки сообщения
        /// </summary>
        static void SendMessage()
        {
            Console.WriteLine("Введите сообщение: ");

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// Операция приема сообщения
        /// </summary>
        static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получения сообщения
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);  //вывод сообщения
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //поведение в случае потери связи
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }
        /// <summary>
        /// Метод оключения (прерывание соединения).
        /// </summary>
        static void Disconnect()
        {
            if (stream != null)
                stream.Close();  //отключение потока
            if (client != null)
                client.Close();  //отключение клиента
            Environment.Exit(0); //завершение процесса
        }
    }
}
