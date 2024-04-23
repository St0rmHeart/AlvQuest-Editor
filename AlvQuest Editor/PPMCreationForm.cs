namespace AlvQuest_Editor
{
    public partial class PPMCreationForm : BaseEditorForm
    {
        private readonly List<ImpactLinkPanel> ImpactLinkPanelList = [];
        private bool _isNewPPM;
        private PPMPanel _editablePanel;
        private string _iconFileData;
        public string IconFile
        {
            get
            {
                return _iconFileData;
            }
            set
            {
                _iconFileData = value;
                _iconPictureBox.Image = value == null ? null : Image.FromFile(value);
                ValidateContent(null, null);
            }
        }
        public PPMCreationForm()
        {
            InitializeComponent();
            _nameTextBox.TextChanged += ValidateContent;
            _decriptionRichTextBox.TextChanged += ValidateContent;
            _impactPanelListPanel.Paint += ValidateContent;
            _addImpactLinkButton.MouseEnter += (sender, e) =>
            {
                _addImpactLinkButton.BackColor = Color.FromArgb(50, 48, 49);
            };
            _addImpactLinkButton.MouseLeave += (sender, e) =>
            {
                _addImpactLinkButton.BackColor = Color.FromArgb(25, 23, 24);
            };
            _addImpactLinkButton.MouseDown += (sender, e) =>
            {
                _addImpactLinkButton.BackColor = Color.FromArgb(150, 148, 149);
            };
            _addImpactLinkButton.MouseUp += (sender, e) =>
            {
                _addImpactLinkButton.BackColor = Color.FromArgb(25, 23, 24);
                AddNewImpactLinkPanel();
            };
            _iconPictureBox.Click += (sender, e) =>
            {
                EditorStatic.IconSelectionForm.Visible = true;
            };
        }
        public void InitPPMCreationForm(PPMPanel editablePanel = null)
        {
            //настройка начального положения
            int x = EditorStatic.MainMenu.Location.X + 461;
            int y = EditorStatic.MainMenu.Location.Y + 217; 
            EditorStatic.PPMCreationForm.Location = new Point(x, y);
            if (editablePanel == null)
            {
                _isNewPPM = true;     
            }
            else
            {
                _isNewPPM = false;
                _editablePanel = editablePanel;
                _nameTextBox.Text = editablePanel.DTO.BaseData.Name;
                _decriptionRichTextBox.Text = editablePanel.DTO.BaseData.Description;
                IconFile = editablePanel.DTO.BaseData.Icon;
                foreach (var impactLink in editablePanel.DTO.Links)
                {
                    AddNewImpactLinkPanel(impactLink);
                }
            }
            Show();
        }
        private void AddNewImpactLinkPanel(Dictionary<string, string> impactLink = null)
        {
            var impactLinkPanel = new ImpactLinkPanel(impactLink);
            impactLinkPanel.LinkDeletionButton.Click += DeleteImpactLinkPanel;
            impactLinkPanel.ModificationValueTextBox.TextChanged += ValidateContent;
            ImpactLinkPanelList.Add(impactLinkPanel);
            _impactPanelListPanel.Controls.Add(impactLinkPanel);
            UpdateImpactLinkPanelList();
        }
        private void DeleteImpactLinkPanel(object sender, EventArgs e)
        {
            var impactLinkPanelDelButton = sender as Button;
            var impactLinkPanel = impactLinkPanelDelButton.Parent as ImpactLinkPanel;
            ImpactLinkPanelList.RemoveAt(impactLinkPanel.Index);
            _impactPanelListPanel.Controls.RemoveAt(impactLinkPanel.Index);
            impactLinkPanel.LinkDeletionButton.Click -= DeleteImpactLinkPanel;
            impactLinkPanel.ModificationValueTextBox.TextChanged -= ValidateContent;
            UpdateImpactLinkPanelList();
        }
        private void UpdateImpactLinkPanelList()
        {
            for (int i = 0; i < ImpactLinkPanelList.Count; i++)
            {
                var currentImpactLinkPanel = ImpactLinkPanelList[i];
                currentImpactLinkPanel.Index = i;
                currentImpactLinkPanel.Location = new Point(0, currentImpactLinkPanel.Height * i);
            }
            _impactPanelListPanel.Invalidate();
        }
        private void ValidateContent(object sender, EventArgs e)
        {
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
            if (string.IsNullOrEmpty(_iconFileData))
            {
                _errorListBox.Items.Add("Отсутствует иконка эффекта.");
            }
            if (ImpactLinkPanelList.Count == 0)
            {
                _errorListBox.Items.Add("Отсутствует ссылка эффекта.");
            }
            else if (ImpactLinkPanelList.Any(x => !double.TryParse(x.ModificationValueTextBox.Text, out _)))
            {
                _errorListBox.Items.Add("Некорректно задана ссылка эффекта.");
            }
            _createEffectButton.Enabled = _errorListBox.Items.Count == 0;
        }
        private void Reset()
        {
            _nameTextBox.Text = string.Empty;
            _decriptionRichTextBox.Text = string.Empty;
            _iconFileData = string.Empty;
            foreach (var currentImpactLinkPanel in ImpactLinkPanelList)
            {
                currentImpactLinkPanel.LinkDeletionButton.Click -= DeleteImpactLinkPanel;
                currentImpactLinkPanel.ModificationValueTextBox.TextChanged -= ValidateContent;
            }
            ImpactLinkPanelList.Clear();
            _impactPanelListPanel.Controls.Clear();
            IconFile = null;
        }
        private void SavePPM(object sender, EventArgs e)
        {
            PassiveParameterModifier.PPM_DTO dto = new();
            dto.BaseData.Name = _nameTextBox.Text;
            dto.BaseData.Description = _decriptionRichTextBox.Text;
            dto.BaseData.Icon = IconFile;
            dto.Links = ImpactLinkPanelList.Select(x => x.GetImpactLinkDTO()).ToList();
            if (_isNewPPM)
            {
                var ppmp = new PPMPanel(dto);
                ppmp.Index = EditorStatic.PPMPanelsList.Count;
                EditorStatic.PPMPanelsList.Add(ppmp);
            }
            else
            {
                _editablePanel.DTO = dto;
            }
            Visible = false;
            Reset();
        }
    }
}
