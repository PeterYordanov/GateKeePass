using System.Collections.ObjectModel;
using System.Windows.Input;
using GateKeePass.Extensions;
using KeePassLib;

namespace GateKeePass;

public partial class MainPage : ContentPage, IQueryAttributable
{
    public ObservableCollection<string> Items { get; set; }
    private PwDatabase db;
    PwGroup currentGroup;
    public MainPage()
    {
        InitializeComponent();
        Items = new ObservableCollection<string>();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    public PwGroup FindGroupRecursive(PwGroup parentGroup, string groupName)
    {
        foreach (PwGroup group in parentGroup.Groups)
        {
            if (group.Name == groupName)
            {
                return group;
            }

            PwGroup foundGroup = FindGroupRecursive(group, groupName);
            if (foundGroup != null)
            {
                return foundGroup;
            }
        }

        return null;
    }

    private void RefreshUI()
    {
        Items.Clear();
        if (currentGroup != null)
        {
            foreach (var item in currentGroup.Groups)
            {
                Items.Add(item.Name);
            }

            foreach (var item in currentGroup.Entries)
            {
                Items.Add(item.Strings.ReadSafe(PwDefs.TitleField));
            }
        }
    }

    public void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        string tappedEntry = e.Item as string;

        Items.Clear();

        if (tappedEntry != null)
        {
            Console.WriteLine(tappedEntry);

            currentGroup = FindGroupRecursive(currentGroup, tappedEntry);

            if (currentGroup != null)
            {
                if (currentGroup.Groups != null)
                {
                    foreach (var item in currentGroup.Groups)
                    {
                        Items.Add(item.Name);
                    }
                }

                if (currentGroup.Entries != null)
                {
                    foreach (var item in currentGroup.Entries)
                    {
                        Items.Add(item.Name);
                    }
                }
            }
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        db = query["Database"] as PwDatabase;
        currentGroup = db.RootGroup;
        foreach (var item in currentGroup.Groups)
        {
            Items.Add(item.Name);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        if (currentGroup.ParentGroup != null)
        {
            currentGroup = currentGroup.ParentGroup;

            RefreshUI();
        }
        else
        {
            db.Close();
            db = null;
            Navigation.PopAsync();
        }
        return true;
    }
}

