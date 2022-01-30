using System;
using System.Runtime.InteropServices;
using System.Threading;
internal class ThroughWindow
{
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    private int index;
    private IntPtr handle;
    private Thread loop;
    public ThroughWindow(IntPtr handle) : this(handle, -20) { }
    public ThroughWindow(IntPtr handle, int index)
    {
        this.handle = handle;
        this.index = index;
        loop = new Thread(() => { while (true) SetWindowLong(handle, index, GetWindowLong(handle, index) | 0x8000 | 0x20);});
    }
    public void Enable() => loop.Start();
    public void Disable() => loop.Abort();
}
