using DrugControl.Shared;
using System.Data;

namespace DrugControl.EmployeeModule
{
    internal class Employee : EntityBase
    {
        public Employee(string name, long cpf, long phone, string login, string password, string address)
        {
            Name = name;
            Cpf = cpf;
            Phone = phone;
            Username = login;
            Password = password;
            Address = address;
        }

        public string Name { get; set; }
        public long Cpf { get; set; }
        public long Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
