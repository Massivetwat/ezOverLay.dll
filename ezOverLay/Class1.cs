using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace ezOverLay
{
    /* This is my first ever dll, I made all of this in about 5 hours. There will be things that might not work.
     * 
     * Subscribe to swedish twat 
     * 
     * TO use ezOverLay.dll compile this poject and there will be a dll located in the bin folder. 
     * Reference it in your own project and add these things.
     * 
     * At the very start add this 
     * 
     * using ezOverLay;
     * 
     * 
     * Under the class 
     * 
     * ez ez = new ez();
     * 
     * 
     * Now add this in your load method
     * 
     * CheckForIllegalThreadCalls = false;
     * ez.SetInvi(this);
     * ez.StartLoop(10, "windowname", this);
     * 
     * If you don't understand this then watch my next video that explains how to use this dll ;)
     */

    public class ez
    {
        public static IntPtr hand;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        public static extern bool GetWindowRect(IntPtr hwnd, out RECT IpRect);

        public static RECT rect;

        public bool threadwire = true;
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        public void setHandle(string window_name)
        {
            hand = FindWindow(null, window_name);
        
        }

       public void SetInvi(Form form)
        {
            form.BackColor = Color.Wheat;
            form.TransparencyKey = Color.Wheat;
            form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.None;
            ClickThrough(form.Handle);
        }

        public void GetRekt()
        {
            GetWindowRect(hand, out rect);
        }

        public void ClickThrough(IntPtr formHandle)
        {
            int initialStyle = GetWindowLong(formHandle, -20);
            SetWindowLong(formHandle, -20, initialStyle | 0x8000 | 0x20);

        }

        public Size CalcSize()
        {
            Size size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            return size;
        }

        public void DoStuff(string WindowName,Form form)
        {
            setHandle(WindowName);
            GetRekt();
            form.Size = CalcSize();
            form.Left = rect.left;
            form.Top = rect.top;

        }

        public void PauseLoop()
        {
            threadwire = false;
        }

        public void UnPauseLoop()
        {
            threadwire = true;
        }
        public void StartLoop(int frequency, string WindowName, Form form)
        {
            Thread lp = new Thread(() => LOOP(frequency, WindowName, form)) { IsBackground = true };
            lp.Start();
           
        }
        
        
        

        public void LOOP(int frequency,string WindowName, Form form)
        {
            while (true)
            {
                if (threadwire == true)
                {
                    DoStuff(WindowName, form);
                    
                }
                Thread.Sleep(frequency);
                
            }
           
        }
    }
}
