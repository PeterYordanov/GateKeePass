using GateKeePass.Database;
using IntelliJ.Lang.Annotations;

namespace GateKeePass;

public partial class EntryPage : ContentPage, IQueryAttributable
{
    KDBXDatabase Database { get; set; }
    string Identifier { get; set; }

    public EntryPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Identifier != null)
        {
            Id.Text = Identifier;
            FloatingActionButton.Text = "Edit";
        }
        else
        {
            FloatingActionButton.Text = "Add";
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Id"))
        {
            Identifier = query["Id"] as string;
        }
        Database = query["Database"] as KDBXDatabase;
    }
}

