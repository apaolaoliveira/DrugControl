using DrugControl.PrescriptionModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.MedicineModule
{
    internal class MedicineInterface : InterfaceBase
    {
        public MedicineRepository medicineRepository = null;
        public SupplierRepository supplierRepository = null;

        public void MedicineOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("medicine options", "Add new medicine", "View medicine's list",
                                             "Edit medicine information", "Remove a medicine", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewMedicine(); break;
                    case 2: ViewMedicineTable(); break;
                    case 3: EditMedicine(); break;
                    case 4: RemoveMedicine(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        public void AddNewMedicine()
        {
            SetHeader("Add new medicine");

            bool hasSupplier = medicineRepository.HasSupplier();

            if (hasSupplier == false)
            {
                ColorfulMessage("\nYou need at least one supplier to add a medicine!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            string name = SetStringField("Name:", ConsoleColor.Cyan);

            string description = SetStringField("Description:", ConsoleColor.Cyan);

            int amount = SetIntField("Total Amount:", ConsoleColor.Cyan);

            int limitAmount = SetIntField("Limit Amount per person:", ConsoleColor.Cyan);

            ColorfulMessage("\nAvailability:", ConsoleColor.Cyan);

            int availability = SetIntField(
                "[1] Avaiable." +
                "\n[2] Unavailable.", ConsoleColor.Gray);

            string availabilityStatus = medicineRepository.GetStatus(availability);

            int supplierId = SetIntField("Supplier Id:", ConsoleColor.Cyan);

            int selecetedId = supplierRepository.isValidId(supplierId, supplierRepository);

            Supplier supplier = (Supplier)supplierRepository.GetId(selecetedId, supplierRepository);

            Medicine newMedicine = new Medicine(name, description, amount, limitAmount, availabilityStatus, supplier);

            medicineRepository.AddNewEntity(newMedicine);

            ColorfulMessage("\nNew medicine successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        public void ViewMedicineTable()
        {
            SetHeader("medicine's added");
            string[] columnNames = { "id", "supplier", "name", "description", "amount", "limit", "status" };
            int[] columnWidths = { 4, 15, 15, 20, 7, 6, 17 };

            List<object> data = new List<object>();

            bool hasMedicine = medicineRepository.HasEntity();

            if (hasMedicine == true)
            {
                foreach (Medicine medicine in medicineRepository.list)
                {
                    data.Add(new object[]
                    {
                               medicine.id, medicine.Supplier.Name, medicine.Name, medicine.Description,
                               medicine.Amount, medicine.LimitAmount, medicine.Status
                    });
                }
            }
            else
            {
                ColorfulMessage("\nYou don't have any medicine in your list yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);
            SetFooter();
        }

        public void EditMedicine()
        {
            SetHeader("edit medicine");

            bool hasMedicine = medicineRepository.HasEntity();

            if (hasMedicine == false)
            {
                ColorfulMessage("\nYou don't have any medicine yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewMedicineTable();

            int selectedId = SetIntField("\nEnter the medicine Id:", ConsoleColor.Cyan);
            int newSelectedId = medicineRepository.isValidId(selectedId, medicineRepository);
            medicineRepository.GetId(newSelectedId, medicineRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                   "\n[1] Supplier"
                                 + "\n[2] Name"
                                 + "\n[3] Description"
                                 + "\n[4] Total Amount"
                                 + "\n[5] Limit per person"
                                 + "\n[6] Status"
                                 , ConsoleColor.Cyan);

            bool validOption = false;

            foreach (Medicine medicine in medicineRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            int newSupplier = SetIntField("New supplier Id:", ConsoleColor.Gray);

                            int selecetedId = supplierRepository.isValidId(newSupplier, supplierRepository);
                            Supplier supplier = (Supplier)supplierRepository.GetId(selecetedId, supplierRepository);

                            medicine.Supplier = supplier;

                            ColorfulMessage("\nSupplier updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            string newName = SetStringField("New name:", ConsoleColor.Gray);
                            medicine.Name = newName;

                            ColorfulMessage("\nName updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            string newDescription = SetStringField("New description:", ConsoleColor.Gray);
                            medicine.Description = newDescription;

                            ColorfulMessage("\nDescription updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            int newAmount = SetIntField("New total amount:", ConsoleColor.Gray);
                            medicine.Amount = newAmount;

                            ColorfulMessage("\nAmount updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            int newLimit = SetIntField("New limit per person:", ConsoleColor.Gray);
                            medicine.LimitAmount = newLimit;

                            ColorfulMessage("\nLimit amount updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 6:
                            int newStatus = SetIntField("New Status:" +
                                                        "[1] Avaiable." +
                                                        "\n[2] Unavailable.", ConsoleColor.Gray);

                            string availabilityStatus = medicineRepository.GetStatus(newStatus);
                            medicine.Status = availabilityStatus;

                            ColorfulMessage("\nStatus updated!", ConsoleColor.Green);

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

        public void RemoveMedicine()
        {
            SetHeader("remove medicine");

            bool hasMedicine = medicineRepository.HasEntity();

            if (hasMedicine == false)
            {
                ColorfulMessage("\nYou don't have any medicine yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewMedicineTable();

            int selectedId = SetIntField("\nEnter the medicine Id:", ConsoleColor.Cyan);

            int newSelectedId = medicineRepository.isValidId(selectedId, medicineRepository);

            medicineRepository.RemoveEntity(newSelectedId, medicineRepository);

            ColorfulMessage("\nMedicine sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
