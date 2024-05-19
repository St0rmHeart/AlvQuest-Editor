namespace AlvQuest_Editor
{
    public partial class PassiveParameterModifier
    {
        /// <summary>
        /// Вложенный класс-строитель объектов класса PassiveParameterModifier
        /// </summary>
        public class PPM_Builder : BGO_Builder<PPM_Builder, PassiveParameterModifier, PPM_DTO>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="target"></param>
            /// <param name="characteristic"></param>
            /// <param name="derivative"></param>
            /// <param name="variable"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
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
            /// <summary>
            /// 
            /// </summary>
            /// <exception cref="ArgumentException"></exception>
            protected override void ValidateAdditionalContent()
            {
                if (_entityData.Links?.Count == 0) throw new ArgumentException("Отсутствует ссылка.");
            }
        }
    }
}
