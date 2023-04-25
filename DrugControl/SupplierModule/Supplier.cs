using DrugControl.MedicineModule;
using DrugControl.Shared;

namespace DrugControl.SupplierModule
{
    internal class Supplier : EntityBase
    {
        public Supplier(string name, long cnpj, long phone, string address /*, Medicine medicine*/ )
        {
            Name = name;
            Cnpj = cnpj;
            Phone = phone;
            Address = address;
            //Medicine = medicine;
        }

        public string Name { get; set; }
        public long Cnpj { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
        //public Medicine Medicine { get; set; }
    }
}
