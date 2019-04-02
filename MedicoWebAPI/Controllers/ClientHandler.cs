using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class ClientHandler
{
    private IPAddress iPAddress;
    private int Port;

    public ClientHandler()
    {
        iPAddress = IPAddress.Parse("127.0.0.1");
        this.Port = 9011;
    }

    public ICollection<Order> GetAllOrders(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Order>(response);
    }

    public ICollection<Medicament> GetAllMedicaments(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Medicament>(response);
    }

    public ICollection<Patient> GetAllPatients(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Patient>(response);
    }

    public ICollection<Doctor> GetAllDoctors(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Doctor>(response);
    }

    public ICollection<Appointment> GetAllAppointments(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Appointment>(response);
    }

    public void AddDoctor(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    internal void AddOrder(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void AddMedicament(Response response)
    {
        Client client = new Client(iPAddress, Port);

        client.runClientSend(response);
    }

    public void UpdateOrder(Response response, int id)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Order> ICollection = client.runClientRecieve<Order>(response);
        ICollection<Order> orders = new HashSet<Order>();
        foreach (Order order in ICollection)
        {
            if (order.ID == id)
            {
                orders.Add(order);
            }
        }

        Order Order = response.Order;

        foreach (Order order in orders)
        {
            if (order.OrderDate <= Order.OrderDate)
            {
                order.OrderDate = Order.OrderDate;
            }
            if (Order.Status != null)
            {
                order.Status = Order.Status;
            }

            if (Order.OrderNumber != null)
            {
                order.OrderNumber = Order.OrderNumber;
            }

            if (Order.PatientID != 0)
            {
                order.PatientID = Order.PatientID;
            }
            if (Order.PharmacyID != 0)
            {
                order.PharmacyID = Order.PharmacyID;
            }
            order.IsSent = Order.IsSent;

            response.Number = 19;
            response.Order = order;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void AddItemsToOrder(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void UpdateDoctor(Response response, int ID)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Doctor> ICollection = client.runClientRecieve<Doctor>(response);
        ICollection<Doctor> doctors = new HashSet<Doctor>();
        foreach (Doctor doctor in ICollection)
        {
            if (doctor.ID == ID)
            {
                doctors.Add(doctor);
            }
        }

        Doctor Doctor = response.Doctor;
        foreach (Doctor doctor in doctors)
        {
            if (Doctor.Name != null)
            {
                doctor.Name = Doctor.Name;
            }

            if (Doctor.Email != null)
            {
                doctor.Email = Doctor.Email;
            }

            if (Doctor.PhoneNumber != null)
            {
                doctor.PhoneNumber = Doctor.PhoneNumber;
            }

            if (Doctor.Username != null)
            {
                doctor.Username = Doctor.Username;
            }

            if (Doctor.Password != null)
            {
                doctor.Password = Doctor.Password;
            }

            doctor.IsAdmin = Doctor.IsAdmin;

            response.Number = 3;
            response.Doctor = doctor;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void DeleteItemFromOrder(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void DeleteOrder(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void DeleteMedicament(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void UpdateMedicament(Response response, int id)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Medicament> ICollection = client.runClientRecieve<Medicament>(response);
        ICollection<Medicament> medicaments = new HashSet<Medicament>();
        foreach (Medicament medicament in ICollection)
        {
            if (medicament.ID == id)
            {
                medicaments.Add(medicament);
            }
        }

        Medicament Medicament = response.Medicament;
        foreach (Medicament medicament in medicaments)
        {
            if (Medicament.Name != null)
            {
                medicament.Name = Medicament.Name;
            }
            medicament.IsPrescribed = Medicament.IsPrescribed;
            if (Medicament.Price != 0)
            {
                medicament.Price = Medicament.Price;
            }

            if (Medicament.Description != null)
            {
                medicament.Description = Medicament.Description;
            }

            response.Number = 15;
            response.Medicament = medicament;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void DeletePatient(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void UpdatePatient(Response response, int id)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Patient> ICollection = client.runClientRecieve<Patient>(response);
        ICollection<Patient> patients = new HashSet<Patient>();
        Doctor doctor = response.Doctor;
        foreach (Patient p in ICollection)
        {
            if (p.ID == id)
            {
                patients.Add(p);
            }
        }

        Patient Patient = response.Patient;
        foreach (Patient p in patients)
        {
            if (Patient.Name != null)
            {
                p.Name = Patient.Name;
            }

            if (Patient.Email != null)
            {
                p.Email = Patient.Email;
            }

            if (Patient.PhoneNumber != null)
            {
                p.PhoneNumber = Patient.PhoneNumber;
            }

            if (Patient.Username != null)
            {
                p.Username = Patient.Username;
            }

            if (Patient.Password != null)
            {
                p.Password = Patient.Password;
            }

            if (response.Doctor != null)
            {
                p.MainDoctor = response.Doctor;
            }

            response.Number = 11;
            response.Patient = p;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void AddPatient(Response response)
    {
        response.Patient.MainDoctor = response.Doctor;
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void UpdateAppointment(Response response, int id)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Appointment> ICollection = client.runClientRecieve<Appointment>(response);
        ICollection<Appointment> appointments = new HashSet<Appointment>();
        foreach (Appointment a in ICollection)
        {
            if (a.ID == id)
            {
                appointments.Add(a);
            }
        }

        Appointment appointment = response.Appointment;
        foreach (Appointment a in appointments)
        {
            if (appointment.DateTime != null)
            {
                a.DateTime = appointment.DateTime;
            }

            if (appointment.Reason != null)
            {
                a.Reason = appointment.Reason;
            }

            if (appointment.Summary != null)
            {
                a.Summary = appointment.Summary;
            }

            a.IsViewed = appointment.IsViewed;

            response.Number = 7;
            response.Appointment = a;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void DeleteAppointment(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void AddAppointment(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void DeleteDoctor(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public ICollection<Prescription> GetAllPrescriptions(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Prescription>(response);
    }

    public void AddPrescription(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void UpdatePrescription(Response response, int id)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Prescription> databasePrescriptions = client.runClientRecieve<Prescription>(response);
        ICollection<Prescription> prescrpitionsToUpdate = new HashSet<Prescription>();
        foreach (Prescription p in databasePrescriptions)
        {
            if (p.ID == id)
            {
                prescrpitionsToUpdate.Add(p);
            }
        }

        Prescription prescription = response.Prescription;
        foreach (Prescription pre in prescrpitionsToUpdate)
        {
            if (prescription.DoctorID != 0)
            {
                pre.DoctorID = prescription.DoctorID;
            }

            if (prescription.Description != null)
            {
                pre.Description = prescription.Description;
            }

            if (prescription.Medicament != null)
            {
                pre.Medicament = prescription.Medicament;
            }

            if (prescription.PatientID != 0)
            {
                pre.PatientID = prescription.PatientID;
            }

            if (prescription.DateTimeFrom != null)
            {
                pre.DateTimeFrom = prescription.DateTimeFrom;
            }

            if (prescription.DateTimeTo != null)
            {
                pre.DateTimeTo = prescription.DateTimeTo;
            }

            if (prescription.MedicamentID != 0)
            {
                pre.MedicamentID = prescription.MedicamentID;
            }


            response.Number = 25;
            response.Prescription = pre;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void DeletePrescription(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public ICollection<Pharmacy> GetAllPharmacies(Response response)
    {
        Client client = new Client(iPAddress, Port);
        return client.runClientRecieve<Pharmacy>(response);
    }

    public void AddPharmacy(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }

    public void UpdatePharmacy(Response response, int id)
    {
        Client client = new Client(iPAddress, Port);
        ICollection<Pharmacy> databasePharmacies = client.runClientRecieve<Pharmacy>(response);
        ICollection<Pharmacy> pharmaciesToUpdate = new HashSet<Pharmacy>();
        foreach (Pharmacy p in databasePharmacies)
        {
            if (p.ID == id)
            {
                pharmaciesToUpdate.Add(p);
            }
        }

        Pharmacy pharmacy = response.Pharmacy;
        foreach (Pharmacy phar in pharmaciesToUpdate)
        {
            if (pharmacy.Email != null)
            {
                phar.Email = pharmacy.Email;
            }

            if (pharmacy.Location != null)
            {
                phar.Location = pharmacy.Location;
            }

            if (pharmacy.Name != null)
            {
                phar.Name = pharmacy.Name;
            }

            if (pharmacy.Password != null)
            {
                phar.Password = pharmacy.Password;
            }

            if (pharmacy.Username != null)
            {
                phar.Username = pharmacy.Username;
            }

            if (pharmacy.PhoneNumber != null)
            {
                phar.PhoneNumber = pharmacy.PhoneNumber;
            }
            phar.IsAdmin = pharmacy.IsAdmin;


            response.Number = 29;
            response.Pharmacy = phar;
            client = new Client(iPAddress, Port);
            client.runClientSend(response);
        }
    }

    public void DeletePharmacy(Response response)
    {
        Client client = new Client(iPAddress, Port);
        client.runClientSend(response);
    }
}