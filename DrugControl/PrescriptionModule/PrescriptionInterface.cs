using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.PatientModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.PrescriptionModule
{
    internal class PrescriptionInterface : InterfaceBase
    {
        public PrescriptionRepository prescriptionRepository = null;
        public PatientRepository patientRepository = null;
        public MedicineRepository medicineRepository = null;
        public EmployeeRepository employeeRepository = null;

        public void PrescriptionOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("prescription options", "Add new prescription", "View prescription's list",
                                             "Edit prescription information", "Remove a prescription", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewPrescription(); break;
                    case 2: ViewPrescriptionTable(); break;
                    case 3: EditPrescription(); break;
                    case 4: RemovePrescription(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        public void AddNewPrescription()
        {
            SetHeader("Add new prescription");

            bool hasRequired = prescriptionRepository.HasEmployeeMedicineAndPatient();

            if (hasRequired == false)
            {
                SetFooter();
                return;
            }

            int patient = SetIntField("Patient Id:", ConsoleColor.Cyan);
            Patient getPatient = (Patient)patientRepository.GetId(patient, patientRepository);

            int medicine = SetIntField("Medicine Id:", ConsoleColor.Cyan);
            Medicine getMedicine = (Medicine)medicineRepository.GetId(medicine, medicineRepository);

            int employee = SetIntField("Employee Id:", ConsoleColor.Cyan);
            Employee getEmployee = (Employee)employeeRepository.GetId(employee, employeeRepository);

            int amount = SetIntField("Amount:", ConsoleColor.Cyan);

            string date = SetStringField("Date:", ConsoleColor.Cyan);

            string doctorName = SetStringField("Doctor's Name:", ConsoleColor.Cyan);

            Prescription newPrescription = new Prescription(getPatient, getMedicine, getEmployee, date, amount, doctorName);

            prescriptionRepository.AddNewEntity(newPrescription);

            ColorfulMessage("\nNew prescription successfully added!", ConsoleColor.Green);
            SetFooter();
        }

        public void ViewPrescriptionTable()
        {
            SetHeader("prescription's table");
            string[] columnNames = { "id", "patient", "medicine", "employee", "amount", "date", "doctor's name" };
            int[] columnWidths = { 4, 15, 15, 15, 10, 12, 15 };

            List<object> data = new List<object>();

            bool hasPrescrotion = prescriptionRepository.HasEntity();

            if (hasPrescrotion == true)
            {
                foreach (Prescription prescription in prescriptionRepository.list)
                {
                    data.Add(new object[]
                    {
                     prescription.id, prescription.Patient.Name, prescription.Medicine.Name, prescription.Employee.Name,
                     prescription.Amount, prescription.Date, prescription.DoctorName
                    });
                }
            }
            else
            {
                ColorfulMessage("\nYou don't have any prescription in your list yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);
            SetFooter();
        }

        public void EditPrescription()
        {
            SetHeader("edit prescription");

            bool hasPrescription = prescriptionRepository.HasEntity();

            if (hasPrescription == false)
            {
                ColorfulMessage("\nYou don't have any prescription yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewPrescriptionTable();

            int selectedId = SetIntField("\nEnter the prescription's Id:", ConsoleColor.Cyan);
            int newSelectedId = prescriptionRepository.isValidId(selectedId, prescriptionRepository);
            prescriptionRepository.GetId(newSelectedId, prescriptionRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                  "\n[1] Patient"
                                + "\n[2] Medicine"
                                + "\n[3] Employee"
                                + "\n[4] Amount"
                                + "\n[5] Date"
                                + "\n[6] Doctor's name"
                                , ConsoleColor.Gray);

            bool validOption = false;

            foreach (Prescription prescription in prescriptionRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            int newPatient = SetIntField("New patient's Id :", ConsoleColor.Gray);

                            int selecetedPatientId = patientRepository.isValidId(newPatient, patientRepository);
                            Patient patient = (Patient)patientRepository.GetId(selecetedPatientId, patientRepository);
                            
                            prescription.Patient = patient;

                            ColorfulMessage("\nPatient updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            int newMedicine = SetIntField("New medicine's Id:", ConsoleColor.Gray);

                            int selecetedMedicineId = medicineRepository.isValidId(newMedicine, medicineRepository);
                            Medicine medicine = (Medicine)medicineRepository.GetId(selecetedMedicineId, medicineRepository);

                            prescription.Medicine = medicine;

                            ColorfulMessage("\nMedicine updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            int newEmployee = SetIntField("New employee's Id:", ConsoleColor.Gray);

                            int selecetedEmployeeId = employeeRepository.isValidId(newEmployee, employeeRepository);
                            Employee employee = (Employee)employeeRepository.GetId(selecetedEmployeeId, employeeRepository);

                            prescription.Employee = employee;

                            ColorfulMessage("\nEmployee updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            int newAmount = SetIntField("New amount:", ConsoleColor.Gray);
                            prescription.Amount = newAmount;

                            ColorfulMessage("\nAmount updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            string newDate = SetStringField("New date:", ConsoleColor.Gray);
                            prescription.Date = newDate;

                            ColorfulMessage("\nDate updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 6:
                            string newDoctorName = SetStringField("New doctor's name:", ConsoleColor.Gray);
                            prescription.DoctorName = newDoctorName;

                            ColorfulMessage("\nDoctor's name updated!", ConsoleColor.Green);

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

        public void RemovePrescription()
        {
            SetHeader("remove prescription");

            bool hasPrescription = prescriptionRepository.HasEntity();

            if (hasPrescription == false)
            {
                ColorfulMessage("\nYou don't have any prescription yet!", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewPrescriptionTable();

            int selectedId = SetIntField("\nEnter the prescription Id:", ConsoleColor.Cyan);

            int newSelectedId = prescriptionRepository.isValidId(selectedId, prescriptionRepository);

            prescriptionRepository.RemoveEntity(newSelectedId, prescriptionRepository);

            ColorfulMessage("\nPrescription sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
