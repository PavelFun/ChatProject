using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Models
{
    public class Client
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Login { get; set; }
    }
}
