using Android.Views;

namespace MauiApp1;

public partial class EditGroupPage : ContentPage
{
	public EditGroupPage()
	{
		InitializeComponent();
        Update();
	}

    private void Update()
    {
        GroupPicker.ItemsSource = null;
        GroupPicker.ItemsSource = MainPage.Groups;
    }

    private async void BtnСancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(false);
    }

    private async void BtnDeleteClicked(object sender, EventArgs e)
    {
        if (GroupPicker.SelectedItem != null)
        {
            if (GroupPicker.SelectedItem != null)
            {
                if (MainPage.Notes.Exists(x=>x.Group==(string)GroupPicker.SelectedItem))
                {
                    if (await DisplayAlert("Удалить?", "Похоже в этой группе есть заметки. Удалить их?", "Да", "Нет"))
                        MainPage.Notes.RemoveAll(x => x.Group == (string)GroupPicker.SelectedItem);
                    else
                    {
                        foreach (var item in MainPage.Notes.FindAll(x => x.Group == (string)GroupPicker.SelectedItem))
                            item.Group = string.Empty;
                    }
                }
                MainPage.Groups.Remove((string)GroupPicker.SelectedItem);
                Update();
            }
        }
        else
            await DisplayAlert("Внимание", "Нельзя удалить пустую группу", "ОК");
    }

    private async void BtnEditClicked(object sender, EventArgs e)
    {
        if (GroupPicker.SelectedItem != null)
        {
            string NewName = await DisplayPromptAsync("Переименование группы", "Введите название:", "Сохранить", "Отменить");
            if (NewName != null)
            {
                if (NewName != string.Empty)
                {
                    MainPage.Groups[MainPage.Groups.FindIndex(X => X == (string)GroupPicker.SelectedItem)] = NewName;
                    foreach (var item in MainPage.Notes.FindAll(x => x.Group == (string)GroupPicker.SelectedItem))
                        item.Group = NewName;
                    Update();
                }
                else
                    await DisplayAlert("Внимание", "Пустое название недопустимо", "ОК");
            }
        }
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        Update();
    }
}