using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

    [DataContract]
    public class Patient
    {
        [DataMember] 
        public int ID { get; set; }
        [DataMember] 
        public string Name { get; set; }
        [DataMember] 
        public string Email { get; set; }
        [DataMember] 
        public string PhoneNumber { get; set; }
        [DataMember]
        [StringLength(15, MinimumLength = 5)]
        public string Username { get; set; }
        [DataMember] 
        [StringLength(15, MinimumLength = 5)]
        public string Password { get; set; }
        [DataMember]
        public int MainDoctorID { get; set; }
        [DataMember]
        public Doctor MainDoctor { get; set; }

    }
