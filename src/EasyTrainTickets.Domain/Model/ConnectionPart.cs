using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Model
{
    [DataContract(IsReference = true)]
    public class ConnectionPart : Entity
    {
        [DataMember]
        public virtual Route Route { get; set; }
        [DataMember]
        public virtual Connection Connection { get; set; }
        [DataMember]
        public virtual DateTime StartTime { get; set; }
        [DataMember]
        public virtual DateTime EndTime { get; set; }
        [DataMember]
        public virtual string Seats { get; set; }
        [DataMember]
        public virtual int FreeSeats { get; set; }
    }
}
