﻿using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Ouranos.NextBootOSSelector.ViewModels
{

    internal sealed class NavigatorViewModel : ViewModelBase
    {

        #region イベント
        #endregion

        #region フィールド

        #endregion

        #region コンストラクタ

        #endregion

        #region プロパティ

        private object _Content;

        public object Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Name;

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
                this.RaisePropertyChanged();
            }
        }

        private Geometry _IconPath;

        public Geometry IconPath
        {
            get
            {
                return this._IconPath;
            }
            set
            {
                this._IconPath = value;
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
