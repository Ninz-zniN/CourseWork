using System.Timers;
using Zxcvbn;

namespace MauiApp1;

public partial class Challenge : ContentPage
{
    private System.Timers.Timer? _timer;
    private double _initialWidth = 300;
    private bool waiting = true;
    private string guessedNum = "0";//"142597";
    private int lengthNum = 5;//����� �����
    private bool end = false;
    private int waitingTime = 10;//����� �� �����������
    private int guessTime = 30; //����� �� ����������� � ���������
    private int currentGuesse = 1;
    private int maxGuesse = 3; //���������� ����� ������� ���� ������� �� ������
    private int riseLenNum = 1; //�� ������� �������� ���������� ����� ����� ������� ���������� �� ������
    public Challenge()
    {
        InitializeComponent();
        StartLevel();
        NumberLabel.Text = guessedNum;

    }
    private void ReloadLevel()
    {
        waiting = true;
        end = false;
        currentGuesse++;
        lengthNum += riseLenNum;
        StartLevel();
        ReloadLevelText();
    }
    private void StartLevel()
    {
        do
        {
            guessedNum = GenerateRandom(lengthNum);
        } while (!CheckStrongNum(guessedNum));

        StartTimer();
    }
    private string GenerateRandom(int length)
    {
        var random = new Random();
        string num = string.Empty;
        for (int i = 0; i < length; i++)
        {
            num += random.Next(10);
        }
        return num;
    }
    private bool CheckStrongNum(string num)
    {
        var result = Core.EvaluatePassword(num);
        if (result.Score > 0)
            return true;
        return false;
        //ZxcvbnResult result = Estimator.Estimate(password);
    }
    private void StartTimer()
    {
        // ���� ������ ��� �������, ������ �� ������
        if (_timer != null && _timer.Enabled)
            return;

        // ������������� ��������� �������� �������
        int remainingSeconds = 0;
        if (waiting)
            remainingSeconds = waitingTime;
        else
            remainingSeconds = guessTime;
        TimerLabel.Text = remainingSeconds.ToString();

        // ������� ������
        _timer = new System.Timers.Timer(1000); // �������� � �������������
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;

        // ��������� ������
        _timer.Start();
    }

    private async void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        // ��������� ���������� �����
        int remainingSeconds = int.Parse(TimerLabel.Text);
        remainingSeconds--;

        // ��������� ����� ������ �� �������� ������ UI
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            TimerLabel.Text = remainingSeconds.ToString();

            // ������������ ����� ������ �������
            double currentWidth = 0;
            if (waiting)
                currentWidth = _initialWidth * ((double)remainingSeconds / waitingTime);
            else
                currentWidth = _initialWidth * ((double)remainingSeconds / guessTime);
            TimerBar.WidthRequest = currentWidth;

            // ���������, ���������� �� ������
            if (remainingSeconds <= 0)
            {
                // ������������� ������
                TimerTurnOff();

                // ���������� �����������
                ShowAlert();  // ���������� ����� ����������� � ��������� �����
                if (waiting)
                {
                    waiting = false;
                    WaitingEnd();
                    StartTimer();
                }
                else
                {
                    end = true;
                    CompleteChallenge();
                }
            }
        });
    }
    private void TimerTurnOff()
    {
        _timer.Stop();
        _timer.Dispose();
        _timer = null;
    }
    // ��������� ����� ��� ������ �����������
    private async Task ShowAlert()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await DisplayAlert("������", "����� �������!", "OK");
        });
    }
    private void ReloadLevelText()
    {
        HelperLabel.Text = "��������� ������� �����:";
        NumberLabel.Text = guessedNum;
        NumberEntry.Text = string.Empty;
        NumberEntry.IsEnabled = false;
    }
    private void WaitingEnd()
    {
        HelperLabel.Text = "������ �����";
        NumberLabel.Text = string.Empty;
        NumberEntry.IsEnabled = true;
    }
    private async void CompleteChallenge()
    {
        if (guessedNum == NumberEntry.Text)
        {
            await DisplayAlert("�����", "�� ������� ������� �����!", "OK");

            if (currentGuesse == maxGuesse)
            {
                await DisplayAlert("�����", "�����������! �� ������ �������!", "OK");
                await Navigation.PopModalAsync();
            }
            if (!end)
                TimerLabel.Text = "0";
            //while (_timer != null) ;
            TimerTurnOff();
            ReloadLevel();
        }
        else
        {
            await DisplayAlert("������", "������� ������������ �����", "OK");
            if (end)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
    private void Button_Clicked(object sender, EventArgs e) => CompleteChallenge();
}