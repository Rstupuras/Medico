using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;

public class Client
{
    private Socket socket { get; set; }

    private MemoryStream memoryStream { get; set; }

    public Client(IPAddress iPAddress, int Port)
    {
        
        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(iPAddress, Port);
        this.memoryStream = new MemoryStream();
    }

    public ICollection<T> runClientRecieve<T>(Response response)
    {
        string jsonData = JsonConvert.SerializeObject(response);
        byte[] dataBytes = Encoding.Default.GetBytes(jsonData);
        socket.Send(dataBytes);
        byte[] buffer = new byte[1024 * 4];
        int readBytes = socket.Receive(buffer);
        while (readBytes > 0)
        {
            memoryStream.Write(buffer, 0, readBytes);

            if (socket.Available > 0)
            {
                readBytes = socket.Receive(buffer);
            }
            else
            {
                break;
            }
        }

        byte[] totalBytes = memoryStream.ToArray();
        memoryStream.Close();
        if (response.Number == 1)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Doctor> DoctorList = JsonConvert.DeserializeObject<ICollection<Doctor>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (Doctor doctor in DoctorList)
            {
                listT.Add((T) Convert.ChangeType(doctor, typeof(T)));
            }

            return listT;
        }

        if (response.Number == 5)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Appointment> appointmentList =
                JsonConvert.DeserializeObject<ICollection<Appointment>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (Appointment appointment in appointmentList)
            {
                listT.Add((T) Convert.ChangeType(appointment, typeof(T)));
            }

            return listT;
        }

        if (response.Number == 9)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Patient> patientList = JsonConvert.DeserializeObject<ICollection<Patient>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (Patient patient in patientList)
            {
                listT.Add((T) Convert.ChangeType(patient, typeof(T)));
            }

            return listT;
        }

        if (response.Number == 13)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Medicament> medicamentList = JsonConvert.DeserializeObject<ICollection<Medicament>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (var medicament in medicamentList)
            {
                listT.Add((T) Convert.ChangeType(medicament, typeof(T)));
            }


            return listT;
        }

        if (response.Number == 17)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Order> orderList = JsonConvert.DeserializeObject<ICollection<Order>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (var order in orderList)
            {
                listT.Add((T) Convert.ChangeType(order, typeof(T)));
            }


            return listT;
        }
        if (response.Number == 23)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Prescription> prescriptionList = JsonConvert.DeserializeObject<ICollection<Prescription>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (var prescription in prescriptionList)
            {
                listT.Add((T) Convert.ChangeType(prescription, typeof(T)));
            }


            return listT;
        }

        if (response.Number == 27)
        {
            string readData = Encoding.Default.GetString(totalBytes);
            ICollection<Pharmacy> pharmacyList = JsonConvert.DeserializeObject<ICollection<Pharmacy>>(readData);
            ICollection<T> listT = new HashSet<T>();
            foreach (var pharmacy in pharmacyList)
            {
                listT.Add((T) Convert.ChangeType(pharmacy, typeof(T)));
            }


            return listT;
        }

        else
        {
            ICollection<T> listT = new HashSet<T>();
            return listT;
        }
    }

    public void runClientSend(Response response)
    {
        string jsonData = JsonConvert.SerializeObject(response);
        byte[] dataBytes = Encoding.Default.GetBytes(jsonData);
        socket.Send(dataBytes);
    }
}