using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR32
{
    public class User
    {
        public int UserID { get; set; }
        public string UserSurname { get; set; }
        public string UserName { get; set; }
        public string UserPatronymic { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int UserRole { get; set; }

        // Удобное ФИО
        public string Fio => $"{UserSurname} {UserName} {UserPatronymic}".Trim();

        // Удобное название роли
        public string RoleName
        {
            get
            {
                switch (UserRole)
                {
                    case 1: return "Администратор";
                    case 2: return "Менеджер";
                    case 3: return "Клиент";
                    default: return "Пользователь";
                }
            }
        }

    }
}
