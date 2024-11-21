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
	private void BtnExitClicked(object sender, EventArgs e)
	{
		Close();
	}
	private void BtnSaveClicked(object sender, EventArgs e)
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
		MainPage.Tasks.Add(new WorkTask(Head.Text, Description.Text, DateDeadLine.Date.Add(TimeDeadLine.Time), importance));
        
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