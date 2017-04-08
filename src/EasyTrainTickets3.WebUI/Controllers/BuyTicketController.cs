using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyTrainTickets.Domain.Services;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets3.WebUI.Models;
using System.Web.Helpers;
using Newtonsoft.Json;
using EasyTrainTickets.Domain.Model;
using Microsoft.AspNet.Identity.Owin;

namespace EasyTrainTickets3.WebUI.Controllers
{
    [Authorize]
    public class BuyTicketController : Controller
    {
        IUnitOfWorkFactory unitOfFactory;
        BuyTicketModel buyTicketModel;

        public BuyTicketController(IUnitOfWorkFactory _unitOfWork)
        {
            unitOfFactory = _unitOfWork;
            buyTicketModel = new BuyTicketModel();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // Post: BuyTicket
        [HttpPost]
        public ActionResult Index(string conIds)
        {
            List<ConnectionPart> conParts = new List<ConnectionPart>();
            try
            {
                conParts = JsonConvert.DeserializeObject<List<ConnectionPart>>(conIds);
            }
            catch
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd." });
            }
            if(conParts.Count==0)
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd." });
            if (conParts.FirstOrDefault().StartTime < DateTime.Now.AddHours(1))
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Czas odjazdu minął." });
            BuyTicketDiscountViewModel discountView = new BuyTicketDiscountViewModel(unitOfFactory);
            discountView.conIds = conIds;
            return View(discountView);
        }

        [HttpPost]
        public ActionResult SummaryPrice(string SelectedDiscount, string conIds)
        {
            List<int> qualDiscount;
            try
            {
                qualDiscount = JsonConvert.DeserializeObject<int[]>(SelectedDiscount).ToList();
            }
            catch
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wybrane zniżki mają być liczbami." });
            }
            if (qualDiscount == null || qualDiscount.Sum() == 0 || qualDiscount.Count != 4)
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wybierz coś." });
            }
            foreach (var discount in qualDiscount)
            {
                if(discount<0 || discount>5)
                    return View("MyError", new ErrorViewModel() { ErrorMessage = "Wybrane ilości mają być liczbami od 0 do 5." });
            }
            BuyTicketSummaryPriceViewModel viewModel;
            using (IEasyTrainTicketsDbEntities dbContext = unitOfFactory.CreateUnitOfWork())
            {
                viewModel = new BuyTicketSummaryPriceViewModel(dbContext, conIds, SelectedDiscount);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RandomSeats(string conIds, string dictDiscount, decimal price)
        {
            TicketViewModel viewModel = new TicketViewModel(conIds, dictDiscount, price);

            using (IEasyTrainTicketsDbEntities dbContext = unitOfFactory.CreateUnitOfWork())
            {
                do
                {
                    if (!buyTicketModel.NextPart(viewModel, dbContext))
                        return View("MyError", new ErrorViewModel() { ErrorMessage = "Z przykrością informujemy, że nie ma wystarczającej ilości miejsc w wybranym połączeniu"});


                    viewModel.SeatsChoose = viewModel.FreeSeats.GetRange(0, viewModel.Count);
                } while (!buyTicketModel.IsEnd(viewModel));
            }
            ModelState.Clear();
            return View("SummaryReservation", viewModel);
        }

        [HttpPost]
        public ActionResult PrepareReserveSeats(string conIds, string dictDiscount, decimal price)
        {
            TicketViewModel viewModel;
            try
            {
                viewModel = new TicketViewModel(conIds, dictDiscount, price);
            }
            catch
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd." });
            }
            using (IEasyTrainTicketsDbEntities dbContext = unitOfFactory.CreateUnitOfWork())
            {
                if (!buyTicketModel.NextPart(viewModel, dbContext))
                    return View("MyError", new ErrorViewModel() { ErrorMessage = "Z przykrością informujemy, że nie ma wystarczającej ilości miejsc w wybranym połączeniu" });
            }
            
            return View("ReserveSeats", viewModel);
        }

        [HttpPost]
        public ActionResult ReserveSeats(string ticket, TicketViewModel ticketViewModel)
        {
            TicketViewModel viewModel;
            try
            {
                viewModel = JsonConvert.DeserializeObject<TicketViewModel>(ticket);
            }
            catch
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd." });
            }
            if(viewModel == null)
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd." });
            viewModel.SeatsChoose = ticketViewModel.SeatsChoose;
            if(viewModel.SeatsChoose.Count != viewModel.Count)
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wybrano niepoprawną liczbę miejsc." });
            using (IEasyTrainTicketsDbEntities dbContext = unitOfFactory.CreateUnitOfWork())
            {
                if (!buyTicketModel.NextPart(viewModel, dbContext))
                    return View("MyError", new ErrorViewModel() { ErrorMessage = "Z przykrością informujemy, że nie ma wystarczającej ilości miejsc w wybranym połączeniu" });
            }
            ModelState.Clear();
            if (buyTicketModel.IsEnd(viewModel)) return View("SummaryReservation", viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BuyTicket(string ticket)
        {
            TicketViewModel viewModel;
            try
            {
                viewModel = JsonConvert.DeserializeObject<TicketViewModel>(ticket);
            }
            catch
            {
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd" });
            }
            if (viewModel == null)
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wystąpił nieoczekiwany błąd." });
            using (ITransactionalData dbContext = unitOfFactory.CreateTransactionalUnitOfWork())
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                bool ok = buyTicketModel.BuyTicket(viewModel, dbContext, userManager);

                if (ok)
                {
                    dbContext.Accept();
                }
                else
                {
                    return View("MyError", new ErrorViewModel() { ErrorMessage = "Z przykrością informujemy, że ktoś był szybszy i zajął twoje miejsce" });
                }
            }

            return View("MySuccess", new ErrorViewModel() { ErrorMessage = "Transakcja przebiegła poprawnie. Bilet został zakupiony." });
        }
    }
}