using System.Windows.Input;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using KeePassLib;
using GateKeePass.Extensions;

namespace GateKeePass;

public partial class AuthenticationPage : ContentPage
{
    public ICommand PickDatabaseCommand { get; set; }
    public ICommand PickKeyfileCommand { get; set; }
    public ICommand OpenDatabaseCommand { get; set; }

    public AuthenticationPage()
    {
        InitializeComponent();
        PickDatabaseCommand = new Command(OnDatabasePicked);
        PickKeyfileCommand = new Command(OnKeyfilePicked);
        OpenDatabaseCommand = new Command(OnDatabaseOpened);

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DatabaseFilepath.Text = string.Empty;
        KeyFilepath.Text = string.Empty;
        PasswordField.Text = string.Empty;
    }

    public async void OnDatabaseOpened(object obj)
    {
        // Open the KeePass database
        var ioConnectionInfo = new IOConnectionInfo { Path = DatabaseFilepath.Text };
        var compKey = new CompositeKey();

        // Add the master password and key file to the composite key
        compKey.AddUserKey(new KcpPassword(PasswordField.Text));
        compKey.AddUserKey(new KcpKeyFile(KeyFilepath.Text));

        var database = new PwDatabase();
        database.Open(ioConnectionInfo, compKey, null);

        if(database.IsOpen)
        {
            await Shell.Current.GoToPageAsync(typeof(MainPage), new Dictionary<string, object>() {
                { "Database", database }
            });
        }
        else
        {
            //TODO: Display message
        }
    }

    //TODO: Show only the filename using Path.GetFileName(filePath);
    public async void OnDatabasePicked(object obj)
    {
        FileResult fileResult = await FilePicker.PickAsync();

        DatabaseFilepath.Text = fileResult.FullPath;

        SemanticScreenReader.Announce("Test");
    }

    //TODO: Show only the filename using Path.GetFileName(filePath);
    public async void OnKeyfilePicked(object obj)
    {
        FileResult fileResult = await FilePicker.PickAsync();

        KeyFilepath.Text = fileResult.FullPath;

        SemanticScreenReader.Announce("Test");
    }

}

