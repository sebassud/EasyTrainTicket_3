using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Domain.Model;
using EasyTrainTickets.Domain.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EasyTrainTickets3.WebUI.Models
{
    public class MyTicketModel
    {
        public List<Ticket> GetTicketsUser(IEasyTrainTicketsDbEntities dbContext, PagingInfo PagingInfo)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<Ticket> listTickets = CreateTickets(user);
            PagingInfo.TotalItems = listTickets.Count();
            listTickets = InitializeTickets(listTickets, dbContext);
            listTickets = listTickets.Skip((PagingInfo.CurrentPage - 1) * PagingInfo.ItemsPerPage).Take(PagingInfo.ItemsPerPage).ToList();

            return listTickets;
        }

        private List<Ticket> CreateTickets(ApplicationUser user)
        {
            string[] tickets = user.Tickets.Split(';');
            if (user.Tickets.Length == 0) return new List<Ticket>();
            Ticket[] Tickets = new Ticket[tickets.Length];
            for (int i = 0; i < tickets.Length; i++)
            {
                Tickets[i] = new Ticket();
                Tickets[i].StringTicket = tickets[i];
                string[] discount = tickets[i].Split(':');
                tickets[i] = discount[1];
                string[] conParts = tickets[i].Split('|');
                foreach (var conPart in conParts)
                {
                    string[] part = conPart.Split('-');
                    Tickets[i].conPartsId.Add(int.Parse(part[0]));
                    string[] seats = part[1].Split(',');
                    Tickets[i].Seats.Add(seats.Select(int.Parse).ToArray());
                }
                string[] ids = discount[0].Split(',');
                Tickets[i].discountsId.AddRange(ids.Select(int.Parse).ToList());
            }
            return Tickets.ToList();
        }

        private List<Ticket> InitializeTickets(List<Ticket> tickets, IEasyTrainTicketsDbEntities dbContext)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                HashSet<int> ids = new HashSet<int>();
                foreach (var id in tickets[i].conPartsId)
                {
                    ids.Add(id);
                }
                List<ConnectionPart> parts = dbContext.ConnectionParts.Where(cp => ids.Contains(cp.Id)).ToList();
                for (int j = 0; j < tickets[i].Seats.Count; j++)
                {
                    tickets[i].ConnectionPath.Add(parts.Find(p => p.Id == tickets[i][j]));
                }
                Dictionary<int, int> dict = new Dictionary<int, int>();
                int capacity = dbContext.Discounts.ToList().Count();
                tickets[i].ListDiscount = new int[capacity].ToList();
                for(int j=0; j<tickets[i].ListDiscount.Count; j++) tickets[i].ListDiscount[j] = 0;
                foreach (var id in tickets[i].discountsId)
                {
                    if (dict.ContainsKey(id)) dict[id]++;
                    else
                        dict.Add(id, 1);
                    tickets[i].ListDiscount[id - 1]++;
                }
                foreach (var id in dict.Keys)
                {
                    Discount discount = dbContext.Discounts.Where(d => d.Id == id).First();
                    tickets[i].Discounts.Add(discount, dict[id]);
                }
                tickets[i].ConnectionPath.WriteConnection();
                tickets[i].ConnectionPath.Initialize();
                decimal count = 0;
                foreach (var key in tickets[i].Discounts.Keys)
                {
                    count += (decimal)(tickets[i].Discounts[key] * key.Percent);
                }
                tickets[i].Price = Math.Round(count * tickets[i].ConnectionPath.Price, 2);

                tickets[i].Way = new List<string>();
                for(int k = 0; k < tickets[i].ConnectionPath.Count; k++)
                {
                    tickets[i].Way.Add(string.Format("{0,15} => {1,15}   Odjazd: {2,7}   Przyjazd: {3,7}   Pociąg: {4,10}   Zarezerwowane miejsca: {5,10}",
                        tickets[i].ConnectionPath[k].Route.From, tickets[i].ConnectionPath[k].Route.To, tickets[i].ConnectionPath[k].StartTime.ToShortTimeString(),
                        tickets[i].ConnectionPath[k].EndTime.ToShortTimeString(), tickets[i].ConnectionPath[k].Connection.Name, string.Join(",", tickets[i].Seats[k])));
                }
            }
            tickets = tickets.OrderByDescending(t => t.ConnectionPath.StartTime).ThenByDescending(t => t.ConnectionPath.EndTime).ToList();
            return tickets;
        }

        public bool DeleteTicket(Ticket ticket, IEasyTrainTicketsDbEntities dbContext, ApplicationUserManager userManager)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            string ticketString = ticket.StringTicket;
            string userTickets = user.Tickets;
            int index = userTickets.IndexOf(ticketString);
            string newTickets;

            if (index < 0) return false;
            else if (ticketString.Length == userTickets.Length) newTickets = "";
            else if (index == userTickets.Length - ticketString.Length) newTickets = userTickets.Remove(index - 1, ticketString.Length + 1);
            else
            {
                newTickets = userTickets.Remove(index, ticketString.Length + 1);
            }
            user.Tickets = newTickets;

            for (int i = 0; i < ticket.conPartsId.Count; i++)
            {
                int id = ticket.conPartsId[i];
                ConnectionPart conPart = dbContext.ConnectionParts.Where(cp => cp.Id == id).First();
                if (conPart.Seats == "") conPart.Seats = string.Join(",", ticket.Seats[i]).Replace(',', ';');
                else
                {
                    conPart.Seats = String.Format("{0};{1}", conPart.Seats, string.Join(",", ticket.Seats[i]).Replace(',', ';'));
                }
                conPart.FreeSeats += ticket.Seats[i].Length;
            }

            dbContext.SaveChanges();
            userManager.Update(user);
            return true;
        }
    }
}