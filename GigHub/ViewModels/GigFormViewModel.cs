using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        [DateCustomValidation]
        public string Date { get; set; }

        [Required]
        [TimeCustomValidation]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        
        public DateTime ToDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}