using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class EditNotePage : ContentPage
{
	Note note;
	public EditNotePage(Note note)
	{
		InitializeComponent();
        Description.Text = note.Description;
        this.note = note;
		Header.Text = note.Header;
        Group.ItemsSource = MainPage.Groups;
        Group.SelectedItem = note.Group;
        PickerColor.BackgroundColor = note.HeaderColor;
        PickerColor.ItemsSource = new List<string>(MainPage.ColorsDict.Keys);
        PickerColor.SelectedIndex = (new List<Color>(MainPage.ColorsDict.Values)).FindIndex(x=>x == note.HeaderColor);
    }

    private async void BtnBackClicked(object sender, EventArgs e)
    {
        note.Header = Header.Text;
        note.Group = (string)Group.SelectedItem;
        note.Description = Description.Text;
        await Navigation.PopModalAsync();
    }

    private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MainPage.ColorsDict.ContainsKey((string)PickerColor.SelectedItem))
            PickerColor.BackgroundColor = MainPage.ColorsDict[(string)PickerColor.SelectedItem];
    }

    private async void BtnDeleteClicked(object sender, EventArgs e)
    {
        MainPage.Notes.Remove(note);
        await Navigation.PopModalAsync();
    }
}