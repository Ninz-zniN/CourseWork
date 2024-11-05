using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class WorkTaskPage : ContentPage
{
    private enum FilterIndex
    {
        TODAY,
        WEEK,
        MONTH
    }

    private enum SortIndex
    {
        URGENCY,
        IMPORTANT
    }
	public WorkTaskPage()
	{
		InitializeComponent();
	}

    private async void BtnAddCLicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new CreateTaskPage());

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        PickerSort.SelectedIndex = -1;
        PickerFilter.SelectedIndex = -1;
        DataLoad();
    }
    private void Picker_SelectedIndexChanged(object sender, EventArgs e) => DataLoad();
    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) => DataLoad();
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) => DataLoad();
    private void DataLoad()
    {
        Sort();
        Searching();
    }
    private void Sort()
    {
        var list = Filter();
        switch ((SortIndex)PickerSort.SelectedIndex)
        {
            case SortIndex.URGENCY:
                list.Sort(WorkTask.CompareDate);
                break;
            case SortIndex.IMPORTANT:
                list.Sort(WorkTask.CompareImportance);
                break;
            default:
                list.Reverse();
                break;
        }
        CollectionTasks.ItemsSource = list;
    }
    private List<WorkTask> Filter()
    {
        var list = new List<WorkTask>(MainPage.Tasks);
        switch ((FilterIndex)PickerFilter.SelectedIndex)
        {
            case FilterIndex.TODAY:
                list = list.FindAll(x => x.DeadLine.Date.Equals(DateTime.Now.Date));
                break;
            case FilterIndex.WEEK:
                list = list.FindAll(x => IsCurrentWeek(x.DeadLine.Date));
                break;
            case FilterIndex.MONTH:
                list = list.FindAll(x => x.DeadLine.Month == DateTime.Now.Month);
                break;
            default:
                break;
        }
        if (CB_OnlyNotCompleted.IsChecked)
            list.RemoveAll(x => x.IsCompleted);
        return list;
    }
    private bool IsCurrentWeek(DateTime dateTime)
    {
        DateTime dateStartWeek = DateTime.Now.Date.AddDays(-(int)DateTime.Now.Date.DayOfWeek+1);
        int daysBetween = Math.Abs((dateTime - dateStartWeek).Days);
        return daysBetween <= 6;
    }
    private void Searching()
    {
        if (!(searchBar.Text == null || searchBar.Text.Length == 0))
            CollectionTasks.ItemsSource = ((List<WorkTask>)CollectionTasks.ItemsSource).FindAll(x => x.Header.ToLower().Contains(searchBar.Text.ToLower(), StringComparison.OrdinalIgnoreCase));
    }
    private async void SearchBar_Unfocused(object sender, FocusEventArgs e)
    {
        if (searchBar.IsSoftInputShowing())
            await searchBar.HideSoftInputAsync(System.Threading.CancellationToken.None);
    }
    private async void CollectionTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CollectionTasks.SelectedItem != null)
        {
            WorkTask task = (WorkTask)CollectionTasks.SelectedItem;
            CollectionTasks.SelectedItem = null;
            await Navigation.PushModalAsync(new EditTaskPage(task));
        }
    }
}