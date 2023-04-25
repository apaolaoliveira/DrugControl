using DrugControl.PrescriptionModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;
using System.Collections;

namespace DrugControl.MedicineModule
{
    internal class MedicineRepository : RepositoryBase
    {
        public SupplierRepository supplierRepository = null;

        public string GetStatus(int statusChoice)
        {
            switch (statusChoice)
            {
                case 1: return "AVAILABLE";
                case 2: return "UNAVAILABLE";
                default: throw new ArgumentException("Invalid status choice.");
            }
        }

        public bool HasSupplier()
        {
            if (supplierRepository.list.Count == 0) { return false; }
            else { return true; }
        }
    }
}
