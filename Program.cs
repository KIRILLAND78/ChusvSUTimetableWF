
using System.Windows.Forms;

namespace ChusvSUTimetableWF
{
    internal static class Program
    {
        private static System.Windows.Forms.NotifyIcon _notifyIcon;
        public static Form1? main;
        private static Form3? settings;
        public static void Quack()
        {
            using (System.Media.SoundPlayer player = new System.Media.SoundPlayer())
            {
                player.SoundLocation = "quack.wav";
                player.Play();
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            main = new Form1();
            main.Show();

            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = Properties.Resources.Icon1;
            CreateContextMenu();
            _notifyIcon.DoubleClick += (s, args) => ShowWindow(main);
            _notifyIcon.Visible = true;
            _notifyIcon.MouseClick += ContextMenuChange;
            Application.Run();
        }

        private static void ContextMenuChange(object? sender, MouseEventArgs e)
        {
            login.Enabled = TTApiManager.Instance.State == "Not logged";
            logout.Enabled = TTApiManager.Instance.State != "Not logged";
        }
        static ToolStripItem login;
        static ToolStripItem logout;
        private static void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip =
              new ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("���").Click += (s, e) => Quack();
            _notifyIcon.ContextMenuStrip.Items.Add("������� ����").Click += (s, e) => ShowWindow(main);
            _notifyIcon.ContextMenuStrip.Items.Add("���������").Click += (s, e) => ShowWindow(settings);
            login = _notifyIcon.ContextMenuStrip.Items.Add("���� � �������");
            login.Click += (s, e) => TTApiManager.Instance.LoginCallInput();
            logout = _notifyIcon.ContextMenuStrip.Items.Add("����� �� ��������");
            logout.Click += (s, e) => TTApiManager.Instance.Logout();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => Application.Exit();
        }
        private static void ShowWindow<T>(T win) where T : Form, new()
        {
            if (win == null)
            {
                win = new T();
            }
            if (win.Visible)
            {
                if (win.WindowState == FormWindowState.Minimized)
                {
                    win.WindowState = FormWindowState.Normal;
                }
                win.Activate();
            }
            else
            {
                win.Show();
            }
        }
    }
    public class CTLabel : Label
    {
        private const int HTTRANSPARENT = -1;
        private const int WM_NCHITTEST = 0x84;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    m.Result = (IntPtr)HTTRANSPARENT;
                    return;
            }
            base.WndProc(ref m);
        }
    }
    public class CTPictureBox : PictureBox
    {
        private const int HTTRANSPARENT = -1;
        private const int WM_NCHITTEST = 0x84;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    m.Result = (IntPtr)HTTRANSPARENT;
                    return;
            }
            base.WndProc(ref m);
        }
    }
}