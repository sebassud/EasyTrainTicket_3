using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Model
{
    [DataContract(IsReference = true)]
    public class Discount : Entity
    {
        [DataMember]
        public virtual string Type { get; set; }
        [DataMember]
        public virtual double Percent { get; set; }
    }
}
