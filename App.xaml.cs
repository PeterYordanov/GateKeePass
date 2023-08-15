﻿namespace GateKeePass;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(GateKeePass.MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AuthenticationPage), typeof(AuthenticationPage));

        MainPage = new AppShell();
	}
}
