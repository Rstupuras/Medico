using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
    [DataContract]
    public class Order
    {   
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public Patient Patient { get; set; }
        [DataMember]
        public DateTime OrderDate {get; set;}
        [DataMember]
        public bool IsSent { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string OrderNumber { get; set; }
        [DataMember]
        public ICollection<OrderItem> Items { get; set; }
        [DataMember]
        public int PharmacyID { get; set; }
        public Order()
        {
            Items = new HashSet<OrderItem>();
        }
    }
