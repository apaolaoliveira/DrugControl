using DrugControl.Shared;

namespace DrugControl.PatientModule
{
    internal class Patient : EntityBase
    {
        public Patient(string name, string address, long cpf, long susCard, long phone)
        {
            Name = name;
            Address = address;
            Cpf = cpf;
            SUSCard = susCard;
            Phone = phone;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public long Cpf { get; set; }
        public long SUSCard { get; set; }
        public long Phone { get; set; }
    }
}
