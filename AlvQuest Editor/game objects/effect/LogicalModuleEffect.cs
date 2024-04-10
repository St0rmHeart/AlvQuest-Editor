using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuest_Editor
{
    public class LogicalModuleEffect
    {
        private LogicalModuleEffect(
            string name,
            string description,
            LogicalModule triggerlogicalModule,
            LogicalModule ticklogicalModule,
            List<(EPlayerType, EEvent)> triggerEvents,
            List<(EPlayerType, EEvent)> tickEvents)
        {
            Name = name;
            Description = description;
            _triggerlogicalModule = triggerlogicalModule;
            _ticklogicalModule = ticklogicalModule;
            _triggerEvents = triggerEvents;
            _tickEvents = tickEvents;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly LogicalModule _triggerlogicalModule;

        private readonly LogicalModule _ticklogicalModule;

        private readonly List<(EPlayerType target, EEvent type)> _triggerEvents;

        private readonly List<(EPlayerType target, EEvent type)> _tickEvents;

        private bool _isActive;








        public void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            _isActive = false;
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

        public void Activation()
        {
            _isActive = true;
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

        public void Tick()
        {
            _isActive = false;
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

        public LME_DTO GetDTO()
        {
            var dto = new LME_DTO
            {
                Name = Name,
                Description = Description,
                TriggerLogicalModule_DTO = _triggerlogicalModule.GetDTO(),
                TickLogicalModule_DTO = _ticklogicalModule.GetDTO(),
                TriggerEvents = AlvQuestStatic.DTOConverter.ToDTOEventLinkList(_triggerEvents),
                TickEvents = AlvQuestStatic.DTOConverter.ToDTOEventLinkList(_tickEvents)
            };
            return dto;
        }
        /*public IEffect Clone()
        {
            return new LogicalModuleEffect(
                name: Name,
                description: Description,
                triggerlogicalModule: _triggerlogicalModule.Clone(),
                ticklogicalModule: _ticklogicalModule.Clone(),
                triggerEvents: new List<(EPlayerType, EEvent)>(_triggerEvents),
                tickEvents: new List<(EPlayerType, EEvent)>(_tickEvents));
        }*/


        public class LME_DTO
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public LogicalModule_DTO TriggerLogicalModule_DTO { get; set; }
            public LogicalModule_DTO TickLogicalModule_DTO { get; set; }
            public List<Dictionary<string, string>> TriggerEvents { get; set; }
            public List<Dictionary<string, string>> TickEvents { get; set; }
        }


        public class LMEBuilder
        {
            private string _name;
            private string _description;
            private LogicalModule _triggerlogicalModule;
            private LogicalModule _ticklogicalModule;
            private List<(EPlayerType target, EEvent type)> _triggerEvents = new List<(EPlayerType target, EEvent type)>();
            private EPlayerType _targetTriggerEvent;
            private EEvent _eventTypeTriggerEvent;
            private List<(EPlayerType target, EEvent type)> _tickEvents = new List<(EPlayerType target, EEvent type)>();
            private EPlayerType _targetTickEvent;
            private EEvent _eventTypeTickEvent;

            public void Reset()
            {
                _name = null;
                _description = null;
                _triggerlogicalModule = null;
                _ticklogicalModule = null;
                _triggerEvents.Clear();
                _tickEvents.Clear();
                _targetTriggerEvent = EPlayerType.None;
                _eventTypeTriggerEvent = EEvent.None;
                _targetTickEvent = EPlayerType.None;
                _eventTypeTickEvent = EEvent.None;
            }
            public LMEBuilder Name(string value)
            {
                _name = value; return this;
            }
            public LMEBuilder Description(string value)
            {
                _description = value;
                return this;
            }
            public LMEBuilder TriggerlogicalModule(LogicalModule value)
            {
                _triggerlogicalModule = value;
                return this;
            }
            public LMEBuilder TicklogicalModule(LogicalModule value)
            {
                _ticklogicalModule = value;
                return this;
            }
            public LMEBuilder ComposeTriggerEvent(EPlayerType value)
            {
                _targetTriggerEvent = value;
                return this;
            }
            public LMEBuilder ComposeTriggerEvent(EEvent value)
            {
                _eventTypeTriggerEvent = value;
                return this;
            }
            public LMEBuilder AddTriggerEvent()
            {
                if (_targetTriggerEvent == EPlayerType.None || _eventTypeTriggerEvent == EEvent.None)
                {
                    throw new ArgumentException("Один из элементов ссылки не заполнен");
                }

                _triggerEvents.Add((_targetTriggerEvent, _eventTypeTriggerEvent));
                return this;
            }
            public LMEBuilder AddTriggerEvent(EPlayerType target, EEvent triggerEvent)
            {
                if (target == EPlayerType.None || triggerEvent == EEvent.None)
                {
                    throw new ArgumentException("Один из элементов ссылки не заполнен");
                }

                _triggerEvents.Add((target, triggerEvent));
                return this;
            }
            public LMEBuilder ComposeTickEvent(EPlayerType value)
            {
                _targetTickEvent = value;
                return this;
            }
            public LMEBuilder ComposeTickEvent(EEvent value)
            {
                _eventTypeTickEvent = value;
                return this;
            }
            public LMEBuilder AddTickEventt()
            {
                if (_targetTickEvent == EPlayerType.None || _eventTypeTickEvent == EEvent.None)
                {
                    throw new ArgumentException("Один из элементов ссылки не заполнен");
                }

                _tickEvents.Add((_targetTickEvent, _eventTypeTickEvent));
                return this;
            }
            public LMEBuilder AddTickEventt(EPlayerType target, EEvent triggerEvent)
            {
                if (target == EPlayerType.None || triggerEvent == EEvent.None)
                {
                    throw new ArgumentException("Один из элементов ссылки не заполнен");
                }
                    
                _tickEvents.Add((target, triggerEvent));
                return this;
            }
            public LMEBuilder InstallDTO(LME_DTO dto)
            {
                _name = dto.Name;
                _description = dto.Description;
                _triggerlogicalModule = dto.TriggerLogicalModule_DTO.RecreateLogicalModule();
                _ticklogicalModule = dto.TickLogicalModule_DTO.RecreateLogicalModule();
                _triggerEvents = AlvQuestStatic.DTOConverter.FromDTOEventLinkList(dto.TriggerEvents);
                _tickEvents = AlvQuestStatic.DTOConverter.FromDTOEventLinkList(dto.TickEvents);
                return this;
            }

            public LogicalModuleEffect Build()
            {
                if (_name == null) throw new ArgumentException("Отсутствует название эффекта.");
                if (_description == null) throw new ArgumentException("Отсутствует описание эффекта.");
                if (_triggerlogicalModule == null) throw new ArgumentException("Отсутствует логический модуль триггера.");
                if (_ticklogicalModule == null) throw new ArgumentException("Отсутствует логический модуль счетчика.");
                if (_triggerEvents.Count == 0) throw new ArgumentException("Отсутствует сслыка на ивент-триггер эффекта.");
                if (_tickEvents.Count == 0) throw new ArgumentException("Отсутствует сслыка на ивент-счетчик эффекта.");


                return new LogicalModuleEffect(
                name: _name,
                description: _description,
                triggerlogicalModule: _triggerlogicalModule.Clone(),
                ticklogicalModule: _ticklogicalModule.Clone(),
                triggerEvents: new List<(EPlayerType, EEvent)>(_triggerEvents),
                tickEvents: new List<(EPlayerType, EEvent)>(_tickEvents));
            }
        }
    }
}
