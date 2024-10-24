using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class NotesPage : ContentPage
{
    public NotesPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        FilterGroup.ItemsSource = null;
        List<string> filters = ["��� �������", "��� ������", .. MainPage.Groups];
        FilterGroup.ItemsSource = filters;
        FilterGroup.SelectedIndex = 0;
    }
    private void NoFilter()
    {
        List<Note> col = new List<Note>(MainPage.Notes);
        col.Reverse();
        var collection1 = new List<Note>();
        var collection2 = new List<Note>();
        for (int i = 0; i < col.Count; i++)
        {
            if (i % 2 == 0)
                collection1.Add(col[i]);
            else
                collection2.Add(col[i]);
        }
        CollectionNotes.ItemsSource = collection1;
        CollectionNotes1.ItemsSource = collection2;
    }
    private void Filter(string filterName="")
    {
        List<Note> col = new List<Note>(MainPage.Notes);
        col.Reverse();
        Sort(ref col, filterName);
        var collection1 = new List<Note>();
        var collection2 = new List<Note>();
        for (int i = 0; i < col.Count; i++)
        {
            if (i % 2 == 0)
                collection1.Add(col[i]);
            else
                collection2.Add(col[i]);
        }
        CollectionNotes.ItemsSource = collection1;
        CollectionNotes1.ItemsSource = collection2;
    }
    private void Sort(ref List<Note> list, string sort)
    {
        List<Note> sortedList = new List<Note>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Group == sort)
                sortedList.Add(list[i]);
        }
        list = sortedList;
    }

    private async void CollectionNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CollectionNotes.SelectedItem != null)
        {
            Note note = (Note)CollectionNotes.SelectedItem;
            CollectionNotes.SelectedItem = null;
            await Navigation.PushModalAsync(new EditNotePage(note));
        }
    }

    private void FilterGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        CollectionNotes.ItemsSource = null;
        CollectionNotes1.ItemsSource = null;
        switch ((string)FilterGroup.SelectedItem)
        {
            case "��� �������":
                NoFilter();
                break;
            case "��� ������":
                Filter();
                break;
            default:
                Filter((string)FilterGroup.SelectedItem);
                break;
        }
    }

    private async void BtnAddCLicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new CreateNotePage());
    private async void BtnGroupClicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new EditGroupPage(),false);
}