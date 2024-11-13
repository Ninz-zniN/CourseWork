using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;
using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class CreateReminderPage : ContentPage
{
    List<bool> days = new List<bool>() { false, false, false, false, false, false, false };
	public CreateReminderPage()
	{
		InitializeComponent();
	}
    private async void BtnExitClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    private void BtnSaveClicked(object sender, EventArgs e)
    {
        DateTime dtNow = DateTime.Now;
        DateTime startWeek = DateTime.Now.AddDays((-(int)dtNow.DayOfWeek)+1);
        DateTime NextWeek = DateTime.Now.AddDays(8-(int)dtNow.DayOfWeek);
        Reminder reminder = new Reminder(Head.Text, Description.Text);
        for (int i = 0; i < days.Count; i++)
        {
            if (days[i])
            {
                for (int j = 0; j < Timers.Count; j++)
                {
                    var req = new NotificationRequest()
                    {
                        NotificationId = MainPage.LastIdNotification,
                        Title = reminder.Header,
                        Subtitle = "Напоминание",
                        Description = reminder.Description,
                        BadgeNumber = 1,
                        CategoryType = NotificationCategoryType.Reminder,
                        Schedule = new NotificationRequestSchedule()
                        {
                            RepeatType = NotificationRepeat.Weekly
                        },
                        Android = new AndroidOptions()
                        {
                            Color = new AndroidColor(800080)
                        }
                    };
                    DateTime dt;
                    if ((i+1) >= (int)dtNow.DayOfWeek)
                        dt = startWeek.AddDays(i);
                    else
                        dt = NextWeek.AddDays(i);
                    DateOnly dateOnly = new DateOnly(dt.Year, dt.Month, dt.Day);
                    TimeOnly timeOnly = new TimeOnly(((TimePicker)Timers.ToList()[j]).Time.Ticks);
                    req.Schedule.NotifyTime = new DateTime(dateOnly, timeOnly);

                    LocalNotificationCenter.Current.Show(req);
                    reminder.NotificationID.Add(req.NotificationId);
                    MainPage.LastIdNotification++;
                    
                }
            }
        }
        MainPage.Reminders.Add(reminder);
        Navigation.PopModalAsync();
    }

    private void Btn_Add_Timer_Clicked(object sender, EventArgs e)
    {
        if (Timers.Count < 5)
            Timers.Children.Add(new TimePicker() { WidthRequest = 75, FontSize = 24 });
        else
            Btn_Add_Timer.IsEnabled = false;
    }

    private void Btn_Delete_Timer_Clicked(object sender, EventArgs e)
    {
        if (Timers.Count > 0)
        {
            Timers.Children.RemoveAt(Timers.Children.Count - 1);
            Btn_Add_Timer.IsEnabled = true;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        switch (btn.Text)
        {
            case "пн":
                days[0] = !days[0];
                break;
            case "вт":
                days[1] = !days[1];
                break;
            case "ср":
                days[2] = !days[2];
                break;
            case "чт":
                days[3] = !days[3];
                break;
            case "пт":
                days[4] = !days[4];
                break;
            case "сб":
                days[5] = !days[5];
                break;
            case "вс":
                days[6] = !days[6];
                break;
        }
        if (btn.BackgroundColor == Colors.Gray)
            btn.BackgroundColor = Color.FromArgb("#ffa354");
        else
            btn.BackgroundColor = Colors.Gray;
    }
}