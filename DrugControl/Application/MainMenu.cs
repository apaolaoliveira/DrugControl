using DrugControl.MedicineModule;
using DrugControl.PatientModule;
using DrugControl.PrescriptionModule;
using DrugControl.SupplierModule;
using DrugControl.OrderModule;
using DrugControl.EmployeeModule;
using DrugControl.Shared;
using System;

namespace DrugControl.Application
{
    internal class MainMenu : InterfaceBase
    {
        public MedicineInterface medicineInterface = null;
        public PatientInterface patientInterface = null;
        public PrescriptionInterface prescriptionInterface = null;
        public OrderInterface orderInterface = null;
        public SupplierInterface supplierInterface = null;
        public EmployeeInterface employeeInterface = null;
        public LoginInterface loginInterface = null;

        public void ShowMainMenu()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("main menu", "Medicine", "Patient", "Prescription", 
                                             "Order", "Supplier", "Employee", "Lock Screen", "Exit");

                switch (selectedOption)
                {
                    case 1: medicineInterface.MedicineOptions();         break;
                    case 2: patientInterface.PatientOptions();           break;
                    case 3: prescriptionInterface.PrescriptionOptions(); break;
                    case 4: orderInterface.OrderOptions();               break;
                    case 5: supplierInterface.SupplierOptions();         break;
                    case 6: employeeInterface.SupplierOptions();         break;
                    case 7: loginInterface.LogIn();                      break;
                    case 8: proceed = false;                             break;
                }
            }

            ColorfulMessage("\n\nHave a great day!", ConsoleColor.DarkCyan);
            ColorfulMessage("\n\n<-'", ConsoleColor.DarkCyan);

            Console.ReadKey();
        }
    }
}
