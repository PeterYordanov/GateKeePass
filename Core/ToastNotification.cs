using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

public class ToastNotification
{
    public static async Task ShowAsync(string message)
    {
        ToastDuration toastDuration = ToastDuration.Long;
        IToast toast = Toast.Make(message, toastDuration);
        await toast.Show();
    }
}
