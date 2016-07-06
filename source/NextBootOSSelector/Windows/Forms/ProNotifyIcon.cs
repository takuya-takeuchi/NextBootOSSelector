using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ouranos.NextBootOSSelector.Interop;

namespace Ouranos.NextBootOSSelector.Windows.Forms
{

    /// <summary>
    /// 通知領域にアイコンを作成するコンポーネントを指定します。このクラスは継承できません。
    /// </summary>
    public sealed class ProNotifyIcon : IDisposable
    {

        #region イベント

        /// <summary>
        /// バルーン ヒントがクリックされると発生します。
        /// </summary>
        public event EventHandler BalloonTipClicked
        {
            add
            {
                this._TrayIcon.BalloonTipClicked += value;
            }
            remove
            {
                this._TrayIcon.BalloonTipClicked -= value;
            }
        }

        /// <summary>
        /// ユーザーがバルーン ヒントを閉じたときに発生します。
        /// </summary>
        public event EventHandler BalloonTipClosed
        {
            add
            {
                this._TrayIcon.BalloonTipClosed += value;
            }
            remove
            {
                this._TrayIcon.BalloonTipClosed -= value;
            }
        }

        /// <summary>
        /// バルーン ヒントが画面に表示されたときに発生します。
        /// </summary>
        public event EventHandler BalloonTipShown
        {
            add
            {
                this._TrayIcon.BalloonTipShown += value;
            }
            remove
            {
                this._TrayIcon.BalloonTipShown -= value;
            }
        }

        /// <summary>
        /// 通知領域のアイコンをクリックすると発生します。
        /// </summary>
        public event EventHandler Click
        {
            add
            {
                this._TrayIcon.Click += value;
            }
            remove
            {
                this._TrayIcon.Click -= value;
            }
        }

        /// <summary>
        /// タスクバーの通知領域のアイコンをダブルクリックすると発生します。
        /// </summary>
        public event EventHandler DoubleClick
        {
            add
            {
                this._TrayIcon.DoubleClick += value;
            }
            remove
            {
                this._TrayIcon.DoubleClick -= value;
            }
        }

        /// <summary>
        /// ユーザーがマウスで <see cref="ProNotifyIcon"/> をクリックすると発生します。
        /// </summary>
        public event MouseEventHandler MouseClick
        {
            add
            {
                this._TrayIcon.MouseClick += value;
            }
            remove
            {
                this._TrayIcon.MouseClick -= value;
            }
        }

        /// <summary>
        /// ユーザーがマウスで <see cref="ProNotifyIcon"/> をダブルクリックすると発生します。
        /// </summary>
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                this._TrayIcon.MouseDoubleClick += value;
            }
            remove
            {
                this._TrayIcon.MouseDoubleClick -= value;
            }
        }

        /// <summary>
        /// ポインターがタスクバーの通知領域のアイコンの上にあるときに、マウス ボタンを押すと発生します。
        /// </summary>
        public event MouseEventHandler MouseDown
        {
            add
            {
                this._TrayIcon.MouseDown += value;
            }
            remove
            {
                this._TrayIcon.MouseDown -= value;
            }
        }

        /// <summary>
        /// ポインターがタスクバーの通知領域のアイコンの上にあるときに、マウスを移動すると発生します。
        /// </summary>
        public event MouseEventHandler MouseMove
        {
            add
            {
                this._TrayIcon.MouseMove += value;
            }
            remove
            {
                this._TrayIcon.MouseMove -= value;
            }
        }

        /// <summary>
        /// ポインターがタスクバーの通知領域のアイコンの上にあるときに、マウス ボタンを離すと発生します。
        /// </summary>
        public event MouseEventHandler MouseUp
        {
            add
            {
                this._TrayIcon.MouseUp += value;
            }
            remove
            {
                this._TrayIcon.MouseUp -= value;
            }
        }

        #endregion

        #region フィールド

        /// <summary>
        /// 強制的に生成したウィンドウハンドルを表します。
        /// </summary>
        private volatile IntPtr _DummyHandle;

        /// <summary>
        /// 強制的にウィンドウハンドルを生成するためのコンポーネントを表します。
        /// <remarks>
        /// 問題1. 起動時にフォームを表示せずトレイアイコンのみ表示させる場合の注意点
        /// 
        /// タイマイベントなどから表示更新といったUI操作ができない。そのため、タイマイベントなどの非UIスレッドからの操作では、
        /// NotifyIconのツールチップ表示やアイコン表示を書き換えることは可能ですが、フォームやダイアログの表示といった処理
        /// （UIスレッドへInvokeしなければならない処理）が実行できない。
        /// そのためにハンドルを確保する。
        /// </remarks>
        /// </summary>
        private volatile System.Windows.Forms.Control _DummyComponent;

        /// <summary>
        /// 知領域にアイコンを作成するコンポーネントを表します。
        /// </summary>
        private volatile NotifyIcon _TrayIcon;

        /// <summary>
        /// <see cref="NotifyIcon"/> がショートカットキーに消えてしまうのを防ぐためのウィンドウ プロシージャを表します。
        /// </summary>
        private volatile SuppressCloseNotifyIconWindow _TrayIconWindow;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// <see cref="ProNotifyIcon"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ProNotifyIcon()
        {
            // 通知領域のアイコンを作成
            // Handle へアクセスすることで、ウィンドウ ハンドルを強制作成
            // ただし、タイムアウトの可能性などを考慮する必要あり
            // プロセスの強制終了では ApplicationExit が発生しない
            // 参考:http://d.hatena.ne.jp/hnx8/20131102/1383415896
            this._TrayIcon = new NotifyIcon();
            this._TrayIcon.Visible = true;

            this._DummyComponent = new System.Windows.Forms.Control();
            this._DummyHandle = this._DummyComponent.Handle;

            this._TrayIconWindow = new SuppressCloseNotifyIconWindow(this._TrayIcon);

            // 問題2. アプリケーション起動時の PC 負荷によっては、トレイアイコンが表示されない
            //
            // 自前で Shell_NotifyIcon 関数を呼び出していれば、こちら (http://www2.ttcn.ne.jp/tkky/Tips/Win32API/shellnnotifyicon2.htm) の
            // ように対策すればよいのですが、NotifyIcon クラス内部の挙動に手出しするのは困難。
            // エラー原因がタイムアウトなのか「アイコンの登録に４秒以上かかったか」をもとに判断し、タイムアウトだった場合は再度トレイアイコンを登録することで、代替
            while (true)
            {
                var tickCount = Environment.TickCount;
                this._TrayIcon.Visible = true;
                tickCount = Environment.TickCount - tickCount;

                if (tickCount < 4000)
                {   //４秒以内に登録できていれば成功
                    break;
                }

                // 失敗した場合はVisibleをfalseにしてやりなおし
                this._TrayIcon.Visible = false;
            }
        }

        #endregion

        #region プロパティ

        /// <summary>
        /// アイコンのショートカット メニューを取得または設定します。
        /// </summary>
        /// <returns>アイコンの <see cref="System.Windows.Forms.ContextMenu"/>。既定値は null です。</returns>
        [Browsable(false)]
        [Obsolete]
        public ContextMenu ContextMenu
        {
            get
            {
                return this._TrayIcon.ContextMenu;
            }
            set
            {
                this._TrayIcon.ContextMenu = value;
            }
        }

        /// <summary>
        /// <see cref="ProNotifyIcon"/> に関連付けられたショートカット メニューを取得または設定します。
        /// </summary>
        /// <returns><see cref="ProNotifyIcon"/> に関連付けられている <see cref="System.Windows.Forms.ContextMenuStrip"/>。</returns>
        public ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return this._TrayIcon.ContextMenuStrip;
            }
            set
            {
                var old = this._TrayIcon.ContextMenuStrip;
                if (old != null)
                {
                    this.SetAlt4Resolver(old, false);
                }

                this._TrayIcon.ContextMenuStrip = value;

                if (value != null)
                {
                    // 問題3. トレイアイコン右クリック後にAlt+F4を押すと、トレイアイコンだけが消えてしまう
                    this.SetAlt4Resolver(value, true);
                }
            }
        }

        /// <summary>
        /// 現在のアイコンを取得または設定します。
        /// </summary>
        /// <returns><see cref="ProNotifyIcon"/> コンポーネントによって表示される <see cref="System.Drawing.Icon"/>。既定値は null です。</returns>
        public Icon Icon
        {
            get
            {
                return this._TrayIcon.Icon;
            }
            set
            {
                this._TrayIcon.Icon = value;
            }
        }

        /// <summary>
        /// マウス ポインターが通知領域アイコンの上にあるときに表示されるツールヒント テキストを取得または設定します。
        /// </summary>
        /// <returns>マウス ポインターが通知領域アイコンの上にあるときに表示されるツールヒント テキスト。</returns>
        /// <exception cref="ArgumentException">ツールヒント テキストの長さが 63 文字を超えています。</exception>
        public string Text
        {
            get
            {
                return this._TrayIcon.Text;
            }
            set
            {
                this._TrayIcon.Text = value;
            }
        }

        /// <summary>
        /// アイコンをタスクバーの通知領域に表示するかどうかを示す値を取得または設定します。
        /// </summary>
        /// <returns>通知領域にアイコンを表示する場合は true。それ以外の場合は false。既定値は false です。</returns>
        public bool Visible
        {
            get
            {
                return this._TrayIcon.Visible;
            }
            set
            {
                this._TrayIcon.Visible = value;
            }
        }

        #endregion

        #region メソッド

        #region オーバーライド
        #endregion

        #region イベントハンドラ
        #endregion

        #region ヘルパーメソッド

        private void ContextMenuClosing(object sender, EventArgs e)
        {
            //アクティブなウィンドウのハンドル＝擬似ウィンドウのハンドルか判定
            var handle = SafeNativeMethods.GetForegroundWindow();
            if (handle == this._TrayIconWindow.Handle)
            {
                SafeNativeMethods.SetForegroundWindow(SafeNativeMethods.GetDesktopWindow());
            }
        }

        private void ContextMenuPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Alt)
            {
                SafeNativeMethods.SetForegroundWindow(SafeNativeMethods.GetDesktopWindow());
            }
        }

        private void SetAlt4Resolver(ContextMenuStrip contextMenuStrip, bool subscribe)
        {
            if (subscribe)
            {
                // 対策1. ContextMenuStrip 表示中にAltキーが押されたらデスクトップをアクティブにする
                contextMenuStrip.PreviewKeyDown += this.ContextMenuPreviewKeyDown;

                // 対策2. メニューを閉じた段階でトレイの擬似ウィンドウがアクティブならデスクトップをアクティブにする
                contextMenuStrip.Closing += this.ContextMenuClosing;
            }
            else
            {
                contextMenuStrip.PreviewKeyDown -= this.ContextMenuPreviewKeyDown;
                contextMenuStrip.Closing -= this.ContextMenuClosing;
            }
        }

        #endregion

        #endregion

        #region IDisposable メンバ

        /// <summary>
        /// <see cref="System.IDisposable.Dispose"/> メソッドが呼ばれたかどうかを示す値を表します。
        /// </summary>
        private bool _Disposed;

        /// <summary>
        /// <see cref="ProNotifyIcon"/> によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        /// <summary>
        /// <see cref="ProNotifyIcon"/> によって使用されているすべてのリソースを解放します。
        /// </summary>
        /// <param name="disposing">Dispose メソッドが呼ばれたかどうかを示す値。</param>
        private void Dispose(bool disposing)
        {
            if (this._Disposed)
            {
                return;
            }

            this._Disposed = true;

            if (disposing)
            {
                // マネージリソースの解放処理
                if (this._TrayIcon != null)
                {
                    var old = this._TrayIcon.ContextMenuStrip;
                    if (old != null)
                    {
                        this.SetAlt4Resolver(old, false);
                    }

                    this._TrayIcon.Dispose();
                    this._TrayIcon = null;
                }
            }
        }

        #endregion

    }

}
