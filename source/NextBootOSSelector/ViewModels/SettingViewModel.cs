using System;
using System.Linq;
using GalaSoft.MvvmLight;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Ouranos.NextBootOSSelector.Models;

namespace Ouranos.NextBootOSSelector.ViewModels
{

    public sealed class SettingViewModel : ViewModelBase
    {

        #region イベント
        #endregion

        #region フィールド

        private readonly PaletteHelper _PaletteHelper;

        #endregion

        #region コンストラクタ

        public SettingViewModel()
        {
            if (!(bool)IsInDesignModeStatic)
            {
                var swatches = new SwatchesProvider().Swatches;
                this._PrimarySwatches = swatches.Where(swatch => swatch.ExemplarHue != null).ToArray();
                this._AccentSwatches = swatches.Where(swatch => swatch.AccentExemplarHue != null).ToArray();
                this._PaletteHelper = new PaletteHelper();

                this.SelectedPrimaryColor = this._PrimarySwatches.First(swatch => swatch.Name.Equals(ConfigurationModel.Instance.Primary, StringComparison.InvariantCultureIgnoreCase));
                this.SelectedAccentColor = this._AccentSwatches.First(swatch => swatch.Name.Equals(ConfigurationModel.Instance.Accent, StringComparison.InvariantCultureIgnoreCase));
                this.IsDark = ConfigurationModel.Instance.Dark;
            }
        }

        #endregion

        #region プロパティ

        private bool _IsDark;

        public bool IsDark
        {
            get
            {
                return this._IsDark;
            }
            set
            {
                if (this._IsDark != value)
                {
                    this._IsDark = value;

                    this.RaisePropertyChanged();

                    new PaletteHelper().SetLightDark(value);

                    ConfigurationModel.Instance.Dark = value;
                }
            }
        }

        private readonly Swatch[] _AccentSwatches;

        public Swatch[] AccentSwatches
        {
            get
            {
                return this._AccentSwatches;
            }
        }

        private readonly Swatch[] _PrimarySwatches;

        public Swatch[] PrimarySwatches
        {
            get
            {
                return this._PrimarySwatches;
            }
        }

        private Swatch _SelectedAccentColor;

        public Swatch SelectedAccentColor
        {
            get
            {
                return this._SelectedAccentColor;
            }
            set
            {
                if (this._SelectedAccentColor != value)
                {
                    this._SelectedAccentColor = value;

                    this.RaisePropertyChanged();

                    this._PaletteHelper.ReplaceAccentColor(value.Name);

                    ConfigurationModel.Instance.Accent = value.Name;
                }
            }
        }

        private Swatch _SelectedPrimaryColor;

        public Swatch SelectedPrimaryColor
        {
            get
            {
                return this._SelectedPrimaryColor;
            }
            set
            {
                if (this._SelectedPrimaryColor != value)
                {
                    this._SelectedPrimaryColor = value;

                    this.RaisePropertyChanged();

                    this._PaletteHelper.ReplacePrimaryColor(value.Name);

                    ConfigurationModel.Instance.Primary = value.Name;
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
        #endregion

        #endregion

    }

}