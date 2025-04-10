 using MauiApp1.Resources.ClassLb;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Maui.Storage;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Text.Json.Serialization;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public static readonly string JsonFileName = "DataJson";

        public static int LastIdNotification;//= 1;
        static List<WorkTask> tasks = new List<WorkTask>();
        static List<Note> notes = new List<Note>();// { new Note("") { Header="лен"}, new Note("") { Header = "ален" }, new Note("") { Header = "ЛенЬ" }, new Note("") { Header = "Арбуз" }, new Note("") { Header = "лин" } };
        static Dictionary<string, Color> colorsDict= new Dictionary<string, Color> { { "красный", Colors.Red }, { "зеленый", Colors.Green }, { "синий", Colors.Blue } };
        static List<string> groups;//= new List<string>{ "da", "da1", "da2" };
        static List<Reminder> reminders;//= new List<Reminder>() { new Reminder("ам ам ам", "am am am am am")};
        public static List<WorkTask> Tasks { get { return tasks; } }
        public static List<Note> Notes { get { return notes; } }
        public static Dictionary<string, Color> ColorsDict { get { return colorsDict; } }
        public static List<string> Groups { get { return groups; } }
        public static List<Reminder> Reminders { get { return reminders; } }

        public MainPage()
        {
            InitializeComponent();
            DataLoading();
        }

        private void DataLoading()
        {
            //DateTime dt = DateTime.Now.AddDays(1);
            //tasks.AddRange(new List<WorkTask> { new WorkTask("Жиес", "сделай то", dt, ImportanceOfTask.NOTIMPORTANT), new WorkTask("опа", "сделай это", dt, ImportanceOfTask.IMPORTANT), new WorkTask("хы", "ничего не делай eeeeee", dt, ImportanceOfTask.NORMAL) });
            //string json = JsonSerializer.Serialize(tasks) + JsonSerializer.Serialize(notes) + JsonSerializer.Serialize(groups) + JsonSerializer.Serialize(reminders);
            ImportDataFromJson();
            FindMostImportantTask();
        }
        private async void ImportDataFromJson()
        {
            var appStorage = FileSystem.Current.AppDataDirectory;
            string[] fPath = 
            { 
                Path.Combine(appStorage, JsonFileName + "LastIdNotification"), 
                Path.Combine(appStorage, JsonFileName + "Tasks"),
                Path.Combine(appStorage, JsonFileName + "Notes"),
                Path.Combine(appStorage, JsonFileName + "Groups"),
                Path.Combine(appStorage, JsonFileName + "Reminders")
            };
            try
            {
                if (File.Exists(fPath[0]))
                {
                    LastIdNotification = JsonSerializer.Deserialize<int>(File.OpenRead(fPath[0]));

                    tasks = JsonSerializer.Deserialize<List<WorkTask>>(File.OpenRead(fPath[1]));

                    notes = JsonSerializer.Deserialize<List<Note>>(File.OpenRead(fPath[2]));

                    groups = JsonSerializer.Deserialize<List<string>>(File.OpenRead(fPath[3]));

                    reminders = JsonSerializer.Deserialize<List<Reminder>>(File.OpenRead(fPath[4]));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ОШИБКА", $"Не удалось импортировать данные. ошибка: {ex.Message}", "ОК");
            }
        }
        private void FindMostImportantTask()
        {
            if (tasks.Count != 0)
            {
                List<WorkTask> cloneTasks = new List<WorkTask>(tasks);
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
                OtherTasks.ItemsSource = FindOtherTasks(cloneTasks);
            }
        }
        private List<WorkTask> FindOtherTasks(List<WorkTask> workTasks)
        {
            workTasks = workTasks.FindAll(x=>(!x.IsCompleted&&((int)x.Importance)>0));
            if (workTasks.Count > 8)
                workTasks.RemoveRange(8, workTasks.Count - 8);
            workTasks.Sort(WorkTask.CompareDate);
            return workTasks;
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
                    NotifyTime = DateTime.Now.AddSeconds(10),
                    RepeatType = NotificationRepeat.Weekly
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

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView cw = (CollectionView)sender;
            if (cw.SelectedItem != null)
            {
                WorkTask workTask = (WorkTask)cw.SelectedItem;
                cw.SelectedItem = null;
                await Navigation.PushModalAsync(new EditTaskPage(workTask));
            }
        }
    }

}
