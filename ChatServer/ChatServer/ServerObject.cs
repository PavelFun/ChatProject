using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace ChatServer
{
    public class ServerObject
    {
        /// <summary>
        /// Объект для прослушивания всех подключений.
        /// </summary>
        static TcpListener tcpListener;
        /// <summary>
        /// Список всех подключений.
        /// </summary>
        List<ClientObject> clients = new List<ClientObject>();
        /// <summary>
        /// Добавление в список клиента.
        /// </summary>
        /// <param name="clientObject">Объект "Клиент".</param>
        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        /// <summary>
        /// Удаление клиента из списка подключений.
        /// </summary>
        /// <param name="id">id клиента.</param>
        protected internal void RemoveConnection(string id)
        {
            //Получаем id клиента из списка.
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);
            //Удаляем этого клиента.
            if (client != null)
                clients.Remove(client);
        }
        /// <summary>
        /// Прослушивание подключений.
        /// </summary>
        protected internal void Listen()
        {
            try
            {
                //Создание потока прослушивания и его запуск
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                //Сообщение о запуске.
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                ///Бесконечный цикл, открываем новые потоки для каждого принятого подключения.
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        /// <summary>
        /// Трансляция сообщения всем клиентам сервера.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        protected internal void BroadcastMessage(string message, string id)
        {
            
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
               // if (clients[i].Id != id) // если id клиента не равно id отправляющего
               // {
                    clients[i].Stream.Write(data, 0, data.Length); //передача данных
               // }
            }
        }
        /// <summary>
        /// Оключение всех клиентов.
        /// </summary>
        protected internal void Disconnect()
        {
            tcpListener.Stop(); //Остановка сервера.
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //Отключение клиента.
            }
            Environment.Exit(0); //Завершение процесса (работы программы).
        }
    }
}
