using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ouranos.NextBootOSSelector.BootConfigurationData;
using Ouranos.NextBootOSSelector.Models;

namespace Ouranos.NextBootOSSelector.ViewModels
{

    public sealed class EntriesViewModel : ViewModelBase
    {

        #region イベント
        #endregion

        #region フィールド
        #endregion

        #region コンストラクタ

        public EntriesViewModel()
        {
            this.RebootCommand = new RelayCommand(this.Reboot, this.CanReboot);
            this.SaveCommand = new RelayCommand(this.Save, this.CanSave);
            this.MoveDownCommand = new RelayCommand(this.MoveDown, this.CanMoveDown);
            this.MoveUpCommand = new RelayCommand(this.MoveUp, this.CanMoveUp);

            BootConfigurationDataEditModel.Instance.ModelChanged += this.ModelChanged;
            BootConfigurationDataEditModel.Instance.Refresh();
        }

        #endregion

        #region プロパティ

        public bool CanChangeDefaultOperatingSystem
        {
            get
            {
                return BootConfigurationDataEditModel.Instance.CanChangeDefaultOperatingSystem;
            }
            set
            {
                this.RaisePropertyChanged();
            }
        }

        public bool IsDefaultOperatingSystem
        {
            get
            {
                return BootConfigurationDataEditModel.Instance.IsDefaultOperatingSystem;
            }
            set
            {
                BootConfigurationDataEditModel.Instance.IsDefaultOperatingSystem = value;

                this.RaisePropertyChanged();

                this.CanChangeDefaultOperatingSystem = !value;
            }
        }

        public bool IsNextOperatingSystem
        {
            get
            {
                return BootConfigurationDataEditModel.Instance.IsNextOperatingSystem;
            }
            set
            {
                BootConfigurationDataEditModel.Instance.IsNextOperatingSystem = value;

                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<OperatingSystemModel> OperatingSystems
        {
            get
            {
                return BootConfigurationDataEditModel.Instance.OperatingSystems;
            }
            set
            {
                BootConfigurationDataEditModel.Instance.OperatingSystems = value;

                this.RaisePropertyChanged();
            }
        }

        public ICommand MoveDownCommand
        {
            get;
            private set;
        }

        public ICommand MoveUpCommand
        {
            get;
            private set;
        }

        public ICommand RebootCommand
        {
            get;
            private set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public OperatingSystemModel SelectedOperatingSystem
        {
            get
            {
                return BootConfigurationDataEditModel.Instance.CurrentOperatingSystem;
            }
            set
            {
                BootConfigurationDataEditModel.Instance.CurrentOperatingSystem = value;

                this.RaisePropertyChanged();

                if (value != null)
                {
                    this.IsDefaultOperatingSystem = value.IsDefault;
                    this.IsNextOperatingSystem = value.IsNext;
                }

                var commands = new[]
                {
                    this.MoveDownCommand,
                    this.MoveUpCommand,
                    this.SaveCommand
                };

                foreach (var command in commands)
                {
                    this.RaiseCanExecuteChanged(command);
                }
            }
        }

        #endregion

        #region メソッド

        private bool CanMoveDown()
        {
            return BootConfigurationDataEditModel.Instance.CanMoveDown();
        }

        private bool CanMoveUp()
        {
            return BootConfigurationDataEditModel.Instance.CanMoveUp();
        }

        private bool CanReboot()
        {
            return BootConfigurationDataEditModel.Instance.CanReboot();
        }

        private bool CanSave()
        {
            return BootConfigurationDataEditModel.Instance.CanSave();
        }

        private void MoveDown()
        {
            if (BootConfigurationDataEditModel.Instance.MoveDown())
            {
                this.OperatingSystems = BootConfigurationDataEditModel.Instance.OperatingSystems;

                this.RaiseCanExecuteChanged(this.SaveCommand);
            }
        }

        private void MoveUp()
        {
            if (BootConfigurationDataEditModel.Instance.MoveUp())
            {
                this.OperatingSystems = BootConfigurationDataEditModel.Instance.OperatingSystems;

                this.RaiseCanExecuteChanged(this.SaveCommand);
            }
        }

        private void Reboot()
        {
            // ダイアログで確認

            // 次回起動する OS を指定
            if (BcdManager.UpdateNextBootOperatingSystem(this.SelectedOperatingSystem.Identifier))
            {
                BcdManager.Reboot();
            }
        }

        private void Refresh()
        {
            this.OperatingSystems = BootConfigurationDataEditModel.Instance.OperatingSystems;
            if (this.OperatingSystems.Any())
            {
                this.SelectedOperatingSystem = this.OperatingSystems[0];
            }
        }

        private void Save()
        {
            if (BootConfigurationDataEditModel.Instance.Save())
            {
                this.OperatingSystems = BootConfigurationDataEditModel.Instance.OperatingSystems;
                if (this.OperatingSystems.Any())
                {
                    this.SelectedOperatingSystem = this.OperatingSystems[0];
                }
            }
        }

        #region オーバーライド
        #endregion

        #region イベントハンドラ

        private void ModelChanged(object sender, EventArgs eventArgs)
        {
            this.Refresh();
        }

        #endregion

        #region ヘルパーメソッド

        private void RaiseCanExecuteChanged(ICommand command)
        {
            var relayCommand = command as RelayCommand;
            if (relayCommand != null)
            {
                relayCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #endregion

    }

}