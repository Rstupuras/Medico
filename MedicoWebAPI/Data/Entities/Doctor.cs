using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
    [DataContract]
    public class Doctor
    {
        [DataMember]
        
        public int ID { get; set; }
        [DataMember] 
        [StringLength(20, MinimumLength = 2)]
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
        public bool IsAdmin {get;set;}
    

    }
