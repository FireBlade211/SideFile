using FireBlade.WinInteropUtils;
using FireBlade.WinInteropUtils.Dialogs;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace SideFile.Classic
{
    public partial class FileListView : ListView
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= LVS_NOCOLUMNHEADER;

                return cp;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageList? SmallImageList
        {
            get => base.SmallImageList;
            private set => base.SmallImageList = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ListViewItemCollection Items => base.Items;

        public string CurrentDir = "";
        public string DisplayDir => string.IsNullOrEmpty(CurrentDir) ? "This PC" : Path.GetFullPath(CurrentDir);

        private const int LVS_NOCOLUMNHEADER = 0x4000;

        [Category("Navigation")]
        public event EventHandler<string>? Navigated;
        [Category("Navigation")]
        public event EventHandler<string>? Navigating;

        private nint _imgListHandle;

        public FileListView() : base()
        {
            InitializeComponent();

            View = View.Details;
            Clear();
            Columns.Add(new ColumnHeader()
            {
                Text = "Name",
                Width = 250
            });

            DoubleBuffered = true;

            //SmallImageList = new ImageList();
            //SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

            //SmallImageList?.Images.Add(StockIconHelper.GetIcon(StockIcon.DesktopPC));
            
            CurrentDir = string.Empty;
        }

        private void Navigate()
        {
            Items.Clear();
            BeginUpdate();
            Navigating?.Invoke(this, DisplayDir);

            try
            {
                var backDir = Path.GetDirectoryName(CurrentDir);

                if (CurrentDir != string.Empty)
                {
                    if (backDir != null)
                    {
                        var info = new DirectoryInfo(backDir);
                        InsertFsItem(info, "..");
                    }
                    else
                    {
                        var item = new ListViewItem("..", 0);

                        unsafe
                        {
                            // WinInteropUtils does not provide the icon index retrieval
                            SHSTOCKICONINFO sii = new();
                            sii.cbSize = (uint)Marshal.SizeOf<SHSTOCKICONINFO>();

                            if (PInvoke.SHGetStockIconInfo((SHSTOCKICONID)StockIcon.DesktopPC, SHGSI_FLAGS.SHGSI_SYSICONINDEX | SHGSI_FLAGS.SHGSI_SMALLICON,
                                &sii).Succeeded)
                            {
                                item.ImageIndex = sii.iSysImageIndex;
                            }
                        }

                        item.Tag = new ListItemInfo
                        {
                            Path = string.Empty
                        };

                        Items.Add(item);
                    }
                }

                if (string.IsNullOrEmpty(CurrentDir))
                {
                    Task.Run(() =>
                    {
                        foreach (var drive in DriveInfo.GetDrives())
                        {
                            try
                            {
                                var item = new ListViewItem($"{drive.VolumeLabel} ({drive.Name.TrimEnd('\\')})");
                                Shell32.SHFILEINFO shfi = new();

                                int iconIndex = 0;

                                if (Shell32.GetFileInfoEx(
                                    drive.Name,
                                    0,
                                    Shell32.SHGetFileInfoFlags.SHGFI_SYSICONINDEX |
                                    Shell32.SHGetFileInfoFlags.SHGFI_SMALLICON,
                                    ref shfi) != 0)
                                {
                                    using var file = new WindowsFile(shfi, drive.Name);
                                    iconIndex = file.IconIndex;
                                }

                                item.Tag = new ListItemInfo { Path = drive.Name };

                                BeginInvoke(() =>
                                {
                                    try
                                    {
                                        item.ImageIndex = iconIndex;
                                        Items.Add(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        DebugPanel.OnExceptionReceived(ex);
                                    }
                                });
                            }
                            catch (Exception ex)
                            {
                                DebugPanel.OnExceptionReceived(ex);
                            }
                        }
                    });
                }
                else
                {
                    Task.Run(() =>
                    {
                        foreach (var info in new DirectoryInfo(CurrentDir).GetFileSystemInfos().OrderBy(x => x is DirectoryInfo ? 0 : 1))
                            Invoke(() => InsertFsItem(info));
                    });
                }
            }
            catch (Exception ex) { DebugPanel.OnExceptionReceived(ex); }

            EndUpdate();
            Navigated?.Invoke(this, DisplayDir);
        }

        private void InsertFsItem(FileSystemInfo info, string? text = null)
        {
            var item = new ListViewItem(text ?? info.Name);

            Shell32.SHFILEINFO shfi = new();

            if (Shell32.GetFileInfoEx(info.FullName, 0, Shell32.SHGetFileInfoFlags.SHGFI_SYSICONINDEX | Shell32.SHGetFileInfoFlags.SHGFI_SMALLICON
                | Shell32.SHGetFileInfoFlags.SHGFI_DISPLAYNAME, ref shfi) != 0)
            {
                using (var file = new WindowsFile(shfi, info.FullName))
                {
                    item.ImageIndex = file.IconIndex;
                }

                item.Tag = new ListItemInfo
                {
                    Path = info.FullName
                };

                Items.Add(item);
            }
        }

        /// <inheritdoc/>
        protected override void OnItemActivate(EventArgs e)
        {
            base.OnItemActivate(e);

            if (SelectedItems.Count == 1)
            {
                var item = SelectedItems[0];

                if (item.Tag is ListItemInfo info)
                {
                    var oldDir = CurrentDir;

                    try
                    {
                        CurrentDir = info?.Path ?? string.Empty;
                        Navigate();
                    }
                    catch (Exception ex)
                    {
                        DebugPanel.OnExceptionReceived(ex);

                        WinMessageBox mb = new WinMessageBox();
                        mb.Culture = CultureInfo.CurrentUICulture;
                        mb.Text = $"Error accessing path {Path.GetFullPath(CurrentDir)}: {ex.Message}";
                        mb.Icon = WinMessageBoxIcon.Error;
                        mb.Caption = null;

                        if (Window.FromHandle(Form1.Current.Handle) is Window wnd)
                            mb.Show(wnd);

                        CurrentDir = oldDir;
                        Navigate();
                    }
                }
                else
                {
                    WinMessageBox mb = new WinMessageBox();
                    mb.Culture = CultureInfo.CurrentUICulture;
                    mb.Text = $"Error accessing path {Path.GetFullPath(CurrentDir)}: Cannot access directory.";
                    mb.Icon = WinMessageBoxIcon.Error;
                    mb.Caption = null;

                    if (Window.FromHandle(Form1.Current.Handle) is Window wnd)
                        mb.Show(wnd);
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            Shell32.SHFILEINFO shfi = new();

            _imgListHandle = (nint)Shell32.GetFileInfoEx(string.Empty, 0,
                Shell32.SHGetFileInfoFlags.SHGFI_SYSICONINDEX | Shell32.SHGetFileInfoFlags.SHGFI_SMALLICON, ref shfi);

            PInvoke.SendMessage(new HWND(Handle), PInvoke.LVM_SETIMAGELIST, PInvoke.LVSIL_SMALL, new LPARAM(_imgListHandle));

            try
            {
                Navigate();
            }
            catch (Exception ex) { DebugPanel.OnExceptionReceived(ex); }
        }
        
        public class ListItemInfo
        {
            public string Path = string.Empty;
        }
    }
}
