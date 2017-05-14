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
        static List<Client> Users = new List<Client>();

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="_name">Имя пользователя</param>
        /// <param name="_mess">Текст сообщения</param>
        public void SMess(string _name, string _mess)
        {
            Clients.All.addMessage(_name, _mess);
        }
        /// <summary>
        /// Установить соединение с приложением
        /// </summary>
        /// <param name="_name">Имя пользователя.</param>
        public void Connect(string _name)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ID == id))
            {
                Users.Add(new Client { ID = id, Login = _name });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, _name, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, _name);
            }
        }

        /// <summary>
        /// Отключение пользователя
        /// </summary>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ID == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Login);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}