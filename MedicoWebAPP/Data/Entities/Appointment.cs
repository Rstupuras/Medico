using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
    [DataContract]
    public class Appointment
    {
        [DataMember] 
        public int ID { get; set; }
        [DataMember] 
        public int DoctorID { get; set; }
        [DataMember]
        public Doctor Doctor { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember] 
        public int PatientID { get; set; }
        [DataMember] 
        public Patient Patient { get; set; }
        [DataMember]
        public Boolean IsViewed {get; set;}
        [DataMember]
        public string Summary {get; set;}
        [DataMember]
        public string Reason {get; set;}
        
    }
