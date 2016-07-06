using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Ouranos.NextBootOSSelector.Models
{

    internal sealed class ConfigurationModel
    {

        #region イベント
        #endregion

        #region フィールド

        private const string PrimaryKey = "Primary";

        private const string AccentKey = "Accent";

        private const string DarkKey = "Dark";

        private const string ImmediatelyRebootKey = "ImmediatelyReboot";

        #endregion

        #region コンストラクタ

        private ConfigurationModel()
        {
            this._Primary = this.GetConfig<string>(PrimaryKey);
            this._Accent = this.GetConfig<string>(AccentKey);
            this._Dark = this.GetConfig<bool>(DarkKey);
            this._ImmediatelyReboot = this.GetConfig<bool>(ImmediatelyRebootKey);
        }

        #endregion

        #region プロパティ

        private static ConfigurationModel _Instance;

        public static ConfigurationModel Instance
        {
            get
            {
                return _Instance ?? (_Instance = new ConfigurationModel());
            }
        }
        private string _Accent;

        public string Accent
        {
            get
            {
                return this._Accent;
            }
            set
            {
                if (this._Accent != value)
                {
                    this._Accent = value;
                    this.SetConfig(AccentKey, value);
                }
            }
        }

        private bool _Dark;

        public bool Dark
        {
            get
            {
                return this._Dark;
            }
            set
            {
                if (this._Dark != value)
                {
                    this._Dark = value;
                    this.SetConfig(DarkKey, value.ToString().ToLowerInvariant());
                }
            }
        }

        private bool _ImmediatelyReboot;

        public bool ImmediatelyReboot
        {
            get
            {
                return this._ImmediatelyReboot;
            }
            set
            {
                if (this._ImmediatelyReboot != value)
                {
                    this._ImmediatelyReboot = value;
                    this.SetConfig(ImmediatelyRebootKey, value.ToString().ToLowerInvariant());
                }
            }
        }


        private string _Primary;

        public string Primary
        {
            get
            {
                return this._Primary;
            }
            set
            {
                if (this._Primary != value)
                {
                    this._Primary = value;
                    this.SetConfig(PrimaryKey, value);
                }
            }
        }

        #endregion

        #region メソッド

        #region オーバーライド
        #endregion

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド

        private T GetConfig<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (appSetting == null)
            {
                throw new KeyNotFoundException(key);
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }

        private void SetConfig(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion

        #endregion

    }

}
