
using System.Data;
using System.Text.Json;
using System.Text.Encodings.Web;

/*var newTriggerParameter = new TriggerParameterModifier.TPM_Builder()
               .Name("Наростающая ярость.")
               .Description(
                   "При нанесении более 7 едениц физического урона ваша сила увеличиваете на 5% на 2 хода.\n" +
                   "Может складываться до 4х раз.")
               .TriggerlogicalModule(new LM_02_damageThreshold(damageType: EDamageType.PhysicalDamage, threshold: 7))
               .TicklogicalModule(new LM_CONSTANT_TRUE())
               .Duration(2)
               .MaxStack(4)
               .ComposeLink(EPlayerType.Self)
               .ComposeLink(ECharacteristic.Strength)
               .ComposeLink(EDerivative.Value)
               .ComposeLink(EVariable.C1)
               .AddLink()
               .AddValue(0.05)
               .ComposeTriggerEvent(EPlayerType.Enemy)
               .ComposeTriggerEvent(EEvent.DamageTaking)
               .AddTriggerEvent()
               .ComposeTickEvent(EPlayerType.Self)
               .ComposeTickEvent(EEvent.StepExecution)
               .AddTickEventt()
               .Build();
                _effectPanelsList.Add(new EffectPanel(newTriggerParameter));*/

namespace AlvQuest_Editor
{
    public partial class MainMenu : Form
    {
        private static readonly string _filePathPPM = "json data/effectsPPM.json";
        private static readonly string _filePathTPM = "json data/effectsTPM.json";
        private static readonly string _filePathLME = "json data/effectsLME.json";
        private readonly CharacterCard _characterCard;
        private readonly Arena _arena;
        private readonly List<EffectPanel> _effectPanelsList = [];
        private void DisplayAllEffectPanels()
        {
            _gameEntityListPanel.Controls.Clear();
            for (int i = 0; i < _effectPanelsList.Count; i++)
            {
                _gameEntityListPanel.Controls.Add(_effectPanelsList[i].SetSerialPosition(i));
            }
        }
        public MainMenu(CharacterCard characterCard)
        {

            #region GameData
            var CBuilder = new Character.CBuilder();
            var PPMBuilder = new PassiveParameterModifier.PPM_Builder();
            var TPMBuilder = new TriggerParameterModifier.TPM_Builder();


            var sword = new Equipment("Древний Эльфийский Меч", EBodyPart.Weapon);

            var newPassiveEffect = PPMBuilder
                .Name("Живучесть.")
                .Description("Увеличивает максимальное здоровье на 10.")
                .Icon("FireKnightIcon")
                .AddLink(EPlayerType.Self, ECharacteristic.Endurance, EDerivative.MaxHealth, EVariable.C2)
                .AddValue(10)
                .Build();
            sword.Effects.Add(newPassiveEffect);

            var newTriggerParameter = TPMBuilder
                .Name("Наростающая ярость.")
                .Description(
                    "При нанесении более 7 едениц физического урона ваша сила увеличиваете на 5% на 2 хода.\n" +
                    "Может складываться до 4х раз.")
                .Icon("FireKnightIcon")
                .TriggerlogicalModule(new LM_02_damageThreshold(damageType: EDamageType.PhysicalDamage, threshold: 7))
                .TicklogicalModule(new LM_CONSTANT_TRUE())
                .Duration(2)
                .MaxStack(4)
                .AddLink(EPlayerType.Self, ECharacteristic.Strength, EDerivative.Value, EVariable.C1)
                .AddValue(0.05)
                .AddTriggerEvent(EPlayerType.Enemy, EEvent.DamageTaking)
                .AddTickEventt(EPlayerType.Self, EEvent.StepExecution)
                .Build();
            sword.Effects.Add(newTriggerParameter);

            var testHero = CBuilder
                .With_Name("Огн. Рыцарь")
                .With_XP(1874)
                .With_Characteristic(ECharacteristic.Strength, 55)
                .With_Characteristic(ECharacteristic.Dexterity, 40)
                .With_Characteristic(ECharacteristic.Endurance, 95)
                .With_Characteristic(ECharacteristic.Fire, 175)
                .With_Characteristic(ECharacteristic.Water, 5)
                .With_Characteristic(ECharacteristic.Air, 80)
                .With_Characteristic(ECharacteristic.Earth, 15)
                .With_Equipment(sword.BodyPart, sword)
                .Build();
            testHero.Icon = Properties.Resources.FireKnightIcon;
            var templateArema = new Arena(testHero, AlvQuestStatic.TEMPLATE_CHARACTER);
            #endregion
            _arena = templateArema;
            _characterCard = characterCard;
            InitializeComponent();
            // Подписываемся на события мыши
            MouseDown += MainMenuMouseDown;
            MouseMove += MainMenuMouseMove;
            MouseUp += MainMenuMouseUp;
            //Подписываем загрузку и сохрание данных
            Load += EditorJSONDataLoad;
            FormClosing += EditorJSONDataSave;
            //реализуем работу поисковика
            _searchByNameTextBox.TextChanged += SearchByName;
        }

        #region Работа с Json
        //__ЗАГРУЗКА__
        private void EditorJSONDataLoad(object sender, EventArgs e)
        {
            #region Загрузка Эффектов
            // Загрузка из JSON для PassiveParameterModifier
            if (File.Exists(_filePathPPM))
            {
                string jsonPPM = File.ReadAllText(_filePathPPM);
                var dtoList = JsonSerializer.Deserialize<List<PPM_DTO>>(jsonPPM);
                if (dtoList != null)
                {
                    foreach (var dto in dtoList)
                    {
                        var ppm = AlvQuestStatic.DTOConverter.ConvertDTOtoPPM(dto);
                        _effectPanelsList.Add(new EffectPanel(ppm));
                    }
                }
            }
            // Загрузка из JSON для TriggerParameterModifier
            if (File.Exists(_filePathTPM))
            {
                string jsonTPM = File.ReadAllText(_filePathTPM);
                var dtoList = JsonSerializer.Deserialize<List<TPM_DTO>>(jsonTPM);
                if (dtoList != null)
                {
                    foreach (var dto in dtoList)
                    {
                        var tpm = AlvQuestStatic.DTOConverter.ConvertDTOtoTPM(dto);
                        _effectPanelsList.Add(new EffectPanel(tpm));
                    }
                }
            }
            // Загрузка из JSON для LogicalModuleEffect
            /*if (File.Exists(_filePathLME))
            {
                string jsonLME = File.ReadAllText(_filePathLME);
                var dtoList = JsonSerializer.Deserialize<List<LogicalModuleEffect.LME_DTO>>(jsonLME);
                if (dtoList != null)
                {
                    foreach (var dto in dtoList)
                    {
                        var lme = AlvQuestStatic.DTOConverter.ConvertDTOtoLME(dto);
                        _effectPanelsList.Add(new EffectPanel(lme));
                    }
                }
            }*/

            /*_effectPanelsList.Add(new EffectPanel(new TriggerParameterModifier.TPM_Builder()
                .Name("Наростающая ярость.")
                .Description(
                    "При нанесении более 7 едениц физического урона ваша сила увеличиваете на 5% на 2 хода.\n" +
                    "Может складываться до 4х раз.")
                .Icon("FireKnightIcon")
                .TriggerlogicalModule(new LM_02_damageThreshold(damageType: EDamageType.PhysicalDamage, threshold: 7))
                .TicklogicalModule(new LM_CONSTANT_TRUE())
                .Duration(2)
                .MaxStack(4)
                .AddLink(EPlayerType.Self, ECharacteristic.Strength, EDerivative.Value, EVariable.C1)
                .AddValue(0.05)
                .AddTriggerEvent(EPlayerType.Enemy, EEvent.DamageTaking)
                .AddTickEventt(EPlayerType.Self, EEvent.StepExecution)
                .Build()));
            _effectPanelsList.Add(new EffectPanel(new PassiveParameterModifier.PPM_Builder()
                .Name("Живучесть.")
                .Icon("FireKnightIcon")
                .Description("Увеличивает максимальное здоровье на 10.")
                .AddLink(EPlayerType.Self, ECharacteristic.Endurance, EDerivative.MaxHealth, EVariable.C2)
                .AddValue(10)
                .Build()));*/
            #endregion
        }

        //__СОХРАНЕНИЕ__
        private void EditorJSONDataSave(object sender, FormClosingEventArgs e)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true, 
                IncludeFields = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            // Сохранение в JSON для PassiveParameterModifier
            var PPMdtoList = _effectPanelsList
                .Where(ppm => ppm.Effect is PassiveParameterModifier) // Отфильтровать только объекты типа PassiveParameterModifier
                .Select(ppm => ((PassiveParameterModifier)ppm.Effect).GetDTO()) // Привести объекты к типу PassiveParameterModifier и вызвать метод GetDTO()
                .ToList();

            string jsonPPM = JsonSerializer.Serialize(PPMdtoList, options);
            File.WriteAllText(_filePathPPM, jsonPPM);

            // Сохранение в JSON для TriggerParameterModifier
            var TPMdtoList = _effectPanelsList
                .Where(tpm => tpm.Effect is TriggerParameterModifier) // Отфильтровать только объекты типа TriggerParameterModifier
                .Select(tpm => ((TriggerParameterModifier)tpm.Effect).GetDTO()) // Привести объекты к типу TriggerParameterModifier и вызвать метод GetDTO()
                .ToList();

            string jsonTPM = JsonSerializer.Serialize(TPMdtoList, options);
            File.WriteAllText(_filePathTPM, jsonTPM);

            /*// Сохранение в JSON для LogicalModuleEffect
            var LMEdtoList = _effectPanelsList
                .Where(lme => lme.Effect is LogicalModuleEffect) // Отфильтровать только объекты типа LogicalModuleEffect
                .Select(lme => ((LogicalModuleEffect)lme.Effect).GetDTO()) // Привести объекты к типу LogicalModuleEffect и вызвать метод GetDTO()
                .ToList();

            string jsonLME = JsonSerializer.Serialize(LMEdtoList, options);
            File.WriteAllText(_filePathLME, jsonLME);*/
        }
        #endregion

        private void SearchByName(object sender, EventArgs e)
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
        }

        #region Обработка кнопок
        private void UpdateCardButton_Click(object sender, EventArgs e)
        {
            _characterCard.UpdateCard(_arena._player);
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
            UpdateButtons(sender as Button);
            DisplayAllEffectPanels();
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
            if(_searchModeButton.Text == "Категория")
            {
                ResetButtonsColors();
                _searchModeButton.Text = "Все объекты";
            }
            else
            {
                _searchModeButton.Text = "Категория";
            }
        }
        #endregion
    }












}
