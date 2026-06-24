using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DeskLink.Agent.Services;

public class SystemCommandService
{
    [DllImport("PowrProf.dll", SetLastError = true)]
    private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

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

    public bool Sleep()
    {
        try
        {
            return SetSuspendState(false, false, false);
        }
        catch
        {
            return false;
        }
    }
}