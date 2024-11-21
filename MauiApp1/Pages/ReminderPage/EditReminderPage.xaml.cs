using MauiApp1.Resources.ClassLb;
using Plugin.LocalNotification;

namespace MauiApp1;

public partial class EditReminderPage : ContentPage
{
    Reminder reminder;
	public EditReminderPage(Reminder reminder)
	{
		InitializeComponent();
        this.reminder = reminder;
        Head.Text = reminder.Header;
        Description.Text = reminder.Description;
	}

    private async void BtnDeleteClicked(object sender, EventArgs e)
    {
        LocalNotificationCenter.Current.Cancel(reminder.NotificationID.ToArray());
        MainPage.Reminders.Remove(reminder);
        await Navigation.PopModalAsync();
    }

    private async void BtnExitClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}