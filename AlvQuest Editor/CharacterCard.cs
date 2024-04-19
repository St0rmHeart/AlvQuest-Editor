using System.Drawing.Drawing2D;

namespace AlvQuest_Editor
{
    public partial class CharacterCard : BaseEditorForm
    {
        private void DrawButton_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            // Определяем размеры квадрата
            int size = 200; // Размер квадрата

            // Определяем координаты квадрата
            int x = (Width - size) / 2;
            int y = (Height - size) / 2;

            // Создаем объект LinearGradientBrush для создания градиента
            LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle(x, y, size, size),
                Color.RoyalBlue,
                Color.MidnightBlue,
                LinearGradientMode.Horizontal)
            {
                // Определяем распределение цвета по градиенту
                Blend = new Blend
                {
                    Factors = [0.0f, 1.0f, 0.0f],
                    Positions = [0.0f, 0.5f, 1.0f]
                }
            };

            // Рисуем квадрат с дугой окружности внутри с градиентной заливкой
            g.FillRectangle(brush, x, y, size, size);
            g.FillPie(brush, x, y - size / 2, size, size, 180, 180);
        }
        public void UpdateCard(CharacterSlot character)
        {
            _nameLabel.Text = character.Character.Name;
            _iconPictureBox.Image = character.Character.Icon;
            var allMana = new[]
            {
                character.Data[ECharacteristic.Earth][EDerivative.MaxMana].FinalValue,
                character.Data[ECharacteristic.Fire][EDerivative.MaxMana].FinalValue,
                character.Data[ECharacteristic.Air][EDerivative.MaxMana].FinalValue,
                character.Data[ECharacteristic.Water][EDerivative.MaxMana].FinalValue,
            };
            var startMana = new[]
            {
                character.Data[ECharacteristic.Earth][EDerivative.CurrentMana].FinalValue,
                character.Data[ECharacteristic.Fire][EDerivative.CurrentMana].FinalValue,
                character.Data[ECharacteristic.Air][EDerivative.CurrentMana].FinalValue,
                character.Data[ECharacteristic.Water][EDerivative.CurrentMana].FinalValue,
            };
            var maxMana = allMana.Max();
            Size size = new Size(width: 201, height: 220);
            var manaBarWidth = 30;
            var radius = manaBarWidth / 2;
            Graphics g = _manaBarsPanel.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.FromArgb(25, 23, 24)), 0, 0, size.Width, size.Height);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var manaColors = new (Color center, Color side)[]
            {
                (Color.LightGreen, Color.Green), 
                (Color.Red, Color.Red),
                (Color.Gold, Color.Goldenrod),
                (Color.SkyBlue, Color.DodgerBlue),
            };


            for (int i = 0; i < 4; i++)
            {
                var currentX =  57 * i;
                // Пересчитываем Y-координату так, чтобы верхней точкой была нижняя граница size.Y + size.Height,
                // а высота полосы маны была направлена вверх
                var currentY = size.Height - (int)(size.Height * allMana[i] / maxMana) + radius;
                var currentHeight = (int)(size.Height * allMana[i] / maxMana) - radius;
                var currentWidth = manaBarWidth;

                // Создаем объект LinearGradientBrush для создания градиента
                LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(currentX, currentY, currentWidth, currentHeight),
                    manaColors[i].side,
                    Color.FromArgb(25, 23, 24),
                    LinearGradientMode.Horizontal)
                {
                    // Определяем распределение цвета по градиенту
                    Blend = new Blend
                    {
                        Factors = [0.7f, 1.0f, 0.7f],
                        Positions = [0.0f, 0.5f, 1.0f]
                    }
                };

                g.FillRectangle(brush, currentX, currentY, currentWidth, currentHeight);
                g.FillPie(brush, currentX, currentY - radius+1, manaBarWidth, manaBarWidth, 180, 180);

                currentX = 57 * i;
                // Пересчитываем Y-координату так, чтобы верхней точкой была нижняя граница size.Y + size.Height,
                // а высота полосы маны была направлена вверх
                currentY = size.Height - (int)(size.Height * startMana[i] / maxMana) + radius;
                currentHeight = (int)(size.Height * startMana[i] / maxMana) - radius;
                currentWidth = manaBarWidth;

                // Создаем объект LinearGradientBrush для создания градиента
                brush = new LinearGradientBrush(
                    new Rectangle(currentX, currentY, currentWidth, currentHeight),
                    manaColors[i].center,
                    Color.FromArgb(55, 53, 54),
                    LinearGradientMode.Horizontal)
                {
                    // Определяем распределение цвета по градиенту
                    Blend = new Blend
                    {
                        Factors = [0.7f, 0.0f, 0.7f],
                        Positions = [0.0f, 0.5f, 1.0f]
                    }
                };

                g.FillRectangle(brush, currentX, currentY, currentWidth, currentHeight);
                g.FillPie(brush, currentX, currentY - radius + 1, manaBarWidth, manaBarWidth, 180, 180);
            }
            _manaCountEarthLabel.Text = startMana[0].ToString("F1");
            _manaCountFireLabel.Text = startMana[1].ToString("F1");
            _manaCountAirLabel.Text = startMana[2].ToString("F1");
            _manaCountWaterLabel.Text = startMana[3].ToString("F1");

        }
        public CharacterCard()
        {
            InitializeComponent();
            // Подписываемся на события мыши
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            //распологаем форму справа по центру
            Rectangle screenBounds = Screen.PrimaryScreen?.Bounds ?? Rectangle.Empty;
            Location = new Point(screenBounds.Width - Width, (screenBounds.Height - Height) / 2);
        }
    }
}
