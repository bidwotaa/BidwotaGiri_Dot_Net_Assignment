using System;
using System.ComponentModel.DataAnnotations;

namespace BidwotaGiri_Dot_Net_Assignment.Models
{
    public class AddEventViewModel
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }

        public string Location { get; set; }

        //public string CreatedBy { get; set; }
    }
}
