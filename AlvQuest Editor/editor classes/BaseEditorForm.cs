namespace AlvQuest_Editor
{
    public class BaseEditorForm : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public BaseEditorForm()
        {
            InitializeComponent();
        }
        protected void FormShiftingDragMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = Location;
            }
        }
        protected void FormShiftingDragMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursor = Cursor.Position;
                Location = new Point(lastForm.X + (currentCursor.X - lastCursor.X),
                                     lastForm.Y + (currentCursor.Y - lastCursor.Y));
            }
        }
        protected void FormShiftingDragMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Создаем контур для формы
            using (Pen pen = new Pen(Color.FromArgb(98, 96, 100), 1))
            {
                // Рисуем контур по границам формы
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            FormBorderStyle = FormBorderStyle.None;
            MouseDown += FormShiftingDragMouseDown;
            MouseMove += FormShiftingDragMouseMove;
            MouseUp += FormShiftingDragMouseUp;
            ResumeLayout(false);
        }

        #endregion
    }
}
