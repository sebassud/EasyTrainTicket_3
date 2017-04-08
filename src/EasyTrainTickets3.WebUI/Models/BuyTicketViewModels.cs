using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Domain.Model;
using Newtonsoft.Json;
using EasyTrainTickets.Domain.Services;
using System.Web.Script.Serialization;

namespace EasyTrainTickets3.WebUI.Models
{
    public class BuyTicketDiscountViewModel
    {
        public string conIds;
        public List<Discount> discounts;
        public int[] qualityDiscounts;

        public string SelectedDiscount { get; set; }

        public BuyTicketDiscountViewModel() { }

        public BuyTicketDiscountViewModel(IUnitOfWorkFactory unitOfFactory)
        {
            using (IEasyTrainTicketsDbEntities dbContext = unitOfFactory.CreateUnitOfWork())
            {
                discounts = dbContext.Discounts.ToList();
                qualityDiscounts = new int[discounts.Count];
            }
        }
    }

    public class BuyTicketSummaryPriceViewModel
    {
        public string ConIds { get; set; }
        public string ListDiscount { get; set; }
        public decimal Price { get; set; }
        public List<string> Way { get; set; }
        public Dictionary<Discount, int> Dict { get; set; }

        public BuyTicketSummaryPriceViewModel() { }

        public BuyTicketSummaryPriceViewModel(IEasyTrainTicketsDbEntities dbContext, string conIds, string qualityDiscount)
        {
            List<int> qualDiscount = JsonConvert.DeserializeObject<int[]>(qualityDiscount).ToList();
            Dict = new Dictionary<Discount, int>();
            List<Discount> discounts = dbContext.Discounts.ToList();
            for (int i = 0; i < discounts.Count; i++)
            {
                Dict.Add(discounts[i], qualDiscount[i]);
            }
            List<int> list = new List<int>();
            foreach (var key in Dict.Keys) list.Add(Dict[key]);
            ListDiscount = JsonConvert.SerializeObject(list);

            ConIds = conIds;
            List<ConnectionPart> conParts = JsonConvert.DeserializeObject<List<ConnectionPart>>(conIds);
            ConnectionPath conPath = new ConnectionPath() { ConnectionsParts = conParts };
            conPath.Initialize();
            decimal count = 0;
            foreach(var key in Dict.Keys)
            {
                count += (decimal)(Dict[key] * key.Percent);
            }
            Price = Math.Round(count * conPath.Price, 2);
            conPath.WriteConnection();
            Way = conPath.Way;
        }
    }

    public class TicketViewModel
    {
        public List<int> ListDiscounts { get; set; }
        public List<int[]> Seats { get; set; }
        public List<ConnectionPart> ConnectionsParts { get; set; }
        public string SourcePicture { get; set; }
        public List<int> FreeSeats { get; set; }
        public List<int> SeatsChoose { get; set; }
        public string FromStation { get; set; }
        public string EndStation { get; set; }
        public int Count { get; set; }
        public int CountParts { get; set; }
        public List<string> Way { get; set; }
        public decimal Price { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        public TicketViewModel() { }

        public TicketViewModel(string conIds, string dict, decimal price)
        {
            ListDiscounts = JsonConvert.DeserializeObject<List<int>>(dict);
            ConnectionsParts = JsonConvert.DeserializeObject<List<ConnectionPart>>(conIds);
            Count = 0;
            Seats = new List<int[]>();
            foreach (var i in ListDiscounts) Count += i;
            Way = new List<string>();
            Price = price;
        }
    }

}