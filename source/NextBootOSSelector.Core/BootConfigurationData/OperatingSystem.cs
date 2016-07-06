using System;
using System.Management;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{

    public sealed class OperatingSystem : IEquatable<OperatingSystem>, ICloneable
    {

        #region イベント
        #endregion

        #region フィールド
        #endregion

        #region コンストラクタ

        private OperatingSystem(OperatingSystem operatingSystem)
        {
            if (operatingSystem == null)
            {
                throw new ArgumentNullException("operatingSystem");
            }

            this._ApplicationDevice = operatingSystem._ApplicationDevice;
            this._ApplicationPath = operatingSystem._ApplicationPath;
            this._Description = operatingSystem._Description;
            this._Identifier = operatingSystem._Identifier;
            this._IsCurrent = operatingSystem._IsCurrent;
            this._IsDefault = operatingSystem._IsDefault;
            this._IsNext = operatingSystem._IsNext;
            this._Order = operatingSystem._Order;
            this._Locale = operatingSystem._Locale;
            this._SystemRoot = operatingSystem._SystemRoot;

            this.Object = operatingSystem.Object;
        }

        internal OperatingSystem(BcdObject bcdObject, string identifier)
        {
            if (bcdObject == null)
            {
                throw new ArgumentNullException("bcdObject");
            }

            this.Object = bcdObject;
            this._Identifier = identifier;

            var bcdLibraryElementTypes = new[]
            {
                BcdLibraryElementTypes.BcdLibraryString_Description,
                BcdLibraryElementTypes.BcdLibraryString_ApplicationPath,
                BcdLibraryElementTypes.BcdLibraryString_PreferredLocale,
                BcdLibraryElementTypes.BcdLibraryDevice_ApplicationDevice
            };

            foreach (var type in bcdLibraryElementTypes)
            {
                ManagementBaseObject mboOut;
                var success = bcdObject.GetElement((uint)type, out mboOut);
                if (!success)
                {
                    throw new Exception();
                }

                switch (type)
                {
                    case BcdLibraryElementTypes.BcdLibraryDevice_ApplicationDevice:
                        //var deviceElement = new BcdDeviceElement(mboOut);
                        //var storeFilePath = deviceElement.StoreFilePath;
                        //var managementPath = deviceElement.Path;
                        //var u = deviceElement.Type;

                        //var deviceData = new BcdDeviceData(deviceElement.Device);

                        ////object parent;
                        ////if (!this.TryGetProperty(device as ManagementBaseObject, "Parent", out parent))
                        ////{
                        ////    break;
                        ////}

                        ////object path;
                        ////if (!this.TryGetProperty(parent as ManagementBaseObject, "Path", out path))
                        ////{
                        ////    break;
                        ////}

                        ////this._ApplicationDevice = path.ToString();
                        //var additionalOptions = deviceData.AdditionalOptions;
                        //var deviceTypeValues = deviceData.DeviceType;
                        //var lateBoundObject = deviceData.LateBoundObject;
                        //var deviceLocateElementChildData = new BcdDeviceLocateElementChildData(lateBoundObject);
                        //var element = deviceLocateElementChildData.Element;
                        //var deviceData2 = new BcdDeviceData(deviceLocateElementChildData.Parent);

                        //var path = deviceData.Path;
                        break;
                    case BcdLibraryElementTypes.BcdLibraryString_ApplicationPath:
                        this._ApplicationPath = mboOut.GetPropertyValue("String").ToString();
                        break;
                    case BcdLibraryElementTypes.BcdLibraryString_Description:
                        this._Description = mboOut.GetPropertyValue("String").ToString();
                        break;
                    case BcdLibraryElementTypes.BcdLibraryString_PreferredLocale:
                        this._Locale = mboOut.GetPropertyValue("String").ToString();
                        break;
                }
            }

            var bcdOSLoaderElementTypes = new[]
            {
                BcdOSLoaderElementTypes.BcdOSLoaderDevice_OSDevice,
                BcdOSLoaderElementTypes.BcdOSLoaderString_SystemRoot
            };

            foreach (var type in bcdOSLoaderElementTypes)
            {
                ManagementBaseObject mboOut;
                var success = bcdObject.GetElement((uint)type, out mboOut);
                if (!success)
                {
                    throw new Exception();
                }

                switch (type)
                {
                    case BcdOSLoaderElementTypes.BcdOSLoaderString_SystemRoot:
                        this._SystemRoot = mboOut.GetPropertyValue("String").ToString();
                        break;
                        //case BcdOSLoaderElementTypes.BcdOSLoaderDevice_OSDevice:
                        //    object device;
                        //    if (!this.TryGetProperty(mboOut, "Device", out device))
                        //    {
                        //        break;
                        //    }

                        //    object parent;
                        //    if (!this.TryGetProperty(device as ManagementBaseObject, "Parent", out parent))
                        //    {
                        //        break;
                        //    }

                        //    object path;
                        //    if (!this.TryGetProperty(parent as ManagementBaseObject, "Path", out path))
                        //    {
                        //        break;
                        //    }

                        //    this._ApplicationDevice = path.ToString();
                        //    break;
                }
            }
        }

        #endregion

        #region プロパティ

        private string _ApplicationDevice;

        public string ApplicationDevice
        {
            get
            {
                return this._ApplicationDevice;
            }
            set
            {
                this._ApplicationDevice = value;
            }
        }

        private string _ApplicationPath;

        public string ApplicationPath
        {
            get
            {
                return this._ApplicationPath;
            }
            set
            {
                this._ApplicationPath = value;
            }
        }

        private string _Description;

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
                //this.Object.SetStringElement(value, (uint)BcdLibraryElementTypes.BcdLibraryString_Description);
            }
        }

        private string _Identifier;

        public string Identifier
        {
            get
            {
                return this._Identifier;
            }
            set
            {
                this._Identifier = value;
            }
        }

        private bool _IsCurrent;

        public bool IsCurrent
        {
            get
            {
                return this._IsCurrent;
            }
            set
            {
                this._IsCurrent = value;
            }
        }

        private bool _IsNext;

        public bool IsNext
        {
            get
            {
                return this._IsNext;
            }
            set
            {
                this._IsNext = value;
            }
        }

        private bool _IsDefault;

        public bool IsDefault
        {
            get
            {
                return this._IsDefault;
            }
            set
            {
                this._IsDefault = value;
            }
        }

        private string _Locale;

        public string Locale
        {
            get
            {
                return this._Locale;
            }
            set
            {
                this._Locale = value;
            }
        }

        internal BcdObject Object
        {
            get;
            set;
        }

        private int _Order;

        public int Order
        {
            get
            {
                return this._Order;
            }
            set
            {
                this._Order = value;
            }
        }

        private string _SystemRoot;

        public string SystemRoot
        {
            get
            {
                return this._SystemRoot;
            }
            set
            {
                this._SystemRoot = value;
            }
        }

        #endregion

        #region メソッド

        public bool Update(OperatingSystem original)
        {
            var bcdObject = this.Object;

            var operatingSystem = this;
            var bcdLibraryElementTypes = new[]
            {
                BcdLibraryElementTypes.BcdLibraryString_Description
            };

            var update = false;
            foreach (var type in bcdLibraryElementTypes)
            {
                var result = false;
                switch (type)
                {
                    case BcdLibraryElementTypes.BcdLibraryString_Description:
                        if (!original.Description.Equals(operatingSystem.Description))
                        {
                            result = bcdObject.SetStringElement(operatingSystem.Description, (uint)type);
                            if (!result)
                            {
                                throw new Exception();
                            }
                        }
                        break;
                }

                if (result && !update)
                {
                    update = true;
                }
            }

            return update;
        }

        #region オーバーライド

        #endregion

        #region イベントハンドラ

        #endregion

        #region ヘルパーメソッド
        #endregion

        #endregion

        public bool Equals(OperatingSystem other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(this._ApplicationDevice, other._ApplicationDevice) &&
                string.Equals(this._ApplicationPath, other._ApplicationPath) &&
                string.Equals(this._Description, other._Description) &&
                string.Equals(this._Identifier, other._Identifier) &&
                this._IsCurrent == other._IsCurrent &&
                this._IsDefault == other._IsDefault &&
                this._IsNext == other._IsNext &&
                string.Equals(this._Locale, other._Locale) &&
                this._Order == other._Order &&
                string.Equals(this._SystemRoot, other._SystemRoot) &&
                Equals(this.Object, other.Object);
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
            return obj is OperatingSystem && this.Equals((OperatingSystem)obj);
        }

        // 実装禁止
        //
        // Entries.xaml 内で Description を変更後、OS を変更して、もとの OS に戻り、また直前の OS に戻ると
        // クラッシュする
        //
        // http://stackoverflow.com/questions/15365905/listbox-loses-the-ability-to-change-selecteditem-when-selecteditem-data-is-modif
        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        var hashCode = (this._ApplicationDevice != null ? this._ApplicationDevice.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this._ApplicationPath != null ? this._ApplicationPath.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this._Description != null ? this._Description.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this._Identifier != null ? this._Identifier.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ this._IsCurrent.GetHashCode();
        //        hashCode = (hashCode * 397) ^ this._IsDefault.GetHashCode();
        //        hashCode = (hashCode * 397) ^ (this._Locale != null ? this._Locale.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this._SystemRoot != null ? this._SystemRoot.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this.Object != null ? this.Object.GetHashCode() : 0);
        //        return hashCode;
        //    }
        //}

        public object Clone()
        {
            return new OperatingSystem(this);
        }

    }
}