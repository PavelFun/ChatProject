using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Берем метод взаимодействия клиента и сервера "Hub".
using Microsoft.AspNet.SignalR;
// Используем описанную модель клиента.
using ChatProject.Models;

namespace ChatProject.Hubs
{
    class CHub : Hub
    {
        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="_name">Имя пользователя</param>
        /// <param name="_mess">Текст сообщения</param>
        public void SMess(string _name, string _mess)
        {
            
        }
        /// <summary>
        /// Установить соединение с приложением
        /// </summary>
        /// <param name="_name">Имя пользователя.</param>
        public void Connect(string _name)
        {

        }

        /// <summary>
        /// Отключение пользователя
        /// </summary>
        public void Stop()
        {

        }

    }
}
