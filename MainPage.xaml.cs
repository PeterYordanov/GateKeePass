using System.Collections.ObjectModel;
using System.Windows.Input;
using GateKeePass.Database;
using GateKeePass.Extensions;
using KeePassLib;

namespace GateKeePass;

public partial class MainPage : ContentPage, IQueryAttributable
{
    public ObservableCollection<KeePassEntry> Items { get; set; }
    private KDBXDatabase db;
    PwGroup currentGroup;

    public ICommand AddCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    private async void DeleteItem(KeePassEntry item)
    {
        await ToastNotification.ShowAsync(item.Name);
    }

    private async void EditItem(KeePassEntry item)
    {
        await ToastNotification.ShowAsync(item.Name);

        Type type = null;

        if(item.Type == EntryType.Password)
        {
            type = typeof(EntryPage);
        }
        else if(item.Type == EntryType.Group)
        {
            type = typeof(GroupPage);
        }

        await Shell.Current.GoToPageAsync(type, new Dictionary<string, object>
        {
            { "Id", item.Id },
            { "Database", db }
        });
    }

    private async void AddItem(KeePassEntry item)
    {
        string action = await DisplayActionSheet("Choose an option:", "Cancel", null, EntryType.Password.ToString(), EntryType.Group.ToString());
        switch (action)
        {
            case "Password":
                await ToastNotification.ShowAsync("Password");
                await Shell.Current.GoToPageAsync(typeof(EntryPage), new Dictionary<string, object>
                {
                    { "Database", db }
                });
                break;
            case "Group":
                await ToastNotification.ShowAsync("Group");
                await Shell.Current.GoToPageAsync(typeof(GroupPage), new Dictionary<string, object>
                {
                    { "Database", db }
                });
                break;
        }
        await ToastNotification.ShowAsync("Add");
    }

    public MainPage()
    {
        InitializeComponent();

        Items = new ObservableCollection<KeePassEntry>();

        AddCommand = new Command<KeePassEntry>(item => AddItem(item));
        EditCommand = new Command<KeePassEntry>(item => EditItem(item));
        DeleteCommand = new Command<KeePassEntry>(item => DeleteItem(item));

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    public PwGroup FindGroupRecursive(PwGroup parentGroup, string id)
    {
        foreach (PwGroup group in parentGroup.Groups)
        {
            if (group.Uuid.ToString() == id)
            {
                return group;
            }

            PwGroup foundGroup = FindGroupRecursive(group, id);
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
                Items.Add(new KeePassEntry 
                {
                    Name = item.Name,
                    Id = item.Id,
                    Uuid = item.Uuid.ToString(),
                    Type = EntryType.Group
                });
            }

            foreach (var item in currentGroup.Entries)
            {
                Items.Add(new KeePassEntry
                {
                    Name = item.Name,
                    Id = item.Id,
                    Uuid = item.Uuid.ToString(),
                    Type = EntryType.Password
                });
            }
        }
    }

    public void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        Items.Clear();

        if (e.Item is KeePassEntry tappedEntry)
        {
            Console.WriteLine(tappedEntry);

            currentGroup = FindGroupRecursive(currentGroup, tappedEntry.Uuid);

            if (currentGroup != null)
            {
                if (currentGroup.Groups != null)
                {
                    foreach (var item in currentGroup.Groups)
                    {
                        Items.Add(new KeePassEntry
                        {
                            Name = item.Name,
                            Id = item.Id,
                            Uuid = item.Uuid.ToString()
                        });
                    }
                }

                if (currentGroup.Entries != null)
                {
                    foreach (var item in currentGroup.Entries)
                    {
                        Items.Add(new KeePassEntry
                        {
                            Name = item.Name,
                            Id = item.Id,
                            Uuid = item.Uuid.ToString()
                        });
                    }
                }
            }
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        db = query["Database"] as KDBXDatabase;
        currentGroup = db.GetRootGroup();
        RefreshUI();
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

