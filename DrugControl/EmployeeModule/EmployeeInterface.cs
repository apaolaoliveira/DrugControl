using DrugControl.PatientModule;
using DrugControl.PrescriptionModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.EmployeeModule
{
    internal class EmployeeInterface : InterfaceBase
    {
        public EmployeeRepository employeeRepository = null;

        public void SupplierOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("employee options", "Add new employee", "View employee's list",
                                             "Edit employee information", "Remove a employee", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewEmployee();    break;
                    case 2: ViewEmployeeTable(); break;
                    case 3: EditEmployee();      break;
                    case 4: RemoveEmployee();    break;
                    case 5: proceed = false;     break;
                }
            }
        }

        public void AddNewEmployee()
        {
            SetHeader("Add new employee");

            string name = SetStringField("Name:", ConsoleColor.Cyan);

            string address = SetStringField("Address:", ConsoleColor.Cyan);

            long phone = SetLongField("Phone:", ConsoleColor.Cyan);

            long cpf = SetLongField("CPF:", ConsoleColor.Cyan);

            string username = SetStringField("Username:", ConsoleColor.Cyan);

            string password = SetStringField("Password:", ConsoleColor.Cyan);

            employeeRepository.SetKeys(username, password);

            Employee newEmployee = new Employee(name, cpf, phone, username, password, address);

            employeeRepository.AddNewEntity(newEmployee);

            ColorfulMessage("\nNew employee successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        public void ViewEmployeeTable()
        {
            SetHeader("employee's table");
            string[] columnNames = { "id", "name", "cpf", "phone", "address", "username" };
            int[] columnWidths = { 4, 15, 15, 15, 30, 15 };

            List<object> data = new List<object>();

            bool hasEmployee = employeeRepository.HasEntity();

            if (hasEmployee == true)
            {
                foreach (Employee employee in employeeRepository.list)
                {
                    data.Add(new object[] { employee.id, employee.Name, employee.Cpf, employee.Phone, employee.Address, employee.Username });
                }
            }
            else
            {
                ColorfulMessage("\nYou don't have any employee in your list yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);
            SetFooter();
        }

        public void ChangeUsernameOrPassword(string change)
        {
            ColorfulMessage($"\n\nChange {change}", ConsoleColor.DarkYellow);

            bool validPassword = false;

            while (!validPassword)
            {
                string oldPassword = SetStringField("\n\nOld password:", ConsoleColor.Cyan);

                if (employeeRepository.isValidPassword(oldPassword))
                {
                    validPassword = true;
                }
                else
                {
                    ColorfulMessage("\nInvalid password. " +
                                    "\n\nPlease check your password and try again.", ConsoleColor.Red);
                    SetFooter();
                }
            }
        }

        public void EditEmployee()
        {
            SetHeader("edit employee");

            bool hasEmployee = employeeRepository.HasEntity();

            if (hasEmployee == false)
            {
                ColorfulMessage("\nYou don't have any employee yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewEmployeeTable();

            int selectedId = SetIntField("\nEnter the employee Id:", ConsoleColor.Cyan);
            int newSelectedId = employeeRepository.isValidId(selectedId, employeeRepository);
            employeeRepository.GetId(newSelectedId, employeeRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                  "\n[1] Name"
                                + "\n[2] CPF"
                                + "\n[3] Phone"
                                + "\n[4] Address"
                                + "\n[5] Username"
                                + "\n[6] Password"
                                + "\n* For security reasons, to change your username and password, you will have to input your old password.\n"
                                , ConsoleColor.Cyan);

            bool validOption = false;

            foreach (Employee employee in employeeRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            string newName = SetStringField("New name:", ConsoleColor.Gray);
                            employee.Name = newName;

                            ColorfulMessage("\nName updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            long newCpf = SetIntField("New CPF:", ConsoleColor.Gray);
                            employee.Cpf = newCpf;

                            ColorfulMessage("\nCPF updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            long newPhone = SetIntField("New Phone:", ConsoleColor.Gray);
                            employee.Phone = newPhone;

                            ColorfulMessage("\nPhone updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            string newAddress = SetStringField("New Address:", ConsoleColor.Gray);
                            employee.Address = newAddress;

                            ColorfulMessage("\nAddress updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            string username = "username";
                            ChangeUsernameOrPassword(username);
                            
                            string newUsername = SetStringField("New username:", ConsoleColor.Gray);
                            employee.Username = newUsername;

                            ColorfulMessage("\nUsername updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 6:
                            string password = "password";
                            ChangeUsernameOrPassword(password);
                            
                            string newPassword = SetStringField("New password:", ConsoleColor.Gray);
                            employee.Password = newPassword;

                            ColorfulMessage("\nPassword updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        default:
                            ColorfulMessage("\nInvalid option selected! Try again:" + "\n→ ", ConsoleColor.Red);
                            selectedChange = Convert.ToInt32(Console.ReadLine());
                            break;
                    }
                    break;
                }
            }
            SetFooter();
        }    

        public void RemoveEmployee()
        {
            SetHeader("remove employee");

            bool hasEmployee = employeeRepository.HasEntity();

            if (hasEmployee == false)
            {
                ColorfulMessage("\nYou don't have any employee yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewEmployeeTable();

            int selectedId = SetIntField("\nEnter the employee Id:", ConsoleColor.Cyan);

            int newSelectedId = employeeRepository.isValidId(selectedId, employeeRepository);

            employeeRepository.RemoveEntity(newSelectedId, employeeRepository);

            ColorfulMessage("\nEmployee sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
