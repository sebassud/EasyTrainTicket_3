using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Model
{
    [DataContract(IsReference = true)]
    public class Route : Entity
    {
        [DataMember]
        public virtual string From { get; set; }
        [DataMember]
        public virtual string To { get; set; }
        [DataMember]
        public virtual int Distance { get; set; }
        [DataMember]
        public virtual int BestTime { get; set; }
    }
}
