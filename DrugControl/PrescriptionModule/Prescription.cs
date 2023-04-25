using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.PatientModule;
using DrugControl.Shared;

namespace DrugControl.PrescriptionModule
{
    internal class Prescription : EntityBase
    {
        public Prescription(Patient patient, Medicine medicine, Employee employee, string date, int amount, string doctorName)
        {
            Patient = patient;
            Medicine = medicine;
            Employee = employee;
            Date = date;
            Amount = amount;
            DoctorName = doctorName;
        }

        public Patient Patient { get; set; }
        public Medicine Medicine { get; set; }
        public Employee Employee { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public string DoctorName { get; set; } 
    }
}
