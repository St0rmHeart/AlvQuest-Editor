using AlvQuest_Editor.Properties;
using System.Windows.Forms;

namespace AlvQuest_Editor
{
    partial class MainMenu
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
                _gameEntityListPanel.Location.X - 1,
                _gameEntityListPanel.Location.Y - 1,
                _gameEntityListPanel.Size.Width + 2,
                _gameEntityListPanel.Size.Height + 2);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            UpdateCardButton = new Button();
            _gameEntityListPanel = new Panel();
            label1 = new Label();
            _effectListButton = new Button();
            _searchByNameTextBox = new TextBox();
            _characterListButton = new Button();
            _equipmentListButton = new Button();
            _spellListButton = new Button();
            _perkListButton = new Button();
            _searchModeButton = new Button();
            _equipmentCreationModeButon = new Button();
            _spellCreationModeButon = new Button();
            _characterCreationModeButon = new Button();
            _perkCreationModeButon = new Button();
            _effectsCreationModeButon = new Button();
            _EntityListLabel = new Label();
            _effectTypeSelectionPanel = new Panel();
            button2 = new Button();
            PPMCreationMenuButton = new Button();
            _gameEntityListPanel.SuspendLayout();
            _effectTypeSelectionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // UpdateCardButton
            // 
            UpdateCardButton.Location = new Point(12, 12);
            UpdateCardButton.Name = "UpdateCardButton";
            UpdateCardButton.Size = new Size(133, 37);
            UpdateCardButton.TabIndex = 1;
            UpdateCardButton.Text = "UpdateCardButton";
            UpdateCardButton.UseVisualStyleBackColor = true;
            UpdateCardButton.Click += UpdateCardButton_Click;
            // 
            // _gameEntityListPanel
            // 
            _gameEntityListPanel.AutoScroll = true;
            _gameEntityListPanel.BackColor = Color.FromArgb(25, 23, 24);
            _gameEntityListPanel.Controls.Add(label1);
            _gameEntityListPanel.Location = new Point(13, 218);
            _gameEntityListPanel.Name = "_gameEntityListPanel";
            _gameEntityListPanel.Size = new Size(448, 850);
            _gameEntityListPanel.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("VCROSDMonoRUSbyD", 48F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(3, 15);
            label1.Name = "label1";
            label1.Size = new Size(419, 64);
            label1.TabIndex = 1;
            label1.Text = "Test 100";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _effectListButton
            // 
            _effectListButton.Font = new Font("Century Gothic", 14F);
            _effectListButton.Location = new Point(12, 136);
            _effectListButton.Name = "_effectListButton";
            _effectListButton.Size = new Size(150, 40);
            _effectListButton.TabIndex = 3;
            _effectListButton.Text = "Эффекты";
            _effectListButton.UseVisualStyleBackColor = false;
            _effectListButton.Click += EffectListButton_Click;
            // 
            // _searchByNameTextBox
            // 
            _searchByNameTextBox.Font = new Font("Century Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _searchByNameTextBox.Location = new Point(12, 182);
            _searchByNameTextBox.Name = "_searchByNameTextBox";
            _searchByNameTextBox.Size = new Size(297, 30);
            _searchByNameTextBox.TabIndex = 4;
            // 
            // _characterListButton
            // 
            _characterListButton.Font = new Font("Century Gothic", 14F);
            _characterListButton.Location = new Point(312, 136);
            _characterListButton.Name = "_characterListButton";
            _characterListButton.Size = new Size(150, 40);
            _characterListButton.TabIndex = 5;
            _characterListButton.Text = "Персонажи";
            _characterListButton.UseVisualStyleBackColor = false;
            _characterListButton.Click += CharacterListButton_Click;
            // 
            // _equipmentListButton
            // 
            _equipmentListButton.Font = new Font("Century Gothic", 14F);
            _equipmentListButton.Location = new Point(87, 96);
            _equipmentListButton.Name = "_equipmentListButton";
            _equipmentListButton.Size = new Size(150, 40);
            _equipmentListButton.TabIndex = 6;
            _equipmentListButton.Text = "Снаряжение";
            _equipmentListButton.UseVisualStyleBackColor = false;
            _equipmentListButton.Click += EquipmentListButton_Click;
            // 
            // _spellListButton
            // 
            _spellListButton.Font = new Font("Century Gothic", 14F);
            _spellListButton.Location = new Point(162, 136);
            _spellListButton.Name = "_spellListButton";
            _spellListButton.Size = new Size(150, 40);
            _spellListButton.TabIndex = 7;
            _spellListButton.Text = "Заклинания";
            _spellListButton.UseVisualStyleBackColor = false;
            _spellListButton.Click += SpellListButton_Click;
            // 
            // _perkListButton
            // 
            _perkListButton.Font = new Font("Century Gothic", 14F);
            _perkListButton.Location = new Point(237, 96);
            _perkListButton.Name = "_perkListButton";
            _perkListButton.Size = new Size(150, 40);
            _perkListButton.TabIndex = 8;
            _perkListButton.Text = "Перки";
            _perkListButton.UseVisualStyleBackColor = false;
            _perkListButton.Click += PerkListButton_Click;
            // 
            // _searchModeButton
            // 
            _searchModeButton.Font = new Font("Century Gothic", 14F);
            _searchModeButton.Location = new Point(315, 182);
            _searchModeButton.Name = "_searchModeButton";
            _searchModeButton.Size = new Size(147, 30);
            _searchModeButton.TabIndex = 9;
            _searchModeButton.Text = "Категория";
            _searchModeButton.UseVisualStyleBackColor = false;
            _searchModeButton.Click += SearchModeButton_Click;
            // 
            // _equipmentCreationModeButon
            // 
            _equipmentCreationModeButon.Font = new Font("Century Gothic", 14F);
            _equipmentCreationModeButon.Location = new Point(885, 96);
            _equipmentCreationModeButon.Name = "_equipmentCreationModeButon";
            _equipmentCreationModeButon.Size = new Size(150, 40);
            _equipmentCreationModeButon.TabIndex = 10;
            _equipmentCreationModeButon.Text = "Снаряжение";
            _equipmentCreationModeButon.UseVisualStyleBackColor = false;
            // 
            // _spellCreationModeButon
            // 
            _spellCreationModeButon.Font = new Font("Century Gothic", 14F);
            _spellCreationModeButon.Location = new Point(1041, 96);
            _spellCreationModeButon.Name = "_spellCreationModeButon";
            _spellCreationModeButon.Size = new Size(150, 40);
            _spellCreationModeButon.TabIndex = 11;
            _spellCreationModeButon.Text = "Снаряжение";
            _spellCreationModeButon.UseVisualStyleBackColor = false;
            // 
            // _characterCreationModeButon
            // 
            _characterCreationModeButon.Font = new Font("Century Gothic", 14F);
            _characterCreationModeButon.Location = new Point(1197, 96);
            _characterCreationModeButon.Name = "_characterCreationModeButon";
            _characterCreationModeButon.Size = new Size(150, 40);
            _characterCreationModeButon.TabIndex = 12;
            _characterCreationModeButon.Text = "Снаряжение";
            _characterCreationModeButon.UseVisualStyleBackColor = false;
            // 
            // _perkCreationModeButon
            // 
            _perkCreationModeButon.Font = new Font("Century Gothic", 14F);
            _perkCreationModeButon.Location = new Point(729, 96);
            _perkCreationModeButon.Name = "_perkCreationModeButon";
            _perkCreationModeButon.Size = new Size(150, 40);
            _perkCreationModeButon.TabIndex = 13;
            _perkCreationModeButon.Text = "Перки";
            _perkCreationModeButon.UseVisualStyleBackColor = false;
            // 
            // _effectsCreationModeButon
            // 
            _effectsCreationModeButon.Font = new Font("Century Gothic", 14F);
            _effectsCreationModeButon.Location = new Point(573, 96);
            _effectsCreationModeButon.Name = "_effectsCreationModeButon";
            _effectsCreationModeButon.Size = new Size(150, 40);
            _effectsCreationModeButon.TabIndex = 14;
            _effectsCreationModeButon.Text = "Эффекты";
            _effectsCreationModeButon.UseVisualStyleBackColor = false;
            _effectsCreationModeButon.Click += EffectsCreationModeButon_Click;
            // 
            // _EntityListLabel
            // 
            _EntityListLabel.AutoSize = true;
            _EntityListLabel.BackColor = Color.Transparent;
            _EntityListLabel.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _EntityListLabel.ForeColor = SystemColors.ControlLight;
            _EntityListLabel.Location = new Point(31, 58);
            _EntityListLabel.Name = "_EntityListLabel";
            _EntityListLabel.Size = new Size(419, 30);
            _EntityListLabel.TabIndex = 15;
            _EntityListLabel.Text = "Список всех созданных объектов";
            // 
            // _effectTypeSelectionPanel
            // 
            _effectTypeSelectionPanel.AutoScroll = true;
            _effectTypeSelectionPanel.BackColor = Color.FromArgb(25, 23, 24);
            _effectTypeSelectionPanel.Controls.Add(button2);
            _effectTypeSelectionPanel.Controls.Add(PPMCreationMenuButton);
            _effectTypeSelectionPanel.Location = new Point(523, 136);
            _effectTypeSelectionPanel.Name = "_effectTypeSelectionPanel";
            _effectTypeSelectionPanel.Size = new Size(250, 94);
            _effectTypeSelectionPanel.TabIndex = 3;
            _effectTypeSelectionPanel.Visible = false;
            _effectTypeSelectionPanel.MouseLeave += EffectTypeSelectionPane_MouseLeave;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Control;
            button2.Font = new Font("Century Gothic", 12F);
            button2.Location = new Point(3, 49);
            button2.Name = "button2";
            button2.Size = new Size(244, 40);
            button2.TabIndex = 17;
            button2.Text = "TriggerParameterModifier";
            button2.UseVisualStyleBackColor = false;
            // 
            // PPMCreationMenuButton
            // 
            PPMCreationMenuButton.BackColor = SystemColors.Control;
            PPMCreationMenuButton.Font = new Font("Century Gothic", 12F);
            PPMCreationMenuButton.Location = new Point(3, 3);
            PPMCreationMenuButton.Name = "PPMCreationMenuButton";
            PPMCreationMenuButton.Size = new Size(244, 40);
            PPMCreationMenuButton.TabIndex = 16;
            PPMCreationMenuButton.Text = "PassiveParameterModifier";
            PPMCreationMenuButton.UseVisualStyleBackColor = false;
            PPMCreationMenuButton.Click += PPMCreationMenuButton_Click;
            // 
            // MainMenu
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1920, 1080);
            Controls.Add(_effectTypeSelectionPanel);
            Controls.Add(_EntityListLabel);
            Controls.Add(_effectsCreationModeButon);
            Controls.Add(_perkCreationModeButon);
            Controls.Add(_characterCreationModeButon);
            Controls.Add(_spellCreationModeButon);
            Controls.Add(_equipmentCreationModeButon);
            Controls.Add(_searchModeButton);
            Controls.Add(_perkListButton);
            Controls.Add(_spellListButton);
            Controls.Add(_equipmentListButton);
            Controls.Add(_characterListButton);
            Controls.Add(_searchByNameTextBox);
            Controls.Add(_effectListButton);
            Controls.Add(_gameEntityListPanel);
            Controls.Add(UpdateCardButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AlvQuest Editor";
            KeyDown += MainMenu_KeyDown_Esc;
            _gameEntityListPanel.ResumeLayout(false);
            _gameEntityListPanel.PerformLayout();
            _effectTypeSelectionPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button UpdateCardButton;
        private Panel _gameEntityListPanel;
        private Button _effectListButton;
        private TextBox _searchByNameTextBox;
        private Button _characterListButton;
        private Button _equipmentListButton;
        private Button _spellListButton;
        private Button _perkListButton;
        private Button _searchModeButton;
        private Button _equipmentCreationModeButon;
        private Button _spellCreationModeButon;
        private Button _characterCreationModeButon;
        private Button _perkCreationModeButon;
        private Button _effectsCreationModeButon;
        private Label _EntityListLabel;
        private Panel _effectTypeSelectionPanel;
        private Button PPMCreationMenuButton;
        private Button button2;
        private Label label1;
    }
}