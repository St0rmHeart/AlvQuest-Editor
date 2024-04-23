using System.Collections.Generic;
using static System.Windows.Forms.LinkLabel;

namespace AlvQuest_Editor
{
    public class ImpactLinkPanel : Panel
    {
        public ComboBox TargetComboBox { get; } = new();
        public ComboBox CharacteristicComboBox { get; } = new();
        public ComboBox DerivativeComboBox { get; } = new();
        public ComboBox VariableComboBox { get; } = new();
        public TextBox ModificationValueTextBox { get; } = new();
        public Button LinkDeletionButton { get; } = new();
        public int Index { get; set; }
        public ImpactLinkPanel(Dictionary<string, string> impactLinkData = null)
        {
            SuspendLayout();
            #region Стандартная настройка элементов панели
            // 
            // _targetComboBox
            //
            TargetComboBox.Items.AddRange([EPlayerType.None, EPlayerType.Self, EPlayerType.Enemy]);
            TargetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TargetComboBox.SelectedIndex = 0;
            TargetComboBox.Enabled = true;
            TargetComboBox.Font = new Font("Century Gothic", 14F);
            TargetComboBox.FormattingEnabled = true;
            TargetComboBox.Location = new Point(3, 3);
            TargetComboBox.Size = new Size(90, 30);
            TargetComboBox.SelectionChangeCommitted += _targetComboBox_SelectionChangeCommitted;
            // 
            // _characteristicComboBox
            //
            CharacteristicComboBox.Items.AddRange([ECharacteristic.None, ECharacteristic.Strength, ECharacteristic.Dexterity, ECharacteristic.Endurance, ECharacteristic.Fire, ECharacteristic.Water, ECharacteristic.Air, ECharacteristic.Earth]);
            CharacteristicComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CharacteristicComboBox.SelectedIndex = -1;
            CharacteristicComboBox.Enabled = false;
            CharacteristicComboBox.Font = new Font("Century Gothic", 14F);
            CharacteristicComboBox.FormattingEnabled = true;
            CharacteristicComboBox.Location = new Point(96, 3);
            CharacteristicComboBox.Size = new Size(128, 30);
            CharacteristicComboBox.SelectionChangeCommitted += _characteristicComboBox_SelectionChangeCommitted;
            // 
            // _derivativeComboBox
            // 
            DerivativeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DerivativeComboBox.Enabled = false;
            DerivativeComboBox.Font = new Font("Century Gothic", 14F);
            DerivativeComboBox.FormattingEnabled = true;
            DerivativeComboBox.Location = new Point(227, 3);
            DerivativeComboBox.Size = new Size(180, 30);
            DerivativeComboBox.SelectionChangeCommitted += _derivativeComboBox_SelectionChangeCommitted;
            // 
            // _variableComboBox
            //
            VariableComboBox.Items.AddRange([EVariable.None, EVariable.A0, EVariable.B1, EVariable.B2, EVariable.C1, EVariable.C2, EVariable.D1, EVariable.D2]);
            VariableComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            VariableComboBox.SelectedIndex = -1;
            VariableComboBox.Enabled = false;
            VariableComboBox.Font = new Font("Century Gothic", 14F);
            VariableComboBox.FormattingEnabled = true;
            VariableComboBox.Location = new Point(410, 3);
            VariableComboBox.Size = new Size(73, 30);
            VariableComboBox.SelectionChangeCommitted += _variableComboBox_SelectionChangeCommitted;
            // 
            // _modificationValueTextBox
            // 
            ModificationValueTextBox.BackColor = SystemColors.Window;
            ModificationValueTextBox.Enabled = false;
            ModificationValueTextBox.Font = new Font("Century Gothic", 14F);
            ModificationValueTextBox.ForeColor = Color.FromArgb(25, 23, 24);
            ModificationValueTextBox.Location = new Point(486, 3);
            ModificationValueTextBox.Name = "_modificationValueTextBox";
            ModificationValueTextBox.Size = new Size(73, 30);
            ModificationValueTextBox.TextAlign = HorizontalAlignment.Center;
            ModificationValueTextBox.KeyPress += _modificationValueTextBox_KeyPress;
            // 
            // _linkDeletionButton
            // 
            LinkDeletionButton.BackColor = Color.DarkRed;
            LinkDeletionButton.FlatStyle = FlatStyle.Popup;
            LinkDeletionButton.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LinkDeletionButton.Location = new Point(562, 3);
            LinkDeletionButton.Size = new Size(30, 30);
            LinkDeletionButton.Text = "X";
            LinkDeletionButton.UseVisualStyleBackColor = false;
            
            // 
            // ImpactLinkPanel
            //
            Controls.Add(LinkDeletionButton);
            Controls.Add(TargetComboBox);
            Controls.Add(CharacteristicComboBox);
            Controls.Add(ModificationValueTextBox);
            Controls.Add(DerivativeComboBox);
            Controls.Add(VariableComboBox);
            Size = new Size(595, 36);
            #endregion

            #region Установка ссылки, если она передана
            if (impactLinkData != null &&
                impactLinkData.TryGetValue("Target", out string playerTypeStr) &&
                impactLinkData.TryGetValue("Characteristic", out string characteristicStr) &&
                impactLinkData.TryGetValue("Derivative", out string derivativeStr) &&
                impactLinkData.TryGetValue("Variable", out string variableStr) &&
                impactLinkData.TryGetValue("Value", out string valueStr) &&
                Enum.TryParse<EPlayerType>(playerTypeStr, out var playerType) &&
                Enum.TryParse<ECharacteristic>(characteristicStr, out var characteristic) &&
                Enum.TryParse<EDerivative>(derivativeStr, out var derivative) &&
                Enum.TryParse<EVariable>(variableStr, out var variable) &&
                double.TryParse(valueStr, out _))
            {
                TargetComboBox.SelectedIndex = (int)playerType;

                CharacteristicComboBox.SelectedIndex = (int)characteristic;
                CharacteristicComboBox.Enabled = true;

                List<EDerivative> dataList = new(AlvQuestStatic.CHAR_DER_PAIRS[characteristic]);
                object[] dataArray = [EDerivative.None, .. dataList.Cast<object>()];
                DerivativeComboBox.Items.AddRange(dataArray);
                DerivativeComboBox.SelectedIndex = AlvQuestStatic.CHAR_DER_PAIRS[characteristic].IndexOf(derivative) + 1;
                DerivativeComboBox.Enabled = true;

                VariableComboBox.SelectedIndex = (int)variable;
                VariableComboBox.Enabled = true;

                ModificationValueTextBox.Text = valueStr;
                ModificationValueTextBox.Enabled = true;
            }
            #endregion
            PerformLayout();
        }

        #region Реализация последовательного выбора
        private void _targetComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (TargetComboBox.SelectedIndex == 0)
            {
                CharacteristicComboBox.Enabled = false;
                CharacteristicComboBox.SelectedIndex = -1;
            }
            else
            {
                CharacteristicComboBox.SelectedIndex = 0;
                CharacteristicComboBox.Enabled = true;
            }
            DerivativeComboBox.Enabled = false;
            DerivativeComboBox.Items.Clear();
            VariableComboBox.Enabled = false;
            VariableComboBox.SelectedIndex = -1;
            ModificationValueTextBox.Enabled = false;
            ModificationValueTextBox.Text = string.Empty;
        }
        private void _characteristicComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CharacteristicComboBox.SelectedIndex == 0)
            {
                DerivativeComboBox.Enabled = false;
                DerivativeComboBox.Items.Clear();
            }
            else
            {
                DerivativeComboBox.Items.Clear();
                var characteristic = (ECharacteristic)CharacteristicComboBox.SelectedIndex;
                List<EDerivative> dataList = new(AlvQuestStatic.CHAR_DER_PAIRS[characteristic]);
                object[] dataArray = [EDerivative.None, .. dataList.Cast<object>()];
                DerivativeComboBox.Items.AddRange(dataArray);
                DerivativeComboBox.SelectedIndex = 0;
                DerivativeComboBox.Enabled = true;
            }
            VariableComboBox.Enabled = false;
            VariableComboBox.SelectedIndex = -1;
            ModificationValueTextBox.Enabled = false;
            ModificationValueTextBox.Text = string.Empty;
        }
        private void _derivativeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (DerivativeComboBox.SelectedIndex == 0)
            {
                VariableComboBox.Enabled = false;
                VariableComboBox.SelectedIndex = -1;
            }
            else
            {
                VariableComboBox.SelectedIndex = 0;
                VariableComboBox.Enabled = true;
            }
            ModificationValueTextBox.Enabled = false;
            ModificationValueTextBox.Text = string.Empty;
        }
        private void _variableComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (VariableComboBox.SelectedIndex == 0)
            {
                ModificationValueTextBox.Enabled = false;
                ModificationValueTextBox.Text = string.Empty;
            }
            else
            {
                ModificationValueTextBox.Enabled = true;
            }
        }
        private void _modificationValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // Проверяем, является ли введенный символ цифрой, точкой, или клавишей Backspace или минусом
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            // Проверяем, что введенная точка не является первым символом и что она вводится только один раз
            if ((e.KeyChar == ',') && (textBox.Text.IndexOf(',') > -1 || textBox.Text.Length == 0))
            {
                e.Handled = true;
            }
            // Проверяем, что нули не вводятся в начале числа или после точки
            if (e.KeyChar == '0' && (textBox.Text == "0" || textBox.Text == "-0"))
            {
                e.Handled = true;
            }
            // 
            if (textBox.Text.Contains(','))
            {
                int dotIndex = textBox.Text.IndexOf(',');
                if (textBox.SelectionStart > dotIndex && textBox.Text.Length - dotIndex >= 5 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            // Проверяем, что минус введен только в начале строки и только один раз
            if ((e.KeyChar == '-') && (textBox.Text.IndexOf('-') > -1 || textBox.SelectionStart != 0))
            {
                e.Handled = true;
            }
        }
        #endregion

        public (EPlayerType, ECharacteristic, EDerivative, EVariable, double) GetImpactLink()
        {
            var playerType = (EPlayerType)TargetComboBox.SelectedIndex;
            var characteristic = (ECharacteristic)CharacteristicComboBox.SelectedIndex;
            var derivative = AlvQuestStatic.CHAR_DER_PAIRS[characteristic][DerivativeComboBox.SelectedIndex - 1];
            var variable = (EVariable)VariableComboBox.SelectedIndex;
            _ = double.TryParse(ModificationValueTextBox.Text, out double value);
            return (playerType, characteristic, derivative, variable, value);
        }
        public Dictionary<string, string> GetImpactLinkDTO()
        {
            return AlvQuestStatic.DTOConverter.ToDTOImpactLink(GetImpactLink());
        }
    }
}
