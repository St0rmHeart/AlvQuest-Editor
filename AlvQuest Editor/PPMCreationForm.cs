namespace AlvQuest_Editor
{
    public partial class PPMCreationForm : BaseEditorForm
    {
        public PassiveParameterModifier.PPM_DTO EditableEffect { get; set; } = new();
        private readonly List<ImpactLinkPanel> ImpactLinkPanelsList = [];
        private string _iconFile;
        public string IconFile
        {
            get
            {
                return _iconFile;
            }
            set
            {
                _iconFile = value;
                _iconPictureBox.Image = Image.FromFile(value);
                ValidateContent(null, null);
            }
        }
        public PPMCreationForm()
        {
            InitializeComponent();
            _nameTextBox.TextChanged += ValidateContent;
            _decriptionRichTextBox.TextChanged += ValidateContent;
            _impactPanelListPanel.Paint += ValidateContent;
        }
        #region Кнопка добавления эффектта
        private void _addImpactLinkButton_MouseEnter(object sender, EventArgs e)
        {
            _addImpactLinkButton.BackColor = Color.FromArgb(50, 48, 49);
        }

        private void _addImpactLinkButton_MouseLeave(object sender, EventArgs e)
        {
            _addImpactLinkButton.BackColor = Color.FromArgb(25, 23, 24);
        }
        private void _addImpactLinkButton_MouseDown(object sender, MouseEventArgs e)
        {
            _addImpactLinkButton.BackColor = Color.FromArgb(150, 148, 149);
        }
        private void _addImpactLinkButton_MouseUp(object sender, MouseEventArgs e)
        {
            _addImpactLinkButton.BackColor = Color.FromArgb(25, 23, 24);
            AddNewImpactLinkPanel();
        }
        #endregion
        private void AddNewImpactLinkPanel()
        {
            
            var impactLinkPanel = new ImpactLinkPanel();
            impactLinkPanel.LinkDeletionButton.Click += DeleteImpactLinkPanel;
            impactLinkPanel.ModificationValueTextBox.TextChanged += ValidateContent;
            ImpactLinkPanelsList.Add(impactLinkPanel);
            _impactPanelListPanel.Controls.Add(impactLinkPanel);
            UpdateImpactLinkPanelList();
        }
        private void DeleteImpactLinkPanel(object sender, EventArgs e)
        {
            var impactLinkPanelDelButton = sender as Button;
            var impactLinkPanel = impactLinkPanelDelButton.Parent as ImpactLinkPanel;
            ImpactLinkPanelsList.RemoveAt(impactLinkPanel.Index);
            _impactPanelListPanel.Controls.RemoveAt(impactLinkPanel.Index);
            impactLinkPanel.LinkDeletionButton.Click -= DeleteImpactLinkPanel;
            impactLinkPanel.ModificationValueTextBox.TextChanged -= ValidateContent;
            UpdateImpactLinkPanelList();
        }
        private void UpdateImpactLinkPanelList()
        {
            //_impactPanelListPanel.Controls.Clear();
            for (int i = 0; i < ImpactLinkPanelsList.Count; i++)
            {
                var currentImpactLinkPanel = ImpactLinkPanelsList[i];
                currentImpactLinkPanel.Index = i;
                currentImpactLinkPanel.Location = new Point(0, 36 * i);
                
                //_impactPanelListPanel.Controls.Add(currentImpactLinkPanel);
            }
            _impactPanelListPanel.Invalidate();
        }

        private void OpenIconSelectionForm(object sender, EventArgs e)
        {
            EditorStatic.IconSelectionForm.Visible = true;
        }
        private void ValidateContent(object sender, EventArgs e)
        {
            SuspendLayout();
            _createEffectButton.Enabled = false;
            _errorListBox.Items.Clear();
            if (string.IsNullOrEmpty(_nameTextBox.Text))
            {
                _errorListBox.Items.Add("Отсутствует имя эффекта.");
            }
            if (string.IsNullOrEmpty(_decriptionRichTextBox.Text))
            {
                _errorListBox.Items.Add("Отсутствует описание эффекта.");
            }
            if (string.IsNullOrEmpty(_iconFile))
            {
                _errorListBox.Items.Add("Отсутствует иконка эффекта.");
            }
            if (ImpactLinkPanelsList.Count == 0)
            {
                _errorListBox.Items.Add("Отсутствует ссылка эффекта.");
            }
            else if (ImpactLinkPanelsList.Any(x => !double.TryParse(x.ModificationValueTextBox.Text, out _ )))
            {
                _errorListBox.Items.Add("Некорректно задана ссылка эффекта.");
            }
            _createEffectButton.Enabled = _errorListBox.Items.Count == 0;
            PerformLayout();
        }
        public void SavePPM()
        {

        }
    }
}
