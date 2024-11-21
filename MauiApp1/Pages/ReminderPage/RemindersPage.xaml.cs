using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class RemindersPage : ContentPage
{
	public RemindersPage()
	{
		InitializeComponent();
	}
    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        Update();
    }
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) => Update();
    private void Update()
    {
        CollectionReminder.ItemsSource = null;
        CollectionReminder.ItemsSource = MainPage.Reminders;
        Searching();
    }
    private void Searching()
    {
        if (!(searchBar.Text == null || searchBar.Text.Length == 0))
            CollectionReminder.ItemsSource = ((List<Reminder>)CollectionReminder.ItemsSource).FindAll(x => x.Header.ToLower().Contains(searchBar.Text.ToLower(), StringComparison.OrdinalIgnoreCase));
    }
    private async void CollectionTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CollectionReminder.SelectedItem != null)
        {
            Reminder reminder = (Reminder)CollectionReminder.SelectedItem;
            CollectionReminder.SelectedItem = null;
            await Navigation.PushModalAsync(new EditReminderPage(reminder));
        }
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CreateReminderPage());
    }
    private async void SearchBar_Unfocused(object sender, FocusEventArgs e)
    {
        if (searchBar.IsSoftInputShowing())
            await searchBar.HideSoftInputAsync(System.Threading.CancellationToken.None);
    }
}