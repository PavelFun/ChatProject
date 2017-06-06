using System;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    /// <summary>
    /// Описание клиента.
    /// </summary>
    public class ClientObject
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        protected internal string Id { get; private set; }
        /// <summary>
        /// Поток взаимодействия с клиентом.
        /// </summary>
        protected internal NetworkStream Stream { get; private set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        string userName;
        /// <summary>
        /// Объект класса "Клиент" (TcpClient).
        /// </summary>
        TcpClient client;
        /// <summary>
        /// Объект класса "Сервер".
        /// </summary>
        ServerObject server;

        /// <summary>
        /// Создание клиента.
        /// </summary>
        /// <param name="tcpClient">Объект класса "Клиент"(TcpClient).</param>
        /// <param name="serverObject">Объект класса "Сервер.</param>
        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            
            //Глобальный уникальный идентификатор.
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            //Добавление в список подключений данного объекта "Клиент".
            serverObject.AddConnection(this);
        }

        /// <summary>
        /// Операция обмена сообщениями.
        /// </summary>
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                // Получение имени пользователя.
                string message = GetMessage();
                userName = message;

                message = userName + "    вошел в чат   ";
                // Сообщение о подключении какого-либо пользователя для всех пользователей.
                server.BroadcastMessage(message, this.Id);
                Console.WriteLine(message);
                //Бесконечный цикл, получения сообщения от клиентов.
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        message = String.Format("{0}: {1}", userName, message); //При отправке сообщений.
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                    }
                    catch
                    {
                        message = String.Format("{0}: покинул чат", userName); //При отключении.
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); //Сообщение о возможной ошибке.
            }
            finally
            {
                //Закрываем соединение.
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        /// <summary>
        /// Преобразование получаемого сообщения в строку.
        /// </summary>
        /// <returns></returns>
        private string GetMessage()
        {
            byte[] data = new byte[64]; // Буфер получения.
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        /// <summary>
        /// Операция по закрытию подключения.
        /// </summary>
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close(); //Закрываем поток.
            if (client != null)
                client.Close(); //Закрываем подключение.
        }
    }
}