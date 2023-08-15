namespace GateKeePass.Extensions
{
    public static class ShellExtensions
    {
        public static Task GoToPageAsync(this Shell shell, Type page)
        {
            return shell.GoToAsync("/" + page.Name);
        }

        public static Task GoToPageAsync(this Shell shell, Type page, Dictionary<string, object> parameters)
        {
            return shell.GoToAsync("/" + page.Name, parameters);
        }
    }
}
