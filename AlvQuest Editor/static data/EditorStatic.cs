﻿using System.Text.Encodings.Web;
using System.Text.Json;

namespace AlvQuest_Editor
{
    public static class EditorStatic
    {
        public static string FilePathPPM { get; } = "../../../Resources/json data/effectsPPM.json";
        public static string FilePathTPM { get; } = "json data/effectsTPM.json";
        public static Arena Arena { get; set; }
        public static List<PPMPanel> PPMPanelsList { get; } = new();
        public static MainMenu MainMenu { get; } = new();
        public static CharacterCard CharacterCard { get; } = new();
        public static PPMCreationForm PPMCreationForm { get; } = new();
        public static IconSelectionForm IconSelectionForm { get; } = new();

        static EditorStatic()
        {
            #region GameData
            //var CBuilder = new Character.CharacterBuilder();
            var PPMBuilder = new PassiveParameterModifier.PPM_Builder();
            var TPMBuilder = new TriggerParameterModifier.TPM_Builder();

            var newPassiveEffect = PPMBuilder
                .SetName("Живучесть.")
                .SetDescription("Увеличивает максимальное здоровье на 10.")
                .SetIcon("FireKnightIcon")
                .SetLink(EPlayerType.Self, ECharacteristic.Endurance, EDerivative.MaxHealth, EVariable.C2, 10)
                .Build();

            var newTriggerParameter = TPMBuilder
                .SetName("Нарастающая ярость.")
                .SetDescription(
                    "При нанесении более 7 единиц физического урона ваша сила увеличивается на 5% на 2 хода.\n" +
                    "Может складываться до 4х раз.")
                .SetIcon("FireKnightIcon")
                .SetTriggerlogicalModule(new LM_02_damageThreshold(damageType: EDamageType.PhysicalDamage, threshold: 7))
                .SetTicklogicalModule(new LM_CONSTANT_TRUE())
                .SetDuration(2)
                .SetMaxStack(4)
                .SetLink(EPlayerType.Self, ECharacteristic.Strength, EDerivative.Value, EVariable.C1, 0.05)
                .SetTriggerEvent(EPlayerType.Enemy, EEvent.DamageTaking)
                .SetTickEventt(EPlayerType.Self, EEvent.StepExecution)
                .Build();
            #endregion

            JsonLoad();
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => JsonSave();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void JsonLoad()
        {
            //__ЗАГРУЗКА__
            #region Загрузка Эффектов
            // Загрузка из JSON для PassiveParameterModifier
            if (File.Exists(FilePathPPM))
            {
                string jsonPPM = File.ReadAllText(FilePathPPM);
                var PPMdtoList = JsonSerializer.Deserialize<List<PassiveParameterModifier.PPM_DTO>>(jsonPPM);
                PPMdtoList?.ForEach(dto => PPMPanelsList.Add(new PPMPanel(dto)));
            }
            /*// Загрузка из JSON для TriggerParameterModifier
            if (File.Exists(_filePathTPM))
            {
                string jsonTPM = File.ReadAllText(_filePathTPM);
                var dtoList = JsonSerializer.Deserialize<List<TriggerParameterModifier.TPM_DTO>>(jsonTPM);
                if (dtoList != null)
                {
                    foreach (var dto in dtoList)
                    {
                        var tpm = AlvQuestStatic.DTOConverter.ConvertDTOtoTPM(dto);
                        _effectPanelsList.Add(new EffectPanel(tpm));
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
        public static void JsonSave()
        {  
            //__СОХРАНЕНИЕ__
            #region Сохранение Эффектов
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            // Сохранение в JSON для PassiveParameterModifier
            var PPMdtoList = PPMPanelsList.Select(ppm => ppm.DTO).ToList();
            string jsonPPM = JsonSerializer.Serialize(PPMdtoList, options);
            File.WriteAllText(FilePathPPM, jsonPPM);

            /*// Сохранение в JSON для TriggerParameterModifier
            var TPMdtoList = _effectPanelsList
                .Where(tpm => tpm.Effect is TriggerParameterModifier) // Отфильтровать только объекты типа TriggerParameterModifier
                .Select(tpm => ((TriggerParameterModifier)tpm.Effect).GetDTO()) // Привести объекты к типу TriggerParameterModifier и вызвать метод GetDTO()
                .ToList();
            string jsonTPM = JsonSerializer.Serialize(TPMdtoList, options);
            File.WriteAllText(_filePathTPM, jsonTPM);*/
            #endregion
        }
    }
}
 