using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace MauiApp1.Resources.ClassLb
{
    public class Reminder
    {
        public string Header { get; set; }
        public string Description { get; set; }
        private List<int> notificationId = new List<int>();
        public List<int> NotificationID { get{ return notificationId; } }
        //private List<NotificationRequest> notifications = new List<NotificationRequest>();
        //public List<NotificationRequest> Notifications { get{ return notifications; } }
        public Reminder() 
        {
            Header = string.Empty;
            Description = string.Empty;
        }
        public Reminder(string header, string description)
        {
            Header = header;
            Description = description;
        }
    }
}
