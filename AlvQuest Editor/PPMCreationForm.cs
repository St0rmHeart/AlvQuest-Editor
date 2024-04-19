namespace AlvQuest_Editor
{
    public partial class PPMCreationForm : BaseEditorForm
    {
        public PassiveParameterModifier.PPM_DTO EditableEffect { get; set; } = new();
        private readonly List<ImpactLinkPanel> ImpactLinkPanelsList = [];
        public PPMCreationForm()
        {
            InitializeComponent();
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
            ImpactLinkPanelsList.Add(impactLinkPanel);
            UpdateImpactLinkPanelList();
        }
        private void DeleteImpactLinkPanel(object sender, EventArgs e)
        {
            var impactLinkPanelDelButton = sender as Button;
            var impactLinkPanel = impactLinkPanelDelButton.Parent as ImpactLinkPanel;
            ImpactLinkPanelsList.RemoveAt(impactLinkPanel.Index);
            UpdateImpactLinkPanelList();
        }
        private void UpdateImpactLinkPanelList()
        {
            _impactPanelListPanel.Controls.Clear();
            for (int i = 0; i < ImpactLinkPanelsList.Count; i++)
            {
                var currentImpactLinkPanel = ImpactLinkPanelsList[i];
                currentImpactLinkPanel.Index = i;
                currentImpactLinkPanel.Location = new Point(0, 36 * i);
                _impactPanelListPanel.Controls.Add(currentImpactLinkPanel);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EditorStatic.IconSelectionForm.Visible = true;
        }


    }
}
