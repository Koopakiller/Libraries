using System;
using System.Runtime.InteropServices;

namespace Koopakiller.ClassicDesktop.Native
{
    [ComImport, Guid(NativeClassIds.IShellItemArray), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItemArray
    {
        void BindToHandler([In, MarshalAs(UnmanagedType.Interface)] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);
        void GetPropertyStore([In] int flags, [In] ref Guid riid, out IntPtr ppv);
        void GetPropertyDescriptionList([In, MarshalAs(UnmanagedType.Struct)] ref IntPtr keyType, [In] ref Guid riid, out IntPtr ppv);
        void GetAttributes([In, MarshalAs(UnmanagedType.I4)] IntPtr dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);
        void GetCount(out uint pdwNumItems);
        void GetItemAt([In] uint dwIndex, [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);
        void EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
    }
}