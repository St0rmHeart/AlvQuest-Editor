namespace AlvQuest_Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TriggerParameterModifier : BaseEffect
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly LogicalModule _triggerlogicalModule;

        /// <summary>
        /// 
        /// </summary>
        private readonly LogicalModule _ticklogicalModule;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _duration;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _maxStack;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<(EPlayerType target, EEvent type)> _triggerEvents;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<(EPlayerType target, EEvent type)> _tickEvents;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> _links;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<Parameter> _parameters = new();

        /// <summary>
        /// 
        /// </summary>
        private bool _isActive;

        /// <summary>
        /// 
        /// </summary>
        private int _counterTick;

        /// <summary>
        /// 
        /// </summary>
        private int _counterStack;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="triggerlogicalModule"></param>
        /// <param name="ticklogicalModule"></param>
        /// <param name="duration"></param>
        /// <param name="maxStack"></param>
        /// <param name="links"></param>
        /// <param name="triggerEvents"></param>
        /// <param name="tickEvents"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public override void Uninstallation()
        {
            _parameters.Clear();
            _triggerlogicalModule.Uninstallation();
            _ticklogicalModule.Uninstallation();
        }
        #region Реализация функционала потомка
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        /// <param name="triggerEvent"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SubscribeTrigger(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) triggerEvent)
        {
            var target = (triggerEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (triggerEvent.type)
            {
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        /// <param name="tickEvent"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SubscribeTick(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) tickEvent)
        {
            var target = (tickEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (tickEvent.type)
            {
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Activation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Activation(object sender, (EDamageType DamageType, double Value) arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Activation(object sender, (EEvent Event, double Args) arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void Tick(object sender, EEvent arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void Tick(object sender, (EEvent eEvent, EDamageType damageType, double value) arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void Tick(object sender, (EEvent eEvent, double args) arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TriggerParameterModifier Clone()
        {
            return new TriggerParameterModifier(
                name: Name,
                description: Description,
                iconName: Icon,
                triggerlogicalModule: _triggerlogicalModule.Clone(),
                ticklogicalModule: _ticklogicalModule.Clone(),
                duration: _duration,
                maxStack: _maxStack,
                links: new List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)>(_links),
                triggerEvents: new List<(EPlayerType, EEvent)>(_triggerEvents),
                tickEvents: new List<(EPlayerType, EEvent)>(_tickEvents));
        }
    }
}
