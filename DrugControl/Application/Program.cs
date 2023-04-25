using DrugControl.EmployeeModule;
using DrugControl.MedicineModule;
using DrugControl.OrderModule;
using DrugControl.PatientModule;
using DrugControl.PrescriptionModule;
using DrugControl.SupplierModule;

namespace DrugControl.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {        
            // Patient ---------------------------------------------------------------------
            PatientRepository patientRepository = new PatientRepository();

            PatientInterface patientInterface = new PatientInterface();
            patientInterface.patientRepository = patientRepository;

            // Supplier --------------------------------------------------------------------
            SupplierRepository supplierRepository = new SupplierRepository();

            SupplierInterface supplierInterface = new SupplierInterface();
            supplierInterface.supplierRepository = supplierRepository;

            // Employee --------------------------------------------------------------------
            EmployeeRepository employeeRepository = new EmployeeRepository();

            EmployeeInterface employeeInterface = new EmployeeInterface();
            employeeInterface.employeeRepository = employeeRepository;

            // Medicine --------------------------------------------------------------------
            MedicineRepository medicineRepository = new MedicineRepository();
            medicineRepository.supplierRepository = supplierRepository;            

            MedicineInterface medicineInterface = new MedicineInterface();
            medicineInterface.medicineRepository = medicineRepository;
            medicineInterface.supplierRepository = supplierRepository;
            
            // Prescription ----------------------------------------------------------------
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            prescriptionRepository.patientRepository = patientRepository;
            prescriptionRepository.employeeRepository = employeeRepository;
            prescriptionRepository.medicineRepository = medicineRepository;

            PrescriptionInterface prescriptionInterface = new PrescriptionInterface();
            prescriptionInterface.prescriptionRepository = prescriptionRepository;
            prescriptionInterface.patientRepository = patientRepository;
            prescriptionInterface.employeeRepository = employeeRepository;
            prescriptionInterface.medicineRepository = medicineRepository;

            // Order -----------------------------------------------------------------------
            OrderRepository orderRepository = new OrderRepository();
            orderRepository.supplierRepository = supplierRepository;
            orderRepository.employeeRepository = employeeRepository;
            orderRepository.medicineRepository = medicineRepository;

            OrderInterface orderInterface = new OrderInterface();
            orderInterface.orderRepository = orderRepository;
            orderInterface.supplierRepository = supplierRepository;
            orderInterface.employeeRepository = employeeRepository;
            orderInterface.medicineRepository = medicineRepository;            

            // MainMenu --------------------------------------------------------------------
            MainMenu mainMenu = new MainMenu();
            mainMenu.medicineInterface = medicineInterface;
            mainMenu.patientInterface = patientInterface;
            mainMenu.prescriptionInterface = prescriptionInterface;
            mainMenu.orderInterface = orderInterface;
            mainMenu.supplierInterface = supplierInterface;
            mainMenu.employeeInterface = employeeInterface;

            // Login -----------------------------------------------------------------------
            LoginInterface loginInterface = new LoginInterface();
            loginInterface.employeeRepository = employeeRepository;
            loginInterface.mainMenu = mainMenu;

            mainMenu.loginInterface = loginInterface;

            employeeRepository.Mock();
            loginInterface.LogIn();
        }
    }
}