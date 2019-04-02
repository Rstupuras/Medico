using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
    [DataContract]
    public class Medicament
    {
        [DataMember] 
        public int ID { get; set; }
        [DataMember] 
        public bool IsPrescribed { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
