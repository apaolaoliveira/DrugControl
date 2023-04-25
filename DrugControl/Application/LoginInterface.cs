using DrugControl.EmployeeModule;
using DrugControl.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugControl.Application
{
    internal class LoginInterface : InterfaceBase
    {
        public EmployeeRepository employeeRepository = null;
        public MainMenu mainMenu = null;

        public void LogIn()
        {
            bool loggedIn = false;

            while (!loggedIn)
            {
                ColorfulMessage("\nWelcome to the Drug Control!"
                              + "\n----------------------------"
                                , ConsoleColor.DarkYellow);

                string login = SetStringField("\nLogin:", ConsoleColor.Cyan);

                string password = SetStringField("\nPassword:", ConsoleColor.Cyan);

                if (employeeRepository.isValidKey(login, password))
                {
                    mainMenu.ShowMainMenu();
                    loggedIn = true;
                }
                else
                {
                    ColorfulMessage("\nInvalid login credentials. " +
                                    "\n\nPlease check your username and password and try again.", ConsoleColor.Red);
                    SetFooter();
                    Console.Clear();
                }
            }
        }
    }
}
