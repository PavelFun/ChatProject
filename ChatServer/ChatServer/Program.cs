using System;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        /// <summary>
        /// Объект "Сервер".
        /// </summary>
        static ServerObject server;
        /// <summary>
        /// Поток для прослушивания.
        /// </summary>
        static Thread listenThread;

        static void Main(string[] args)
        {
            try
            {
                //Запуск сервера и запуск потока прослушивания.
                server = new ServerObject();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                //В случае ошибки закрываем сервер и выводим сообщение об ошибке.
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
