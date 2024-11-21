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
        Group.ItemsSource = new List<string>(["Нет группы", .. MainPage.Groups, "Добавить группу"]);
        Group.SelectedItem = note.Group;
        PickerColor.BackgroundColor = note.HeaderColor;
        PickerColor.ItemsSource = new List<string>(["Нет цвета", .. MainPage.ColorsDict.Keys]);
        PickerColor.SelectedIndex = (new List<Color>(MainPage.ColorsDict.Values)).FindIndex(x=>x == note.HeaderColor);
    }

    private async void BtnBackClicked(object sender, EventArgs e)
    {
        note.Header = Header.Text;
        if (Group.SelectedItem.ToString() != "Нет группы")
            note.Group = (string)Group.SelectedItem;
        else
            note.Group = string.Empty;
        note.Description = Description.Text;

        note.HeaderColor = PickerColor.BackgroundColor;
        await Navigation.PopModalAsync();
    }

    private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MainPage.ColorsDict.ContainsKey((string)PickerColor.SelectedItem))
            PickerColor.BackgroundColor = MainPage.ColorsDict[(string)PickerColor.SelectedItem];
        else
            PickerColor.BackgroundColor = Colors.Transparent;
    }

    private async void BtnDeleteClicked(object sender, EventArgs e)
    {
        MainPage.Notes.Remove(note);
        await Navigation.PopModalAsync();
    }

    private async void Group_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((string)Group.SelectedItem == "Добавить группу")
        {
            string NewGroup = await DisplayPromptAsync("Новая группа", "Введите название:", "Сохранить", "Отменить");
            if (NewGroup != null)
            {
                if (NewGroup != string.Empty)
                {
                    MainPage.Groups.Add(NewGroup);
                    var list = new List<string>(MainPage.Groups) { "Добавить группу" };
                    Group.ItemsSource = null;
                    Group.ItemsSource = list;
                    Group.SelectedIndex = Group.ItemsSource.Count - 2; //GroupPicker.SelectedItem = GroupPicker.ItemsSource[GroupPicker.ItemsSource.Count - 2]; //MainPage.Groups[MainPage.Groups.Count - 1];
                }
                else
                {
                    await DisplayAlert("Внимание", "Пустое название недопустимо", "ОК");
                    Group.SelectedItem = null;
                }
            }
            else
                Group.SelectedItem = null;
        }
    }
}