using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class RemindersPage : ContentPage
{
	public RemindersPage()
	{
		InitializeComponent();
        Update();
	}

    private void Update()
    {
        CollectionReminder.ItemsSource = null;
        CollectionReminder.ItemsSource = MainPage.Reminders;
    }

    private async void CollectionTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CollectionReminder.SelectedItem != null)
        {
            await Navigation.PushModalAsync(new EditReminderPage((Reminder)CollectionReminder.SelectedItem));
            CollectionReminder.SelectedItem = null;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CreateReminderPage());
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        Update();
    }
}