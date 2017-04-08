using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EasyTrainTickets.Domain.Model
{
    [DataContract(IsReference = true)]
    public abstract class Entity
    {
        [DataMember]
        [Key]
        public virtual int Id { get; set; }
        [DataMember]
        [Timestamp]
        public byte[] Version { get; set; }
    }
}
