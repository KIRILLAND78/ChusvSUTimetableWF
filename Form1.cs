using System.Runtime.InteropServices;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ChusvSUTimetableWF
{
    public partial class Form1 : Form
    {
        const uint LWA_ALPHA = 0x00000002;
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;
        public bool init = false;
        Timer updateTimer;
        //[DllImport("kernel32.dll")]
        //public static extern uint GetLastError();
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hP, IntPtr hC, string sC, string sW);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        public void SetFormTransparent()
        {
            Opacity = 0.1f;//я не знаю, оно не работает без этого!
            var style = GetWindowLong(Handle, GWL_EXSTYLE);//я никогда не писал настолько страшный код
            if (!Settings.Instance.Draggable)
                style |= WS_EX_TRANSPARENT;
            else style = ((style) & ~WS_EX_TRANSPARENT);
            SetWindowLong(Handle, GWL_EXSTYLE, style | WS_EX_LAYERED);
            //WS_EX_LAYERED для прозрачности
            //WS_EX_TRANSPARENT для кликабельности сквозь окно
            var h = (byte)(Settings.Instance.Transparency * 2.55);
            SetLayeredWindowAttributes(Handle, 0, (byte)(Settings.Instance.Transparency*2.55), LWA_ALPHA);
        }

        public void SetFormNormal()
        {
            if (!Settings.Instance.Draggable) return;
            var oldWindowLong = GetWindowLong(Handle, GWL_EXSTYLE);
            SetWindowLong(Handle, GWL_EXSTYLE, Convert.ToInt32(WS_EX_LAYERED));
        }
        public void MakeWin()
        {
            SetFormTransparent();
            IntPtr nWinHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            nWinHandle = FindWindowEx(nWinHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
            SetParent(Handle, nWinHandle);
        }
        public Form1()
        {
            MakeWin();
            updateTimer = new Timer();
            updateTimer.Interval = 1000 * 60;
            InitializeComponent();
            TTApiManager.Instance.StateChanged += Instance_StateChanged;
            TTApiManager.Instance.LoginCallInput();
            init = true;
        }

        private void Instance_StateChanged(string message, string[] strings)
        {
            label10.Text = message;
            label1.Text = strings[0];
            label2.Text = strings[1];
            label3.Text = strings[2];
            label4.Text = strings[3];
            label5.Text = strings[4];
            label6.Text = strings[5];
            label7.Text = strings[6];
            label8.Text = strings[7];
            label9.Text = strings[8];
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int HTTRANSPARENT = -1;
        private const int WM_NCHITTEST = 0x84;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((Settings.Instance.Draggable) && (e.Button == MouseButtons.Left))
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return;
            }
            base.OnMouseMove(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MakeWin();
            this.Location = new Point(Settings.Instance.X, Settings.Instance.Y);
            Settings.Instance.Save();
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            if (init) {
                Settings.Instance.X = this.Location.X;
                Settings.Instance.Y = this.Location.Y;
                Settings.Instance.Save();
            }
            base.OnLocationChanged(e);
        }
    }
}