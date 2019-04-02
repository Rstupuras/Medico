using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDbRepository
{
    ICollection<Doctor> GetAllDoctors();
    ICollection<Appointment> GetAllAppointments();
    ICollection<Patient> getAllPatients();
    void AddDoctor(Doctor doctor);
    void AddAppointment(Appointment appointment, Doctor doctor, Patient patient);
    void AddPatient(Patient patient);
    void UpdateDoctor(Doctor doctor);
    void UpdateAppointment(Appointment appointment);
    void UpdatePatient(Patient patient);
    void DeleteDoctor(Doctor doctor);
    void DeleteAppointment(Appointment appointment);
    void DeletePatient(Patient patient);
    ICollection<Medicament> getAllMedicaments();
    void AddMedicament(Medicament medicament);
    void UpdateMedicament(Medicament medicament);
    void DeleteMedicament(Medicament medicament);
    ICollection<Order> getAllOrders();
    void AddOrder(Order order, Patient patient);
    void UpdateOrder(Order order);
    void AddItemsToOrder(Order order, OrderItem orderItem);
    void DeleteOrder(Order order);
    void DeleteItemFromOrder(Order order, OrderItem orderItem);
    ICollection<Prescription> GetAllPrescriptions();
    void AddPrescription(Prescription prescription);
    void UpdatePrescription(Prescription prescription);
    void DeletePrescription(Prescription prescription);
    ICollection<Pharmacy> GetAllPharmacies();
    void AddPharmacy(Pharmacy pharmacy);
    void UpdatePharmacy(Pharmacy pharmacy);
    void DeletePharmacy(Pharmacy pharmacy);
    // Task<ICollection<Medicament>> getAllMedicamentsAsync();

}