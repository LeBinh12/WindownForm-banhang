using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTapHoa.Presentation
{
    public class Toast : Form
    {
        private readonly System.Windows.Forms.Timer _timer;

        protected override bool ShowWithoutActivation => true; // Make non-blocking

        public Toast(string message, string type)
        {
            this.Size = new Size(320, 65);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            // Position at top right of primary screen
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(workingArea.Right - this.Width - 20, workingArea.Top + 25);

            var panel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Padding = new Padding(12)
            };

            Color color = ThemeHelper.Primary;
            string emoji = "ℹ️";

            switch (type.ToLower())
            {
                case "success":
                    color = ThemeHelper.Success;
                    emoji = "🟢";
                    panel.FillColor = ThemeHelper.SuccessLight;
                    break;
                case "danger":
                    color = ThemeHelper.Danger;
                    emoji = "🔴";
                    panel.FillColor = ThemeHelper.DangerLight;
                    break;
                case "warning":
                    color = ThemeHelper.Warning;
                    emoji = "🟡";
                    panel.FillColor = ThemeHelper.WarningLight;
                    break;
            }

            panel.BorderColor = color;

            var lbl = new Label
            {
                Text = $"{emoji}  {message}",
                Font = ThemeHelper.FontBodyBold,
                ForeColor = color,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lbl);
            this.Controls.Add(panel);
            ThemeHelper.RoundControl(this, 12);

            _timer = new System.Windows.Forms.Timer { Interval = 3000 };
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                this.Close();
            };

            this.Load += (s, e) => _timer.Start();
        }

        public static void Show(string message, string type = "success")
        {
            // Run inside UI thread context safely
            var toast = new Toast(message, type);
            toast.Show();
        }
    }
}
