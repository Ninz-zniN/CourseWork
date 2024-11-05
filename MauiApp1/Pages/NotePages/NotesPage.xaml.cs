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
        List<string> filters = ["Нет фильтра", "Без группы", .. MainPage.Groups];
        FilterGroup.ItemsSource = filters;
        FilterGroup.SelectedIndex = 0;
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
    private void FilterGroup_SelectedIndexChanged(object sender, EventArgs e) => DataLoad();
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) => DataLoad();
    private void DataLoad()
    {
        FilterControl();
        Searching();
    }
    private void FilterControl()
    {
        CollectionNotes.ItemsSource = null;
        switch ((string)FilterGroup.SelectedItem)
        {
            case "Нет фильтра":
                NoFilter();
                break;
            case "Без группы":
                Filter();
                break;
            default:
                Filter((string)FilterGroup.SelectedItem);
                break;
        }
    }
    private void NoFilter()
    {
        List<Note> col = new List<Note>(MainPage.Notes);
        col.Reverse();
        CollectionNotes.ItemsSource = col;
    }
    private void Filter(string filterName="")
    {
        List<Note> col = new List<Note>(MainPage.Notes);
        col.Reverse();
        CollectionNotes.ItemsSource = col.FindAll(x=>x.Group==filterName);
    }
    private void Searching()
    {
        if (!(searchBar.Text == null || searchBar.Text.Length == 0))
            CollectionNotes.ItemsSource = ((List<Note>)CollectionNotes.ItemsSource).FindAll(x => x.Header.ToLower().Contains(searchBar.Text.ToLower(),StringComparison.OrdinalIgnoreCase));
    }

    private async void BtnAddCLicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new CreateNotePage());
    private async void BtnGroupClicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new EditGroupPage(),false);

    private async void SearchBar_Unfocused(object sender, FocusEventArgs e)
    {
        if (searchBar.IsSoftInputShowing())
            await searchBar.HideSoftInputAsync(System.Threading.CancellationToken.None);
    }
}