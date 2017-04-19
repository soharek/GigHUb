using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
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
        public string Heading { get; set; }
        public string Action
        {

            get
            {
                Expression<Func<GigsController, ActionResult>> update = (u => u.Update(this));
                Expression<Func<GigsController, ActionResult>> create = (u => u.Create(this));

                var action = (Id == 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;

                 
            }
        }


        public DateTime ToDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}