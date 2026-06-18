using System.Diagnostics;

namespace DeskLink.Agent.Services;

public class SystemCommandService
{
    public bool LockWorkstation()
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "rundll32.exe",
                Arguments = "user32.dll,LockWorkStation",
                CreateNoWindow = true,
                UseShellExecute = false
            });

            return true;
        }
        catch
        {
            return false;
        }
    }
}