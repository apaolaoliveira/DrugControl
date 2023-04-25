using DrugControl.MedicineModule;
using DrugControl.OrderModule;
using DrugControl.PatientModule;
using DrugControl.PrescriptionModule;
using DrugControl.Shared;

namespace DrugControl.SupplierModule
{
    internal class SupplierInterface : InterfaceBase
    {
        public SupplierRepository supplierRepository = null;

        public void SupplierOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("supplier options", "Add new supplier", "View supplier's list",
                                             "Edit supplier information", "Remove a supplier", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewSupplier();    break;
                    case 2: ViewSupplierTable(); break;
                    case 3: EditSupplier();      break;
                    case 4: RemoveSupplier();    break;
                    case 5: proceed = false;     break;
                }
            }
        }

        public void AddNewSupplier()
        {
            SetHeader("Add new supplier");

            string name = SetStringField("Name:", ConsoleColor.Cyan);

            string address = SetStringField("Address:", ConsoleColor.Cyan);

            long phone = SetLongField("Phone:", ConsoleColor.Cyan);

            long cnpj = SetLongField("CNPJ:", ConsoleColor.Cyan);

            Supplier newSupplier = new Supplier(name, cnpj, phone, address);   
            
            supplierRepository.AddNewEntity(newSupplier);

            ColorfulMessage("\nNew supplier successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        public void ViewSupplierTable()
        {
            SetHeader("supplier's table");
            string[] columnNames = { "id", "name", "cnpj", "phone", "address" };
            int[] columnWidths = { 4, 15, 15, 15, 30 };

            List<object> data = new List<object>();

            bool hasSupplier = supplierRepository.HasEntity();

            if (hasSupplier == true)
            {
                foreach (Supplier supplier in supplierRepository.list)
                {
                    data.Add(new object[] { supplier.id, supplier.Name, supplier.Cnpj, supplier.Phone, supplier.Address });
                }
            }
            else
            {
                ColorfulMessage("\nYou don't have any supplier in your list yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);
            SetFooter();
        }

        public void EditSupplier()
        {
            SetHeader("edit supplier");

            bool hasSupplier = supplierRepository.HasEntity();

            if (hasSupplier == false)
            {
                ColorfulMessage("\nYou don't have any supplier yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewSupplierTable();

            int selectedId = SetIntField("\nEnter the suppplier's Id:", ConsoleColor.Cyan);
            int newSelectedId = supplierRepository.isValidId(selectedId, supplierRepository);
            supplierRepository.GetId(newSelectedId, supplierRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                  "\n[1] Name"
                                + "\n[2] CNPJ"
                                + "\n[3] Phone"
                                + "\n[4] Address"
                                , ConsoleColor.Gray);
            bool validOption = false;

            foreach (Supplier supplier in supplierRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            string newName = SetStringField("New name:", ConsoleColor.Gray);
                            supplier.Name = newName;

                            ColorfulMessage("\nName updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            long newCnpj = SetLongField("New CNPJ:", ConsoleColor.Gray);
                            supplier.Cnpj = newCnpj;

                            ColorfulMessage("\nCNPJ updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            long newPhone = SetLongField("New phone:", ConsoleColor.Gray);
                            supplier.Phone = newPhone;

                            ColorfulMessage("\nPhone updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            string newAddress = SetStringField("New address:", ConsoleColor.Gray);
                            supplier.Address = newAddress;

                            ColorfulMessage("\nAddress updated!", ConsoleColor.Green);

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

        public void RemoveSupplier()
        {
            SetHeader("remove supplier");

            bool hasSupplier = supplierRepository.HasEntity();

            if (hasSupplier == false)
            {
                ColorfulMessage("\nYou don't have any supplier yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewSupplierTable();

            int selectedId = SetIntField("\nEnter the supplier Id:", ConsoleColor.Cyan);
            int newSelectedId = supplierRepository.isValidId(selectedId, supplierRepository);
            supplierRepository.RemoveEntity(newSelectedId, supplierRepository);

            ColorfulMessage("\nSupplier sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
