using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
[DataContract]
public class Prescription
{
    [DataMember]
    public int ID { get; set; }
    [DataMember]
    public int MedicamentId {get;set;}
    [DataMember]
    public Medicament Medicament { get; set; }
    [DataMember]
    public int PatientId { get; set; }
    [DataMember]
    public Patient patient { get; set; }
    [DataMember]
    public int DoctorId { get; set; }
    [DataMember]
    public Doctor Doctor { get; set; }
    [DataMember]
    public DateTime DateTimeFrom { get; set; }
    [DataMember]
    public DateTime DateTimeTo { get; set; }

    [DataMember]
    public string Description { get; set; }
}
