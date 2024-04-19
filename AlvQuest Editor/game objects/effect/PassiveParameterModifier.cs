using System;
using System.Security.Policy;

namespace AlvQuest_Editor
{
    /// <summary>
    /// Эффект, модифицирующий набор указанных переменных в указанных параметрах в начале сражения. Может либо мофицировать все ссылки одним значением,
    /// либо модифицировать каждую ссылку своим уникальным значением
    /// </summary>
    public class PassiveParameterModifier : BaseEffect
    {
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> _links;
        private PassiveParameterModifier(
            string name,
            string description,
            string iconName,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)> links) : base(name, description, iconName)
        {
            _links = links;
        }
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            for (int i = 0; i < _links.Count; i++)
            {
                var link = _links[i];
                var target = (link.target == EPlayerType.Self) ? owner : enemy;
                var currentParameter = target.Data[link.characteristic][link.derivative];
                currentParameter.ChangeVariable(link.variable, link.value);
            }
        }
        public override void Uninstallation()
        {
            //никаких действий не требуется, так как объект не формирует никаких связей в методе Installation
        }

        public override PassiveParameterModifier Clone()
        {
            return new PassiveParameterModifier(
                name: Name,
                description: Description,
                iconName: IconName,
                new List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)>(_links));
        }
        public override PPM_DTO GetDTO()
        {
            var dto = new PPM_DTO
            {
                BaseData = GetBaseData(),
                Links = AlvQuestStatic.DTOConverter.ToDTOImpactLinkList(_links)
            };
            return dto;
        }

        /// <summary>
        /// Вложенная Data Transfer Object версия класса PassiveParameterModifier
        /// </summary>
        public class PPM_DTO : BaseEffectDTO
        {
            public List<Dictionary<string, string>> Links { get; set; } = [];
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = base.GetHashCode();
                    hashCode ^= AlvQuestStatic.GetHashCode(Links);
                    return hashCode;
                }
            }
            public override PassiveParameterModifier RecreateOriginal()
            {
                return new PassiveParameterModifier(
                        name: BaseData.Name,
                        description: BaseData.Description,
                        iconName: BaseData.IconName,
                        links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(Links));
            }
        }
        /// <summary>
        /// Вложенный класс-строитель объектов класса PassiveParameterModifier
        /// </summary>
        public class PPM_Builder : BaseBuilder<PPM_Builder, PassiveParameterModifier, PPM_DTO>
        {
            public PPM_Builder SetLink(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)
            {
                if (target == EPlayerType.None || characteristic == ECharacteristic.None || derivative == EDerivative.None || variable == EVariable.None || value == 0)
                {
                    throw new ArgumentException("Ссылка некорректно заполнена");
                }
                if (!AlvQuestStatic.CHAR_DER_PAIRS[characteristic].Contains(derivative))
                {
                    throw new ArgumentException("Невозможная ссылка. У " + nameof(characteristic) + " нет производной " + nameof(derivative) + ".");
                }
                var newLink = (target, characteristic, derivative, variable, value);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOImpactLink(newLink);
                bool dictionaryExists = _entityData.Links.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _entityData.Links.Add(newDTOLink);
                return this;
            }
            protected override void ValidateAdditionalContent()
            {
                if (_entityData.Links?.Count == 0) throw new ArgumentException("Отсутствует ссылка.");
            }
        }
    }
}
