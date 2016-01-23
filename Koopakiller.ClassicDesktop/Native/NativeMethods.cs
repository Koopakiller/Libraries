using System;
using System.Runtime.InteropServices;

namespace Koopakiller.ClassicDesktop.Native
{
    internal static class NativeMethods
    {
        [DllImport(NativeLibraryNames.Shell32)]
        internal static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

        [DllImport(NativeLibraryNames.Shell32)]
        internal static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

        [DllImport(NativeLibraryNames.User32)]
        internal static extern IntPtr GetActiveWindow();

    }
}
