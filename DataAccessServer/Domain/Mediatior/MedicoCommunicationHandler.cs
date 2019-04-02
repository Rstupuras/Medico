using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class MedicoCommunicationHandler {
    private Socket socket;
    private MemoryStream memoryStream;
    private IMedicoModel medicoModel;
    public MedicoCommunicationHandler(Socket socket,IMedicoModel medico)
    {
        this.medicoModel = medico;
        this.socket = socket;
        this.memoryStream = new MemoryStream();
        Console.WriteLine("");
        Console.ForegroundColor= ConsoleColor.Blue;
        Console.WriteLine("Connected");
    }
    public void run(){
        byte[] buffer=new byte[1024];
        int readBytes = socket.Receive(buffer);
        while (readBytes>0)
        {
            memoryStream.Write(buffer,0,readBytes);

            if (socket.Available > 0)
                {
                    readBytes = socket.Receive(buffer);    
                }
                else
                {
                   break;
                }
        }
        Console.ForegroundColor= ConsoleColor.Green;
        Console.WriteLine("Data received");
        
        byte[] totalBytes = memoryStream.ToArray();
        memoryStream.Close();
        string readData = Encoding.Default.GetString(totalBytes);
        Response r= JsonConvert.DeserializeObject<Response>(readData);
        if (r.Number == 2 || r.Number ==3 || r.Number==4 || r.Number == 6 || r.Number == 7 || r.Number == 8 || r.Number == 10 || r.Number == 11 || r.Number == 12 || r.Number == 14 || r.Number == 15 || r.Number == 16 || r.Number == 18 || r.Number == 19 || r.Number == 20 || r.Number == 21 || r.Number == 22 || r.Number==24 || r.Number == 25 || r.Number ==26 || r.Number == 28 || r.Number ==29 || r.Number == 30)  {
            receiveOperation(r);
            socket.Close();
            Console.WriteLine("");
        }
        else {
            socket.Send(sendOperation(r));
            socket.Close();
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Data sent");
            Console.WriteLine("");
        }
        
    }
    public byte[] sendOperation(Response r)
    {
        byte[] dataToSendBytes = null;
        
        if (r.Number ==1)
        {
            ICollection<Doctor> doctor = medicoModel.getAllDoctors();
            string dataToSend = JsonConvert.SerializeObject(doctor,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);
        }
        if (r.Number ==5)
        {
            ICollection<Appointment> appointment = medicoModel.getAllAppointments();
            string dataToSend = JsonConvert.SerializeObject(appointment,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);   
        }
        if (r.Number ==9)
        {
            ICollection<Patient> patients = medicoModel.getAllPatients();
            string dataToSend = JsonConvert.SerializeObject(patients,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);   
        }
        if (r.Number ==13)
        {
            ICollection<Medicament> medicaments = medicoModel.getAllMedicaments();
            string dataToSend = JsonConvert.SerializeObject(medicaments,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);   
        }
        if (r.Number ==17)
        {
            ICollection<Order> orders = medicoModel.getAllOrders();
            string dataToSend = JsonConvert.SerializeObject(orders,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);   
        }
        if (r.Number ==23)
        {
            ICollection<Prescription> prescriptions = medicoModel.GetAllPrescriptions();
            string dataToSend = JsonConvert.SerializeObject(prescriptions,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);   
        }

        if (r.Number == 27)
        {
            ICollection<Pharmacy> pharmacies = medicoModel.GetAllPharmacies();

            string dataToSend = JsonConvert.SerializeObject(pharmacies,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            dataToSendBytes = Encoding.Default.GetBytes(dataToSend);   
        }
        return dataToSendBytes;
    }
    public void receiveOperation(Response r)
    {
        if (r.Number == 2)
        {
            medicoModel.AddDoctor(r.Doctor);
        }
        if (r.Number == 3)
        {
            medicoModel.UpdateDoctor(r.Doctor);
        }
        if (r.Number == 4)
        {
            medicoModel.DeleteDoctor(r.Doctor);
        }
        if (r.Number == 6)
        {
            medicoModel.AddAppointment(r.Appointment,r.Doctor,r.Patient);
        }
        if (r.Number == 7)
        {
            medicoModel.UpdateAppointment(r.Appointment);
        }
        if (r.Number ==8)
        {
            medicoModel.DeleteAppointment(r.Appointment);
        }
        if (r.Number ==10)
        {
            medicoModel.AddPatient(r.Patient);
        }
        if (r.Number ==11)
        {
            medicoModel.UpdatePatient(r.Patient);
        }
        if (r.Number ==12)
        {
            medicoModel.DeletePatient(r.Patient);
        }
        if (r.Number ==14)
        {
            medicoModel.AddMedicament(r.Medicament);
        }
        if (r.Number ==15)
        {
            medicoModel.UpdateMedicament(r.Medicament);
        }
        if (r.Number ==16)
        {
            medicoModel.DeleteMedicament(r.Medicament);
        }
        if (r.Number ==18)
        {
            medicoModel.AddOrder(r.Order,r.Patient);
        }
        if (r.Number ==19)
        {
            medicoModel.UpdateOrder(r.Order);
        }
        if (r.Number ==20)
        {
            medicoModel.AddItemsToOrder(r.Order,r.OrderItem);
        }
        if (r.Number ==21)
        {
            medicoModel.DeleteOrder(r.Order);
        }
        if (r.Number ==22)
        {
            medicoModel.DeleteItemFromOrder(r.Order,r.OrderItem);
        }

        if (r.Number == 24)
        {
            medicoModel.AddPrescription(r.Prescription);
        }

        if (r.Number == 25)
        {
            medicoModel.UpdatePrescription(r.Prescription);
        }

        if (r.Number == 26)
        {
            medicoModel.DeletePrescription(r.Prescription);
        }

        if (r.Number == 28)
        {
            medicoModel.AddPharmacy(r.Pharmacy);
        }

        if (r.Number == 29)
        {
            medicoModel.UpdatePharmacy(r.Pharmacy);
        }

        if (r.Number == 30)
        {
            medicoModel.DeletePharmacy(r.Pharmacy);
        }
      

    }
                            

                
}