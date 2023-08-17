using GateKeePass.Database;
using static System.Net.Mime.MediaTypeNames;

namespace GateKeePass;

public partial class GroupPage : ContentPage, IQueryAttributable
{
    KDBXDatabase Database { get; set; }
    string Identifier { get; set; }


    public GroupPage()
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

