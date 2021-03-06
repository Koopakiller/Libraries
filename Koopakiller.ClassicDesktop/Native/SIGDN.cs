// ReSharper disable InconsistentNaming

namespace Koopakiller.ClassicDesktop.Native
{
    internal enum SIGDN : uint
    {
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        DESKTOPABSOLUTEPARSING = 0x80028000,
        FILESYSPATH = 0x80058000,
        NORMALDISPLAY = 0,
        PARENTRELATIVE = 0x80080001,
        PARENTRELATIVEEDITING = 0x80031001,
        PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
        PARENTRELATIVEPARSING = 0x80018001,
        URL = 0x80068000
    }
}