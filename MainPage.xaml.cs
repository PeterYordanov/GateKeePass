namespace GateKeePass;

public partial class MainPage : ContentPage
{
	public Command PickDatabaseCommand { get; set; }

	public MainPage()
	{
		InitializeComponent();
		PickDatabaseCommand = new Command(OnDatabasePicked);
	}

    private async void OnDatabasePicked(object obj)
    {
		FileResult fileResult = await FilePicker.PickAsync();

        CounterBtn.Text = fileResult.FullPath;
        SemanticScreenReader.Announce("Test");
    }
}

