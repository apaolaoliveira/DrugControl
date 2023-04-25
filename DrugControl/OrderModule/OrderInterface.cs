using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.PatientModule;
using DrugControl.PrescriptionModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.OrderModule
{
    internal class OrderInterface : InterfaceBase
    {
        public OrderRepository orderRepository = null;
        public SupplierRepository supplierRepository = null;
        public MedicineRepository medicineRepository = null;
        public EmployeeRepository employeeRepository = null;

        public void OrderOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("order options", "Add new order", "View order's list",
                                             "Edit order information", "Remove a order", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewOrder();    break;
                    case 2: ViewOrderTable(); break;
                    case 3: EditOrder();      break;
                    case 4: RemoveOrder();    break;
                    case 5: proceed = false;  break;
                }
            }
        }

        public void AddNewOrder()
        {
            SetHeader("Add new order");

            int supplier = SetIntField("Supplier Id:", ConsoleColor.Cyan);
            Supplier getSupplier = (Supplier)supplierRepository.GetId(supplier, supplierRepository);

            int medicine = SetIntField("Medicine Id:", ConsoleColor.Cyan);
            Medicine getMedicine = (Medicine)medicineRepository.GetId(medicine, medicineRepository);

            int employee = SetIntField("Employee Id:", ConsoleColor.Cyan);
            Employee getEmployee = (Employee)employeeRepository.GetId(employee, employeeRepository);

            int amount = SetIntField("Amount:", ConsoleColor.Cyan);

            string date = SetStringField("Date:", ConsoleColor.Cyan);

            Order newOrder = new Order(getSupplier, getMedicine, getEmployee, date, amount);

            orderRepository.AddNewEntity(newOrder);

            ColorfulMessage("\nNew order successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        public void ViewOrderTable()
        {
            SetHeader("order's table");
            string[] columnNames = { "id", "supplier", "medicine", "employee", "amount", "date"};
            int[] columnWidths = { 4, 15, 15, 15, 10, 12 };

            List<object> data = new List<object>();

            bool hasOrder = orderRepository.HasEntity();

            if (hasOrder == true)
            {
                foreach (Order order in orderRepository.list)
                {
                    data.Add(new object[]
                    {
                       order.id, order.Supplier.Name, order.Employee.Name, order.Amount, order.Date
                    });
                }
            }
            else
            {
                ColorfulMessage("\nYou don't have any order in your list yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);
            SetFooter();
        }

        public void EditOrder()
        {
            SetHeader("edit order");

            bool hasOrder = orderRepository.HasEntity();

            if (hasOrder == false)
            {
                ColorfulMessage("\nYou don't have any order yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewOrderTable();

            int selectedId = SetIntField("\nEnter the order Id:", ConsoleColor.Cyan);
            int newSelectedId = orderRepository.isValidId(selectedId, orderRepository);
            orderRepository.GetId(newSelectedId, orderRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                  "\n[1] Supplier"
                                + "\n[2] Medicine"
                                + "\n[3] Employee"
                                + "\n[4] Amount"
                                + "\n[5] Date"
                                , ConsoleColor.Gray);

            bool validOption = false;

            foreach (Order order in orderRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            int newSupplier = SetIntField("New supplier's Id :", ConsoleColor.Gray);

                            int selecetedSupplierId = supplierRepository.isValidId(newSupplier, supplierRepository);
                            Supplier supplier = (Supplier)supplierRepository.GetId(selecetedSupplierId, supplierRepository);

                            order.Supplier = supplier;

                            ColorfulMessage("\nSupplier updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            int newMedicine = SetIntField("New medicine's Id:", ConsoleColor.Gray);

                            int selecetedMedicineId = medicineRepository.isValidId(newMedicine, medicineRepository);
                            Medicine medicine = (Medicine)medicineRepository.GetId(selecetedMedicineId, medicineRepository);

                            order.Medicine = medicine;

                            ColorfulMessage("\nMedicine updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            int newEmployee = SetIntField("New employee's Id:", ConsoleColor.Gray);

                            int selecetedEmployeeId = employeeRepository.isValidId(newEmployee, employeeRepository);
                            Employee employee = (Employee)employeeRepository.GetId(selecetedEmployeeId, employeeRepository);

                            order.Employee = employee;

                            ColorfulMessage("\nEmployee updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            int newAmount = SetIntField("New amount:", ConsoleColor.Gray);
                            order.Amount = newAmount;

                            ColorfulMessage("\nAmount updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            string newDate = SetStringField("New date:", ConsoleColor.Gray);
                            order.Date = newDate;

                            ColorfulMessage("\nDate updated!", ConsoleColor.Green);

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

        public void RemoveOrder()
        {
            SetHeader("remove order");

            bool hasOrder = orderRepository.HasEntity();

            if (hasOrder == false)
            {
                ColorfulMessage("\nYou don't have any order yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewOrderTable();

            int selectedId = SetIntField("\nEnter the order Id:", ConsoleColor.Cyan);

            int newSelectedId = orderRepository.isValidId(selectedId, orderRepository);

            orderRepository.RemoveEntity(newSelectedId, orderRepository);

            ColorfulMessage("\nOrder sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
