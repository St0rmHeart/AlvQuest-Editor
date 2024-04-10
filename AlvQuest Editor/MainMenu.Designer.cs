using AlvQuest_Editor.Properties;

namespace AlvQuest_Editor
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

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
        private void MainMenuMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = Location;
            }
        }
        private void MainMenuMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursor = Cursor.Position;
                Location = new Point(lastForm.X + (currentCursor.X - lastCursor.X),
                                     lastForm.Y + (currentCursor.Y - lastCursor.Y));
            }
        }
        private void MainMenuMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        private void MainMenuSizeChanging(object sender, EventArgs e)
        {
            // Получаем координаты центра кнопки
            int centerX = this.Location.X + this.Width / 2;
            int centerY = this.Location.Y + this.Height / 2;

            if (this.Size == new Size(1920, 1080)) // Проверяем текущий размер формы
            {
                // Если текущий размер - 1920x1080, меняем на 1280x720
                this.Size = new Size(1280, 720);
                this.BackgroundImage = Resources.фон_1_1280_720_Editor;
                // После изменения размера пересчитываем координаты левого верхнего угла кнопки,
                // чтобы её центр оставался на том же месте
                this.Location = new Point(centerX - 1280 / 2, centerY - 720 / 2);
            }
            else
            {
                // Иначе меняем на 1920x1080
                this.Size = new Size(1920, 1080);
                this.BackgroundImage = Resources.фон_1_1920_1080_Editor;

                // После изменения размера пересчитываем координаты левого верхнего угла кнопки,
                // чтобы её центр оставался на том же месте
                this.Location = new Point(centerX - 1920 / 2, centerY - 1080 / 2);

                //получаем размеры экрана
                int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
                int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

                // Получаем координаты левого верхнего угла формы
                int formLeft = this.Left;
                int formTop = this.Top;

                // Получаем координаты правого нижнего угла формы
                int formRight = this.Right;
                int formBottom = this.Bottom;

                // Проверяем, не выходит ли форма за границы экрана по левому и верхнему краю
                if (formLeft < 0)
                {
                    formLeft = 0; // Если выходит за левую границу, устанавливаем координату X равной 0
                }
                else if (formLeft + 1920 > ScreenWidth)
                {
                    formLeft = ScreenWidth - 1920; // Если выходит за правую границу, корректируем координату X
                }

                if (formTop < 0)
                {
                    formTop = 0; // Если выходит за верхнюю границу, устанавливаем координату Y равной 0
                }
                else if (formTop + 1080 > ScreenHeight)
                {
                    formTop = ScreenHeight - 1080; // Если выходит за нижнюю границу, корректируем координату Y
                }

                // Устанавливаем новое положение формы
                this.Location = new Point(formLeft, formTop);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.sizeSwitchButton = new System.Windows.Forms.Button();
            this.UpdateCardButton = new System.Windows.Forms.Button();
            this._gameEntityListPanel = new System.Windows.Forms.Panel();
            this._effectListButton = new System.Windows.Forms.Button();
            this._searchByNameTextBox = new System.Windows.Forms.TextBox();
            this._characterListButton = new System.Windows.Forms.Button();
            this._equipmentListButton = new System.Windows.Forms.Button();
            this._spellListButton = new System.Windows.Forms.Button();
            this._perkListButton = new System.Windows.Forms.Button();
            this._searchModeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sizeSwitchButton
            // 
            this.sizeSwitchButton.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.sizeSwitchButton.Location = new System.Drawing.Point(1752, 12);
            this.sizeSwitchButton.Name = "sizeSwitchButton";
            this.sizeSwitchButton.Size = new System.Drawing.Size(156, 66);
            this.sizeSwitchButton.TabIndex = 0;
            this.sizeSwitchButton.Text = "Переключиться на 1280x720";
            this.sizeSwitchButton.UseVisualStyleBackColor = true;
            this.sizeSwitchButton.Click += new System.EventHandler(this.MainMenuSizeChanging);
            // 
            // UpdateCardButton
            // 
            this.UpdateCardButton.Location = new System.Drawing.Point(12, 12);
            this.UpdateCardButton.Name = "UpdateCardButton";
            this.UpdateCardButton.Size = new System.Drawing.Size(133, 37);
            this.UpdateCardButton.TabIndex = 1;
            this.UpdateCardButton.Text = "UpdateCardButton";
            this.UpdateCardButton.UseVisualStyleBackColor = true;
            this.UpdateCardButton.Click += new System.EventHandler(this.UpdateCardButton_Click);
            // 
            // _gameEntityListPanel
            // 
            this._gameEntityListPanel.AutoScroll = true;
            this._gameEntityListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
            this._gameEntityListPanel.Location = new System.Drawing.Point(12, 218);
            this._gameEntityListPanel.Name = "_gameEntityListPanel";
            this._gameEntityListPanel.Size = new System.Drawing.Size(450, 850);
            this._gameEntityListPanel.TabIndex = 2;
            // 
            // _effectListButton
            // 
            this._effectListButton.Font = new System.Drawing.Font("Century Gothic", 14F);
            this._effectListButton.Location = new System.Drawing.Point(12, 136);
            this._effectListButton.Name = "_effectListButton";
            this._effectListButton.Size = new System.Drawing.Size(150, 40);
            this._effectListButton.TabIndex = 3;
            this._effectListButton.Text = "Эффекты";
            this._effectListButton.UseVisualStyleBackColor = false;
            this._effectListButton.Click += new System.EventHandler(this.EffectListButton_Click);
            // 
            // _searchByNameTextBox
            // 
            this._searchByNameTextBox.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._searchByNameTextBox.Location = new System.Drawing.Point(12, 182);
            this._searchByNameTextBox.Name = "_searchByNameTextBox";
            this._searchByNameTextBox.Size = new System.Drawing.Size(297, 30);
            this._searchByNameTextBox.TabIndex = 4;
            // 
            // _characterListButton
            // 
            this._characterListButton.Font = new System.Drawing.Font("Century Gothic", 14F);
            this._characterListButton.Location = new System.Drawing.Point(312, 136);
            this._characterListButton.Name = "_characterListButton";
            this._characterListButton.Size = new System.Drawing.Size(150, 40);
            this._characterListButton.TabIndex = 5;
            this._characterListButton.Text = "Персонажи";
            this._characterListButton.UseVisualStyleBackColor = false;
            this._characterListButton.Click += new System.EventHandler(this.CharacterListButton_Click);
            // 
            // _equipmentListButton
            // 
            this._equipmentListButton.Font = new System.Drawing.Font("Century Gothic", 14F);
            this._equipmentListButton.Location = new System.Drawing.Point(87, 96);
            this._equipmentListButton.Name = "_equipmentListButton";
            this._equipmentListButton.Size = new System.Drawing.Size(150, 40);
            this._equipmentListButton.TabIndex = 6;
            this._equipmentListButton.Text = "Снаряжение";
            this._equipmentListButton.UseVisualStyleBackColor = false;
            this._equipmentListButton.Click += new System.EventHandler(this.EquipmentListButton_Click);
            // 
            // _spellListButton
            // 
            this._spellListButton.Font = new System.Drawing.Font("Century Gothic", 14F);
            this._spellListButton.Location = new System.Drawing.Point(162, 136);
            this._spellListButton.Name = "_spellListButton";
            this._spellListButton.Size = new System.Drawing.Size(150, 40);
            this._spellListButton.TabIndex = 7;
            this._spellListButton.Text = "Заклинания";
            this._spellListButton.UseVisualStyleBackColor = false;
            this._spellListButton.Click += new System.EventHandler(this.SpellListButton_Click);
            // 
            // _perkListButton
            // 
            this._perkListButton.Font = new System.Drawing.Font("Century Gothic", 14F);
            this._perkListButton.Location = new System.Drawing.Point(237, 96);
            this._perkListButton.Name = "_perkListButton";
            this._perkListButton.Size = new System.Drawing.Size(150, 40);
            this._perkListButton.TabIndex = 8;
            this._perkListButton.Text = "Перки";
            this._perkListButton.UseVisualStyleBackColor = false;
            this._perkListButton.Click += new System.EventHandler(this.PerkListButton_Click);
            // 
            // _searchModeButton
            // 
            this._searchModeButton.Font = new System.Drawing.Font("Century Gothic", 14F);
            this._searchModeButton.Location = new System.Drawing.Point(315, 182);
            this._searchModeButton.Name = "_searchModeButton";
            this._searchModeButton.Size = new System.Drawing.Size(147, 30);
            this._searchModeButton.TabIndex = 9;
            this._searchModeButton.Text = "Категория";
            this._searchModeButton.UseVisualStyleBackColor = false;
            this._searchModeButton.Click += new System.EventHandler(this.SearchModeButton_Click);
            // 
            // MainMenu
            //
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this._searchModeButton);
            this.Controls.Add(this._perkListButton);
            this.Controls.Add(this._spellListButton);
            this.Controls.Add(this._equipmentListButton);
            this.Controls.Add(this._characterListButton);
            this.Controls.Add(this._searchByNameTextBox);
            this.Controls.Add(this._effectListButton);
            this.Controls.Add(this._gameEntityListPanel);
            this.Controls.Add(this.UpdateCardButton);
            this.Controls.Add(this.sizeSwitchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlvQuest Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button sizeSwitchButton;
        private Button UpdateCardButton;
        private Panel _gameEntityListPanel;
        private Button _effectListButton;
        private TextBox _searchByNameTextBox;
        private Button _characterListButton;
        private Button _equipmentListButton;
        private Button _spellListButton;
        private Button _perkListButton;
        private Button _searchModeButton;
    }
}