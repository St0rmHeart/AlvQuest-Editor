namespace AlvQuest_Editor
{
    partial class IconSelectionForm
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

            // Получаем контекст Graphics из аргумента PaintEventArgs
            Graphics g = e.Graphics;

            // Рисуем ваш прямоугольник
            g.FillRectangle(new SolidBrush(Color.FromArgb(98, 96, 100)),
                _tableAreaPanel.Location.X - 1,
                _tableAreaPanel.Location.Y - 1,
                _tableAreaPanel.Size.Width + 2,
                _tableAreaPanel.Size.Height + 2);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _PPMEditorLabel = new Label();
            _effectsIconsButon = new Button();
            _perkIconsButon = new Button();
            _characterIconsButon = new Button();
            _spellIconsButon = new Button();
            _equipmentIconsButon = new Button();
            vScrollBar1 = new VScrollBar();
            _tableAreaPanel = new Panel();
            SuspendLayout();
            // 
            // _PPMEditorLabel
            // 
            _PPMEditorLabel.AutoSize = true;
            _PPMEditorLabel.BackColor = Color.Transparent;
            _PPMEditorLabel.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _PPMEditorLabel.ForeColor = Color.FromArgb(98, 96, 100);
            _PPMEditorLabel.Location = new Point(300, 0);
            _PPMEditorLabel.Name = "_PPMEditorLabel";
            _PPMEditorLabel.Size = new Size(184, 30);
            _PPMEditorLabel.TabIndex = 17;
            _PPMEditorLabel.Text = "Icon Selection";
            // 
            // _effectsIconsButon
            // 
            _effectsIconsButon.BackColor = SystemColors.Control;
            _effectsIconsButon.Font = new Font("Century Gothic", 14F);
            _effectsIconsButon.Location = new Point(6, 33);
            _effectsIconsButon.Name = "_effectsIconsButon";
            _effectsIconsButon.Size = new Size(150, 40);
            _effectsIconsButon.TabIndex = 23;
            _effectsIconsButon.Text = "Эффекты";
            _effectsIconsButon.UseVisualStyleBackColor = false;
            _effectsIconsButon.Click += _effectsIconsButon_Click;
            // 
            // _perkIconsButon
            // 
            _perkIconsButon.BackColor = SystemColors.Control;
            _perkIconsButon.Font = new Font("Century Gothic", 14F);
            _perkIconsButon.Location = new Point(162, 33);
            _perkIconsButon.Name = "_perkIconsButon";
            _perkIconsButon.Size = new Size(150, 40);
            _perkIconsButon.TabIndex = 22;
            _perkIconsButon.Text = "Перки";
            _perkIconsButon.UseVisualStyleBackColor = false;
            // 
            // _characterIconsButon
            // 
            _characterIconsButon.BackColor = SystemColors.Control;
            _characterIconsButon.Font = new Font("Century Gothic", 14F);
            _characterIconsButon.Location = new Point(630, 33);
            _characterIconsButon.Name = "_characterIconsButon";
            _characterIconsButon.Size = new Size(150, 40);
            _characterIconsButon.TabIndex = 21;
            _characterIconsButon.Text = "Персонажи";
            _characterIconsButon.UseVisualStyleBackColor = false;
            // 
            // _spellIconsButon
            // 
            _spellIconsButon.BackColor = SystemColors.Control;
            _spellIconsButon.Font = new Font("Century Gothic", 14F);
            _spellIconsButon.Location = new Point(474, 33);
            _spellIconsButon.Name = "_spellIconsButon";
            _spellIconsButon.Size = new Size(150, 40);
            _spellIconsButon.TabIndex = 20;
            _spellIconsButon.Text = "Заклинания";
            _spellIconsButon.UseVisualStyleBackColor = false;
            // 
            // _equipmentIconsButon
            // 
            _equipmentIconsButon.BackColor = SystemColors.Control;
            _equipmentIconsButon.Font = new Font("Century Gothic", 14F);
            _equipmentIconsButon.Location = new Point(318, 33);
            _equipmentIconsButon.Name = "_equipmentIconsButon";
            _equipmentIconsButon.Size = new Size(150, 40);
            _equipmentIconsButon.TabIndex = 19;
            _equipmentIconsButon.Text = "Снаряжение";
            _equipmentIconsButon.UseVisualStyleBackColor = false;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(764, 79);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(16, 752);
            vScrollBar1.TabIndex = 0;
            // 
            // _tableAreaPanel
            // 
            _tableAreaPanel.Location = new Point(7, 80);
            _tableAreaPanel.Name = "_tableAreaPanel";
            _tableAreaPanel.Size = new Size(750, 750);
            _tableAreaPanel.TabIndex = 24;
            // 
            // IconSelectionForm
            // 
            BackColor = Color.FromArgb(25, 23, 24);
            ClientSize = new Size(786, 839);
            Controls.Add(vScrollBar1);
            Controls.Add(_tableAreaPanel);
            Controls.Add(_effectsIconsButon);
            Controls.Add(_perkIconsButon);
            Controls.Add(_characterIconsButon);
            Controls.Add(_spellIconsButon);
            Controls.Add(_equipmentIconsButon);
            Controls.Add(_PPMEditorLabel);
            Name = "IconSelectionForm";
            Text = "IconSelectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _PPMEditorLabel;
        private Button _effectsIconsButon;
        private Button _perkIconsButon;
        private Button _characterIconsButon;
        private Button _spellIconsButon;
        private Button _equipmentIconsButon;
        private VScrollBar vScrollBar1;
        private Panel _tableAreaPanel;
    }
}