using System;
using System.Collections.ObjectModel;
using System.Linq;
using Ouranos.NextBootOSSelector.BootConfigurationData;

namespace Ouranos.NextBootOSSelector.Models
{

    internal sealed class BootConfigurationDataEditModel
    {

        #region イベント

        public event EventHandler ModelChanged;

        #endregion

        #region フィールド
        #endregion

        #region コンストラクタ

        private BootConfigurationDataEditModel()
        {
        }

        #endregion

        #region プロパティ

        public bool CanChangeDefaultOperatingSystem
        {
            get
            {
                return !this.IsDefaultOperatingSystem;
            }
        }

        public OperatingSystemModel CurrentOperatingSystem
        {
            get;
            set;
        }

        private static BootConfigurationDataEditModel _Instance;

        public static BootConfigurationDataEditModel Instance
        {
            get
            {
                return _Instance ?? (_Instance = new BootConfigurationDataEditModel());
            }
        }

        public bool IsDefaultOperatingSystem
        {
            get
            {
                return this.CurrentOperatingSystem.IsDefault;
            }
            set
            {
                var target = this.CurrentOperatingSystem;
                target.IsDefault = value;

                if (value)
                {
                    foreach (var operatingSystem in this.OperatingSystems)
                    {
                        if (operatingSystem != target)
                        {
                            operatingSystem.IsDefault = false;
                        }
                    }
                }
            }
        }

        public bool IsNextOperatingSystem
        {
            get
            {
                return this.CurrentOperatingSystem.IsNext;
            }
            set
            {
                var target = this.CurrentOperatingSystem;
                target.IsNext = value;

                if (value)
                {
                    foreach (var operatingSystem in this.OperatingSystems)
                    {
                        if (operatingSystem != target)
                        {
                            operatingSystem.IsNext = false;
                        }
                    }
                }
            }
        }

        public ObservableCollection<OperatingSystemModel> OperatingSystems
        {
            get;
            set;
        }

        #endregion

        #region メソッド

        public bool CanMoveDown()
        {
            var selectedOperatingSystem = this.CurrentOperatingSystem;
            if (selectedOperatingSystem == null)
            {
                return false;
            }

            if (selectedOperatingSystem.HasErrors)
            {
                return false;
            }

            var operatingSystems = this.OperatingSystems;
            if (operatingSystems == null)
            {
                return false;
            }

            return operatingSystems.IndexOf(selectedOperatingSystem) != operatingSystems.Count - 1;
        }

        public bool CanMoveUp()
        {
            var selectedOperatingSystem = this.CurrentOperatingSystem;
            if (selectedOperatingSystem == null)
            {
                return false;
            }

            if (selectedOperatingSystem.HasErrors)
            {
                return false;
            }

            var operatingSystems = this.OperatingSystems;
            if (operatingSystems == null)
            {
                return false;
            }

            return operatingSystems.IndexOf(selectedOperatingSystem) != 0;
        }

        public bool CanReboot()
        {
            var selectedOperatingSystem = this.CurrentOperatingSystem;
            if (selectedOperatingSystem == null)
            {
                return false;
            }

            if (selectedOperatingSystem.HasErrors)
            {
                return false;
            }

            return true;
        }

        public bool CanSave()
        {
            var selectedOperatingSystem = this.CurrentOperatingSystem;
            if (selectedOperatingSystem == null)
            {
                return false;
            }

            if (selectedOperatingSystem.HasErrors)
            {
                return false;
            }

            for (int index = 0, count = this.OperatingSystems.Count; index < count; index++)
            {
                var operatingSystem = this.OperatingSystems[index];
                if (!operatingSystem.IsModified)
                {
                    return true;
                }
            }

            return false;
        }

        public bool MoveDown()
        {
            var operatingSystems = this.OperatingSystems.ToList();
            var operatingSystem = this.CurrentOperatingSystem;

            var indexOf = operatingSystems.IndexOf(operatingSystem);
            if (indexOf != -1)
            {
                var newIndex = indexOf + 1;
                var temp = operatingSystems[indexOf];
                operatingSystems[indexOf] = operatingSystems[newIndex];
                operatingSystems[newIndex] = temp;

                var order = operatingSystems[indexOf].Order;
                operatingSystems[indexOf].Order = operatingSystems[newIndex].Order;
                operatingSystems[newIndex].Order = order;

                this.OperatingSystems = new ObservableCollection<OperatingSystemModel>(operatingSystems);
            }

            return indexOf != -1;
        }

        public bool MoveUp()
        {
            var operatingSystems = this.OperatingSystems.ToList();
            var operatingSystem = this.CurrentOperatingSystem;

            var indexOf = operatingSystems.IndexOf(operatingSystem);
            if (indexOf != -1)
            {
                var newIndex = indexOf - 1;
                var temp = operatingSystems[indexOf];
                operatingSystems[indexOf] = operatingSystems[newIndex];
                operatingSystems[newIndex] = temp;

                var order = operatingSystems[indexOf].Order;
                operatingSystems[indexOf].Order = operatingSystems[newIndex].Order;
                operatingSystems[newIndex].Order = order;

                this.OperatingSystems = new ObservableCollection<OperatingSystemModel>(operatingSystems);
            }

            return indexOf != -1;
        }

        public void Refresh()
        {
            var operatingSystems = BcdManager.GetOperatingSystems().Select(system => new OperatingSystemModel(system)).ToArray();
            this.OperatingSystems = new ObservableCollection<OperatingSystemModel>(operatingSystems);

            if (this.OperatingSystems.Any())
            {
                this.CurrentOperatingSystem = this.OperatingSystems[0];
            }

            this.OnModelChanged(EventArgs.Empty);
        }

        public bool Save()
        {
            var updated = false;

            var operatingSystems = this.OperatingSystems;

            // 個々の編集結果をセーブ
            foreach (var operatingSystem in operatingSystems)
            {
                updated |= operatingSystem.Update();
            }

            // オーダーをセーブ
            updated |= BcdManager.UpdateDisplayOrder(operatingSystems.Select(model => model.Identifier));

            // 既定の OS をセーブ
            var defaultOperatingSystem = operatingSystems.FirstOrDefault(model => model.IsDefault);
            if (defaultOperatingSystem != null)
            {
                updated |= BcdManager.UpdateDefaultOperatingSystem(defaultOperatingSystem.Identifier);
            }

            // 次回起動の OS をセーブ
            var nextOperatingSystem = operatingSystems.FirstOrDefault(model => model.IsNext);
            if (nextOperatingSystem != null)
            {
                updated |= BcdManager.UpdateNextBootOperatingSystem(nextOperatingSystem.Identifier);
            }
            else
            {
                // Windows ブートマネージャーに戻す
                updated |= BcdManager.UpdateNextBootOperatingSystem();
            }

            // 表示を更新
            if (updated)
            {
                this.Refresh();
            }

            return updated;
        }

        #region オーバーライド
        #endregion

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド

        private void OnModelChanged(EventArgs e)
        {
            var handler = this.ModelChanged;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        #endregion

        #endregion

    }

}
