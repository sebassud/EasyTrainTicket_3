using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyTrainTickets3.WebUI.Models;
using EasyTrainTickets.Domain.Model;
using Newtonsoft.Json;
using EasyTrainTickets.Domain.Data;

namespace EasyTrainTickets3.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddConnectionsController : Controller
    {
        IUnitOfWorkFactory unitOfWorkFactory;
        private int PageSize = 20;
        private AddConnectionModel addConnectionModel;
        public AddConnectionsController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            addConnectionModel = new AddConnectionModel();
        }
        // GET: AddConnections
        public ActionResult Index()
        {
            DateViewModel viewModel = new DateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteConnection(int? page, string stringConnection)
        {
            Connection connection = JsonConvert.DeserializeObject<Connection>(stringConnection);
            List<Connection> connections = Session["connections"] as List<Connection>;
            connections.RemoveAll(con => con.StartPlace == connection.StartPlace && con.EndPlace == connection.EndPlace && 
            con.Name == connection.Name && con.Parts.First().StartTime == connection.Parts.First().StartTime);

            PagingInfo PagingInfo = new PagingInfo() { CurrentPage = (int)page, ItemsPerPage = PageSize, TotalItems = connections.Count };
            ListConnectionsViewModel viewModel = new ListConnectionsViewModel();
            viewModel.Show(connections, PagingInfo);
            Session["connections"] = connections;
            ModelState.Clear();

            return View("ListConnections", viewModel);
        }

        [HttpPost]
        public ActionResult AddConnections(DateViewModel viewModel)
        {
            using(IEasyTrainTicketsDbEntities dbContext = unitOfWorkFactory.CreateUnitOfWork())
            {
                List<Connection> connections = Session["connections"] as List<Connection>;
                foreach (var con in connections)
                {
                    con.Train = dbContext.Trains.Where(c => c.Id == con.Train.Id).First();
                    dbContext.Trains.Attach(con.Train);
                    foreach (var part in con.Parts)
                    {
                        part.Route = dbContext.Routes.Where(r => r.Id == part.Route.Id).First();
                        dbContext.Routes.Attach(part.Route);

                    }
                    
                    dbContext.Connections.Add(con);
                }
                dbContext.SaveChanges();
            }
            Session["connections"] = null;
            return View("MySuccess", new ErrorViewModel() { ErrorMessage = "Udało się dodać rozkład jazdy na wybrany dzień" });
        }

        [HttpPost]
        public ActionResult PrepareListConnections(DateViewModel dateViewModel)
        {
            List<Connection> connections;
            using (IEasyTrainTicketsDbEntities dbContext = unitOfWorkFactory.CreateUnitOfWork())
            {
                int number = dbContext.Connections.Where(c => c.Parts.FirstOrDefault().StartTime.Day == dateViewModel.Date.Day && c.Parts.FirstOrDefault().StartTime.Month == dateViewModel.Date.Month && c.Parts.FirstOrDefault().StartTime.Year == dateViewModel.Date.Year).Count();
                if (number > 0)
                {
                    return View("MyError", new ErrorViewModel() { ErrorMessage = "Na ten dzień istnieja już połączenia" });
                }
                connections = ConnectionsGenerator.PerDay(dateViewModel.Date, dbContext);
            }
            PagingInfo PagingInfo = new PagingInfo() { CurrentPage = 1, ItemsPerPage = PageSize, TotalItems = connections.Count };
            ListConnectionsViewModel viewModel = new ListConnectionsViewModel();
            viewModel.Show(connections, PagingInfo);
            Session["connections"] = connections;
            Session["Date"] = dateViewModel.Date;

            return View("ListConnections", viewModel);
        }

        public ActionResult ListConnections(int page)
        {
            List<Connection> connections = Session["connections"] as List<Connection>;
            PagingInfo PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = connections.Count };
            ListConnectionsViewModel viewModel = new ListConnectionsViewModel();
            viewModel.Show(connections, PagingInfo);
            ModelState.Clear();

            return View("ListConnections", viewModel);
        }

        public ActionResult PrepareAddConnection()
        {
            AddConnectionViewModel viewModel;
            using (IEasyTrainTicketsDbEntities dbContext = unitOfWorkFactory.CreateUnitOfWork())
            {
                viewModel = new AddConnectionViewModel(dbContext);
            }
            ViewBag.Date = (DateTime)Session["Date"];
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddConnectionParts(AddConnectionViewModel viewModel, string stringConnection)
        {
            if (viewModel.SelectRoute == null)
            {
                DateTime date = (DateTime)Session["Date"];
                viewModel.StartTime = date.Date + viewModel.TimeOfDay;
                using (IEasyTrainTicketsDbEntities dbContext = unitOfWorkFactory.CreateUnitOfWork())
                {
                    addConnectionModel.CreateConnection(viewModel, dbContext);
                }
            }
            else
            {
                using (IEasyTrainTicketsDbEntities dbContext = unitOfWorkFactory.CreateUnitOfWork())
                {
                    addConnectionModel.AddConnectionPart(viewModel, dbContext, JsonConvert.DeserializeObject<Connection>(stringConnection));
                }
            }
            ModelState.Clear();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddConnection(string stringConnection)
        {
            Connection connection = JsonConvert.DeserializeObject<Connection>(stringConnection);
            addConnectionModel.EndInitializationConnection(connection);

            if(Session["connections"] is List<Connection>)
            {
                List<Connection> connections = Session["connections"] as List<Connection>;
                connections.Add(connection);
                Session["connections"] = connections;
                return RedirectToAction("ListConnections", new { page = 1 });
            }
            else
            {
                using (IEasyTrainTicketsDbEntities dbContext = unitOfWorkFactory.CreateUnitOfWork())
                {
                    dbContext.Trains.Attach(connection.Train);
                    foreach (var part in connection.Parts)
                    {
                        dbContext.Routes.Attach(part.Route);
                 
                    }            
                    dbContext.Connections.Add(connection);
                    dbContext.SaveChanges();
                }
                   
                return View("MySuccess", new ErrorViewModel() { ErrorMessage = "Udało się dodać wybrane połączenie" });
            }
        }
    }
}