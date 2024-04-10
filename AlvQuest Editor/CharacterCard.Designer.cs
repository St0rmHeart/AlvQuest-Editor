using System.Drawing;
using System.Windows.Forms;

namespace AlvQuest_Editor
{
    partial class CharacterCard
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = Location;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursor = Cursor.Position;
                Location = new Point(lastForm.X + (currentCursor.X - lastCursor.X),
                                           lastForm.Y + (currentCursor.Y - lastCursor.Y));
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterCard));
            _nameLabel = new Label();
            _iconPictureBox = new PictureBox();
            _manaBarsPanel = new Panel();
            _manaCountEarthLabel = new Label();
            _manaCountFireLabel = new Label();
            _manaCountAirLabel = new Label();
            _manaCountWaterLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)_iconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // _nameLabel
            // 
            _nameLabel.BackColor = Color.Transparent;
            _nameLabel.Font = new Font("Century Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _nameLabel.ForeColor = SystemColors.AppWorkspace;
            _nameLabel.Location = new Point(16, 63);
            _nameLabel.Name = "_nameLabel";
            _nameLabel.Size = new Size(284, 39);
            _nameLabel.TabIndex = 0;
            _nameLabel.Text = "Имя персонажа";
            // 
            // _iconPictureBox
            // 
            _iconPictureBox.BackColor = Color.Transparent;
            _iconPictureBox.Location = new Point(17, 119);
            _iconPictureBox.Name = "_iconPictureBox";
            _iconPictureBox.Size = new Size(306, 306);
            _iconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _iconPictureBox.TabIndex = 1;
            _iconPictureBox.TabStop = false;
            // 
            // _manaBarsPanel
            // 
            _manaBarsPanel.BackColor = Color.FromArgb(25, 23, 24);
            _manaBarsPanel.Location = new Point(340, 126);
            _manaBarsPanel.Name = "_manaBarsPanel";
            _manaBarsPanel.Size = new Size(201, 220);
            _manaBarsPanel.TabIndex = 2;
            // 
            // _manaCountEarthLabel
            // 
            _manaCountEarthLabel.BackColor = Color.Transparent;
            _manaCountEarthLabel.Font = new Font("Century Gothic", 14F);
            _manaCountEarthLabel.ForeColor = SystemColors.AppWorkspace;
            _manaCountEarthLabel.Location = new Point(329, 385);
            _manaCountEarthLabel.Name = "_manaCountEarthLabel";
            _manaCountEarthLabel.Size = new Size(51, 29);
            _manaCountEarthLabel.TabIndex = 3;
            _manaCountEarthLabel.Text = "123";
            _manaCountEarthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _manaCountFireLabel
            // 
            _manaCountFireLabel.BackColor = Color.Transparent;
            _manaCountFireLabel.Font = new Font("Century Gothic", 14F);
            _manaCountFireLabel.ForeColor = SystemColors.AppWorkspace;
            _manaCountFireLabel.Location = new Point(383, 385);
            _manaCountFireLabel.Name = "_manaCountFireLabel";
            _manaCountFireLabel.Size = new Size(51, 29);
            _manaCountFireLabel.TabIndex = 2;
            _manaCountFireLabel.Text = "123";
            _manaCountFireLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _manaCountAirLabel
            // 
            _manaCountAirLabel.BackColor = Color.Transparent;
            _manaCountAirLabel.Font = new Font("Century Gothic", 14F);
            _manaCountAirLabel.ForeColor = SystemColors.AppWorkspace;
            _manaCountAirLabel.Location = new Point(442, 385);
            _manaCountAirLabel.Name = "_manaCountAirLabel";
            _manaCountAirLabel.Size = new Size(51, 29);
            _manaCountAirLabel.TabIndex = 1;
            _manaCountAirLabel.Text = "123";
            _manaCountAirLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _manaCountWaterLabel
            // 
            _manaCountWaterLabel.BackColor = Color.Transparent;
            _manaCountWaterLabel.Font = new Font("Century Gothic", 14F);
            _manaCountWaterLabel.ForeColor = SystemColors.AppWorkspace;
            _manaCountWaterLabel.Location = new Point(498, 385);
            _manaCountWaterLabel.Name = "_manaCountWaterLabel";
            _manaCountWaterLabel.Size = new Size(51, 29);
            _manaCountWaterLabel.TabIndex = 0;
            _manaCountWaterLabel.Text = "123";
            _manaCountWaterLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CharacterCard
            // 
            BackgroundImage = Properties.Resources.cardBG;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(564, 1080);
            Controls.Add(_manaCountWaterLabel);
            Controls.Add(_manaCountAirLabel);
            Controls.Add(_manaCountFireLabel);
            Controls.Add(_manaCountEarthLabel);
            Controls.Add(_manaBarsPanel);
            Controls.Add(_iconPictureBox);
            Controls.Add(_nameLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CharacterCard";
            StartPosition = FormStartPosition.Manual;
            Text = "Карта персонажа";
            ((System.ComponentModel.ISupportInitialize)_iconPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label _nameLabel;
        private PictureBox _iconPictureBox;
        private Panel _manaBarsPanel;
        private Label _manaCountEarthLabel;
        private Label _manaCountFireLabel;
        private Label _manaCountAirLabel;
        private Label _manaCountWaterLabel;
    }
}

