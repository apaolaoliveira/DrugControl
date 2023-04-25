using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.Shared;
using DrugControl.SupplierModule;

namespace DrugControl.OrderModule
{
    internal class Order : EntityBase
    {
        public Order(Supplier supplier, Medicine medicine, Employee employee, string date, int amount)
        {
            Supplier = supplier;
            Medicine = medicine;
            Employee = employee;
            Date = date;
            Amount = amount;
        }

        public Supplier Supplier { get; set; }
        public Medicine Medicine { get; set; }
        public Employee Employee { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
    }
}
