using DrugControl.Shared;
using System.Collections;

namespace DrugControl.EmployeeModule
{
    internal class EmployeeRepository : RepositoryBase
    {
        public List<string> keys = new List<string>();

        public void SetKeys (string login, string password)
        {
            keys.Add(login + ":"+ password);
        }

        public void Mock() 
        {
            SetKeys("master", "masterkey");          
        }

        public bool isValidKey(string login, string password)
        {
            foreach (string key in keys)
            {
                string[] separator = key.Split(":");
                if (separator[0] == login && separator[1] == password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isValidPassword(string password)
        {
            foreach (string key in keys)
            {
                string[] separator = key.Split(":");
                if (separator[1] == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
