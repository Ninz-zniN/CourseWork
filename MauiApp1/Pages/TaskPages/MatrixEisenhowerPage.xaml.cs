using MauiApp1.Resources.ClassLb;

namespace MauiApp1;

public partial class MatrixEisenhowerPage : ContentPage
{
	
	public MatrixEisenhowerPage()
	{
		InitializeComponent();
	}
	private void Update()
	{
		ImportantUrgent.ItemsSource = Sort(true,true);
		NotImportantUrgent.ItemsSource = Sort(false,true);
		ImportantNotUrgent.ItemsSource = Sort(true, false);
		NotImportantNotUrgent.ItemsSource = Sort(false, false);
	}
	private List<WorkTask> Sort(bool Important, bool Urgent)
	{
		var list = new List<WorkTask>(MainPage.Tasks);
		TimeSpan ts = DateTime.Now.Subtract(list[list.Count-1].DeadLine);

        if (Important && Urgent)
			list = list.FindAll(x => (x.Importance == ImportanceOfTask.IMPORTANT) && (x.DeadLine.Subtract(DateTime.Now).TotalDays <= 2));

		else if (!Important && Urgent)
            list = list.FindAll(x => (x.Importance != ImportanceOfTask.IMPORTANT) && (x.DeadLine.Subtract(DateTime.Now).TotalDays <= 2));

		else if (Important && !Urgent)
			list = list.FindAll(x => (x.Importance == ImportanceOfTask.IMPORTANT) && (x.DeadLine.Subtract(DateTime.Now).TotalDays > 2));

		else
            list = list.FindAll(x => (x.Importance != ImportanceOfTask.IMPORTANT) && (x.DeadLine.Subtract(DateTime.Now).TotalDays > 2));

		list=list.FindAll(x => !x.IsCompleted);
		if (Important)
			list.Sort(WorkTask.CompareDate);
		else
			list.Sort(WorkTask.CompareImportance);
        return list;
	}

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
		Update();
    }
    private async void CollectionTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		CollectionView CollectionTasks = (CollectionView)sender;
        if (CollectionTasks.SelectedItem != null)
        {
            WorkTask task = (WorkTask)CollectionTasks.SelectedItem;
            CollectionTasks.SelectedItem = null;
            await Navigation.PushModalAsync(new EditTaskPage(task));
        }
    }
}