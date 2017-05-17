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
        static List<User> Users = new List<User>();

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="_name">Имя пользователя</param>
        /// <param name="_mess">Текст сообщения</param>
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
        /// <summary>
        /// Установить соединение с приложением
        /// </summary>
        /// <param name="_name">Имя пользователя.</param>
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        /// <summary>
        /// Отключение пользователя
        /// </summary>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}