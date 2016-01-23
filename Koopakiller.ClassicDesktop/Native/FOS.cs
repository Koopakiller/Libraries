using System;
// ReSharper disable InconsistentNaming

namespace Koopakiller.ClassicDesktop.Native
{
    [Flags]
    internal enum FOS
    {
        ALLNONSTORAGEITEMS = 0x80,
        ALLOWMULTISELECT = 0x200,
        CREATEPROMPT = 0x2000,
        DEFAULTNOMINIMODE = 0x20000000,
        DONTADDTORECENT = 0x2000000,
        FILEMUSTEXIST = 0x1000,
        FORCEFILESYSTEM = 0x40,
        FORCESHOWHIDDEN = 0x10000000,
        HIDEMRUPLACES = 0x20000,
        HIDEPINNEDPLACES = 0x40000,
        NOCHANGEDIR = 8,
        NODEREFERENCELINKS = 0x100000,
        NOREADONLYRETURN = 0x8000,
        NOTESTFILECREATE = 0x10000,
        NOVALIDATE = 0x100,
        OVERWRITEPROMPT = 2,
        PATHMUSTEXIST = 0x800,
        PICKFOLDERS = 0x20,
        SHAREAWARE = 0x4000,
        STRICTFILETYPES = 4
    }
}