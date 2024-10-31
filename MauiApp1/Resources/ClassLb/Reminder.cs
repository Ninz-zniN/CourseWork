using Plugin.LocalNotification;

namespace MauiApp1.Resources.ClassLb
{
    public class Reminder
    {
        public string Header { get; set; }

        public string Description { get; set; }

        private List<int> notificationId = new List<int>();
        public List<int> NotificationID { get{ return notificationId; } }
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
