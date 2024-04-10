using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlvQuest_Editor
{
    public class GameEntityPanel : Panel
    {
        public PictureBox Icon { get; set; }
        public Label EffectName { get; set; }
        protected GameEntityPanel()
        {
            //настройка панели
            Size = new Size(450, 60);
            BackColor = Color.FromArgb(25, 23, 24);
            MouseEnter += ElementPanel_MouseEnter;
            MouseLeave += ElementPanel_MouseLeave;

            //настройка Иконки сущности
            Icon = new PictureBox();
            Icon.SizeMode = PictureBoxSizeMode.StretchImage;
            Icon.Size = new Size(50, 50);
            Icon.Location = new Point(5, 5); // Устанавливаем расположение
            Controls.Add(Icon);

            //Настройка надписи-имени
            EffectName = new Label();
            EffectName.AutoSize = true;
            EffectName.Font = new Font("Century Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            EffectName.ForeColor = SystemColors.AppWorkspace;
            EffectName.Size = new Size(100, 50);
            EffectName.Location = new Point(60, 5); //Устанавливаем расположение
            Controls.Add(EffectName);
        }
        // Обработчик события MouseEnter для панели
        private void ElementPanel_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(50, 48, 49); // Устанавливаем цвет фона при наведении
        }

        // Обработчик события MouseLeave для панели
        private void ElementPanel_MouseLeave(object sender, EventArgs e)
        {
            Point localMousePosition = PointToClient(MousePosition);
            if (!ClientRectangle.Contains(localMousePosition))
            {
                BackColor = Color.FromArgb(25, 23, 24); // Устанавливаем исходный цвет фона, если мышь не находится над панелью
            }
        }

        public GameEntityPanel SetSerialPosition(int number)
        {
            Location = new Point(0, 60 * number);
            return this;
        }
    }
}
