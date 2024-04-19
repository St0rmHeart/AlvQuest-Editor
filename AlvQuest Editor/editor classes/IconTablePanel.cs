namespace AlvQuest_Editor.editor_classes
{
    public class IconTablePanel : Panel
    {
        public PictureBox IconPictureBox { get; } = new();

        private string _iconFile;
        public string IconFile
        {
            get
            {
                return _iconFile;
            }
            set
            {
                _iconFile = value;
                IconPictureBox.Image = Image.FromFile(value);
            }
        }

        // Переопределяем метод OnPaint, чтобы нарисовать границу
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Устанавливаем цвет границы
            Color borderColor = Color.FromArgb(98, 96, 100);
            // Толщина границы
            int borderWidth = 1;
            // Создаем перо с заданным цветом и толщиной
            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                // Рисуем прямоугольник вокруг панели, чтобы создать границу
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }
        // Обработчик события MouseEnter для панели
        private void IconTablePanel_MouseEnter(object sender, EventArgs e)
        {
            using Graphics graphics = CreateGraphics();
            using Pen pen = new Pen(Color.Red, 1);
            graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
        }
        // Обработчик события MouseLeave для панели
        private void IconTablePanel_MouseLeave(object sender, EventArgs e)
        {
            Point localMousePosition = PointToClient(MousePosition);
            if (!ClientRectangle.Contains(localMousePosition))
            {
                using Graphics graphics = CreateGraphics();
                using Pen pen = new Pen(Color.FromArgb(98, 96, 100), 1);
                graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }
        private void IconSelection(object sender, EventArgs e)
        {
            EditorStatic.PPMCreationForm.IconFile = IconFile;
            EditorStatic.IconSelectionForm.Visible = false;
        }
        public IconTablePanel(int cellSize)
        {
            Location = new Point(0, 0);
            Size = new Size(cellSize, cellSize);
            MouseEnter += IconTablePanel_MouseEnter;
            MouseLeave += IconTablePanel_MouseLeave;

            IconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            IconPictureBox.Location = new Point(1, 1);
            IconPictureBox.Size = new Size(Size.Width - 2, Size.Height - 2);
            IconPictureBox.Click += IconSelection;
            Controls.Add(IconPictureBox);
        }
    }
}
