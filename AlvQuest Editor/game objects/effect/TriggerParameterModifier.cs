namespace AlvQuest_Editor
{
    public class TriggerParameterModifier : BaseEffect
    {
        private readonly LogicalModule _triggerlogicalModule;
        private readonly LogicalModule _ticklogicalModule;
        private readonly int _duration;
        private readonly int _maxStack;
        private readonly List<(EPlayerType target, EEvent type)> _triggerEvents;
        private readonly List<(EPlayerType target, EEvent type)> _tickEvents;
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> _links;
        private readonly List<Parameter> _parameters = [];
        private bool _isActive;
        private int _counterTick;
        private int _counterStack;
        private TriggerParameterModifier(
            string name,
            string description,
            string iconName,
            LogicalModule triggerlogicalModule,
            LogicalModule ticklogicalModule,
            int duration,
            int maxStack,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)> links,
            List<(EPlayerType, EEvent)> triggerEvents,
            List<(EPlayerType, EEvent)> tickEvents) : base(name, description, iconName)
        {
            _triggerlogicalModule = triggerlogicalModule;
            _ticklogicalModule = ticklogicalModule;
            _duration = duration;
            _maxStack = maxStack;
            _links = links;
            _triggerEvents = triggerEvents;
            _tickEvents = tickEvents;
        }
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            _isActive = false;

            for (int i = 0; i < _links.Count; i++)
            {
                var link = _links[i];
                var target = (link.target == EPlayerType.Self) ? owner : enemy;
                var currentParameter = target.Data[link.characteristic][link.derivative];
                _parameters.Add(currentParameter);
            }
            foreach (var triggerEvent in _triggerEvents)
            {
                SubscribeTrigger(owner, enemy, triggerEvent);
            }
            foreach (var tickEvent in _tickEvents)
            {
                SubscribeTick(owner, enemy, tickEvent);
            }
            _triggerlogicalModule.Installation(owner, enemy);
            _ticklogicalModule.Installation(owner, enemy);
        }
        public override void Uninstallation()
        {
            _parameters.Clear();
            _triggerlogicalModule.Uninstallation();
            _ticklogicalModule.Uninstallation();
        }
        #region Реализация функционала потомка
        private void SubscribeTrigger(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) triggerEvent)
        {
            var target = (triggerEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (triggerEvent.type)
            {
                default: throw new NotImplementedException();
            }
        }
        private void SubscribeTick(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) tickEvent)
        {
            var target = (tickEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (tickEvent.type)
            {
                default: throw new NotImplementedException();
            }
        }
        private void Activation()
        {
            _isActive = true;
            _counterTick = _duration;
            if (_counterStack < _maxStack)
            {
                for (int i = 0; i < _parameters.Count; i++)
                {
                    if (_parameters[i] is CurrentCommonParameter parameter)
                        parameter.CurrentValue += _links[i].value;
                    else
                        _parameters[i].ChangeVariable(_links[i].variable, _links[i].value);
                }
                _counterStack++;
            }
        }
        public void Activation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        public void Activation(object sender, (EDamageType DamageType, double Value) arg)
        {
            throw new NotImplementedException();
        }
        public void Activation(object sender, (EEvent Event, double Args) arg)
        {
            throw new NotImplementedException();
        }
        private void Tick()
        {
            if (_counterTick > 1)
            {
                _counterTick--;
            }
            else
            {
                for (int i = 0; i < _parameters.Count; i++)
                {
                    _parameters[i].ChangeVariable(_links[i].variable, - (_links[i].value) * _counterStack);
                }
                _isActive = false;
                _counterStack = 0;
            }
        }
        public void Tick(object sender, EEvent arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }
        public void Tick(object sender, (EEvent eEvent, EDamageType damageType, double value) arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }
        public void Tick(object sender, (EEvent eEvent, double args) arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }
        #endregion
        public override TPM_DTO GetDTO()
        {
            var dto = new TPM_DTO
            {
                BaseData = GetBaseData(),
                TriggerLogicalModule_DTO = _triggerlogicalModule.GetDTO(),
                TickLogicalModule_DTO = _ticklogicalModule.GetDTO(),
                Duration = _duration,
                MaxStack = _maxStack,
                Links = AlvQuestStatic.DTOConverter.ToDTOImpactLinkList(_links),
                TriggerEvents = AlvQuestStatic.DTOConverter.ToDTOEventLinkList(_triggerEvents),
                TickEvents = AlvQuestStatic.DTOConverter.ToDTOEventLinkList(_tickEvents)
            };
            return dto;
        }
        public override TriggerParameterModifier Clone()
        {
            return new TriggerParameterModifier(
                name: Name,
                description: Description,
                iconName: IconName,
                triggerlogicalModule: _triggerlogicalModule.Clone(),
                ticklogicalModule: _ticklogicalModule.Clone(),
                duration: _duration,
                maxStack: _maxStack,
                links: new List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)>(_links),
                triggerEvents: new List<(EPlayerType, EEvent)>(_triggerEvents),
                tickEvents: new List<(EPlayerType, EEvent)>(_tickEvents));
        }

        public class TPM_DTO : BaseEffectDTO
        {
            public LogicalModule_DTO TriggerLogicalModule_DTO { get; set; }
            public LogicalModule_DTO TickLogicalModule_DTO { get; set; }
            public int Duration { get; set; }
            public int MaxStack { get; set; }
            public List<Dictionary<string, string>> TriggerEvents { get; set; } = [];
            public List<Dictionary<string, string>> TickEvents { get; set; } = [];
            public List<Dictionary<string, string>> Links { get; set; } = [];
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = base.GetHashCode();
                    hashCode ^= TriggerLogicalModule_DTO.GetHashCode();
                    hashCode ^= TickLogicalModule_DTO.GetHashCode();
                    hashCode ^= Duration.GetHashCode();
                    hashCode ^= MaxStack.GetHashCode();
                    hashCode ^= AlvQuestStatic.GetHashCode(TriggerEvents);
                    hashCode ^= AlvQuestStatic.GetHashCode(TickEvents);
                    hashCode ^= AlvQuestStatic.GetHashCode(Links);
                    return hashCode;
                }
            }
            public override TriggerParameterModifier RecreateOriginal()
            {
                return new TriggerParameterModifier(
                    name: BaseData.Name,
                    description: BaseData.Description,
                    iconName: BaseData.Icon,
                    triggerlogicalModule: TriggerLogicalModule_DTO.RecreateLogicalModule(),
                    ticklogicalModule: TickLogicalModule_DTO.RecreateLogicalModule(),
                    duration: Duration,
                    maxStack: MaxStack,
                    links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(Links),
                    triggerEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(TriggerEvents),
                    tickEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(TickEvents));
            }
        }

        public class TPM_Builder : BaseBuilder<TPM_Builder, TriggerParameterModifier, TPM_DTO>
        {
            public TPM_Builder SetTriggerlogicalModule(LogicalModule value)
            {
                _entityData.TriggerLogicalModule_DTO = value.GetDTO();
                return this;
            }
            public TPM_Builder SetTicklogicalModule(LogicalModule value)
            {
                _entityData.TickLogicalModule_DTO = value.GetDTO();
                return this;
            }
            public TPM_Builder SetDuration(int value)
            {
                _entityData.Duration = value;
                return this;
            }
            public TPM_Builder SetMaxStack(int value)
            {
                _entityData.MaxStack = value;
                return this;
            }
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
