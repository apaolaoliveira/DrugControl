using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.PatientModule
{
    internal class PatientInterface : InterfaceBase
    {
        public PatientRepository patientRepository = null;

        public void PatientOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("patient options", "Add new patient", "View patient's list", 
                                             "Edit patient information", "Remove a patient", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewPatient();    break;
                    case 2: ViewPatientTable(); break;
                    case 3: EditPatient();      break;
                    case 4: RemovePatient();    break;
                    case 5: proceed = false;    break;
                }
            }
        }

        public void AddNewPatient()
        {
            SetHeader("Add new patient");

            string name = SetStringField("Name:", ConsoleColor.Cyan);

            string address = SetStringField("Address:", ConsoleColor.Cyan);

            long phone = SetLongField("Phone:", ConsoleColor.Cyan);

            long cpf = SetLongField("CPF:", ConsoleColor.Cyan);

            long susCard = SetLongField("SUS card:", ConsoleColor.Cyan);

            Patient newPatient = new Patient(name, address, cpf, susCard, phone);

            patientRepository.AddNewEntity(newPatient);

            ColorfulMessage("\nNew patient successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        public void ViewPatientTable()
        {
            SetHeader("patient's table");
            string[] columnNames = { "id", "name", "cpf", "sus card", "phone", "address" };
            int[] columnWidths = { 4, 15, 15, 15, 15, 30 };

            List<object> data = new List<object>();

            bool hasPatient = patientRepository.HasEntity();

            if (hasPatient == true)
            {
                foreach (Patient patient in patientRepository.list)
                {
                    data.Add(new object[]
                    {
                        patient.id, patient.Name, patient.Cpf,
                        patient.SUSCard, patient.Phone, patient.Address
                    });
                }
            }
            else
            {
                ColorfulMessage("\nYou don't have any patient in your list yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);
            SetFooter();
        }

        public void EditPatient()
        {
            SetHeader("edit patient");

            bool hasPatient = patientRepository.HasEntity();

            if (hasPatient == false)
            {
                ColorfulMessage("\nYou don't have any patient yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewPatientTable();

            int selectedId = SetIntField("\nEnter the patient Id:", ConsoleColor.Cyan);
            int newSelectedId = patientRepository.isValidId(selectedId, patientRepository);
            patientRepository.GetId(newSelectedId, patientRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                  "\n[1] Name"
                                + "\n[2] CPF"
                                + "\n[3] SUS card"
                                + "\n[4] Phone"
                                + "\n[5] Address"
                                , ConsoleColor.Gray);
            bool validOption = false;

            foreach (Patient patient in patientRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            string newName = SetStringField("New name:", ConsoleColor.Gray);
                            patient.Name = newName;

                            ColorfulMessage("\nName updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            long newCpf = SetIntField("New CPF:", ConsoleColor.Gray);
                            patient.Cpf = newCpf;

                            ColorfulMessage("\nCPF updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            long newSUSCard = SetIntField("New SUS card:", ConsoleColor.Gray);
                            patient.SUSCard = newSUSCard;

                            ColorfulMessage("\nSUS Card updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            long newPhone = SetIntField("New phone:", ConsoleColor.Gray);
                            patient.Phone = newPhone;

                            ColorfulMessage("\nPhone updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            string newAddress = SetStringField("New address:", ConsoleColor.Gray);
                            patient.Address = newAddress;

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

        public void RemovePatient()
        {
            SetHeader("remove patient");

            bool hasPatient = patientRepository.HasEntity();

            if (hasPatient == false)
            {
                ColorfulMessage("\nYou don't have any patient yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewPatientTable();

            int selectedId = SetIntField("\nEnter the patient Id:", ConsoleColor.Cyan);

            int newSelectedId = patientRepository.isValidId(selectedId, patientRepository);

            patientRepository.RemoveEntity(newSelectedId, patientRepository);

            ColorfulMessage("\nPatient sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
