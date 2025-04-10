using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Resources.ClassLb
{
    public enum ImportanceOfTask
    {
        NOTIMPORTANT,
        NORMAL,
        IMPORTANT
    }

    public class WorkTask
    {
        private readonly bool containsNotification;
        public string Header { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskText { get; set; }
        private bool isCompleted;
        private int notificationID;

        public bool IsCompleted
        {
            get { return isCompleted; }
            set 
            { 
                isCompleted = value;
                if (containsNotification)
                    CreateNotification();
            }
        }

        //public bool IsCompleted { get; set; }
        private ImportanceOfTask importance;

        public string IsCompletedText
        {
            get
            {
                if (IsCompleted)
                    return "✓ Выполнено";
                else
                    return string.Empty;
            }
        }
        public ImportanceOfTask Importance
        {
            get { return importance; }
            set { importance = value; }
        }

        public string ImportanceText 
        {
            get 
            {
                switch (importance)
                {
                    case ImportanceOfTask.NOTIMPORTANT:
                        return "Не важно";
                    case ImportanceOfTask.NORMAL:
                        return "Средне";
                    case ImportanceOfTask.IMPORTANT:
                        return "Важно";
                    default:
                        return "Нет";
                }
            } 
        }
        public WorkTask() 
        {
            containsNotification = true;
            notificationID = -1;
            Header = string.Empty;
            TaskText = string.Empty;
            DeadLine = DateTime.Now;
            Importance = ImportanceOfTask.NORMAL;
            IsCompleted = false;
        }
        public WorkTask(string header, string taskText, DateTime deadLine, ImportanceOfTask importance, bool ContainsNotification=true) 
        {
            containsNotification=ContainsNotification;
            notificationID = -1;
            Header = header;
            TaskText = taskText;
            DeadLine = deadLine;
            Importance = importance;
            IsCompleted = false;
        }
        public static bool operator ==(WorkTask left, WorkTask right) 
        { 
            return (left.Header == right.Header) && (left.TaskText == right.TaskText) && (left.DeadLine == right.DeadLine) && (left.Importance == right.Importance) && (left.IsCompleted==right.IsCompleted); 
        }
        public static bool operator !=(WorkTask left, WorkTask right)
        {
            return !((left.Header == right.Header) && (left.TaskText == right.TaskText) && (left.DeadLine == right.DeadLine) && (left.Importance == right.Importance) && (left.IsCompleted == right.IsCompleted));
        }
        public static int CompareImportance(WorkTask workTask1, WorkTask workTask2)
        {
            return -(workTask1.Importance.CompareTo(workTask2.Importance));
        }
        public static int CompareDate(WorkTask workTask1, WorkTask workTask2)
        {
            return workTask1.DeadLine.CompareTo(workTask2.DeadLine);
        }
        private void CreateNotification()
        {
            if (!isCompleted)
            {
                if (notificationID != -1) //если уже есть уведомление то мы отменяем его и делаем новое
                {
                    LocalNotificationCenter.Current.Cancel(notificationID);
                }
                var req = new NotificationRequest()
                {
                    NotificationId = MainPage.LastIdNotification,
                    Title = "Вышло время выполнения задачи",
                    Subtitle = "DeadLine",
                    Description = $"Вы не успели выполнить задачу: {Header}",
                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule() //дата уведомления
                    {
                        NotifyTime = DeadLine
                    },
                    Android = new AndroidOptions()
                    {
                        Color = new AndroidColor(800080)
                    }
                };
                notificationID = MainPage.LastIdNotification;
                MainPage.LastIdNotification++;
                LocalNotificationCenter.Current.Show(req);
            }
            else
            {
                LocalNotificationCenter.Current.Cancel(notificationID);//Если задача выполнена мы отменяем уведомление
                notificationID = -1;
            }
        }
    }
}
