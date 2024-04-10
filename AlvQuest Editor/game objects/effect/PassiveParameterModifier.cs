namespace AlvQuest_Editor
{
    /// <summary>
    /// Data Transfer Object версия класса PassiveParameterModifier
    /// </summary>
    public class PPM_DTO : BaseEffect_DTO
    {
        public List<double> Values { get; set; } = [];
        public List<Dictionary<string, string>> Links { get; set; } = [];
    }

    /// <summary>
    /// Эффект, модифицирующий набор указанных переменных в указанных параметрах в начале сражения. Может либо мофицировать все ссылки одним значением,
    /// либо модифицировать каждую ссылку своим уникальным значением
    /// </summary>
    public class PassiveParameterModifier : BaseEffect
    {
        private readonly List<double> _values;
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable)> _links;
        private PassiveParameterModifier(
            string name,
            string description,
            string iconName,
            List<double> values,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable)> links) : base(name, description, iconName)
        {
            _values = values;
            _links = links;
        }
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            for (int i = 0; i < _values.Count; i++)
            {
                var link = _links[i];
                var target = (link.target == EPlayerType.Self) ? owner : enemy;
                var currentParameter = target.Data[link.characteristic][link.derivative];
                currentParameter.ChangeVariable(link.variable, _values[i]);
            }
        }
        public override PassiveParameterModifier Clone()
        {
            return new PassiveParameterModifier(
                name: Name,
                description: Description,
                iconName: IconName,
                new List<double>(_values),
                new List<(EPlayerType, ECharacteristic, EDerivative, EVariable)>(_links));
        }
        public override PPM_DTO GetDTO()
        {
            var dto = new PPM_DTO
            {
                BaseData = GetBaseData(),
                Values = new List<double>(_values),
                Links = AlvQuestStatic.DTOConverter.ToDTOImpactLinkList(_links)
            };
            return dto;
        }

        /// <summary>
        /// Вложенный класс-строитель объектов класса PassiveParameterModifier
        /// </summary>
        public class PPM_Builder : BaseEffect_Builder<PPM_Builder, PassiveParameterModifier, PPM_DTO>
        {
            public PPM_Builder AddLink(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable)
            {
                if (target == EPlayerType.None || characteristic == ECharacteristic.None || derivative == EDerivative.None || variable == EVariable.None)
                {
                    throw new ArgumentException("Значение None недопустимо");
                }
                if (!AlvQuestStatic.CHAR_DER_PAIRS[characteristic].Contains(derivative))
                {
                    throw new ArgumentException("Невозможная ссылка. У " + nameof(characteristic) + " нет производной " + nameof(derivative) + ".");
                }
                var newLink = (target, characteristic, derivative, variable);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOImpactLink(newLink);
                bool dictionaryExists = _effectData.Links.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _effectData.Links.Add(newDTOLink);
                return this;
            }
            public PPM_Builder AddValue(double value)
            {
                _effectData.Values.Add(value);
                return this;
            }
            protected override void ValidateAdditionalContent()
            {
                if (_effectData.Values?.Count == 0) throw new ArgumentException("Отсутствует значение эффекта.");
                if (_effectData.Links?.Count == 0) throw new ArgumentException("Отсутствует ссылка.");
            }
            protected override PassiveParameterModifier Construct()
            {
                List<double> values;
                if (_effectData.Values.Count == 1)
                {
                    values = new List<double>(new int[_effectData.Links.Count].Select(x => _effectData.Values[0]));
                }
                else
                {
                    if (_effectData.Values.Count != _effectData.Links.Count)
                    {
                        throw new ArgumentException("Values.Count должно быть либо равно 1, рибо быть равным Links.Count");
                    }
                    values = new List<double>(_effectData.Values);
                }
                var links = AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(_effectData.Links);
                return new PassiveParameterModifier(
                    name: _effectData.BaseData.Name,
                    description: _effectData.BaseData.Description,
                    iconName: _effectData.BaseData.IconName,
                    values: values,
                    links: new List<(EPlayerType, ECharacteristic, EDerivative, EVariable)>(links));
            }
        }
    }
}
