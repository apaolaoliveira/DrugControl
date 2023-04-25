using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.PatientModule;
using DrugControl.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugControl.PrescriptionModule
{
    internal class PrescriptionRepository : RepositoryBase
    {
        public PatientRepository patientRepository = null;
        public EmployeeRepository employeeRepository = null;
        public MedicineRepository medicineRepository = null;

        public bool HasEmployeeMedicineAndPatient()
        {
            bool employee = employeeRepository.list.Count == 0;
            bool medicine = medicineRepository.list.Count == 0;
            bool patient = patientRepository.list.Count == 0;

            if (employee || medicine || patient)
            {
                if (employee && medicine && patient)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee, one medicine and one patient to set a prescription.", ConsoleColor.Red);

                else if (employee && medicine)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee and one medicine to set a prescription.", ConsoleColor.Red);

                else if (employee && patient)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee and one patient to set a prescription.", ConsoleColor.Red);

                else if (medicine && patient)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one medicine and one patient to set a prescription.", ConsoleColor.Red);

                else if (medicine)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one medicine to set a prescription.", ConsoleColor.Red);

                else if (employee)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee to set a prescription.", ConsoleColor.Red);

                else if (patient)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one patient to set a prescription.", ConsoleColor.Red);

                return false;
            }
            else { return true; }
        }
    }
}
