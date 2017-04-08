using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyTrainTickets.Domain.Services
{
    public class ConnectionPath
    {
        public List<ConnectionPart> ConnectionsParts { get; set; } = new List<ConnectionPart>();
        public List<Connection> Connections { get; } = new List<Connection>();

        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(ConnectionsParts);
            }
        }

        public int Changes
        {
            get
            {
                int changes = 0;
                for(int i = 0; i < ConnectionsParts.Count - 1; i++)
                {
                    if (ConnectionsParts[i].Connection.Id != ConnectionsParts[i + 1].Connection.Id)
                        changes++;
                }
                return changes;
            }
        }
        public int Count
        {
            get
            {
                return ConnectionsParts.Count;
            }
        }
        private int change;
        public int Change
        {
            get
            {
                return change;
            }
        }
        public List<string> Way { get; set; }
        private decimal price;
        public decimal Price
        {
            get
            {
                return price;
            }
        }
        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
        }

        private string startStation;
        public string StartStation
        {
            get
            {
                return startStation;
            }
        }
        private string endStation;
        public string EndStation
        {
            get
            {
                return endStation;
            }
        }
        private TimeSpan journeyTime;
        public TimeSpan JourneyTime
        {
            get
            {
                return journeyTime;
            }
        }

        public DateTime? JourneyEnd
        {
            get
            {
                if (startTime.Date == endTime.Date)
                    return null;
                else
                    return EndTime.Date;
            }
        }
        private List<string> sourcePictures;
        public List<string> SourcePictures
        {
            get
            {
                return sourcePictures;
            }
        }

        public int JourneyTimeInMinutes
        {
            get
            {
                return (int)(ConnectionsParts.Last().EndTime - ConnectionsParts.First().StartTime).TotalMinutes;
            }
        }
        public void Add(ConnectionPart conPart)
        {
            ConnectionsParts.Add(conPart);
        }

        public ConnectionPart this[int index]
        {
            get
            {
                return ConnectionsParts[index];
            }
            set
            {
                ConnectionsParts[index] = value;
            }
        }

        public void WriteConnection()
        {
            Way = new List<string>();
            StringBuilder sb = new StringBuilder();
            int k = 0;
            for (int i = 0; i < Count - 1; i++)
            {
                if (ConnectionsParts[i].Connection.Id != ConnectionsParts[i + 1].Connection.Id)
                {
                    Way.Add(String.Format("{0,15} => {1,15} {2,10} {3,7} {4,10} {5,7} {6,10} {7,10}", ConnectionsParts[k].Route.From, ConnectionsParts[i].Route.To, "Odjazd:", ConnectionsParts[k].StartTime.ToShortTimeString(), "Przyjazd:", ConnectionsParts[i].EndTime.ToShortTimeString(), "Pociąg:", ConnectionsParts[i].Connection.Name));
                    k = i + 1;
                }
            }
            Way.Add(String.Format("{0,15} => {1,15} {2,10} {3,7} {4,10} {5,7} {6,10} {7,10}", ConnectionsParts[k].Route.From, ConnectionsParts[ConnectionsParts.Count - 1].Route.To, "Odjazd:", ConnectionsParts[k].StartTime.ToShortTimeString(), "Przyjazd:", ConnectionsParts[ConnectionsParts.Count - 1].EndTime.ToShortTimeString(), "Pociąg:", ConnectionsParts[ConnectionsParts.Count - 1].Connection.Name));

        }

        public void Initialize()
        {
            sourcePictures = new List<string>();
            startTime = ConnectionsParts[0].StartTime;
            endTime = ConnectionsParts.Last().EndTime;
            startStation = ConnectionsParts[0].Route.From;
            endStation = ConnectionsParts.Last().Route.To;
            journeyTime = endTime - startTime;
            price = 0;
            foreach (var conPart in ConnectionsParts)
            {
                price += conPart.Connection.Train.PricePerKilometer * conPart.Route.Distance;
            }
            change = 0;
            for (int i = 0; i < Count - 1; i++)
            {
                if (ConnectionsParts[i].Connection.Id != ConnectionsParts[i + 1].Connection.Id)
                {
                    change++;
                    Connections.Add(ConnectionsParts[i].Connection);
                    if (ConnectionsParts[i].Connection.Train.Type == "Pośpieszny") sourcePictures.Add("/Content/Images/pospImg.png");
                    else if (ConnectionsParts[i].Connection.Train.Type == "Ekspres") sourcePictures.Add("/Content/Images/exImg.png");
                }
            }
            Connections.Add(ConnectionsParts.Last().Connection);
            if (ConnectionsParts[Count - 1].Connection.Train.Type == "Pośpieszny") sourcePictures.Add("/Content/Images/pospImg.png");
            else if (ConnectionsParts[Count - 1].Connection.Train.Type == "Ekspres") sourcePictures.Add("/Content/Images/exImg.png");
        }
    }
}
