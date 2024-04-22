namespace AlvQuest_Editor
{
    public abstract class GameEntityPanel<TDTO> : Panel
        where TDTO : BaseDTO
    {
        public Label EntityName { get; } = new();
        public Label EntityDescription { get; } = new();
        public PictureBox EntityIcon { get; } = new();
        public Button DeleteEntityButton { get; } = new();
        public Button AddEntityToCharacterButton { get; } = new();
        public Button EditEntityButton { get; } = new();
        public int Index { get; set; }

        private TDTO _dto;
        public TDTO DTO
        {
            get
            {
                return _dto;
            }
            set
            {
                _dto = value;
                EntityName.Text = value.BaseData.Name;
                EntityDescription.Text = value.BaseData.Description;
                EntityIcon.Image = Image.FromFile(value.BaseData.Icon);
            }
        }



        protected GameEntityPanel(TDTO dto)
        {
            //установка DTO
            DTO = dto;
            //настройка панели
            Size = new Size(431, 149);
            BackColor = Color.FromArgb(25, 23, 24);
            MouseEnter += (sender, e) =>
            {
                BackColor = Color.FromArgb(50, 48, 49); // Устанавливаем цвет фона при наведении
            };
            MouseLeave += (sender, e) =>
            {
                Point localMousePosition = PointToClient(MousePosition);
                if (!ClientRectangle.Contains(localMousePosition))
                {
                    BackColor = Color.FromArgb(25, 23, 24); // Устанавливаем исходный цвет фона, если мышь не находится над панелью
                }
            };
            // 
            // EntityName
            // 
            EntityName.Font = new Font("Century Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            EntityName.ForeColor = SystemColors.AppWorkspace;
            EntityName.Location = new Point(69, 6);
            EntityName.Size = new Size(323, 25);
            
            // 
            // EntityDescription
            // 
            EntityDescription.Font = new Font("Century Gothic", 10F);
            EntityDescription.ForeColor = SystemColors.AppWorkspace;
            EntityDescription.Location = new Point(3, 69);
            EntityDescription.Size = new Size(424, 75);
            
            // 
            // EntityIcon
            // 
            EntityIcon.Location = new Point(3, 3);
            EntityIcon.Name = "EntityIcon";
            EntityIcon.Size = new Size(63, 63);
            EntityIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            
            // 
            // AddEntityToCharacterButton
            // 
            AddEntityToCharacterButton.BackColor = SystemColors.Control;
            AddEntityToCharacterButton.Font = new Font("Century Gothic", 12F);
            AddEntityToCharacterButton.Location = new Point(203, 36);
            AddEntityToCharacterButton.Size = new Size(107, 30);
            AddEntityToCharacterButton.Text = "Добавить";
            // 
            // EditEntityButton
            // 
            EditEntityButton.BackColor = SystemColors.Control;
            EditEntityButton.Font = new Font("Century Gothic", 12F);
            EditEntityButton.Location = new Point(69, 36);
            EditEntityButton.Size = new Size(134, 30);
            EditEntityButton.Text = "Редактировать";
            EditEntityButton.Click += EditEntity;
            // 
            // DeleteEntityButton
            // 
            DeleteEntityButton.BackColor = Color.DarkRed;
            DeleteEntityButton.FlatStyle = FlatStyle.Popup;
            DeleteEntityButton.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            DeleteEntityButton.Location = new Point(397, 3);
            DeleteEntityButton.Size = new Size(30, 30);
            DeleteEntityButton.Text = "X";
            DeleteEntityButton.Click += DeleteEntity;

            //добавление элементов
            Controls.Add(AddEntityToCharacterButton);
            Controls.Add(EditEntityButton);
            Controls.Add(DeleteEntityButton);
            Controls.Add(EntityName);
            Controls.Add(EntityIcon);
        }
        public abstract void EditEntity(object sender, EventArgs e);
        public abstract void DeleteEntity(object sender, EventArgs e);
    }
    public class PPMPanel : GameEntityPanel<PassiveParameterModifier.PPM_DTO>
    {
        public PPMPanel(PassiveParameterModifier.PPM_DTO dto) : base(dto)
        {

        }

        public override void DeleteEntity(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void EditEntity(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
