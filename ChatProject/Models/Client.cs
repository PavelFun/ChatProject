using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Models
{
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }
    }
}