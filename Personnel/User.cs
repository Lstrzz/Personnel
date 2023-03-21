using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    internal class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int FK_role { get; set; }
        public string FIO { get; set; }
        public string Post { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Salary { get; set; }
        public string Inn { get; set; }
        public int FK_Status { get; set; }
        public User() { }
        public User(string login, string password, int fK_role, string fIO, string post, string phone, string mail, string salary, string inn, int fK_Status)
        {
            Login = login;
            Password = password;
            FK_role = fK_role;
            FIO = fIO;
            Post = post;
            Phone = phone;
            Mail = mail;
            Salary = salary;
            Inn = inn;
            FK_Status = fK_Status;
        }
        public User(string login, string password, int fK_role, string fIO, int fk_Status)
        {
            Login = login;
            Password = password;
            FK_role = fK_role;
            FIO = fIO;
            FK_Status = fk_Status;
        }
    }
}
