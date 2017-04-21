using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int  Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OrignalDateTime { get; private set; }
        public string OrginalVenue { get; private set; }
        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
        }

        private Notification(Gig gig, NotificationType type)
        {
            if(gig==null)
                throw new ArgumentNullException("gig");

            
            DateTime = DateTime.Now;
            Gig = gig;
            Type = type;
        }

        //static factory methods
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string orginalVenue)
        {
            var notfication = new Notification(newGig, NotificationType.GigUpdated);
            notfication.OrignalDateTime = originalDateTime;
            notfication.OrginalVenue = orginalVenue;

            return notfication;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig,NotificationType.GigCanceled);
        }
    }
}