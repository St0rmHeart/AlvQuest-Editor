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
                size = _errorListBox.Size;
                location = _errorListBox.Location;
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
            _nameLabel = new Label();
            _decriptionLabel = new Label();
            _modifiableParametersLabel = new Label();
            _iconLabel = new Label();
            _errorsLabel = new Label();
            _nameTextBox = new TextBox();
            _decriptionRichTextBox = new RichTextBox();
            _impactPanelListPanel = new Panel();
            _addImpactLinkButton = new PictureBox();
            _iconPictureBox = new PictureBox();
            _errorListBox = new ListBox();
            _createEffectButton = new Button();
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
            _PPMEditorLabel.Location = new Point(6, 6);
            _PPMEditorLabel.Margin = new Padding(3);
            _PPMEditorLabel.Name = "_PPMEditorLabel";
            _PPMEditorLabel.Size = new Size(415, 30);
            _PPMEditorLabel.TabIndex = 16;
            _PPMEditorLabel.Text = "Passive parameter modifier editor";
            _PPMEditorLabel.MouseDown += FormShiftingDragMouseDown;
            _PPMEditorLabel.MouseMove += FormShiftingDragMouseMove;
            _PPMEditorLabel.MouseUp += FormShiftingDragMouseUp;
            // 
            // _nameLabel
            // 
            _nameLabel.AutoSize = true;
            _nameLabel.BackColor = Color.Transparent;
            _nameLabel.Font = new Font("Century Gothic", 14F);
            _nameLabel.ForeColor = Color.Silver;
            _nameLabel.Location = new Point(6, 41);
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
            _decriptionLabel.Location = new Point(6, 97);
            _decriptionLabel.Name = "_decriptionLabel";
            _decriptionLabel.Size = new Size(110, 22);
            _decriptionLabel.TabIndex = 20;
            _decriptionLabel.Text = "Описание";
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
            // _iconLabel
            // 
            _iconLabel.AutoSize = true;
            _iconLabel.BackColor = Color.Transparent;
            _iconLabel.Font = new Font("Century Gothic", 14F);
            _iconLabel.ForeColor = Color.Silver;
            _iconLabel.Location = new Point(6, 249);
            _iconLabel.Name = "_iconLabel";
            _iconLabel.Size = new Size(80, 22);
            _iconLabel.TabIndex = 33;
            _iconLabel.Text = "Иконка";
            // 
            // _errorsLabel
            // 
            _errorsLabel.AutoSize = true;
            _errorsLabel.BackColor = Color.Transparent;
            _errorsLabel.Font = new Font("Century Gothic", 14F);
            _errorsLabel.ForeColor = Color.Silver;
            _errorsLabel.Location = new Point(313, 249);
            _errorsLabel.Name = "_errorsLabel";
            _errorsLabel.Size = new Size(92, 22);
            _errorsLabel.TabIndex = 35;
            _errorsLabel.Text = "Ошибки";
            // 
            // _nameTextBox
            // 
            _nameTextBox.BackColor = SystemColors.Control;
            _nameTextBox.Font = new Font("Century Gothic", 14F);
            _nameTextBox.ForeColor = Color.FromArgb(25, 23, 24);
            _nameTextBox.Location = new Point(6, 64);
            _nameTextBox.Name = "_nameTextBox";
            _nameTextBox.Size = new Size(300, 30);
            _nameTextBox.TabIndex = 18;
            // 
            // _decriptionRichTextBox
            // 
            _decriptionRichTextBox.BackColor = SystemColors.Control;
            _decriptionRichTextBox.Font = new Font("Century Gothic", 14F);
            _decriptionRichTextBox.ForeColor = Color.FromArgb(25, 23, 24);
            _decriptionRichTextBox.Location = new Point(6, 122);
            _decriptionRichTextBox.MaximumSize = new Size(300, 140);
            _decriptionRichTextBox.Name = "_decriptionRichTextBox";
            _decriptionRichTextBox.Size = new Size(300, 124);
            _decriptionRichTextBox.TabIndex = 17;
            _decriptionRichTextBox.Text = "";
            // 
            // _impactPanelListPanel
            // 
            _impactPanelListPanel.AutoScroll = true;
            _impactPanelListPanel.Location = new Point(313, 65);
            _impactPanelListPanel.Margin = new Padding(4);
            _impactPanelListPanel.Name = "_impactPanelListPanel";
            _impactPanelListPanel.Size = new Size(616, 180);
            _impactPanelListPanel.TabIndex = 31;
            // 
            // _addImpactLinkButton
            // 
            _addImpactLinkButton.Image = Properties.Resources.plus;
            _addImpactLinkButton.Location = new Point(634, 38);
            _addImpactLinkButton.Name = "_addImpactLinkButton";
            _addImpactLinkButton.Size = new Size(22, 22);
            _addImpactLinkButton.SizeMode = PictureBoxSizeMode.StretchImage;
            _addImpactLinkButton.TabIndex = 0;
            _addImpactLinkButton.TabStop = false;
            _addImpactLinkButton.MouseDown += _addImpactLinkButton_MouseDown;
            _addImpactLinkButton.MouseEnter += _addImpactLinkButton_MouseEnter;
            _addImpactLinkButton.MouseLeave += _addImpactLinkButton_MouseLeave;
            _addImpactLinkButton.MouseUp += _addImpactLinkButton_MouseUp;
            // 
            // _iconPictureBox
            // 
            _iconPictureBox.BackColor = Color.FromArgb(25, 23, 24);
            _iconPictureBox.Location = new Point(7, 275);
            _iconPictureBox.Margin = new Padding(4);
            _iconPictureBox.Name = "_iconPictureBox";
            _iconPictureBox.Size = new Size(154, 154);
            _iconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _iconPictureBox.TabIndex = 34;
            _iconPictureBox.TabStop = false;
            _iconPictureBox.Click += OpenIconSelectionForm;
            // 
            // _errorListBox
            // 
            _errorListBox.BackColor = Color.FromArgb(25, 23, 24);
            _errorListBox.BorderStyle = BorderStyle.None;
            _errorListBox.Font = new Font("Century Gothic", 14F);
            _errorListBox.ForeColor = Color.Silver;
            _errorListBox.FormattingEnabled = true;
            _errorListBox.ItemHeight = 22;
            _errorListBox.Location = new Point(313, 275);
            _errorListBox.Name = "_errorListBox";
            _errorListBox.Size = new Size(616, 154);
            _errorListBox.TabIndex = 36;
            // 
            // _createEffectButton
            // 
            _createEffectButton.BackColor = SystemColors.Control;
            _createEffectButton.Enabled = false;
            _createEffectButton.Font = new Font("Century Gothic", 14F);
            _createEffectButton.Location = new Point(765, 6);
            _createEffectButton.Name = "_createEffectButton";
            _createEffectButton.Size = new Size(164, 30);
            _createEffectButton.TabIndex = 37;
            _createEffectButton.Text = "Сохранить PPM";
            _createEffectButton.UseVisualStyleBackColor = false;
            _createEffectButton.Click += SavePPM;
            // 
            // PPMCreationForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(25, 23, 24);
            ClientSize = new Size(936, 436);
            Controls.Add(_createEffectButton);
            Controls.Add(_errorListBox);
            Controls.Add(_errorsLabel);
            Controls.Add(_iconPictureBox);
            Controls.Add(_iconLabel);
            Controls.Add(_addImpactLinkButton);
            Controls.Add(_impactPanelListPanel);
            Controls.Add(_modifiableParametersLabel);
            Controls.Add(_decriptionLabel);
            Controls.Add(_nameLabel);
            Controls.Add(_nameTextBox);
            Controls.Add(_decriptionRichTextBox);
            Controls.Add(_PPMEditorLabel);
            Name = "PPMCreationForm";
            Padding = new Padding(3);
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
        private Label _iconLabel;
        private PictureBox _iconPictureBox;
        private Label _errorsLabel;
        private ListBox _errorListBox;
        private Button _createEffectButton;
        
    }
}