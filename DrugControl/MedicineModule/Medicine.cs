using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.MedicineModule
{
    internal class Medicine : EntityBase
    {
        public Medicine(string name, string description, int amount, int limitAmount, string status, Supplier supplier)
        {
            Name = name;
            Description = description;
            Amount = amount;
            LimitAmount = limitAmount;
            Status = status;
            Supplier = supplier;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int LimitAmount { get; set; }
        public string Status { get; set; }
        public Supplier Supplier { get; set; }

    }
}
