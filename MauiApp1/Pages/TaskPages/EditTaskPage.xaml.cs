using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class EditTaskPage : ContentPage
{
    string rb = string.Empty;
    WorkTask workTask;
	public EditTaskPage(WorkTask workTask)
	{
		InitializeComponent();
        this.workTask = workTask;
        Head.Text = workTask.Header;
        Description.Text = workTask.TaskText;
        DateDeadLine.Date = workTask.DeadLine;
        TimeDeadLine.Time = workTask.DeadLine.TimeOfDay;
        switch (workTask.Importance)
        {
            case ImportanceOfTask.NOTIMPORTANT:
                Min.IsChecked = true;
                break;
            case ImportanceOfTask.NORMAL:
                Medium.IsChecked = true;
                break;
            case ImportanceOfTask.IMPORTANT:
                Max.IsChecked = true;
                break;
        }
        if (workTask.IsCompleted)
        {
            Complete.BackgroundColor = Colors.Gray;
            Complete.Text = "Отменить выполнение";
        }
    }
    private async void BtnExitClicked(object sender, EventArgs e)
    {
        ImportanceOfTask importance = ImportanceOfTask.NOTIMPORTANT;
        switch (rb)
        {
            case "Неважно":
                importance = ImportanceOfTask.NOTIMPORTANT;
                break;
            case "Средне":
                importance = ImportanceOfTask.NORMAL;
                break;
            case "Важно":
                importance = ImportanceOfTask.IMPORTANT;
                break;
        }
        WorkTask current = new WorkTask(Head.Text, Description.Text, DateDeadLine.Date.Add(TimeDeadLine.Time), importance,false);
        current.IsCompleted = Complete.BackgroundColor == Colors.Gray;
        if (workTask != current)
        {
            if (await DisplayAlert("Внимание", "В задание были внесены изменения. Сохранить их?", "Сохранить", "Отменить"))
            {
                workTask.Header = Head.Text;
                workTask.TaskText = Description.Text;
                workTask.DeadLine = DateDeadLine.Date.Add(TimeDeadLine.Time);
                workTask.Importance = importance;
                workTask.IsCompleted = Complete.BackgroundColor == Colors.Gray;
            }
        }
        await Navigation.PopModalAsync();
    }
    private async void BtnDeleteClicked(object sender, EventArgs e)
    {
        workTask.IsCompleted = true;
        MainPage.Tasks.Remove(this.workTask);
        await Navigation.PopModalAsync();
    }
    private void BtnCompleteClicked(object sender, EventArgs e)
    {
        if (Complete.BackgroundColor == Colors.Green)
        {
            Complete.BackgroundColor = Colors.Gray;
            Complete.Text = "Отменить выполнение";
        }
        else
        {
            Complete.BackgroundColor = Colors.Green;
            Complete.Text = "Выполнить дело";
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        rb = (string)((RadioButton)sender).Content;
    }
}