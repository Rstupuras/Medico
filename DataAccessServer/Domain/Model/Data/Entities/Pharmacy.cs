using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

[DataContract]
    public class Pharmacy
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
        public string Location {get;set;}
        [DataMember]
        public ICollection<Order> Orders {get; set;}
        [DataMember]
        public bool IsAdmin {get;set;}
        public Pharmacy(){
            this.Orders = new HashSet<Order>();
        }

    }