using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DbRepository : IDbRepository
{
    private readonly MedicoContext _context;
    public DbContextOptionsBuilder<MedicoContext> optionsBuilder;
    


    public DbRepository(MedicoContext context)
    {
        _context = context;
        optionsBuilder = new DbContextOptionsBuilder<MedicoContext>();
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SQLMedico"].ConnectionString);
    }


    public void AddAppointment(Appointment Appointment, Doctor Doctor, Patient Patient)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            Appointment.Doctor = _context.Doctors.First(d => d.ID == Doctor.ID);
            Appointment.Patient = _context.Patients.First(p => p.ID == Patient.ID);
            _context.Entry(Appointment).State = EntityState.Added;
            _context.Appointments.Add(Appointment);
            _context.SaveChanges();
            
        }
        
    }

    public void AddDoctor(Doctor doctor)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            _context.Doctors.Add(doctor);
            _context.Entry(doctor).State = EntityState.Added;
            _context.SaveChanges();
            
        }
        
    }

    public void AddItemsToOrder(Order order, OrderItem orderItem)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options)) 
        {
            ICollection<Order> orders = _context.Orders.Include(o => o.Items).ToList();
            Order Order = orders.First(o => o.ID == order.ID);
            if (Order.Items.Any(i => i.MedicamentID == orderItem.Medicament.ID)==true)
            {
                Order.Items.First(i => i.MedicamentID == orderItem.Medicament.ID).Quantity +=  orderItem.Quantity;
                _context.SaveChanges();
            }
            else
            {
                Medicament medicament = _context.Medicaments.First(m => m.ID == orderItem.Medicament.ID);
                OrderItem OrderItem = new OrderItem
                {
                    Medicament = medicament,
                    Quantity = orderItem.Quantity,
                    OrderID = Order.ID
                };
                _context.Items.Add(OrderItem);
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }
           
}

    public async void AddMedicament(Medicament medicament)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {

             _context.Medicaments.Add(medicament);
            await _context.SaveChangesAsync();
        }
        
    }

    public async void AddOrder(Order order, Patient patient)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            Patient pat = _context.Patients.First(p => p.ID == patient.ID);
            order.Patient = pat;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        
    }

    public async void AddPatient(Patient patient)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            if (patient.MainDoctor != null)
            {
                Doctor doctor = _context.Doctors.First(d => d.ID == patient.MainDoctor.ID);
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
        
    }

    public async void DeleteAppointment(Appointment appointment)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Appointment a in _context.Appointments.Where(r => r.ID == appointment.ID))
            {
                _context.Appointments.Remove(a);
            }

            
        
        await _context.SaveChangesAsync();
        }
    }

    public async void DeleteDoctor(Doctor doctor)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Doctor d in _context.Doctors.Where(r => r.ID == doctor.ID))
            {
                _context.Doctors.Remove(d);
            }

            
        
        await _context.SaveChangesAsync();
        }
    }

    public async void DeleteItemFromOrder(Order order, OrderItem orderItem)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            ICollection<Order> orders = _context.Orders.ToList();
            ICollection<OrderItem> items = _context.Items.ToList();
            foreach (Order o in orders)
            {
                if (o.ID == order.ID)
                {
                    foreach (var item in items)
                    {

                        if (item.ID == orderItem.ID)
                        {
                            _context.Items.Remove(item);
                            o.Items.Remove(item);
                        }

                    }
                }

            }


        
        await  _context.SaveChangesAsync();
        }

    }

    public async void AddPrescription(Prescription prescription)
    {
        prescription.MedicamentId = prescription.Medicament.ID;
        prescription.Medicament = null;
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            _context.Prescriptions.Add(prescription);
            await  _context.SaveChangesAsync();
        }

    }

    public async void UpdatePrescription(Prescription prescription)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Prescription p in _context.Prescriptions.Where(x => x.ID == prescription.ID))
            {
                p.Description = prescription.Description;

                p.MedicamentId = prescription.MedicamentId;

                p.DateTimeFrom = prescription.DateTimeFrom;
                p.DateTimeTo = prescription.DateTimeTo;
            }

            
        
        await _context.SaveChangesAsync();
        }
    }

    public async void DeletePrescription(Prescription prescription)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Prescription p in _context.Prescriptions.Where(x => x.ID == prescription.ID))
            {
                _context.Prescriptions.Remove(p);
            }

        
        await _context.SaveChangesAsync();
        }

    }

    public ICollection<Pharmacy> GetAllPharmacies()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            
            _context.Items.Include(oi => oi.Medicament).ToList();
           return _context.Pharmacies.Include(o => o.Orders).ToList();
        
        }
       
    }

    public async void AddPharmacy(Pharmacy pharmacy)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            _context.Pharmacies.Add(pharmacy);
            await _context.SaveChangesAsync();
        }
        

    }

    public async void UpdatePharmacy(Pharmacy pharmacy)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Pharmacy p in _context.Pharmacies.Where(r => r.ID == pharmacy.ID))
            {
                p.Email = pharmacy.Email;
                p.Location = pharmacy.Location;
                p.Name = pharmacy.Name;
                p.Orders = pharmacy.Orders;
                p.PhoneNumber = pharmacy.PhoneNumber;
                p.Username = pharmacy.Username;
                p.IsAdmin = pharmacy.IsAdmin;
            }
            await _context.SaveChangesAsync();

        }
        

    }

    public async void DeletePharmacy(Pharmacy pharmacy)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Pharmacy p in _context.Pharmacies.Where(x => x.ID == pharmacy.ID))
            {
                _context.Pharmacies.Remove(p);
            }
            await _context.SaveChangesAsync();
        }
        
    }

    public async void DeleteMedicament(Medicament medicament)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Medicament m in _context.Medicaments.Where(m => m.ID == medicament.ID))
            {
                _context.Medicaments.Remove(m);
            }

            await _context.SaveChangesAsync();
            
        }
        
    }

    public async void DeleteOrder(Order order)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Order o in _context.Orders.Where(r => r.ID == order.ID))
            {
                _context.Orders.Remove(o);
            }

            await _context.SaveChangesAsync();
            
        }
        
    }

    public async void DeletePatient(Patient patient)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Patient p in _context.Patients.Where(r => r.ID == patient.ID))
            {
                _context.Patients.Remove(p);
            }
            await _context.SaveChangesAsync();
            
        }
        
    }

    public ICollection<Appointment> GetAllAppointments()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            ICollection<Appointment> appointments = new HashSet<Appointment>();
            foreach (Appointment a in _context.Appointments.ToList())
            {
                appointments.Add(a);
                int id = a.PatientID;
                int idDocotor = a.DoctorID;
                Patient patient = _context.Patients.First(p => p.ID == id);
                Doctor doctor = _context.Doctors.First(d => d.ID == idDocotor);
                a.Patient = patient;
                a.Doctor = doctor;
            }

            return appointments;
        }
        
    }

    public ICollection<Doctor> GetAllDoctors()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            ICollection<Doctor> Doctors = _context.Doctors.ToList();
            return Doctors;
        }
    }

    public ICollection<Medicament> getAllMedicaments()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            ICollection<Medicament> medicaments = _context.Medicaments.ToList();
            return medicaments;
        }
    }

    public ICollection<Order> getAllOrders()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            ICollection<Order> orders = _context.Orders.ToList();
            foreach (Order o in orders)
            {
                foreach (Patient patient in _context.Patients.ToList())
                {
                    if (o.PatientID == patient.ID)
                    {
                        o.Patient = patient;
                    }
                }
                foreach (OrderItem item in _context.Items.ToList())
                {
                    item.Medicament = _context.Medicaments.First(x => x.ID == item.MedicamentID);
                    if (o.ID == item.OrderID)
                    {
                        o.Items.Add(item);
                    }
                }
            }

            return orders;
        }
    }

    public ICollection<Patient> getAllPatients()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            ICollection<Patient> Patients = _context.Patients.ToList();
            foreach (Patient p in Patients)
            {
                p.MainDoctor = _context.Doctors.First(d => d.ID == p.MainDoctorID);
            }

            return Patients;
        }
    }

    public async void UpdateAppointment(Appointment appointment)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Appointment a in _context.Appointments.Where(r => r.ID == appointment.ID))
            {

                a.IsViewed = appointment.IsViewed;
                a.Reason = appointment.Reason;
                a.Summary = appointment.Summary;
            }
        await _context.SaveChangesAsync();
        }
        
            
    }

    public async void UpdateDoctor(Doctor doctor)
    {
        
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Doctor d in _context.Doctors.Where(r => r.ID == doctor.ID))
            {
                d.Name = doctor.Name;
                d.Password = doctor.Password;
                d.PhoneNumber = doctor.PhoneNumber;
                d.Username = doctor.Username;
                d.Email = doctor.Email;
                d.IsAdmin = doctor.IsAdmin;
            }

            
        
        await _context.SaveChangesAsync();
        }
            
    }

    public async void UpdateMedicament(Medicament medicament)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
            foreach (Medicament m in _context.Medicaments.Where(m => m.ID == medicament.ID))
            {
                m.Description = medicament.Description;
                m.IsPrescribed = medicament.IsPrescribed;
                m.Name = medicament.Name;
                m.Price = medicament.Price;
            }
        await _context.SaveChangesAsync();
        }
        
            
    }

    public async void UpdateOrder(Order order)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options)){
            foreach (Order o in _context.Orders.Where(o => o.ID == order.ID))
            {
                o.Status = order.Status;
                o.OrderNumber = order.OrderNumber;
                o.OrderDate = order.OrderDate;
                o.IsSent = order.IsSent;
                o.PharmacyID = order.PharmacyID;
                o.PatientID = order.PatientID;
            }
        await _context.SaveChangesAsync();
        }
            
    }

    public async void UpdatePatient(Patient patient)
    {
        using (var _context = new MedicoContext(optionsBuilder.Options)){
            foreach (Patient p in _context.Patients.Where(r => r.ID == patient.ID))
            {
                p.Name = patient.Name;
                p.Password = patient.Password;
                p.PhoneNumber = patient.PhoneNumber;
                p.Username = patient.Username;
                p.Email = patient.Email;
                if (patient.MainDoctor != null && _context.Doctors.First(d => d.ID == patient.MainDoctor.ID) != null)
                {
                    p.MainDoctor = _context.Doctors.First(d => d.ID == patient.MainDoctor.ID);
                }
            }

        await _context.SaveChangesAsync();
        }
            
    }

    public ICollection<Prescription> GetAllPrescriptions()
    {
        using (var _context = new MedicoContext(optionsBuilder.Options))
        {
           return  _context.Prescriptions.Include(m => m.Medicament).Include(p => p.patient).Include(d => d.Doctor).ToList();
           
        }
            
    }
}