using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using MaterialDesignThemes.Wpf;
using Ouranos.NextBootOSSelector.ViewModels;

namespace Ouranos.NextBootOSSelector.Views
{

    /// <summary>
    /// ダイアログの表示機能を提供します。このクラスは継承できません。
    /// </summary>
    public sealed class DialogService : IDialogService
    {

        #region イベント
        #endregion

        #region フィールド
        #endregion

        #region コンストラクタ

        public DialogService()
        {
            //var dictionary = new ResourceDictionary();
            //dictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml");
        }

        #endregion

        #region プロパティ
        #endregion

        #region メソッド

        #region オーバーライド
        #endregion

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド
        #endregion

        #endregion

        #region IDialogService メンバ

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return null;
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return null;
        }

        public async Task ShowMessage(string message, string title)
        {
            var viewModel = new MessageDialogViewModel
            {
                Message = message,
                Title = title
            };

            var view = new YesNoDialog
            {
                DataContext = viewModel
            };

            await DialogHost.Show(view, "RootDialog");
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return null;

        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            return null;
        }

        public Task ShowMessageBox(string message, string title)
        {
            return null;
        }

        #endregion

    }

}
