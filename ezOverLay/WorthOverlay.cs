public class WorthOverlay
{
    private enum GetWindowType : uint
    {
        GW_HWNDFIRST = 0,
        GW_HWNDLAST = 1,
        GW_HWNDNEXT = 2,
        GW_HWNDPREV = 3,
        GW_OWNER = 4,
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6
    }
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindow(IntPtr hWnd, GetWindowType uCmd);
    private IntPtr hookWindhWnd = IntPtr.Zero;
    public bool Discover(IntPtr window)
    {
        if ((int)GetWindow(GetForegroundWindow(), GetWindowType.GW_CHILD) != 0)
                hookWindhWnd = GetForegroundWindow();
            if (GetForegroundWindow() != window)
                if ((int)GetWindow(GetForegroundWindow(), GetWindowType.GW_CHILD) != 0 || hookWindhWnd != window)
                    return false;
            return true;
    }
}
