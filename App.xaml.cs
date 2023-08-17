namespace GateKeePass;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(GateKeePass.MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AuthenticationPage), typeof(AuthenticationPage));
        Routing.RegisterRoute(nameof(GroupPage), typeof(GroupPage));
        Routing.RegisterRoute(nameof(EntryPage), typeof(EntryPage));

        MainPage = new AppShell();
	}
}
