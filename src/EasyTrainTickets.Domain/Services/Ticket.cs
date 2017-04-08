using EasyTrainTickets.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Services
{
    public class Ticket
    {
        [JsonIgnore]
        public ConnectionPath ConnectionPath { get; set; } = new ConnectionPath();
        public List<int[]> Seats { get; set; } = new List<int[]>();

        public List<int> conPartsId = new List<int>();
        [JsonIgnore]
        public Dictionary<Discount, int> Discounts { get; set; } = new Dictionary<Discount, int>();
        [JsonIgnore]
        public List<int> ListDiscount { get; set; }
        [JsonIgnore]
        public List<int> discountsId = new List<int>();
        [JsonIgnore]
        public List<string> Way { get; set; }
        [JsonIgnore]
        public decimal Price { get; set; }
        [JsonIgnore]
        public int this[int i]
        {
            get
            {
                return conPartsId[i];
            }
        }
        [JsonIgnore]
        public int Count
        {
            get
            {
                return ConnectionPath.ConnectionsParts.Count;
            }
        }

        [JsonIgnore]
        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(this);
            }
        }
        public string StringTicket { get; set; }
        
    }
}
