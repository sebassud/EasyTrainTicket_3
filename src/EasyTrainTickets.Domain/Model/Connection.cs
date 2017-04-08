using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace EasyTrainTickets.Domain.Model
{
    [DataContract(IsReference = true)]
    public class Connection : Entity
    {   
        public Connection()
        {
            this.Parts = new List<ConnectionPart>();
        }
        [DataMember]
        public virtual string StartPlace { get; set; }
        [DataMember]
        public virtual string EndPlace { get; set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual Train Train { get; set; }
        [DataMember]
        public virtual List<ConnectionPart> Parts { get; set; }

    }
}
