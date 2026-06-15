using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;
using System.Collections.Generic;

namespace QuanLyCuaHangTapHoa.Presentation
{
    public static class ThemeHelper
    {
        // Design System Colors
        public static readonly Color Primary = Color.FromArgb(26, 60, 143);         // #1A3C8F
        public static readonly Color PrimaryHover = Color.FromArgb(21, 47, 115);     // #152F73
        public static readonly Color PrimaryLight = Color.FromArgb(232, 237, 245);   // #E8EDF5
        
        public static readonly Color SidebarBg = Color.FromArgb(30, 42, 74);         // #1E2A4A
        public static readonly Color SidebarActiveBg = Color.FromArgb(46, 63, 102);   // #2E3F66
        
        public static readonly Color Success = Color.FromArgb(5, 150, 105);         // #059669
        public static readonly Color SuccessLight = Color.FromArgb(236, 253, 245);   // #ECFDF5
        
        public static readonly Color Warning = Color.FromArgb(217, 119, 6);         // #D97706
        public static readonly Color WarningLight = Color.FromArgb(255, 251, 235);   // #FFFBEB
        
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);          // #DC2626
        public static readonly Color DangerLight = Color.FromArgb(254, 242, 242);    // #FEF2F2
        
        public static readonly Color BackgroundApp = Color.FromArgb(249, 250, 251);  // #F9FAFB
        public static readonly Color Card = Color.FromArgb(255, 255, 255);           // #FFFFFF
        
        public static readonly Color Text = Color.FromArgb(31, 41, 55);              // #1F2937
        public static readonly Color TextSecondary = Color.FromArgb(107, 114, 128);  // #6B7280
        public static readonly Color TextMuted = Color.FromArgb(156, 163, 175);      // #9CA3AF
        
        public static readonly Color Border = Color.FromArgb(229, 229, 235);         // #E5E7EB
        public static readonly Color BorderLight = Color.FromArgb(243, 244, 246);    // #F3F4F6

        // Typography (Segoe UI matched to requested Pixel sizes)
        public static readonly Font FontH1 = new Font("Segoe UI", 18F, FontStyle.Bold);         // 24px
        public static readonly Font FontH2 = new Font("Segoe UI", 15F, FontStyle.Bold);         // 20px
        public static readonly Font FontSubheading = new Font("Segoe UI", 12F, FontStyle.Bold); // 16px Bold
        public static readonly Font FontBody = new Font("Segoe UI", 10F, FontStyle.Regular);     // 13-14px
        public static readonly Font FontBodyBold = new Font("Segoe UI", 10F, FontStyle.Bold);   // 13-14px Bold
        public static readonly Font FontCaption = new Font("Segoe UI", 8.5F, FontStyle.Regular);  // 11-12px
        public static readonly Font FontCaptionBold = new Font("Segoe UI", 8.5F, FontStyle.Bold);// 11-12px Bold

        // Utilities
        public static GraphicsPath GetRoundRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2F;
            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void RoundControl(Control control, int radius)
        {
            if (control == null) return;
            
            Action updateRegion = () =>
            {
                if (control.Width <= 0 || control.Height <= 0) return;
                using (GraphicsPath path = GetRoundRectPath(new Rectangle(0, 0, control.Width, control.Height), radius))
                {
                    control.Region = new Region(path);
                }
            };

            control.SizeChanged += (s, e) => updateRegion();
            updateRegion();
        }

        public static void StyleForm(Form form)
        {
            form.BackColor = BackgroundApp;
            form.Font = FontBody;
            form.ForeColor = Text;
        }

        public static void StyleFlatDataGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            // Horizontal lines only — no vertical gridlines
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = BorderLight;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.DataError += (s, e) => { e.ThrowException = false; };

            // ── Header ──────────────────────────────────────────────────
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 48;

            var hStyle = dgv.ColumnHeadersDefaultCellStyle;
            hStyle.BackColor  = Color.FromArgb(232, 237, 245); // PrimaryLight #E8EDF5
            hStyle.ForeColor  = Color.FromArgb(31, 41, 55);    // Text #1F2937
            hStyle.Font       = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            hStyle.Alignment  = DataGridViewContentAlignment.MiddleLeft;
            hStyle.Padding    = new Padding(8, 0, 8, 0);
            hStyle.WrapMode   = DataGridViewTriState.True;      // wrap instead of clip

            // ── Rows ────────────────────────────────────────────────────
            var rStyle = dgv.DefaultCellStyle;
            rStyle.BackColor          = Color.White;
            rStyle.ForeColor          = Color.FromArgb(31, 41, 55);
            rStyle.Font               = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            rStyle.SelectionBackColor = Color.FromArgb(232, 237, 245);
            rStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            rStyle.Padding            = new Padding(8, 0, 8, 0);
            rStyle.WrapMode           = DataGridViewTriState.False;

            dgv.RowTemplate.Height = 40;

            // Alternating row: #F9FAFB
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
        }

        public static Label MakeBadge(string text, string status)
        {
            Label lbl = new Label
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Font = FontCaptionBold
            };

            Color backColor = PrimaryLight;
            Color foreColor = Primary;

            switch (status.ToLower())
            {
                case "success":
                    backColor = SuccessLight;
                    foreColor = Success;
                    break;
                case "warning":
                    backColor = WarningLight;
                    foreColor = Warning;
                    break;
                case "danger":
                    backColor = DangerLight;
                    foreColor = Danger;
                    break;
                case "primary":
                    backColor = PrimaryLight;
                    foreColor = Primary;
                    break;
            }

            lbl.BackColor = backColor;
            lbl.ForeColor = foreColor;
            
            // Render nice rounded corners via Region
            RoundControl(lbl, 10);
            return lbl;
        }

        public static void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = FontBody;
            txt.ForeColor = Text;
            txt.BackColor = Color.White;
        }

        public static void StyleButton(Button btn, Color backColor, Color foreColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            btn.Font = FontBodyBold;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        public static bool ShowConfirmDialog(Form parentForm, string title, string message)
        {
            // Create backdrop form that overlays the parent
            Form backdrop = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                Opacity = 0.45,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Location = parentForm.Location,
                Size = parentForm.Size
            };
            backdrop.Show(parentForm);

            // Create prompt modal card
            Form prompt = new Form
            {
                Size = new Size(420, 210),
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White
            };
            RoundControl(prompt, 16);

            Label lblTitle = new Label 
            { 
                Text = "⚠️  " + title.ToUpper(), 
                Font = FontSubheading, 
                ForeColor = Primary, 
                Location = new Point(25, 25), 
                AutoSize = true 
            };
            
            Label lblMsg = new Label 
            { 
                Text = message, 
                Font = FontBody, 
                ForeColor = TextSecondary, 
                Location = new Point(25, 65), 
                Size = new Size(370, 65) 
            };

            Button btnYes = new Button 
            { 
                Text = "Đồng ý", 
                DialogResult = DialogResult.Yes, 
                Location = new Point(220, 145), 
                Width = 85, 
                Height = 36 
            };
            StyleButton(btnYes, Primary, Color.White);
            btnYes.MouseEnter += (s, e) => btnYes.BackColor = PrimaryHover;
            btnYes.MouseLeave += (s, e) => btnYes.BackColor = Primary;

            Button btnNo = new Button 
            { 
                Text = "Hủy bỏ", 
                DialogResult = DialogResult.No, 
                Location = new Point(315, 145), 
                Width = 80, 
                Height = 36 
            };
            StyleButton(btnNo, BorderLight, TextSecondary);
            btnNo.MouseEnter += (s, e) => btnNo.BackColor = Border;
            btnNo.MouseLeave += (s, e) => btnNo.BackColor = BorderLight;

            prompt.Controls.AddRange(new Control[] { lblTitle, lblMsg, btnYes, btnNo });
            prompt.AcceptButton = btnYes;
            prompt.CancelButton = btnNo;

            var result = prompt.ShowDialog(backdrop);
            backdrop.Close();
            return result == DialogResult.Yes;
        }

        public static DialogResult ShowCustomDialog(Form parentForm, Form dialogForm)
        {
            Form backdrop = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                Opacity = 0.45,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Location = parentForm.Location,
                Size = parentForm.Size
            };
            backdrop.Show(parentForm);

            dialogForm.FormBorderStyle = FormBorderStyle.None;
            dialogForm.StartPosition = FormStartPosition.CenterParent;
            RoundControl(dialogForm, 16);

            dialogForm.FormClosed += (s, e) => {
                backdrop.Close();
            };

            var result = dialogForm.ShowDialog(parentForm);
            return result;
        }

        public static void StyleComboBox(Guna2ComboBox cbo)
        {
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.BorderRadius = 8;
            cbo.BorderThickness = 1;
            cbo.BorderColor = Color.FromArgb(229, 229, 235); // ThemeHelper.Border
            cbo.FocusedState.BorderColor = Primary; // ThemeHelper.Primary
            cbo.FillColor = Color.White;
            cbo.ForeColor = Color.FromArgb(31, 41, 55); // ThemeHelper.Text
            cbo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            cbo.ItemHeight = 32;
            cbo.Height = 44; // Standard Height matching textbox
            cbo.HoverState.BorderColor = PrimaryHover;
            cbo.HoverState.FillColor = Color.FromArgb(232, 237, 245); // ThemeHelper.PrimaryLight
        }
    }

    public class ComboItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString() => Text;
    }

    public static class EnumTranslator
    {
        public static List<ComboItem> TranslateTrangThaiSanPham(bool includeAll = false)
        {
            var list = new List<ComboItem>();
            if (includeAll) list.Add(new ComboItem { Text = "Tất cả", Value = null });
            list.Add(new ComboItem { Text = "Sẵn sàng", Value = TrangThaiSanPham.SanSang });
            list.Add(new ComboItem { Text = "Chờ xuất kho", Value = TrangThaiSanPham.ChoXuatKho });
            list.Add(new ComboItem { Text = "Đã bán", Value = TrangThaiSanPham.DaBan });
            list.Add(new ComboItem { Text = "Hết hàng", Value = TrangThaiSanPham.HetHang });
            list.Add(new ComboItem { Text = "Hỏng", Value = TrangThaiSanPham.Hong });
            list.Add(new ComboItem { Text = "Ngừng kinh doanh", Value = TrangThaiSanPham.NgungKinhDoanh });
            return list;
        }

        public static List<ComboItem> TranslateTrangThaiDonHang(bool includeAll = false)
        {
            var list = new List<ComboItem>();
            if (includeAll) list.Add(new ComboItem { Text = "Tất cả", Value = null });
            list.Add(new ComboItem { Text = "Chờ duyệt", Value = TrangThaiDonHang.ChoDuyet });
            list.Add(new ComboItem { Text = "Đã duyệt", Value = TrangThaiDonHang.DaDuyet });
            list.Add(new ComboItem { Text = "Từ chối", Value = TrangThaiDonHang.TuChoi });
            list.Add(new ComboItem { Text = "Đã thanh toán", Value = TrangThaiDonHang.DaThanhToan });
            list.Add(new ComboItem { Text = "Đã hủy", Value = TrangThaiDonHang.DaHuy });
            return list;
        }

        public static List<ComboItem> TranslateTrangThaiTaiKhoan(bool includeAll = false)
        {
            var list = new List<ComboItem>();
            if (includeAll) list.Add(new ComboItem { Text = "Tất cả", Value = null });
            list.Add(new ComboItem { Text = "Hoạt động", Value = TrangThaiTaiKhoan.HoatDong });
            list.Add(new ComboItem { Text = "Bị khóa", Value = TrangThaiTaiKhoan.BiKhoa });
            return list;
        }

        public static List<ComboItem> TranslateVaiTro(bool includeAll = false)
        {
            var list = new List<ComboItem>();
            if (includeAll) list.Add(new ComboItem { Text = "Tất cả", Value = null });
            list.Add(new ComboItem { Text = "Quản trị viên", Value = "Admin" });
            list.Add(new ComboItem { Text = "Nhân viên", Value = "NhanVien" });
            list.Add(new ComboItem { Text = "Khách hàng", Value = "KhachHang" });
            return list;
        }
    }
}
