using System;
using System.Reflection;
using System.Windows.Forms;
using Ouranos.NextBootOSSelector.Interop;

namespace Ouranos.NextBootOSSelector.Windows.Forms
{

    /// <summary>
    /// <see cref="NotifyIcon"/> がショートカットキーによって消えてしまうのを防ぎます。このクラスは継承できません。
    /// </summary>
    /// <remarks>http://social.msdn.microsoft.com/Forums/ja-JP/d8399049-71c6-4e55-a192-9dfd82eed626/notifyiconaltf4?forum=csharpgeneralja</remarks>
    public sealed class SuppressCloseNotifyIconWindow : NativeWindow
    {

        #region フィールド

        /// <summary>
        /// サブクラス化される <see cref="NotifyIcon"/> を表します。
        /// </summary>
        private readonly NotifyIcon _NotifyIcon;


        /// <summary>
        /// <see cref="NotifyIcon"/> が内部で保持するウィンドウ プロシージャを表します。
        /// </summary>
        private readonly NativeWindow _Window;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// <see cref="SuppressCloseNotifyIconWindow"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="icon"></param>
        /// <exception cref="ArgumentNullException"><paramref name="icon"/> が null 参照 (Visual Basic では Nothing) です。</exception>
        public SuppressCloseNotifyIconWindow(NotifyIcon icon)
            : base()
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }

            this._NotifyIcon = icon;

            // この処理は .NET Framework の仕様変更で動作しなくなる可能性がある
            var field = typeof(NotifyIcon).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                return;
            }

            this._Window = field.GetValue(this._NotifyIcon) as NativeWindow;
            if (this._Window == null)
            {
                return;
            }

            this.AssignHandle(this._Window.Handle);
        }

        #endregion

        #region プロパティ
        #endregion

        #region メソッド

        #region イベントハンドラ

        /// <summary>
        /// <see cref="System.Windows.Forms.Control.HandleCreated"/> イベントを発生させます。
        /// </summary>
        /// <param name="sender">イベントのソース。</param>
        /// <param name="e">イベント データを格納している <see cref="EventArgs"/>。</param>
        private void OnHandleCreated(object sender, EventArgs e)
        {
            this.AssignHandle(((Form)sender).Handle);
        }

        /// <summary>
        /// <see cref="System.Windows.Forms.Control.HandleDestroyed"/> イベントを発生させます。
        /// </summary>
        /// <param name="sender">イベントのソース。</param>
        /// <param name="e">イベント データを格納している <see cref="EventArgs"/>。</param>
        private void OnHandleDestroyed(object sender, EventArgs e)
        {
            this.ReleaseHandle();
        }

        #endregion

        #region オーバーライド

        /// <summary>
        /// ウィンドウに関連付けられている既定のウィンドウ プロシージャを呼び出します。
        /// </summary>
        /// <param name="m">現在の Windows メッセージに関連付けられている <see cref="System.Windows.Forms.Message"/>。</param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case SafeNativeMethods.WM_SYSCOMMAND:
                    {
                        switch ((int)m.WParam & 0xfff0)
                        {
                            case SafeNativeMethods.SC_CLOSE:
                                return;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

            base.WndProc(ref m);
        }

        #endregion

        #endregion

    }

}