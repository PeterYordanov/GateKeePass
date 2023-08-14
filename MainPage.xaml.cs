using System.Windows.Input;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using KeePassLib;
using System.Data;
using KeePassLib.Collections;

namespace GateKeePass;

public partial class MainPage : ContentPage
{
    public ICommand PickDatabaseCommand { get; set; }
    public ICommand PickKeyfileCommand { get; set; }
    public ICommand OpenDatabaseCommand { get; set; }

    string keyFilePath;
    string databaseFilePath;

    public MainPage()
    {
        InitializeComponent();
        PickDatabaseCommand = new Command(OnDatabasePicked);
        PickKeyfileCommand = new Command(OnKeyfilePicked);
        OpenDatabaseCommand = new Command(OnDatabaseOpened);

        BindingContext = this;
    }

    public async void OnDatabaseOpened(object obj)
    {
        // Specify the path to your KeePass database file
        string password = "";

        // Open the KeePass database
        var ioConnectionInfo = new IOConnectionInfo { Path = databaseFilePath };
        var compKey = new CompositeKey();

        // Add the master password and key file to the composite key
        compKey.AddUserKey(new KcpPassword(password));
        compKey.AddUserKey(new KcpKeyFile(keyFilePath));

        var database = new PwDatabase();
        database.Open(ioConnectionInfo, compKey, null);

        bool isOpen = database.IsOpen;

        PwObjectList<PwEntry> entries = database.RootGroup.GetEntries(true);
    }

    public async void OnDatabasePicked(object obj)
    {
        FileResult fileResult = await FilePicker.PickAsync();

        databaseFilePath = fileResult.FullPath;

        SemanticScreenReader.Announce("Test");
    }


    public async void OnKeyfilePicked(object obj)
    {
        FileResult fileResult = await FilePicker.PickAsync();

        keyFilePath = fileResult.FullPath;

        SemanticScreenReader.Announce("Test");
    }

}

