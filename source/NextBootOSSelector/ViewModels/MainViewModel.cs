using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using Ouranos.NextBootOSSelector.BootConfigurationData;
using Ouranos.NextBootOSSelector.Models;
using Ouranos.NextBootOSSelector.Views;
using Ouranos.NextBootOSSelector.Windows.Forms;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Ouranos.NextBootOSSelector.ViewModels
{

    public sealed class MainViewModel : ViewModelBase
    {

        #region イベント
        #endregion

        #region フィールド

        private readonly ProNotifyIcon _NotifyIcon;

        private ToolStripSeparator _BottomSeparator;

        private ToolStripMenuItem _CheckMenu;

        private ToolStripMenuItem _ExitMenu;

        private readonly List<ToolStripMenuItem> _BootMenus = new List<ToolStripMenuItem>();

        private ToolStripSeparator _TopSeparator;

        #endregion

        #region コンストラクタ

        public MainViewModel()
        {
            this.Title = Resource.AssemblyProperties.Title;
            this.ClosingCommand = new RelayCommand<CancelEventArgs>(this.Closing);

            var proNotifyIcon = new ProNotifyIcon();
            proNotifyIcon.Text = Resource.AssemblyProperties.Title;
            proNotifyIcon.Icon = Resource.Properties.Resources.Main;
            proNotifyIcon.Visible = true;
            proNotifyIcon.ContextMenuStrip = this.CreateContextMenuStrip();
            proNotifyIcon.MouseClick += this.ProNotifyIconOnMouseClick;

            this._NotifyIcon = proNotifyIcon;

            var paletteHelper = new PaletteHelper();
            paletteHelper.ReplacePrimaryColor(ConfigurationModel.Instance.Primary);
            paletteHelper.ReplaceAccentColor(ConfigurationModel.Instance.Accent);
            paletteHelper.SetLightDark(ConfigurationModel.Instance.Dark);

            this.UpdateVisibilityState(Visibility.Visible);
        }

        #endregion

        #region プロパティ

        public ICommand ClosingCommand
        {
            get;
            private set;
        }

        private bool _IsEnabled = true;

        public bool IsEnabled
        {
            get
            {
                return this._IsEnabled;
            }
            set
            {
                this._IsEnabled = value;
                this.RaisePropertyChanged();
            }
        }

        private Visibility _IsVisibility;

        public Visibility IsVisibility
        {
            get
            {
                return this._IsVisibility;
            }
            set
            {
                this._IsVisibility = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Title;

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region メソッド

        #region オーバーライド
        #endregion

        #region イベントハンドラ

        private void Close()
        {
            // 注意
            // Closing で無限ループに陥らないようにする
            this.UpdateVisibilityState(Visibility.Visible);
        }

        private async void Closing(CancelEventArgs args)
        {
            args.Cancel = true;

            if (BootConfigurationDataEditModel.Instance.CanSave())
            {
                var viewModel = new MessageDialogViewModel
                {
                    Message = "保存していない変更があります。保存しますか？",
                    Title = ""
                };

                var view = new YesNoCancelDialog()
                {
                    DataContext = viewModel
                };

                this.IsEnabled = false;
                await System.Threading.Tasks.Task.FromResult(DialogHost.Show(view, "RootDialog", this.DialogClosing));

                return;
            }

            this.Close();
        }

        private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
        {
            var bootMenus = BootConfigurationDataEditModel.Instance.OperatingSystems;
            var any = bootMenus.Count > 0;

            this._TopSeparator.Visible = any;
            this._BottomSeparator.Visible = any;

            // 入れ替えを実施
            var contextMenu = this._NotifyIcon.ContextMenuStrip;
            for (var index = contextMenu.Items.Count - 1; index >= 0; index--)
            {
                var item = contextMenu.Items[index];
                if (this._CheckMenu != item &&
                    this._TopSeparator != item &&
                    this._BottomSeparator != item &&
                    this._ExitMenu != item)
                {
                    contextMenu.Items.RemoveAt(index);
                    if (item is ToolStripMenuItem)
                    {
                        ((ToolStripMenuItem)item).CheckedChanged -= this.ItemOnCheckedChanged;
                    }
                    item.Dispose();
                    item = null;
                }
            }

            var botMenuItems = this._BootMenus;
            botMenuItems.Clear();

            // 最低 1 つはあるので、本来ここのチェックは不要
            if (!any)
            {
                return;
            }

            var insertIndex = contextMenu.Items.IndexOf(this._TopSeparator);
            foreach (var bootMenu in bootMenus)
            {
                var item = new ToolStripMenuItem();
                item.CheckOnClick = true;
                item.Checked = bootMenu.IsNext;
                item.Text = bootMenu.Description;
                item.Name = bootMenu.Description;
                item.CheckedChanged += this.ItemOnCheckedChanged;
                item.Tag = bootMenu;

                botMenuItems.Add(item);
            }

            for (int index = 0, count = botMenuItems.Count; index < count; index++)
            {
                contextMenu.Items.Insert(insertIndex + 1 + index, botMenuItems[index]);
            }
        }

        private void DialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            var close = false;
            var result = (MessageBoxResult)eventargs.Parameter;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (!BootConfigurationDataEditModel.Instance.Save())
                    {
                        //Debug.WriteLine();
                    }

                    close = true;
                    break;
                case MessageBoxResult.No:
                    BootConfigurationDataEditModel.Instance.Refresh();
                    close = true;
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

            this.IsEnabled = true;

            if (close)
            {
                this.Close();
            }
        }

        private void ImmediatelyRebootMenuOnClick(object sender, EventArgs e)
        {
            ConfigurationModel.Instance.ImmediatelyReboot = this._CheckMenu.Checked;
        }

        private void ItemOnCheckedChanged(object sender, EventArgs eventArgs)
        {
            var item = sender as ToolStripMenuItem;
            if (item == null)
            {
                return;
            }

            var operatingSystemModel = item.Tag as OperatingSystemModel;
            if (operatingSystemModel == null)
            {
                return;
            }

            var success = false;
            var botMenuItems = this._BootMenus;

            try
            {
                // 他のアイテムのチェック変更が発動しないようイベントを解除
                foreach (var menuItem in botMenuItems)
                {
                    menuItem.CheckedChanged -= this.ItemOnCheckedChanged;
                }

                if (item.Checked)
                {
                    // 他のチェックを全て外す
                    foreach (var menuItem in botMenuItems)
                    {
                        if (menuItem != item)
                        {
                            menuItem.Checked = false;
                        }
                    }
                }
                else
                {
                    // チェックの解除は認めない
                    item.Checked = true;
                }

                success = BcdManager.UpdateNextBootOperatingSystem(operatingSystemModel.Identifier);
                if (success)
                {
                    foreach (var menuItem in botMenuItems)
                    {
                        if (menuItem == null)
                        {
                            continue;
                        }

                        var model = menuItem.Tag as OperatingSystemModel;
                        if (model == null)
                        {
                            return;
                        }

                        model.IsNext = false;
                    }

                    operatingSystemModel.IsNext = true;
                }
            }
            finally
            {
                foreach (var menuItem in botMenuItems)
                {
                    menuItem.CheckedChanged += this.ItemOnCheckedChanged;
                }
            }

            if (success)
            {
                if (this._CheckMenu.Checked && item.Checked)
                {
                    BcdManager.Reboot();
                }
            }
        }

        private void ProNotifyIconOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.Button != MouseButtons.Left)
            {
                return;
            }

            this.UpdateVisibilityState(this._IsVisibility);
        }

        private void TerminateMenuOnClick(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region ヘルパーメソッド

        private ContextMenuStrip CreateContextMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Opening += this.ContextMenuStripOnOpening;

            var items = new[]
            {
                new
                {
                    Type = typeof(ToolStripMenuItem),  
                    Name = MenuNames.ImmediatelyReboot, 
                    Text = "即座に再起動します(&R)", 
                    Action = new EventHandler(this.ImmediatelyRebootMenuOnClick),                        
                    CheckOnClick = true,
                },
                new
                {
                    Type = typeof(ToolStripSeparator),
                    Name = MenuNames.TopSeparator,
                    Text = "",                     
                    Action = new EventHandler(this.TerminateMenuOnClick),
                    CheckOnClick = false
                },
                new
                {
                    Type = typeof(ToolStripSeparator),
                    Name = MenuNames.BottomSeparator,
                    Text = "",                     
                    Action = new EventHandler(this.TerminateMenuOnClick),
                    CheckOnClick = false
                },
                new
                {
                    Type = typeof(ToolStripMenuItem), 
                    Name = MenuNames.Exit,           
                    Text = "終了(&X)",           
                    Action = new EventHandler(this.TerminateMenuOnClick),
                    CheckOnClick = false
                }
            };

            var itemLists = new List<ToolStripItem>();
            foreach (var item in items)
            {
                if (item.Type == typeof(ToolStripMenuItem))
                {
                    var menuItem = new ToolStripMenuItem();
                    menuItem.CheckOnClick = item.CheckOnClick;
                    if (item.Action != null)
                    {
                        menuItem.Click += item.Action;
                    }
                    //menuItem.Image = Resources.Terminate;
                    menuItem.Name = item.Name.ToString();
                    menuItem.Text = item.Text;

                    itemLists.Add(menuItem);

                    switch (item.Name)
                    {
                        case MenuNames.ImmediatelyReboot:
                            this._CheckMenu = menuItem;
                            this._CheckMenu.Checked = ConfigurationModel.Instance.ImmediatelyReboot;
                            break;
                        case MenuNames.Exit:
                            this._ExitMenu = menuItem;
                            break;
                    }
                }
                else if (item.Type == typeof(ToolStripSeparator))
                {
                    var menuItem = new ToolStripSeparator();
                    menuItem.Name = item.Name.ToString();
                    menuItem.Text = item.Text;

                    itemLists.Add(menuItem);

                    switch (item.Name)
                    {
                        case MenuNames.BottomSeparator:
                            this._BottomSeparator = menuItem;
                            break;
                        case MenuNames.TopSeparator:
                            this._TopSeparator = menuItem;
                            break;
                    }
                }
            }

            contextMenuStrip.Items.AddRange(itemLists.ToArray());

            return contextMenuStrip;
        }

        private void UpdateVisibilityState(Visibility current)
        {
            switch (current)
            {
                case Visibility.Visible:
                    this.IsVisibility = Visibility.Hidden;
                    this._NotifyIcon.Visible = true;
                    break;
                case Visibility.Hidden:
                    this.IsVisibility = Visibility.Visible;
                    this._NotifyIcon.Visible = false;
                    break;
            }
        }

        #endregion

        #endregion

        private enum MenuNames
        {
            ImmediatelyReboot,

            Exit,

            BottomSeparator,

            TopSeparator
        }

    }
}
