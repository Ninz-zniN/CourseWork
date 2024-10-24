namespace MauiApp1;
using MauiApp1.Resources.ClassLb;

public partial class CreateNotePage : ContentPage
{
    public CreateNotePage()
	{
		InitializeComponent();
        Update();
	}

    private void Update()
    {
        var list = new List<string>(MainPage.Groups) { "�������� ������" };
        GroupPicker.ItemsSource=null;
        ColorPicker.ItemsSource=null;
        GroupPicker.ItemsSource = list;
        ColorPicker.ItemsSource = new List<string>(MainPage.ColorsDict.Keys);
    }

    private async void BtnExitClicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MainPage.ColorsDict.ContainsKey((string)ColorPicker.SelectedItem))
            ColorPicker.BackgroundColor = MainPage.ColorsDict[(string)ColorPicker.SelectedItem];
    }

    private async void BtnSaveClicked(object sender, EventArgs e)
    {
        if ((Head.Text == string.Empty) || (Head.Text==null))
            Head.Text = DateTime.Now.ToString("H:mm  dd MMM");
        MainPage.Notes.Add(new Note(Head.Text, Description.Text, ColorPicker.BackgroundColor, (string)GroupPicker.SelectedItem));
        await Navigation.PopModalAsync();
    }

    private async void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((string)GroupPicker.SelectedItem == "�������� ������")
        {
            string NewGroup = await DisplayPromptAsync("����� ������", "������� ��������:", "���������", "��������");
            if (NewGroup != null)
            {
                if (NewGroup != string.Empty)
                {
                    MainPage.Groups.Add(NewGroup);
                    var list = new List<string>(MainPage.Groups) { "�������� ������" };
                    GroupPicker.ItemsSource = null;
                    GroupPicker.ItemsSource = list;
                    GroupPicker.SelectedIndex = GroupPicker.ItemsSource.Count - 2; //GroupPicker.SelectedItem = GroupPicker.ItemsSource[GroupPicker.ItemsSource.Count - 2]; //MainPage.Groups[MainPage.Groups.Count - 1];
                }
                else
                {
                    await DisplayAlert("��������", "������ �������� �����������", "��");
                    GroupPicker.SelectedItem = null;
                }
            }
            else
                GroupPicker.SelectedItem = null;
        }
    }
}