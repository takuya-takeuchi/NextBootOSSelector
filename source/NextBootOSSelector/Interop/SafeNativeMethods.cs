using System;
using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.Interop
{
    internal sealed class SafeNativeMethods
    {

        /// <summary>
        /// Closes the window.
        /// </summary>
        public const int SC_CLOSE = 0xF060;

        public const int WM_SYSCOMMAND = 0x0112;

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetDesktopWindow();

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetForegroundWindow();

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

    }
}
