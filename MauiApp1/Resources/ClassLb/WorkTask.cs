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
        public string Header { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskText { get; set; }
        public bool IsCompleted { get; set; }
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

        public String ImportanceText 
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
            Header = string.Empty;
            TaskText = string.Empty;
            DeadLine = DateTime.Now;
            Importance = ImportanceOfTask.NORMAL;
        }
        public WorkTask(string header, string taskText, DateTime deadLine, ImportanceOfTask importance) 
        {
            Header = header;
            TaskText = taskText;
            DeadLine = deadLine;
            Importance = importance;
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
    }
}
