using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.SupplierModule;
using DrugControl.Shared;

namespace DrugControl.OrderModule
{
    internal class OrderRepository : RepositoryBase
    {
        public SupplierRepository supplierRepository = null;
        public EmployeeRepository employeeRepository = null;
        public MedicineRepository medicineRepository = null;

        public bool HasEmployeeMedicineAndsupplier()
        {
            bool employee = employeeRepository.list.Count == 0;
            bool medicine = medicineRepository.list.Count == 0;
            bool supplier = supplierRepository.list.Count == 0;


            if (employee || medicine || supplier)
            {
                if (employee && medicine)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee and one medicine to set a prescription.", ConsoleColor.Red);

                else if (employee && supplier)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee and one supplier to set a prescription.", ConsoleColor.Red);

                else if (medicine && supplier)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one medicine and one supplier to set a prescription.", ConsoleColor.Red);

                else if (medicine)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one medicine to set a prescription.", ConsoleColor.Red);

                else if (employee)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one employee to set a prescription.", ConsoleColor.Red);

                else if (supplier)
                    InterfaceBase.ColorfulMessage("\nYou need to have at least one supplier to set a prescription.", ConsoleColor.Red);

                return false;
            }
            else { return true; }
        }
    }
}
