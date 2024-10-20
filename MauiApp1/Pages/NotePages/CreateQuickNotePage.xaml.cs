namespace MauiApp1;

public partial class CreateQuickNotePage : ContentPage
{
	public CreateQuickNotePage()
	{
		InitializeComponent();
	}
    private async void BtnExitClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}