using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Ouranos.NextBootOSSelector.ViewModels
{

    public sealed class AboutViewModel : ViewModelBase
    {

        #region イベント
        #endregion

        #region フィールド

        #endregion

        #region コンストラクタ

        public AboutViewModel()
        {
            this.Copyright = Resource.AssemblyProperties.Copyright;
            this.Product = Resource.AssemblyProperties.Product;
            this.Version = Resource.AssemblyProperties.Version;
        }

        #endregion

        #region プロパティ

        private Color _ForeColor;

        public Color ForeColor
        {
            get
            {
                return this._ForeColor;
            }
            set
            {
                this._ForeColor = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Copyright;

        public string Copyright
        {
            get
            {
                return this._Copyright;
            }
            set
            {
                this._Copyright = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Product;

        public string Product
        {
            get
            {
                return this._Product;
            }
            set
            {
                this._Product = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Version;

        public string Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region メソッド

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド
        #endregion

        #endregion

    }

}
