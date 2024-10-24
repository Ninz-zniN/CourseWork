﻿ using MauiApp1.Resources.ClassLb;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        static List<WorkTask> tasks = new List<WorkTask>();
        static List<Note> notes = new List<Note>();
        static Dictionary<string, Color> colorsDict = new Dictionary<string, Color> { { "красный", Colors.Red }, { "зеленый", Colors.Green }, { "синий", Colors.Blue } };
        static List<string> groups = new List<string>{ "da", "da1", "da2" };
        public static List<WorkTask> Tasks { get { return tasks; } }
        public static List<Note> Notes { get { return notes; } }
        public static Dictionary<string, Color> ColorsDict { get { return colorsDict; } }
        public static List<string> Groups { get { return groups; } }

        public MainPage()
        {
            InitializeComponent();
            DataLoading();
        }

        private void DataLoading()
        {
            DateTime dt = DateTime.Now;
            tasks.AddRange(new List<WorkTask> { new WorkTask("Жиес", "сделай то", dt, ImportanceOfTask.NOTIMPORTANT), new WorkTask("опа", "сделай это", dt, ImportanceOfTask.IMPORTANT), new WorkTask("хы", "ничего не делай eeeeee", dt, ImportanceOfTask.NORMAL) });
            FindMostImportantTask();
            
        }
        private void FindMostImportantTask()
        {
            List<WorkTask> cloneTasks = new List<WorkTask>(tasks);
            if (tasks.Count != 0)
            {
                WorkTask MostTask = tasks[0];
                foreach (WorkTask task in tasks)
                {
                    if ((DateTime.Compare(MostTask.DeadLine, task.DeadLine)) >= 0 && ((int)task.Importance >= (int)MostTask.Importance))
                        MostTask = task;
                }
                //CurrentTask.ItemsSource = null;
                CurrentTask.ItemsSource = new List<WorkTask> { MostTask };
                cloneTasks.Remove(MostTask);
                //OtherTasks.ItemsSource = null;
                OtherTasks.ItemsSource = cloneTasks;
            }
        }

        private void BtnTasksClicked(object sender, EventArgs e)
        {
            var req = new NotificationRequest()
            {
                NotificationId = 1,
                Title = "Пришло уведомление",
                Subtitle = "HOBA",
                Description = "тут есть описание",
                BadgeNumber = 1,
                CategoryType = NotificationCategoryType.Reminder,
                Schedule = new NotificationRequestSchedule()
                {
                    NotifyTime = DateTime.Now.AddSeconds(10)
                },
                Android = new AndroidOptions()
                {
                    Color = new AndroidColor(800080)
                }
            };
            LocalNotificationCenter.Current.Show(req);
            //await Navigation.PushModalAsync(new WorkTaskPage(),false);
        }

        private async void BtnCreateNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateNotePage());
        }
        private async void BtnCreateQuickNoteClicked(object sender, EventArgs e)
        {
            Note note = new Note(await DisplayPromptAsync("Быстрая заметка", "Введите описание:", "Сохранить", "Отменить"));
            note.Header = $"Заголовок {notes.Count + 1}";
            notes.Add(note);
            //await Navigation.PushModalAsync(new CreateQuickNotePage());
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            FindMostImportantTask();
        }
    }

}
