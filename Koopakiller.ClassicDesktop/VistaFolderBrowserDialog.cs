using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Koopakiller.ClassicDesktop.Native;
using IWin32Window = System.Windows.Forms.IWin32Window;

namespace Koopakiller.ClassicDesktop
{
    /// <summary>
    /// Stellt einen Auswahldialog für Ordner und Systemelemente ab Windows Vista bereit.
    /// </summary>
    public sealed class VistaFolderBrowserDialog
    {
        #region Properties

        /// <summary>
        /// Ruft den ausgewählten Ordnerpfad ab bzw. legt diesen fest.
        /// </summary>
        public string SelectedPath { get; set; }
        /// <summary>
        /// Ruft den Anzeigenamen eines einzelnen, ausgewählten Elements ab.
        /// </summary>
        public string SelectedElementName { get; private set; }
        /// <summary>
        /// Ruft ein Array mit Ordnerpfaden der ausgewählten Ordner ab.
        /// </summary>
        public string[] SelectedPaths { get; private set; }
        /// <summary>
        /// Ruft ein Array mit den Namen der ausgewählten Elemente ab.
        /// </summary>
        public string[] SelectedElementNames { get; private set; }

        /// <summary>
        /// Ruft einen Wert ab der angibt ob auch Elemente ausgewählt werden können, die keine Ordner sind oder legt diesen fest.
        /// </summary>
        public bool AllowNonStoragePlaces { get; set; }
        /// <summary>
        /// Ruft einen Wert ab der angibt ob mehrere Elemente ausgewählt werden können oder legt diesen fest.
        /// </summary>
        public bool Multiselect { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Zeigt den Auswahldialog an.
        /// </summary>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog() => this.ShowDialog(IntPtr.Zero);

        /// <summary>
        /// Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="owner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog(Window owner) => this.ShowDialog(owner == null ? IntPtr.Zero : new WindowInteropHelper(owner).Handle);

        /// <summary>
        /// Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="owner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog(IWin32Window owner) => this.ShowDialog(owner?.Handle ?? IntPtr.Zero);

        /// <summary>
        /// Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="owner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog(IntPtr owner)
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                throw new InvalidOperationException("The dialog need at least Windows Vista to work.");
            }

            var dialog = VistaFolderBrowserDialog.CreateNativeDialog();
            try
            {
                this.SetInitialFolder(dialog);
                this.SetOptions(dialog);

                if (dialog.Show(owner) != 0)
                {
                    return false;
                }

                this.SetDialogResults(dialog);

                return true;
            }
            finally
            {
                Marshal.ReleaseComObject(dialog);
            }
        }

        #endregion

        #region Helper

        private static void GetPathAndElementName(IShellItem item, out string path, out string elementName)
        {
            item.GetDisplayName(SIGDN.PARENTRELATIVEFORADDRESSBAR, out elementName);
            try
            {
                item.GetDisplayName(SIGDN.FILESYSPATH, out path);
            }
            catch (ArgumentException ex) when (ex.HResult == -2147024809)
            {
                path = null;
            }
        }

        private static IFileOpenDialog CreateNativeDialog()
        {
            return (IFileOpenDialog)new FileOpenDialog();
        }

        private void SetInitialFolder(IFileOpenDialog dialog)
        {
            IShellItem item;
            if (string.IsNullOrEmpty(this.SelectedPath)) return;
            IntPtr idl;
            uint atts = 0;
            if (NativeMethods.SHILCreateFromPath(this.SelectedPath, out idl, ref atts) == 0
                && NativeMethods.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0)
            {
                dialog.SetFolder(item);
            }
        }

        private void SetOptions(IFileOpenDialog dialog)
        {
            dialog.SetOptions(this.GetDialogOptions());
        }

        private FOS GetDialogOptions()
        {
            var options = FOS.PICKFOLDERS;
            if (this.Multiselect)
            {
                options |= FOS.ALLOWMULTISELECT;
            }
            if (!this.AllowNonStoragePlaces)
            {
                options |= FOS.FORCEFILESYSTEM;
            }
            return options;
        }

        private void SetDialogResults(IFileOpenDialog dialog)
        {
            IShellItem item;
            try
            {
                dialog.GetResult(out item);
                string path, value;
                VistaFolderBrowserDialog.GetPathAndElementName(item, out path, out value);
                this.SelectedPath = path;
                this.SelectedPaths = new[] { path };
                this.SelectedElementName = value;
                this.SelectedElementNames = new[] { value };
            }
            catch (COMException ex) when (ex.HResult == -2147418113)
            {
                IShellItemArray items;
                dialog.GetResults(out items);

                uint count;
                items.GetCount(out count);

                this.SelectedPaths = new string[count];
                this.SelectedElementNames = new string[count];

                for (uint i = 0; i < count; ++i)
                {
                    items.GetItemAt(i, out item);
                    string path, value;
                    VistaFolderBrowserDialog.GetPathAndElementName(item, out path, out value);
                    this.SelectedPaths[i] = path;
                    this.SelectedElementNames[i] = value;
                }

                this.SelectedPath = null;
                this.SelectedElementName = null;
            }
        }

        #endregion
    }
}
