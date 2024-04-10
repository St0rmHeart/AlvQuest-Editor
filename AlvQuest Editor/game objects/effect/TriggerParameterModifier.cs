namespace AlvQuest_Editor
{
    public class TPM_DTO : BaseEffect_DTO
    {
        public LogicalModule_DTO TriggerLogicalModule_DTO { get; set; }
        public LogicalModule_DTO TickLogicalModule_DTO { get; set; }
        public int Duration { get; set; }
        public int MaxStack { get; set; }
        public List<double> Values { get; set; } = [];
        public List<Dictionary<string, string>> TriggerEvents { get; set; } = [];
        public List<Dictionary<string, string>> TickEvents { get; set; } = [];
        public List<Dictionary<string, string>> Links { get; set; } = [];
    }
    public class TriggerParameterModifier : BaseEffect
    {
        private readonly LogicalModule _triggerlogicalModule;
        private readonly LogicalModule _ticklogicalModule;
        private readonly int _duration;
        private readonly int _maxStack;
        private readonly List<double> _values;
        private readonly List<(EPlayerType target, EEvent type)> _triggerEvents;
        private readonly List<(EPlayerType target, EEvent type)> _tickEvents;
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable)> _links;
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
            List<double> values,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable)> links,
            List<(EPlayerType, EEvent)> triggerEvents,
            List<(EPlayerType, EEvent)> tickEvents) : base(name, description, iconName)
        {
            _triggerlogicalModule = triggerlogicalModule;
            _ticklogicalModule = ticklogicalModule;
            _duration = duration;
            _maxStack = maxStack;
            _values = values;
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
        #region Реализация функционала потомка
        private void SubscribeTrigger(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) triggerEvent)
        {
            var target = (triggerEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (triggerEvent.type)
            {
                case EEvent.StepExecution: target.StepExecution += Activation; break;
                case EEvent.DamageEmitting: target.DamageEmitting += Activation; break;
                case EEvent.DamageAccepting: target.DamageAccepting += Activation; break;
                case EEvent.DamageBlocking: target.DamageBlocking += Activation; break;
                case EEvent.DamageTaking: target.DamageTaking += Activation; break;
                case EEvent.DeltaFireMana: target.DeltaFireMana += Activation; break;
                case EEvent.DeltaWaterMana: target.DeltaWaterMana += Activation; break;
                case EEvent.DeltaAirMana: target.DeltaAirMana += Activation; break;
                case EEvent.DeltaEarthMana: target.DeltaEarthMana += Activation; break;
                case EEvent.DeltaXP: target.DeltaXP += Activation; break;
                case EEvent.DeltaHP: target.DeltaHP += Activation; break;
                case EEvent.DeltaGold: target.DeltaGold += Activation; break;
                default: throw new NotImplementedException();
            }
        }
        private void SubscribeTick(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) tickEvent)
        {
            var target = (tickEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (tickEvent.type)
            {
                case EEvent.StepExecution: target.StepExecution += Tick; break;
                case EEvent.DamageEmitting: target.DamageEmitting += Tick; break;
                case EEvent.DamageAccepting: target.DamageAccepting += Tick; break;
                case EEvent.DamageBlocking: target.DamageBlocking += Tick; break;
                case EEvent.DamageTaking: target.DamageTaking += Tick; break;
                case EEvent.DeltaFireMana: target.DeltaFireMana += Tick; break;
                case EEvent.DeltaWaterMana: target.DeltaWaterMana += Tick; break;
                case EEvent.DeltaAirMana: target.DeltaAirMana += Tick; break;
                case EEvent.DeltaEarthMana: target.DeltaEarthMana += Tick; break;
                case EEvent.DeltaXP: target.DeltaXP += Tick; break;
                case EEvent.DeltaHP: target.DeltaHP += Tick; break;
                case EEvent.DeltaGold: target.DeltaGold += Tick; break;
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
                        parameter.CurrentValue += _values[i];
                    else
                        _parameters[i].ChangeVariable(_links[i].variable, _values[i]);
                }
                _counterStack++;
            }
        }
        public void Activation(object sender, EEvent arg)
        {
            _triggerlogicalModule.SetData(arg);
            if (_triggerlogicalModule.Result()) Activation();
        }
        public void Activation(object sender, (EEvent eEvent, EDamageType damageType, double value) arg)
        {
            _triggerlogicalModule.SetData(arg);
            if (_triggerlogicalModule.Result()) Activation();
        }
        public void Activation(object sender, (EEvent eEvent, double args) arg)
        {
            _triggerlogicalModule.SetData(arg);
            if (_triggerlogicalModule.Result()) Activation();
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
                    _parameters[i].ChangeVariable(_links[i].variable, - (_values[i]) * _counterStack);
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
                Values = new List<double>(_values),
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
                values: new List<double>(_values),
                links: new List<(EPlayerType, ECharacteristic, EDerivative, EVariable)>(_links),
                triggerEvents: new List<(EPlayerType, EEvent)>(_triggerEvents),
                tickEvents: new List<(EPlayerType, EEvent)>(_tickEvents));
        }
        

        public class TPM_Builder : BaseEffect_Builder<TPM_Builder, TriggerParameterModifier, TPM_DTO>
        {
            public TPM_Builder TriggerlogicalModule(LogicalModule value)
            {
                _effectData.TriggerLogicalModule_DTO = value.GetDTO();
                return this;
            }
            public TPM_Builder TicklogicalModule(LogicalModule value)
            {
                _effectData.TickLogicalModule_DTO = value.GetDTO();
                return this;
            }
            public TPM_Builder Duration(int value)
            {
                _effectData.Duration = value;
                return this;
            }
            public TPM_Builder MaxStack(int value)
            {
                _effectData.MaxStack = value;
                return this;
            }
            public TPM_Builder AddValue(double value)
            {
                _effectData.Values.Add(value);
                return this;
            }
            public TPM_Builder AddLink(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable)
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
            public TPM_Builder AddTriggerEvent(EPlayerType target, EEvent triggerEvent)
            {
                if (target == EPlayerType.None || triggerEvent == EEvent.None) throw new ArgumentException("Значение None недопустимо");
                var newLink = (target, triggerEvent);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOEventLink(newLink);
                bool dictionaryExists = _effectData.TriggerEvents.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _effectData.TriggerEvents.Add(newDTOLink);
                return this;
            }
            public TPM_Builder AddTickEventt(EPlayerType target, EEvent tickEvent)
            {
                if (target == EPlayerType.None || tickEvent == EEvent.None) throw new ArgumentException("Значение None недопустимо");
                var newLink = (target, tickEvent);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOEventLink(newLink);
                bool dictionaryExists = _effectData.TickEvents.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _effectData.TickEvents.Add(newDTOLink);
                return this;
            }
            protected override void ValidateAdditionalContent()
            {
                if (_effectData.TriggerLogicalModule_DTO == null) throw new ArgumentException("Не указан логический модуль триггера");
                if (_effectData.TickLogicalModule_DTO == null) throw new ArgumentException("Не указан логический модуль тика");
                if (_effectData.Duration == 0) throw new ArgumentException("Не указана длительность");
                if (_effectData.MaxStack == 0) throw new ArgumentException("Не указан максимальный стак");
                if (_effectData.Values?.Count == 0) throw new ArgumentException("Не указано значение");
                if (_effectData.Links?.Count == 0) throw new ArgumentException("Не указана ссылка");
                if (_effectData.TriggerEvents?.Count == 0) throw new ArgumentException("Не указан ивент-триггер");
                if (_effectData.TickEvents?.Count == 0) throw new ArgumentException("Не указан ивент-тик");
            }
            protected override TriggerParameterModifier Construct()
            {
                return new TriggerParameterModifier(
                    name: _effectData.BaseData.Name,
                    description: _effectData.BaseData.Description,
                    iconName: _effectData.BaseData.IconName,
                    triggerlogicalModule: _effectData.TriggerLogicalModule_DTO.RecreateLogicalModule(),
                    ticklogicalModule: _effectData.TickLogicalModule_DTO.RecreateLogicalModule(),
                    duration: _effectData.Duration,
                    maxStack: _effectData.MaxStack,
                    values: new List<double>(_effectData.Values),
                    links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(_effectData.Links),
                    triggerEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(_effectData.TriggerEvents),
                    tickEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(_effectData.TickEvents));
            }
        }
    }
}
