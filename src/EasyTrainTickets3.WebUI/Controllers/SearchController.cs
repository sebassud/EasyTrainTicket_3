using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets3.WebUI.Models;
using EasyTrainTickets.Domain.Services;

namespace EasyTrainTickets3.WebUI.Controllers
{
    public class SearchController : Controller
    {
        IUnitOfWorkFactory unitOfFactory;
        SearchModel searchModel;

        public SearchController(IUnitOfWorkFactory _unit, IGraph graph)
        {
            unitOfFactory = _unit;
            searchModel = new SearchModel(graph);
        }
        // GET: Search
        [HttpGet]
        public ActionResult Index()
        {
            SearchViewModel search = new SearchViewModel();
            search.Stations = unitOfFactory.CreateUnitOfWork().Routes.Select(r => r.To).Distinct().ToList();
            search.ConnectionPaths = new List<ConnectionPath>();
            search.SearchParameters = new SearchParameters();
            return View(search);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            search.Stations = unitOfFactory.CreateUnitOfWork().Routes.Select(r => r.To).Distinct().ToList();
            return View(search);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel search)
        {
            if(search.SearchParameters.From == search.SearchParameters.To ||
                search.SearchParameters.From == search.SearchParameters.Middle ||
                search.SearchParameters.Middle == search.SearchParameters.To)
                return View("MyError", new ErrorViewModel() { ErrorMessage = "Wybrane stacje mają być różne." });

            search.ConnectionPaths = searchModel.Search(search.SearchParameters, unitOfFactory);
            return View(search);
        }

        [HttpPost]
        public ActionResult BuyTicket(ConnectionPath connectionPath)
        {
            return RedirectToAction("Index","BuyTicket",connectionPath);
        }
    }
}