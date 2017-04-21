using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int  Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OrignalDateTime { get; set; }
        public string OrginalVenue { get; set; }
        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
        }

        public Notification(Gig gig, NotificationType type)
        {
            if(gig==null)
                throw new ArgumentNullException("gig");

            
            DateTime = DateTime.Now;
            Gig = gig;
            Type = type;
        }
    }
}