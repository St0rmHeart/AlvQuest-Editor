namespace AlvQuest_Editor
{
    public partial class TriggerParameterModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public class TPM_Builder : BGO_Builder<TPM_Builder, TriggerParameterModifier, TPM_DTO>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetTriggerlogicalModule(LogicalModule value)
            {
                _entityData.TriggerLogicalModule_DTO = value.GetDTO();
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetTicklogicalModule(LogicalModule value)
            {
                _entityData.TickLogicalModule_DTO = value.GetDTO();
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetDuration(int value)
            {
                _entityData.Duration = value;
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetMaxStack(int value)
            {
                _entityData.MaxStack = value;
                return this;
            }

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
            public TPM_Builder SetLink(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)
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
            /// <param name="target"></param>
            /// <param name="triggerEvent"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public TPM_Builder SetTriggerEvent(EPlayerType target, EEvent triggerEvent)
            {
                if (target == EPlayerType.None || triggerEvent == EEvent.None) throw new ArgumentException("Значение None недопустимо");
                var newLink = (target, triggerEvent);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOEventLink(newLink);
                bool dictionaryExists = _entityData.TriggerEvents.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _entityData.TriggerEvents.Add(newDTOLink);
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target"></param>
            /// <param name="tickEvent"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public TPM_Builder SetTickEventt(EPlayerType target, EEvent tickEvent)
            {
                if (target == EPlayerType.None || tickEvent == EEvent.None) throw new ArgumentException("Значение None недопустимо");
                var newLink = (target, tickEvent);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOEventLink(newLink);
                bool dictionaryExists = _entityData.TickEvents.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _entityData.TickEvents.Add(newDTOLink);
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <exception cref="ArgumentException"></exception>
            protected override void ValidateAdditionalContent()
            {
                if (_entityData.TriggerLogicalModule_DTO == null) throw new ArgumentException("Не указан логический модуль триггера");
                if (_entityData.TickLogicalModule_DTO == null) throw new ArgumentException("Не указан логический модуль тика");
                if (_entityData.Duration == 0) throw new ArgumentException("Не указана длительность");
                if (_entityData.MaxStack == 0) throw new ArgumentException("Не указан максимальный стак");
                if (_entityData.Links?.Count == 0) throw new ArgumentException("Не указана ссылка");
                if (_entityData.TriggerEvents?.Count == 0) throw new ArgumentException("Не указан ивент-триггер");
                if (_entityData.TickEvents?.Count == 0) throw new ArgumentException("Не указан ивент-тик");
            }
        }
    }
}
