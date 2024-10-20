using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class CreateTaskPage : ContentPage
{
	string rb = string.Empty;
	public CreateTaskPage()
	{
		InitializeComponent();
		DateDeadLine.MinimumDate = DateTime.Now;
	}
	private async void BtnExitClicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
	private async void BtnSaveClicked(object sender, EventArgs e)
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
        await Navigation.PopModalAsync();
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		rb = (string)((RadioButton)sender).Content;
    }
}