using EasyTrainTickets.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTrainTickets3.WebUI.Models
{
    public class MyTicketViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}