using GigHub.Dtos;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class NottficationsAPIDataModel
    {
        public IEnumerable<NotificationDto> NotificationDtos { get; private set; }
        public int NumberOfNewNotfications { get; private set; }

        public NottficationsAPIDataModel(IEnumerable<NotificationDto> notificationDtos, int numberOfNewNotfications )
        {
            NotificationDtos = notificationDtos;
            NumberOfNewNotfications = numberOfNewNotfications;
        }
    }
}