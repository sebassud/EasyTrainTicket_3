using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Domain.Model;
using EasyTrainTickets.Domain.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace EasyTrainTickets3.WebUI.Models
{
    public class BuyTicketModel
    {
        
        public bool NextPart(TicketViewModel ticketViewModel, IEasyTrainTicketsDbEntities dbContext)
        {
            if(ticketViewModel.SeatsChoose != null)
            {
                for (int i = 0; i < ticketViewModel.CountParts; i++) ticketViewModel.Seats.Add(ticketViewModel.SeatsChoose.ToArray());
                ticketViewModel.Way.Add(String.Format("{0,15} ==> {1,15}  -  Pociąg: {2,15}    Zarezerwowane miejsca: {3} ", ticketViewModel.FromStation, ticketViewModel.EndStation, ticketViewModel.ConnectionsParts[ticketViewModel.Seats.Count -1].Connection.Name, string.Join(",", ticketViewModel.Seats.Last())));
            }
            if (ticketViewModel.Seats.Count == ticketViewModel.ConnectionsParts.Count) return true;
            int part, to;
            part = to = ticketViewModel.Seats.Count;
            ticketViewModel.FromStation = ticketViewModel.ConnectionsParts[part].Route.From;
            while (to + 1 < ticketViewModel.ConnectionsParts.Count && ticketViewModel.ConnectionsParts[to].Connection.Id == ticketViewModel.ConnectionsParts[to + 1].Connection.Id) to++;
            ticketViewModel.EndStation = ticketViewModel.ConnectionsParts[to].Route.To;

            if (ticketViewModel.ConnectionsParts[to].Connection.Train.Type == "Pośpieszny") ticketViewModel.SourcePicture = "/Content/Images/pospMsc.png"; // popraw
            else if (ticketViewModel.ConnectionsParts[to].Connection.Train.Type == "Ekspres") ticketViewModel.SourcePicture = "/Content/Images/exMsc.png"; // popraw

            ticketViewModel.FreeSeats = GetSeats(ticketViewModel.ConnectionsParts, part, to, dbContext);
            if (ticketViewModel.FreeSeats.Count < ticketViewModel.Count)
            {
                ticketViewModel.EndStation = ticketViewModel.ConnectionsParts[part].Route.To;
                to = part;
                ticketViewModel.FreeSeats = GetSeats(ticketViewModel.ConnectionsParts, part, to, dbContext);
                if (ticketViewModel.FreeSeats.Count < ticketViewModel.Count)
                {
                    //SendMessage(string.Format("Zostało miejsc: {0}", ticketViewModel.FreeSeats.Count));
                    //IsOK = false;
                    //Cancel();
                    return false;
                }
            }
            ticketViewModel.CountParts = to - part + 1;
            return true;
        }

        private List<int> GetSeats(List<ConnectionPart> connectionsParts, int from, int to, IEasyTrainTicketsDbEntities dbContext)
        {
            List<int> freeseats = new List<int>();

            List<ConnectionPart> conparts = new List<ConnectionPart>();
            conparts.AddRange(connectionsParts.GetRange(from, to - from + 1));
            List<int[]> allseats = new List<int[]>();
            foreach (var part in conparts)
            {
                string seats = dbContext.ConnectionParts.Where(cp => cp.Id == part.Id).Select(cp => cp.Seats).First();
                if (seats == "") return freeseats;
                allseats.Add(seats.Split(';').Select(Int32.Parse).ToArray());
            }

            int[] table = new int[120];
            foreach (var list in allseats)
                foreach (var s in list)
                    table[s]++;

            int count = to - from + 1;
            for (int i = 0; i < table.Count(); i++)
            {
                if (table[i] == count) freeseats.Add(i);
            }
            return freeseats;
        }

        public bool IsEnd(TicketViewModel ticketViewModel)
        {
            if (ticketViewModel.Seats.Count == ticketViewModel.ConnectionsParts.Count) return true;
            else
                return false;
        }

        public bool BuyTicket(TicketViewModel ticketViewModel, ITransactionalData dbContext, ApplicationUserManager userManager)
        {

            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Ticket ticket = new Ticket()
            {
                ConnectionPath = new ConnectionPath() { ConnectionsParts = ticketViewModel.ConnectionsParts },
                Seats = ticketViewModel.Seats,
                ListDiscount = ticketViewModel.ListDiscounts,
                Way = ticketViewModel.Way,
                Price = ticketViewModel.Price
            };
            ticket.ConnectionPath.Initialize();

            for (int i = 0; i < ticket.ConnectionPath.ConnectionsParts.Count; i++)
            {
                var tmp = ticket.ConnectionPath[i];
                ticket.ConnectionPath[i] = dbContext.ConnectionParts.Where(cp => cp.Id == tmp.Id).First();

                int[] userSeat = ticket.Seats[i];

                foreach (var s in userSeat)
                {
                    string seat = s.ToString();
                    string seats = ticket.ConnectionPath[i].Seats;
                    int index = seats.IndexOf(seat);
                    string newSeats;
                    if (index < 0)
                    {
                        dbContext.Reject();
                        return false;
                    }
                    if (seats.Length == seat.Length)
                    {
                        newSeats = "";
                    }
                    else if (index == seats.Length - seat.Length) newSeats = seats.Remove(index - 1, seat.Length + 1);
                    else
                    {
                        newSeats = seats.Remove(index, seat.Length + 1);
                    }

                    ticket.ConnectionPath[i].Seats = newSeats;
                    ticket.ConnectionPath[i].FreeSeats--;
                }

            }
            if (currentUser.Tickets.Length == 0)
                currentUser.Tickets = CreateStringTicket(ticket);
            else
                currentUser.Tickets = string.Format(currentUser.Tickets + ";" + CreateStringTicket(ticket));

            userManager.Update(currentUser);
            dbContext.SaveChanges();
            return true;
        }
        private string CreateStringTicket(Ticket ticket)
        {
            StringBuilder sb = new StringBuilder();
            ticket.discountsId.Clear();
            for(int i = 0; i < ticket.ListDiscount.Count; i++)
            {
                for(int k = 0; k < ticket.ListDiscount[i]; k++)
                {
                    ticket.discountsId.Add(i+1);
                }
            }
            sb.Append(string.Join(",", ticket.discountsId));
            sb.Append(":");
            for (int i = 0; i < ticket.Count; i++)
            {
                sb.Append(string.Format("{0}-{1}", ticket.ConnectionPath[i].Id, string.Join(",", ticket.Seats[i])));
                if (i < ticket.Count - 1)
                    sb.Append("|");
            }
            return sb.ToString();
        }
    }
}