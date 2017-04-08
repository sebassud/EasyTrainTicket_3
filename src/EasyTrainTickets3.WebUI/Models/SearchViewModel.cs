using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EasyTrainTickets.Domain.Services;

namespace EasyTrainTickets3.WebUI.Models
{
    public class SearchViewModel
    {
        public List<string> Stations { get; set; }
        public SearchParameters SearchParameters { get; set; }
        public List<ConnectionPath> ConnectionPaths { get; set; }
        public ConnectionPath SelectedConnectionPath { get; set; }
    }

    public class SearchParameters
    {
        [DataType(DataType.Date)]
        public DateTime UserTime { get; set; }
        [Required]
        public string From { get; set; }
        public string Middle { get; set; }
        [Required]
        public string To { get; set; }
        public bool IsExpress { get; set; } = true;
        public bool IsWithoutChange { get; set; }
    }
}