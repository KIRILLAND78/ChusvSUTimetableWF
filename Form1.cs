using System.Runtime.InteropServices;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ChusvSUTimetableWF
{
    public partial class Form1 : Form
    {
        public bool init = false;
        Timer updateTimer;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hP, IntPtr hC, string sC, string sW);

        void MakeWin()
        {
            IntPtr nWinHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            nWinHandle = FindWindowEx(nWinHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
            SetParent(Handle, nWinHandle);
            this.ShowInTaskbar = false;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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