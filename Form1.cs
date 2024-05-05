using System.Runtime.InteropServices;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ChusvSUTimetableWF
{
    public partial class Form1 : Form
    {
        const uint LWA_ALPHA = 0x00000002;
        const uint LWA_COLORKEY = 0x00000001;
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
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public void SetFormTransparent()
        {
            Opacity = ((double)Settings.Instance.Transparency)/100;//я не знаю, оно не работает без этого!
            var style = GetWindowLong(Handle, GWL_EXSTYLE);//я никогда не писал настолько страшный код
            if (!Settings.Instance.Draggable)
                style |= WS_EX_TRANSPARENT;
            else style = ((style) & ~WS_EX_TRANSPARENT);
            SetWindowLong(Handle, GWL_EXSTYLE, style | WS_EX_LAYERED);
            //WS_EX_LAYERED для прозрачности
            //WS_EX_TRANSPARENT для кликабельности сквозь окно
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

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Form1()
        {
            MakeWin();
            updateTimer = new Timer();
            updateTimer.Interval = 1000 * 5;//каждые 5 секунд
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            InitializeComponent();
            Label[] labels = { CTextB1, CTextB2, CTextB3, CTextB4, CTextB5 };
            Label[] subLabels = { CTextS1, CTextS2, CTextS3, CTextS4, CTextS5 };
            for (int i = 0; i < 5; i++)
            {
                labels[i].Text = "-";
                subLabels[i].Text = "-";
            }
            addText.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            TTApiManager.Instance.StateChanged += Instance_StateChanged; 
            var loginTimer = new Timer();
            loginTimer.Interval = 1;//я знаю, я знаю, выглядит ужасно, но это работает. черная магия, никак иначе
            loginTimer.Tick += LoginTimer_Tick;
            loginTimer.Start();
            this.Location = new Point(Settings.Instance.X, Settings.Instance.Y);
            init = true;
        }

        private void LoginTimer_Tick(object? sender, EventArgs e)
        {
            TTApiManager.Instance.LoginCallInput();
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            TTApiManager.Instance.UpdateData();
        }

        private void Instance_StateChanged(string message, string[] strings, string[] additionalData)
        {
            label10.Text = message;
            Label[] labels = { CTextB1, CTextB2, CTextB3, CTextB4, CTextB5 };
            Label[] subLabels = { CTextS1, CTextS2, CTextS3, CTextS4, CTextS5 };
            for (int i=0; i<5; i++)
            {
                labels[i].Text = "-";
                subLabels[i].Text = "-";
            }
            var lab = 0;
            addText.Visible = false;
            headerLabel.Text = $"Расписание на {TTApiManager.Instance.DayName}";
            for (int i=0; i<9; i++)
            {
                if (strings[i] != "-" && strings[i]!=null)
                {
                    labels[lab].Text = strings[i];
                    subLabels[lab].Text = additionalData[i];
                    lab++;
                    if (lab >= 6)
                    {
                        addText.Visible = true;
                        break;
                    }
                }
            }
            //CTextB1.Text = strings[0];
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((Settings.Instance.Draggable) && (e.Button == MouseButtons.Left))
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                //this.Close();
                //this.Dispose();
                //Program.main = new Form1();
                //Program.main.Show();
                //Program.main.TopMost = true;
                //Program.main.TopMost = false;
                return;
            }
            base.OnMouseMove(e);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetFormTransparent();//оно сбрасывает прозрачность почему-то, эта штука здесь нужна
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