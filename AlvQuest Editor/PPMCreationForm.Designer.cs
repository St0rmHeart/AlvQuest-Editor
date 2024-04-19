namespace AlvQuest_Editor
{
    partial class PPMCreationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
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

            // Создание объекта Graphics для рисования
            using (Graphics g = e.Graphics)
            using (Pen pen = new Pen(Color.FromArgb(98, 96, 100), 1))
            {
                var size = _impactPanelListPanel.Size;
                var location = _impactPanelListPanel.Location;
                g.DrawRectangle(pen, location.X - 1, location.Y - 1, size.Width + 1, size.Height + 1);
                size = _iconPictureBox.Size;
                location = _iconPictureBox.Location;
                g.DrawRectangle(pen, location.X - 1, location.Y - 1, size.Width + 1, size.Height + 1);
                size = _errorLlistBox.Size;
                location = _errorLlistBox.Location;
                g.DrawRectangle(pen, location.X - 1, location.Y - 1, size.Width + 1, size.Height + 1);
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _PPMEditorLabel = new Label();
            _nameTextBox = new TextBox();
            _nameLabel = new Label();
            _decriptionLabel = new Label();
            _decriptionRichTextBox = new RichTextBox();
            _modifiableParametersLabel = new Label();
            _impactPanelListPanel = new Panel();
            _addImpactLinkButton = new PictureBox();
            label1 = new Label();
            _iconPictureBox = new PictureBox();
            label2 = new Label();
            _errorLlistBox = new ListBox();
            _searchModeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)_addImpactLinkButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_iconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // _PPMEditorLabel
            // 
            _PPMEditorLabel.AutoSize = true;
            _PPMEditorLabel.BackColor = Color.Transparent;
            _PPMEditorLabel.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _PPMEditorLabel.ForeColor = Color.FromArgb(98, 96, 100);
            _PPMEditorLabel.Location = new Point(0, 0);
            _PPMEditorLabel.Name = "_PPMEditorLabel";
            _PPMEditorLabel.Size = new Size(415, 30);
            _PPMEditorLabel.TabIndex = 16;
            _PPMEditorLabel.Text = "Passive parameter modifier editor";
            _PPMEditorLabel.MouseDown += FormShiftingDragMouseDown;
            _PPMEditorLabel.MouseMove += FormShiftingDragMouseMove;
            _PPMEditorLabel.MouseUp += FormShiftingDragMouseUp;
            // 
            // _nameTextBox
            // 
            _nameTextBox.BackColor = SystemColors.Control;
            _nameTextBox.Font = new Font("Century Gothic", 14F);
            _nameTextBox.ForeColor = Color.FromArgb(25, 23, 24);
            _nameTextBox.Location = new Point(6, 61);
            _nameTextBox.Name = "_nameTextBox";
            _nameTextBox.Size = new Size(300, 30);
            _nameTextBox.TabIndex = 18;
            // 
            // _nameLabel
            // 
            _nameLabel.AutoSize = true;
            _nameLabel.BackColor = Color.Transparent;
            _nameLabel.Font = new Font("Century Gothic", 14F);
            _nameLabel.ForeColor = Color.Silver;
            _nameLabel.Location = new Point(5, 38);
            _nameLabel.Name = "_nameLabel";
            _nameLabel.Size = new Size(101, 22);
            _nameLabel.TabIndex = 19;
            _nameLabel.Text = "Название";
            // 
            // _decriptionLabel
            // 
            _decriptionLabel.AutoSize = true;
            _decriptionLabel.BackColor = Color.Transparent;
            _decriptionLabel.Font = new Font("Century Gothic", 14F);
            _decriptionLabel.ForeColor = Color.Silver;
            _decriptionLabel.Location = new Point(6, 94);
            _decriptionLabel.Name = "_decriptionLabel";
            _decriptionLabel.Size = new Size(110, 22);
            _decriptionLabel.TabIndex = 20;
            _decriptionLabel.Text = "Описание";
            // 
            // _decriptionRichTextBox
            // 
            _decriptionRichTextBox.BackColor = SystemColors.Control;
            _decriptionRichTextBox.Font = new Font("Century Gothic", 14F);
            _decriptionRichTextBox.ForeColor = Color.FromArgb(25, 23, 24);
            _decriptionRichTextBox.Location = new Point(6, 119);
            _decriptionRichTextBox.MaximumSize = new Size(300, 140);
            _decriptionRichTextBox.Name = "_decriptionRichTextBox";
            _decriptionRichTextBox.Size = new Size(300, 124);
            _decriptionRichTextBox.TabIndex = 17;
            _decriptionRichTextBox.Text = "";
            // 
            // _modifiableParametersLabel
            // 
            _modifiableParametersLabel.AutoSize = true;
            _modifiableParametersLabel.BackColor = Color.Transparent;
            _modifiableParametersLabel.Font = new Font("Century Gothic", 14F);
            _modifiableParametersLabel.ForeColor = Color.Silver;
            _modifiableParametersLabel.Location = new Point(313, 38);
            _modifiableParametersLabel.Name = "_modifiableParametersLabel";
            _modifiableParametersLabel.Size = new Size(315, 22);
            _modifiableParametersLabel.TabIndex = 30;
            _modifiableParametersLabel.Text = "Модифицируемые параметры";
            // 
            // _impactPanelListPanel
            // 
            _impactPanelListPanel.AutoScroll = true;
            _impactPanelListPanel.Location = new Point(313, 62);
            _impactPanelListPanel.Name = "_impactPanelListPanel";
            _impactPanelListPanel.Size = new Size(616, 180);
            _impactPanelListPanel.TabIndex = 31;
            // 
            // _addImpactLinkButton
            // 
            _addImpactLinkButton.Image = Properties.Resources.plus;
            _addImpactLinkButton.Location = new Point(634, 40);
            _addImpactLinkButton.Name = "_addImpactLinkButton";
            _addImpactLinkButton.Size = new Size(20, 20);
            _addImpactLinkButton.SizeMode = PictureBoxSizeMode.StretchImage;
            _addImpactLinkButton.TabIndex = 0;
            _addImpactLinkButton.TabStop = false;
            _addImpactLinkButton.MouseDown += _addImpactLinkButton_MouseDown;
            _addImpactLinkButton.MouseEnter += _addImpactLinkButton_MouseEnter;
            _addImpactLinkButton.MouseLeave += _addImpactLinkButton_MouseLeave;
            _addImpactLinkButton.MouseUp += _addImpactLinkButton_MouseUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Century Gothic", 14F);
            label1.ForeColor = Color.Silver;
            label1.Location = new Point(5, 246);
            label1.Name = "label1";
            label1.Size = new Size(80, 22);
            label1.TabIndex = 33;
            label1.Text = "Иконка";
            // 
            // _iconPictureBox
            // 
            _iconPictureBox.BackColor = Color.FromArgb(25, 23, 24);
            _iconPictureBox.Location = new Point(7, 272);
            _iconPictureBox.Name = "_iconPictureBox";
            _iconPictureBox.Size = new Size(154, 154);
            _iconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _iconPictureBox.TabIndex = 34;
            _iconPictureBox.TabStop = false;
            _iconPictureBox.Click += pictureBox1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Century Gothic", 14F);
            label2.ForeColor = Color.Silver;
            label2.Location = new Point(312, 246);
            label2.Name = "label2";
            label2.Size = new Size(92, 22);
            label2.TabIndex = 35;
            label2.Text = "Ошибки";
            // 
            // _errorLlistBox
            // 
            _errorLlistBox.BackColor = Color.FromArgb(25, 23, 24);
            _errorLlistBox.BorderStyle = BorderStyle.None;
            _errorLlistBox.Font = new Font("Century Gothic", 14F);
            _errorLlistBox.ForeColor = Color.Silver;
            _errorLlistBox.FormattingEnabled = true;
            _errorLlistBox.ItemHeight = 22;
            _errorLlistBox.Location = new Point(238, 272);
            _errorLlistBox.Name = "_errorLlistBox";
            _errorLlistBox.Size = new Size(617, 154);
            _errorLlistBox.TabIndex = 36;
            // 
            // _searchModeButton
            // 
            _searchModeButton.BackColor = SystemColors.Control;
            _searchModeButton.Font = new Font("Century Gothic", 14F);
            _searchModeButton.Location = new Point(776, 12);
            _searchModeButton.Name = "_searchModeButton";
            _searchModeButton.Size = new Size(147, 30);
            _searchModeButton.TabIndex = 37;
            _searchModeButton.Text = "Категория";
            _searchModeButton.UseVisualStyleBackColor = false;
            // 
            // PPMCreationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(25, 23, 24);
            ClientSize = new Size(935, 433);
            Controls.Add(_searchModeButton);
            Controls.Add(_errorLlistBox);
            Controls.Add(label2);
            Controls.Add(_iconPictureBox);
            Controls.Add(label1);
            Controls.Add(_addImpactLinkButton);
            Controls.Add(_impactPanelListPanel);
            Controls.Add(_modifiableParametersLabel);
            Controls.Add(_decriptionLabel);
            Controls.Add(_nameLabel);
            Controls.Add(_nameTextBox);
            Controls.Add(_decriptionRichTextBox);
            Controls.Add(_PPMEditorLabel);
            Name = "PPMCreationForm";
            StartPosition = FormStartPosition.Manual;
            Text = "PPMCreationForm";
            ((System.ComponentModel.ISupportInitialize)_addImpactLinkButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)_iconPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _PPMEditorLabel;
        private TextBox _nameTextBox;
        private Label _nameLabel;
        private Label _decriptionLabel;
        private RichTextBox _decriptionRichTextBox;
        private Label _modifiableParametersLabel;
        private Panel _impactPanelListPanel;
        private PictureBox _addImpactLinkButton;
        private Label label1;
        private PictureBox _iconPictureBox;
        private string _iconFile;
        private Label label2;
        private ListBox _errorLlistBox;
        private Button _searchModeButton;

        public string IconFile
        {
            get
            {
                return _iconFile;
            }
            set
            {
                _iconFile = value;
                _iconPictureBox.Image = Image.FromFile(value);
            }
        }
    }
}