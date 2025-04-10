namespace MauiApp1;

public partial class SelectPage : ContentPage
{
	public SelectPage()
	{
		InitializeComponent();
	}
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LevelsPage());
    }
}