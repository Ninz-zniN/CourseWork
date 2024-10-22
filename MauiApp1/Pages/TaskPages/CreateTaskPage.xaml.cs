using MauiApp1.Resources.ClassLb;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;

namespace MauiApp1;

public partial class CreateTaskPage : ContentPage
{
	string rb = string.Empty;
	public CreateTaskPage()
	{
		InitializeComponent();
		DateDeadLine.MinimumDate = DateTime.Now;
	}
	private void BtnExitClicked(object sender, EventArgs e)
	{
		Close();
	}
	private void BtnSaveClicked(object sender, EventArgs e)
	{
		ImportanceOfTask importance = ImportanceOfTask.NOTIMPORTANT;
		switch (rb)
		{
			case "�������":
                importance = ImportanceOfTask.NOTIMPORTANT;
				break;
			case "������":
                importance = ImportanceOfTask.NORMAL;
				break;
			case "�����":
				importance = ImportanceOfTask.IMPORTANT;
				break;
		}
		MainPage.Tasks.Add(new WorkTask(Head.Text, Description.Text, DateDeadLine.Date.Add(TimeDeadLine.Time), importance));
        var req = new NotificationRequest()
        {
            NotificationId = MainPage.LastIdNotification,
            Title = "����� ����� ���������� ������",
            Subtitle = "DeadLine",
            Description = $"�� �� ������ ��������� ������: {Head.Text}",
            BadgeNumber = 1,
            CategoryType = NotificationCategoryType.Reminder,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateDeadLine.Date.Add(TimeDeadLine.Time)
            },
            Android = new AndroidOptions()
            {
                Color = new AndroidColor(800080)
            }
        };
        MainPage.LastIdNotification++;
        var req1 = new NotificationRequest()
        {
            NotificationId = MainPage.LastIdNotification,
            Title = "����� �������������",
            Subtitle = "DeadLine",
            Description = $"� ��� �������� ������ ����� ����� ��������� ������: {Head.Text}",
            BadgeNumber = 1,
            CategoryType = NotificationCategoryType.Reminder,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateDeadLine.Date.Add(TimeDeadLine.Time).AddDays(-1)
            },
            Android = new AndroidOptions()
            {
                Color = new AndroidColor(800080)
            }
        };
        MainPage.LastIdNotification++;
        LocalNotificationCenter.Current.Show(req);
        LocalNotificationCenter.Current.Show(req1);
        Close();
    }
    private async void Close()
    {
        await Navigation.PopModalAsync();
    }
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		rb = (string)((RadioButton)sender).Content;
    }
}