using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets3.WebUI.Models;
using EasyTrainTickets.Domain.Services;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;

namespace EasyTrainTickets3.WebUI.Controllers
{
    [Authorize]
    public class MyTicketController : Controller
    {
        private IUnitOfWorkFactory unitOfWorkFactory;
        private MyTicketModel myTicketModel;
        private readonly int PageSize = 5;

        public MyTicketController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
            myTicketModel = new MyTicketModel();
        }
        // GET: MyTicket
        public ActionResult Index(int page = 1)
        {
            MyTicketViewModel viewModel = new MyTicketViewModel();
            using (var dbContext = unitOfWorkFactory.CreateUnitOfWork())
            {
                PagingInfo PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize
                };
                List<Ticket> tickets = myTicketModel.GetTicketsUser(dbContext, PagingInfo);
                viewModel.Tickets = tickets;
                viewModel.PagingInfo = PagingInfo;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteTicket(string stringTicket)
        {
            Ticket ticket;
            try
            {
                ticket = JsonConvert.DeserializeObject<Ticket>(stringTicket);
            }
            catch
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd" });
            }
            if (ticket == null || ticket.ConnectionPath.StartTime < DateTime.Now.AddHours(1))
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Czas odjazdu minął." });
            using (var dbContext = unitOfWorkFactory.CreateTransactionalUnitOfWork())
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (!myTicketModel.DeleteTicket(ticket, dbContext, userManager))
                    return View("MyError", new ErrorViewModel() { ErrorMessage = "Z przykrością informujemy, że ten bilet został już wcześniej usunięty" });
                dbContext.Accept();
            }
            return RedirectToAction("Index");
        }
    }
}