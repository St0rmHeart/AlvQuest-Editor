using System.Data;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Windows.Forms;

namespace AlvQuest_Editor
{
    public partial class MainMenu : BaseEditorForm
    {
        private void MainMenu_KeyDown_Esc(object sender, KeyEventArgs e)
        {
            // Проверяем, была ли нажата клавиша Esc
            if (e.KeyCode == Keys.Escape)
            {
                // Закрываем форму
                Close();
            }
        }
        public MainMenu()
        {
            KeyDown += MainMenu_KeyDown_Esc;
            InitializeComponent();
        }

        /*private void SearchByName(object sender, EventArgs e)
        {
            var mask = _searchByNameTextBox.Text;
            if (mask == null)
            {
                DisplayAllEffectPanels();
            }
            else
            {
                _gameEntityListPanel.Controls.Clear();
                var pos = 0;
                for (int i = 0; i < _effectPanelsList.Count; i++)
                {
                    if (_effectPanelsList[i].Effect.Name.Contains(mask))
                    {
                        _gameEntityListPanel.Controls.Add(_effectPanelsList[i].SetSerialPosition(pos));
                        pos++;
                    }
                }
            }
        }*/

        #region Обработка кнопок
        private void UpdateCardButton_Click(object sender, EventArgs e)
        {

        }
        private void ResetButtonsColors()
        {
            _characterListButton.BackColor = SystemColors.Control;
            _effectListButton.BackColor = SystemColors.Control;
            _equipmentListButton.BackColor = SystemColors.Control;
            _perkListButton.BackColor = SystemColors.Control;
            _spellListButton.BackColor = SystemColors.Control;
        }
        private void UpdateButtons(Button button)
        {
            ResetButtonsColors();
            _searchModeButton.Text = "Категория";
            button.BackColor = SystemColors.GradientActiveCaption;
        }
        private void EffectListButton_Click(object sender, EventArgs e)
        {
            _gameEntityListPanel.SuspendLayout();
            _gameEntityListPanel.Controls.Clear();
            for (int i = 0; i < EditorStatic.PPMPanelsList.Count; i++)
            {
                var currentPPMP = EditorStatic.PPMPanelsList[i];
                currentPPMP.Location = new Point(0, currentPPMP.Height * i);
                _gameEntityListPanel.Controls.Add(currentPPMP);
            }
            UpdateButtons(sender as Button);
            _gameEntityListPanel.ResumeLayout(false);
            _gameEntityListPanel.PerformLayout();
        }
        private void EquipmentListButton_Click(object sender, EventArgs e)
        {
            UpdateButtons(sender as Button);
        }
        private void PerkListButton_Click(object sender, EventArgs e)
        {
            UpdateButtons(sender as Button);
        }
        private void SpellListButton_Click(object sender, EventArgs e)
        {
            UpdateButtons(sender as Button);
        }
        private void CharacterListButton_Click(object sender, EventArgs e)
        {
            UpdateButtons(sender as Button);
        }
        private void SearchModeButton_Click(object sender, EventArgs e)
        {
            if (_searchModeButton.Text == "Категория")
            {
                ResetButtonsColors();
                _searchModeButton.Text = "Все объекты";
            }
            else
            {
                _searchModeButton.Text = "Категория";
            }
        }
        private void EffectTypeSelectionMode()
        {
            _effectsCreationModeButon.Enabled = !_effectsCreationModeButon.Enabled;
            _perkCreationModeButon.Enabled = !_perkCreationModeButon.Enabled;
            _equipmentCreationModeButon.Enabled = !_equipmentCreationModeButon.Enabled;
            _spellCreationModeButon.Enabled = !_spellCreationModeButon.Enabled;
            _characterCreationModeButon.Enabled = !_characterCreationModeButon.Enabled;
            _effectTypeSelectionPanel.Visible = !_effectTypeSelectionPanel.Visible;
        }
        private void EffectsCreationModeButon_Click(object sender, EventArgs e)
        {
            EffectTypeSelectionMode();
        }
        private void EffectTypeSelectionPane_MouseLeave(object sender, EventArgs e)
        {
            Point relativeMousePos = _effectTypeSelectionPanel.PointToClient(Cursor.Position);
            if (!_effectTypeSelectionPanel.ClientRectangle.Contains(relativeMousePos))
            {
                EffectTypeSelectionMode();
            }
        }

        #endregion

        private void PPMCreationMenuButton_Click(object sender, EventArgs e)
        {
            EffectTypeSelectionMode();
            EditorStatic.PPMCreationForm.InitPPMCreationForm();
        }
    }












}
