using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class MedicoModelManager : IMedicoModel
{
    private IDbRepository dbRepository;
    private MedicoDataServer medicoDataServer;

    public MedicoModelManager(MedicoContext medicoContext, IPAddress Ip, int Port)
    {
        this.dbRepository = new DbRepository(medicoContext);
        medicoDataServer = new MedicoDataServer(this, Ip, Port);
        Thread newThread = new Thread(() => medicoDataServer.run());
        newThread.Start();
    }

    public void AddAppointment(Appointment appointment, Doctor doctor, Patient patient)
    {
        dbRepository.AddAppointment(appointment, doctor, patient);
    }

    public ICollection<Appointment> getAllAppointments()
    {
        return dbRepository.GetAllAppointments();
    }

    public ICollection<Doctor> getAllDoctors()
    {
        return dbRepository.GetAllDoctors();
    }

    public ICollection<Medicament> getAllMedicaments()
    {
        return dbRepository.getAllMedicaments();
    }

    public ICollection<Order> getAllOrders()
    {
        return dbRepository.getAllOrders();
    }

    public void AddDoctor(Doctor doctor)
    {
        dbRepository.AddDoctor(doctor);
    }

    public void UpdateDoctor(Doctor doctor)
    {
        dbRepository.UpdateDoctor(doctor);
    }

    public void DeleteDoctor(Doctor doctor)
    {
        dbRepository.DeleteDoctor(doctor);
    }

    public void UpdateAppointment(Appointment appointment)
    {
        dbRepository.UpdateAppointment(appointment);
    }

    public void DeleteAppointment(Appointment appointment)
    {
        dbRepository.DeleteAppointment(appointment);
    }

    public ICollection<Patient> getAllPatients()
    {
        return dbRepository.getAllPatients();
    }

    public void AddPatient(Patient patient)
    {
        dbRepository.AddPatient(patient);
    }

    public void UpdatePatient(Patient patient)
    {
        dbRepository.UpdatePatient(patient);
    }

    public void DeletePatient(Patient patient)
    {
        dbRepository.DeletePatient(patient);
    }

    public void AddMedicament(Medicament medicament)
    {
        dbRepository.AddMedicament(medicament);
    }

    public void UpdateMedicament(Medicament medicament)
    {
        dbRepository.UpdateMedicament(medicament);
    }

    public void DeleteMedicament(Medicament medicament)
    {
        dbRepository.DeleteMedicament(medicament);
    }

    public void AddOrder(Order order, Patient patient)
    {
        dbRepository.AddOrder(order, patient);
    }

    public void UpdateOrder(Order order)
    {
        dbRepository.UpdateOrder(order);
    }

    public void AddItemsToOrder(Order order, OrderItem orderItem)
    {
        dbRepository.AddItemsToOrder(order, orderItem);
    }

    public void DeleteOrder(Order order)
    {
        dbRepository.DeleteOrder(order);
    }

    public void DeleteItemFromOrder(Order order, OrderItem orderItem)
    {
        dbRepository.DeleteItemFromOrder(order, orderItem);
    }

    public ICollection<Prescription> GetAllPrescriptions()
    {
        return dbRepository.GetAllPrescriptions();
    }

    public void AddPrescription(Prescription prescription)
    {
        dbRepository.AddPrescription(prescription);
    }

    public void UpdatePrescription(Prescription prescription)
    {
        dbRepository.UpdatePrescription(prescription);
    }

    public void DeletePrescription(Prescription prescription)
    {
        dbRepository.DeletePrescription(prescription);
    }

    public ICollection<Pharmacy> GetAllPharmacies()
    {
        return dbRepository.GetAllPharmacies();
    }

    public void AddPharmacy(Pharmacy pharmacy)
    {
        dbRepository.AddPharmacy(pharmacy);
    }

    public void UpdatePharmacy(Pharmacy pharmacy)
    {
        dbRepository.UpdatePharmacy(pharmacy);
    }

    public void DeletePharmacy(Pharmacy pharmacy)
    {
        dbRepository.DeletePharmacy(pharmacy);
    }

}