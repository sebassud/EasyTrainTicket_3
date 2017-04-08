using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTrainTickets.Domain.Data;

namespace EasyTrainTickets3.WebUI.Models
{
    public class AddConnectionModel
    {
        public void CreateConnection(AddConnectionViewModel viewModel, IEasyTrainTicketsDbEntities dbContext)
        {
            Connection connection = new Connection();
            string idTrain = viewModel.SelectTrain.Split('.')[0];
            int id = Int16.Parse(idTrain);
            connection.Name = viewModel.Name;
            connection.Train = dbContext.Trains.Where(t => t.Id == id).First();
            connection.Parts = new List<ConnectionPart>();

            List<Route> routes = dbContext.Routes.ToList();
            foreach (var r in routes)
            {
                viewModel.AvailableRoutes.Add(String.Format("{0}. {1} => {2}", r.Id, r.From, r.To));
            }

            viewModel.Connection.Connection = connection;
        }

        public void AddConnectionPart(AddConnectionViewModel viewModel, IEasyTrainTicketsDbEntities dbContext, Connection connection)
        {
            List<Route> routes = dbContext.Routes.ToList();
            string idRoute = viewModel.SelectRoute.Split('.').First();
            int id = Int16.Parse(idRoute);
            Route route = routes.Where(r => r.Id == id).First();
            ConnectionPart conPart = new ConnectionPart()
            {
                Route = route,
                Connection = connection
            };
            if(connection.Parts.Count == 0)
            {
                conPart.StartTime = viewModel.StartTime;
                conPart.EndTime = conPart.StartTime.AddMinutes(viewModel.Variance + route.BestTime);
            }
            else
            {
                conPart.StartTime = connection.Parts.Last().EndTime.AddMinutes(viewModel.StopTime);
                conPart.EndTime = conPart.StartTime.AddMinutes(viewModel.Variance + route.BestTime);
            }
            string seats = @"11;12;13;14;15;16;17;18;21;22;23;24;25;26;27;28;31;32;33;34;35;36;37;38;41;42;43;44;45;46;47;48;51;52;53;54;55;56;57;58;61;62;63;64;65;66;67;68;71;72;73;74;75;76;77;78;81;82;83;84;85;86;87;88;91;92;93;94;95;96;97;98;101;102;103;104;105;106;107;108";
            string expressSeats = @"11;12;13;14;15;16;21;22;23;24;25;26;31;32;33;34;35;36;41;42;43;44;45;46;51;52;53;54;55;56;61;62;63;64;65;66;71;72;73;74;75;76;81;82;83;84;85;86;91;92;93;94;95;96;101;102;103;104;105;106";
            if (connection.Train.Type == "Ekspres")
            {
                conPart.Seats = expressSeats;
                conPart.FreeSeats = 60;
            }
            else if(connection.Train.Type == "Pośpieszny")
            {
                conPart.Seats = seats;
                conPart.FreeSeats = 80;
            }
            string endStation = viewModel.SelectRoute.Split('>').Last();
            endStation = endStation.Substring(1);
            foreach (var r in routes.Where(r => r.From == endStation))
            {
                viewModel.AvailableRoutes.Add(String.Format("{0}. {1} => {2}", r.Id, r.From, r.To));
            }
            viewModel.SelectRoute = null;

            connection.Parts.Add(conPart);
            viewModel.Connection.Connection = connection;
        }

        public void EndInitializationConnection(Connection connection)
        {
            connection.StartPlace = connection.Parts.First().Route.From;
            connection.EndPlace = connection.Parts.Last().Route.To;
        }
    }
}