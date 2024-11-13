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
        GroupPicker.ItemsSource = null;
        ColorPicker.ItemsSource = null;
        GroupPicker.ItemsSource = new List<string>(["Нет группы", .. MainPage.Groups, "Добавить группу"]);
        ColorPicker.ItemsSource = new List<string>(["Нет цвета", .. MainPage.ColorsDict.Keys]);
    }

    private async void BtnExitClicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MainPage.ColorsDict.ContainsKey((string)ColorPicker.SelectedItem))
            ColorPicker.BackgroundColor = MainPage.ColorsDict[(string)ColorPicker.SelectedItem];
        else
            ColorPicker.BackgroundColor = Colors.Transparent;
    }

    private async void BtnSaveClicked(object sender, EventArgs e)
    {
        if ((Head.Text == string.Empty) || (Head.Text==null))
            Head.Text = DateTime.Now.ToString("H:mm  dd MMM");
        string group;
        if (GroupPicker.SelectedItem == null || GroupPicker.SelectedItem.ToString()=="Нет группы")
            group = string.Empty;
        else
            group = (string)GroupPicker.SelectedItem;
        MainPage.Notes.Add(new Note(Head.Text, Description.Text, ColorPicker.BackgroundColor, group));
        await Navigation.PopModalAsync();
    }

    private async void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((string)GroupPicker.SelectedItem == "Добавить группу")
        {
            string NewGroup = await DisplayPromptAsync("Новая группа", "Введите название:", "Сохранить", "Отменить");
            if (NewGroup != null)
            {
                if (NewGroup != string.Empty)
                {
                    MainPage.Groups.Add(NewGroup);
                    var list = new List<string>(MainPage.Groups) { "Добавить группу" };
                    GroupPicker.ItemsSource = null;
                    GroupPicker.ItemsSource = list;
                    GroupPicker.SelectedIndex = GroupPicker.ItemsSource.Count - 2; //GroupPicker.SelectedItem = GroupPicker.ItemsSource[GroupPicker.ItemsSource.Count - 2]; //MainPage.Groups[MainPage.Groups.Count - 1];
                }
                else
                {
                    await DisplayAlert("Внимание", "Пустое название недопустимо", "ОК");
                    GroupPicker.SelectedItem = null;
                }
            }
            else
                GroupPicker.SelectedItem = null;
        }
    }
}