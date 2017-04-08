using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTrainTickets.Domain.Services;
using EasyTrainTickets.Domain.Data;
using System.Threading.Tasks;

namespace EasyTrainTickets3.WebUI.Models
{
    public class SearchModel
    {
        private IGraph graph;

        public SearchModel(IGraph _graph)
        {
            graph = _graph;
        }

        public List<ConnectionPath> Search(SearchParameters searchParameters, IUnitOfWorkFactory unitOfWorkFactory)
        {
            List<Path> paths = SearchPaths(searchParameters.From, searchParameters.Middle, searchParameters.To);

            List<ConnectionPath> conPaths = new List<ConnectionPath>();
            IEasyTrainTicketsDbEntities[] dbContexts = new IEasyTrainTicketsDbEntities[paths.Count];
            Parallel.For(0, paths.Count, i =>
            {
                dbContexts[i] = unitOfWorkFactory.CreateUnitOfWork();

                List<ConnectionPath> candidatePaths = paths[i].SecondSearch(searchParameters.UserTime, dbContexts[i]);
                foreach (var conpath in candidatePaths)
                {
                    conPaths.Add(conpath);
                }

            });

            foreach (var conPath in conPaths)
            {
                conPath.Initialize();
            }

            if (searchParameters.IsWithoutChange)
                WithoutChangeFilter(ref conPaths);
            if (!searchParameters.IsExpress)
                WithoutExpressFilter(ref conPaths);

            JourneyTimeFilter(ref conPaths);

            StartTimeFilter(ref conPaths);

            EndTimeFilter(ref conPaths);

            TakeBestFilter(ref conPaths);

            return conPaths;

        }

        private void WithoutExpressFilter(ref List<ConnectionPath> conPaths)
        {
            for (int i = 0; i < conPaths.Count; i++)
            {
                foreach (var con in conPaths[i].Connections)
                {
                    if (con.Train.Type == "Ekspres")
                    {
                        conPaths.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
        }

        private void WithoutChangeFilter(ref List<ConnectionPath> conPaths)
        {
            for (int i = 0; i < conPaths.Count; i++)
            {
                if(conPaths[i].Changes>0)
                {
                    conPaths.RemoveAt(i);
                    i--;
                }
            }
        }

        private void TakeBestFilter(ref List<ConnectionPath> conPaths)
        {
            conPaths = conPaths.OrderBy(c => c.StartTime).ThenBy(c => c.EndTime).Take(10).ToList();
            foreach (var conPath in conPaths)
            {
                conPath.WriteConnection();
            }
        }

        private void JourneyTimeFilter(ref List<ConnectionPath> conPaths)
        {
            if (conPaths.Count == 0) return;
            int best = conPaths.Min(cp => cp.JourneyTimeInMinutes);
            for (int i = 0; i < conPaths.Count; i++)
            {
                if (conPaths[i].JourneyTimeInMinutes > 2.5 * best)
                {
                    conPaths.RemoveAt(i);
                    i--;
                }
            }
        }

        private void StartTimeFilter(ref List<ConnectionPath> conPaths)
        {
            if (conPaths.Count == 0) return;
            var startTimes = conPaths.Select(cp => cp.ConnectionsParts.First().StartTime).Distinct().ToList();
            foreach (var startTime in startTimes)
            {
                var tracks = conPaths.Where(cp => DateTime.Compare(cp.ConnectionsParts[0].StartTime, startTime) == 0).ToList();
                int minChanges = tracks.Min(cp => cp.Changes);
                int bestTime = tracks.Min(cp => cp.JourneyTimeInMinutes);

                var bestTracks = tracks.FindAll(cp => cp.JourneyTimeInMinutes == bestTime).ToList();
                int minBestChanges = bestTracks.Min(cp => cp.Changes);
                var bestTrack = bestTracks.Find(cp => cp.Changes == minBestChanges);

                foreach (var path in tracks)
                {
                    if ((path.JourneyTimeInMinutes > bestTime + 120 && path.Changes > 0) || path.JourneyTimeInMinutes > bestTime && path.Changes >= minBestChanges)
                        conPaths.Remove(path);
                }
            }
        }

        private void EndTimeFilter(ref List<ConnectionPath> conPaths)
        {
            if (conPaths.Count == 0) return;
            var endTimes = conPaths.Select(cp => cp.ConnectionsParts.Last().EndTime).Distinct().ToList();
            foreach (var endTime in endTimes)
            {
                var tracks = conPaths.Where(cp => DateTime.Compare(cp.ConnectionsParts.Last().EndTime, endTime) == 0).ToList();
                int minChanges = tracks.Min(cp => cp.Changes);
                int bestTime = tracks.Min(cp => cp.JourneyTimeInMinutes);

                var bestTracks = tracks.FindAll(cp => cp.JourneyTimeInMinutes == bestTime).ToList();
                int minBestChanges = bestTracks.Min(cp => cp.Changes);
                var bestTrack = bestTracks.Find(cp => cp.Changes == minBestChanges);
                foreach (var path in tracks)
                {
                    if ((path.JourneyTimeInMinutes > bestTime + 120 && path.Changes > 0) || path.JourneyTimeInMinutes > bestTime && path.Changes >= minBestChanges)
                        conPaths.Remove(path);
                }
            }
        }

        private List<Path> SearchPaths(string from, string middle, string to)
        {
            List<Path> paths;
            if (string.IsNullOrEmpty(middle))
                paths = graph.SearchPaths(from, to);
            else
                paths = graph.SearchPaths(from, middle, to);

            return paths;
        }
    }
}