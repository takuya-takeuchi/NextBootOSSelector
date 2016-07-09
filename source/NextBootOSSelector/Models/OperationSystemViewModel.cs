using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using OperatingSystem = Ouranos.NextBootOSSelector.BootConfigurationData.OperatingSystem;

namespace Ouranos.NextBootOSSelector.Models
{

    public sealed class OperatingSystemModel : ViewModelBase, IEquatable<OperatingSystemModel>, ICloneable, INotifyDataErrorInfo
    {

        #region イベント
        #endregion

        #region フィールド

        private readonly OperatingSystem _OperatingSystem;

        private readonly OperatingSystem _OriginalOperatingSystem;

        #endregion

        #region コンストラクタ

        private OperatingSystemModel(OperatingSystemModel operatingSystemModel)
        {
            if (operatingSystemModel == null)
            {
                throw new ArgumentNullException("operatingSystemModel");
            }

            this._OperatingSystem = (OperatingSystem)operatingSystemModel._OperatingSystem.Clone();
        }

        public OperatingSystemModel(OperatingSystem operatingSystem)
        {
            if (operatingSystem == null)
            {
                throw new ArgumentNullException("operatingSystem");
            }

            this._OperatingSystem = operatingSystem;
            this._OriginalOperatingSystem = (OperatingSystem)operatingSystem.Clone();
        }

        #endregion

        #region プロパティ

        public string ApplicationDevice
        {
            get
            {
                return this._OperatingSystem.ApplicationDevice;
            }
            set
            {
                this._OperatingSystem.ApplicationDevice = value;
            }
        }

        public string ApplicationPath
        {
            get
            {
                return this._OperatingSystem.ApplicationPath;
            }
            set
            {
                this._OperatingSystem.ApplicationPath = value;
            }
        }

        public string Description
        {
            get
            {
                return this._OperatingSystem.Description;
            }
            set
            {
                this.ValidateProperty("Description", value);
                this._OperatingSystem.Description = value;
                this.RaisePropertyChanged();
            }
        }

        public string Identifier
        {
            get
            {
                return this._OperatingSystem.Identifier;
            }
            set
            {
                this._OperatingSystem.Identifier = value;
            }
        }

        public bool IsCurrent
        {
            get
            {
                return this._OperatingSystem.IsCurrent;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this._OperatingSystem.IsDefault;
            }
            set
            {
                this._OperatingSystem.IsDefault = value;
            }
        }

        public bool IsModified
        {
            get
            {
                return this._OriginalOperatingSystem.Equals(this._OperatingSystem);
            }
        }

        public bool IsNext
        {
            get
            {
                return this._OperatingSystem.IsNext;
            }
            set
            {
                this._OperatingSystem.IsNext = value;
            }
        }

        public int Order
        {
            get
            {
                return this._OperatingSystem.Order;
            }
            set
            {
                this._OperatingSystem.Order = value;
                this.RaisePropertyChanged();
            }
        }

        public string Locale
        {
            get
            {
                return this._OperatingSystem.Locale;
            }
            set
            {
                this._OperatingSystem.Locale = value;
            }
        }

        public string SystemRoot
        {
            get
            {
                return this._OperatingSystem.SystemRoot;
            }
            set
            {
                this._OperatingSystem.SystemRoot = value;
            }
        }

        #endregion

        #region メソッド

        public bool Update()
        {
            return this._OperatingSystem.Update(this._OriginalOperatingSystem);
        }

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド

        #endregion

        #endregion

        #region IEquatable<OperatingSystemModel> メンバー

        public bool Equals(OperatingSystemModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(this._OperatingSystem, other._OperatingSystem);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is OperatingSystemModel && this.Equals((OperatingSystemModel)obj);
        }

        //public override int GetHashCode()
        //{
        //    return (this._OperatingSystem != null ? this._OperatingSystem.GetHashCode() : 0);
        //}

        #endregion

        #region ICloneable メンバー

        public object Clone()
        {
            return new OperatingSystemModel(this);
        }

        #endregion

        #region INotifyDataErrorInfo メンバー


        readonly Dictionary<string, List<string>> _CurrentErrors = new Dictionary<string, List<string>>();

        private void ValidateProperty(string propertyName, object value)
        {
            switch (propertyName)
            {
                //case "ApplicationDevice":
                //case "ApplicationPath":
                case "Description":
                    //case "Identifier":
                    //case "IsCurrent":
                    //case "IsDefault":
                    //case "Locale":
                    //case "SystemRoot":
                    if (string.IsNullOrWhiteSpace((string)value))
                        this.AddError(propertyName, "Description を空にすることはできません。");
                    else
                        this.RemoveError(propertyName, null);
                    break;
                default:
                    break;
            }
        }

        private void AddError(string propertyName, string error)
        {
            var currentErrors = this._CurrentErrors;
            if (!currentErrors.ContainsKey(propertyName))
            {
                currentErrors[propertyName] = new List<string>();
            }

            if (!currentErrors[propertyName].Contains(error))
            {
                currentErrors[propertyName].Add(error);
                this.OnErrorsChanged(propertyName);
            }

            this.OnErrorsChanged(propertyName);
        }

        private void RemoveError(string propertyName, string error)
        {
            if (this._CurrentErrors.ContainsKey(propertyName))
            {
                this._CurrentErrors.Remove(propertyName);
            }

            this.OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            var @event = this.ErrorsChanged;
            if (@event != null)
            {
                @event.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
                !this._CurrentErrors.ContainsKey(propertyName))
            {
                return null;
            }

            return this._CurrentErrors[propertyName];
        }

        public bool HasErrors
        {
            get
            {
                return this._CurrentErrors.Count > 0;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion


    }

}
