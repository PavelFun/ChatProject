using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;


namespace ChatClientV2._0
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Имя пользователя, или логин.
        /// </summary>
        static string userName;
        /// <summary>
        /// Адрес для подключения.
        /// </summary>
        private const string host = "192.168.1.102";
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

        public Form1()
        {
            InitializeComponent();
            rtb_chat.Enabled = false;
           
        }
        /// <summary>
        /// Операция отправки сообщения
        /// </summary>
        static void SendMessage()
        {
            while (true)
            {
                string message;
                Form1 fm = new Form1();
                message= fm.rtb_send.Text;
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
                    Form1 fm = new Form1();
                    fm.rtb_chat.Text = message;

                }
                catch
                {
                    MessageBox.Show("Подключение прервано!"); //поведение в случае потери связи
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
        private void rtb_chat_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tb_NIC_TextChanged(object sender, EventArgs e)
        {
            userName = tb_NIC.Text;
        }
        private void conect()
        {
            client = new TcpClient();
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
                rtb_chat.Text = ("Добро пожаловать" + userName);
                SendMessage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Иначе выводим сообщение о возникшей ошибке.
            }
            finally
            {
                Disconnect();
            }
        }
        private void btn_enter_Click(object sender, EventArgs e)
        {
            conect();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SendMessage();
            rtb_send.Clear();
        }

        private void btn_send_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
