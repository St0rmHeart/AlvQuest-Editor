using AlvQuest_Editor.editor_classes;
using System.Windows.Forms;

namespace AlvQuest_Editor
{
    public partial class IconSelectionForm : BaseEditorForm
    {
        private static readonly int columnCount = 5;
        private static readonly string[] _effectIconsfiles = Directory.GetFiles("EntityIcons\\Effects", "*.jpg");
        public IconSelectionForm()
        {
            InitializeComponent();
            
        }

        private void _effectsIconsButon_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            _tableAreaPanel.Controls.Clear();
            var cellSize = _tableAreaPanel.Width / columnCount;
            for (int i = 0; i < _effectIconsfiles.Length; i++)
            {
                var iconPanel = new IconTablePanel(cellSize - 6);
                iconPanel.IconFile = _effectIconsfiles[i];
                var x = i % columnCount;
                var y = i / columnCount;
                iconPanel.Location = new Point(x * cellSize + 3, y * cellSize + 3);
                _tableAreaPanel.Controls.Add(iconPanel);
            }
            PerformLayout();
        }
    }
}
