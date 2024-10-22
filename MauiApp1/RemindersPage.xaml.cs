namespace MauiApp1;

public partial class RemindersPage : ContentPage
{
	public RemindersPage()
	{
		InitializeComponent();
	}

    private void CollectionTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CreateReminderPage());
    }
}