using EasyTrainTickets.Domain.Model;
using EasyTrainTickets.Domain.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EasyTrainTickets3.WebUI.Models
{
    public class DateViewModel
    {
        public DateTime Date { get; set; }
    }

    public class ConnectionViewModel
    {
        public Connection Connection { get; set; }

        [JsonIgnore]
        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(this.Connection);
            }
        }
        [JsonIgnore]
        public List<string> Way
        {
            get
            {
                List<string> way = new List<string>();
                foreach(var conPart in Connection.Parts)
                {
                    way.Add(string.Format("{0,15} => {1,15}   Odjazd: {2,7}   Przyjazd: {3,7}", conPart.Route.From,
                        conPart.Route.To, conPart.StartTime.ToShortTimeString(), conPart.EndTime.ToShortTimeString()));
                }

                return way;
            }
        }
    }

    public class ListConnectionsViewModel
    {       
        public List<ConnectionViewModel> ListConnections { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public ListConnectionsViewModel()
        {
            ListConnections = new List<ConnectionViewModel>();
        }

        public void Show(List<Connection> connections, PagingInfo pagingInfo)
        {
            PagingInfo = pagingInfo;
            ListConnections = new List<ConnectionViewModel>();

            foreach (Connection con in connections.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.ItemsPerPage).Take(pagingInfo.ItemsPerPage).ToList())
            {
                ListConnections.Add(new ConnectionViewModel() { Connection = con });
            }
        }
    }

    public class AddConnectionViewModel
    {
        public ConnectionViewModel Connection { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        [Required]
        public int StopTime { get; set; }
        [Required]
        public int Variance { get; set; }
        public List<string> AvailableRoutes { get; set; }
        [Required]
        public String SelectRoute { get; set; }
        public List<string> Trains { get; set; }
        [Required]
        public string SelectTrain { get; set; }
        [Required]
        public string Name { get; set; }

        public AddConnectionViewModel()
        {
            AvailableRoutes = new List<string>();
            Connection = new ConnectionViewModel();
        }
        public AddConnectionViewModel(IEasyTrainTicketsDbEntities dbContext)
        {
            List<Train> trains = dbContext.Trains.ToList();
            Trains = new List<string>();
            foreach(var t in trains)
            {
                Trains.Add(String.Format("{0}. {1}", t.Id, t.Type));
            }
        }

    }

}