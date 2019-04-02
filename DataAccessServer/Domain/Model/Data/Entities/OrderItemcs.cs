using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
    [DataContract]
    public class OrderItem
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int MedicamentID {get;set;}
        [DataMember]
        public Medicament Medicament { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int OrderID { get; set; }

    }
