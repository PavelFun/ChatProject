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
        /// Адрес для подключения.
        /// </summary>
        private const string host = "127.0.0.1";
        /// <summary>
        /// Порт для подключения
        /// <summary>
        /// </summary>
        private const int port = 8888;
        /// Объект класса "Клиент".
        /// </summary>
        static TcpClient client;
        /// <summary>
        /// Объект класса "Сетевой поток".
        /// </summary>
        static NetworkStream stream;
        /// <summary>
        /// пустая строка 
        /// </summary>
        string readData = null;

        public Form1()
        {
            InitializeComponent();
            client = new TcpClient(); // Создание нового объекта "Клиент".
        }
        /// <summary>
        /// Операция отправки сообщения
        /// </summary>
         void SendMessage()
        {
                byte[] data = Encoding.Unicode.GetBytes(rtb_send.Text);
                stream.Write(data, 0, data.Length);
                stream.Flush();//читска буфера
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
            rtb_chat.SelectionStart = rtb_chat.Text.Length;//автоматическая прокрутка чата 
            rtb_chat.ScrollToCaret();
        }

        private void tb_NIC_TextChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// метод который реагирует на нажатие кнопки вход 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_enter_Click(object sender, EventArgs e)
        {
           
            client.Connect(host, port); //подключение клиента.
            stream = client.GetStream(); // получаем поток.
            
            //Создание буффера для передачи ника на сервер 
            byte[] buff_Nic = Encoding.Unicode.GetBytes(tb_NIC.Text);
            stream.Write(buff_Nic, 0, buff_Nic.Length);//запись буффера в поток
            stream.Flush();//читска буффера
            rtb_chat.Text = "Подключение выполнено";
            btn_send_Click(this, e);// запуск приема сообщений от чата .

        }
        /// <summary>
        /// метод который реагирует на нажатие кнопки отправить .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            stream = client.GetStream();

            byte[] outStream = Encoding.Unicode.GetBytes(rtb_send.Text);// создание буфера и записывание туда содержимого текст бокса .
            stream.Write(outStream, 0, outStream.Length);//запись в поток буфера
            stream.Flush();//Чистка буфера.
            rtb_send.Text = null;//Очистка текст бокса .
            Thread chatTH = new Thread(Get_message);//создание нового потока .
            chatTH.Start();// запуск потока . 
            SendMessage();// вызов метода отправки сообщений .
        }

        private void btn_send_KeyDown(object sender, KeyEventArgs e)
        {
     
        }
        /// <summary>
        /// метод для получения сообщений .
        /// </summary>
        private void Get_message()
        {
            while (true)
            {
                try
                {

                    stream = client.GetStream(); // получени потока для с работы с сообщениями 
                    int buff_size = 0;
                    byte[] inStream = new byte[10025];
                    buff_size = 1000;
                    stream.Read(inStream, 0, buff_size);
                    string returndata = Encoding.Unicode.GetString(inStream);
                    readData = " " + returndata;
                    msg();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Разорвано соединение с сервером("); // Иначе выводим сообщение о возникшей ошибке.
                    Disconnect();
                }
            }
            
        }
        /// <summary>
        /// метод передачи сообщения в чат .
        /// </summary>
        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                rtb_chat.Text = rtb_chat.Text + Environment.NewLine + ">>" + readData;
        }

        private void rtb_send_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// метод копирования .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_send.Copy();
        }
        /// <summary>
        /// метод вставки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_send.Paste();
        }
        /// <summary>
        /// метод вырезать.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_send.Cut();
        }
        /// <summary>
        /// метод Об авторах.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработчиками данной программы являются : Павел Гуничев , Морозов Иван, Мокров ФАН Макс,Смирнов Дмитрий , Зарубина Анастасия. ");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Disconnect();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
