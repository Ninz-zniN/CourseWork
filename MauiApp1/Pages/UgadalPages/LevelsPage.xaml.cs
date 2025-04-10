namespace MauiApp1;

public partial class LevelsPage : ContentPage
{
	public LevelsPage()
	{
		InitializeComponent();
	}
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Challenge());
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}