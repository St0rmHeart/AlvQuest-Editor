﻿using System.Text.Json.Serialization;

namespace AlvQuest_Editor
{
    //абстрактный предок всех логических модулей
    public abstract class LogicalModule
    {
        protected CharacterSlot _owner;
        protected CharacterSlot _enemy;
        //StepExecution, Death,
        protected EEvent? _simpleData;
        //DeltaFireMana, DeltaWaterMana, DeltaAirMana, DeltaEarthMana, DeltaXP, DeltaHP, DeltaGold
        protected (EEvent eEvent, double value)? _deltaData;
        //DamageEmitting, DamageAccepting, DamageBlocking, DamageTaking,
        protected (EEvent eEvent, EDamageType damageType, double value)? _damageData;
   
        public void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            _owner = owner;
            _enemy = enemy;
        }
        public void Uninstallation()
        {
            _owner = null;
            _enemy = null;
        }
        public void SetData(EEvent arg)
        {
            _simpleData = arg;
            _deltaData = null;
            _damageData = null;
        }
        public void SetData((EEvent eEvent, double args) arg)
        {
            _simpleData = null;
            _deltaData = arg;
            _damageData = null;
        }
        public void SetData((EEvent eEvent, EDamageType damageType, double value) arg)
        {
            _simpleData = null;
            _deltaData = null;
            _damageData = arg;
        }
        public abstract bool Result();
        public abstract LogicalModule Clone();
        public abstract LogicalModule_DTO GetDTO();
    }

    [JsonDerivedType(typeof(LM_CONSTANT_TRUE_DTO), typeDiscriminator: "CONSTANT_TRUE")]
    [JsonDerivedType(typeof(LM_01_deltaThreshold_DTO), typeDiscriminator: "deltaThreshold")]
    [JsonDerivedType(typeof(LM_02_damageThreshold_DTO), typeDiscriminator: "damageThreshold")]
    [JsonDerivedType(typeof(LM_03_manaShieldTickModule_DTO), typeDiscriminator: "manaShieldTickModule")]
    public abstract class LogicalModule_DTO
    {
        public abstract LogicalModule RecreateLogicalModule();
    }

    /// <summary>
    /// Модуль, всегда возвращающий True
    /// </summary>
    public class LM_CONSTANT_TRUE : LogicalModule
    {
        public override bool Result()
        {
            return true;
        }
        public override LogicalModule Clone()
        {
            return new LM_CONSTANT_TRUE();
        }
        public override LM_CONSTANT_TRUE_DTO GetDTO()
        {
            return new LM_CONSTANT_TRUE_DTO();
        }
    }
    public class LM_CONSTANT_TRUE_DTO : LogicalModule_DTO
    {
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_CONSTANT_TRUE();
        }
    }


    /// <summary>
    /// _deltaData должен содержать value больше, чем _threshold. Иначе возвращается False
    /// </summary>
    public class LM_01_deltaThreshold : LogicalModule
    {
        private readonly double _threshold;
        public LM_01_deltaThreshold(double threshold)
        {
            _threshold = threshold;
        }
        public override bool Result()
        {
            return _deltaData?.value >= _threshold;
        }
        public override LogicalModule Clone()
        {
            return new LM_01_deltaThreshold(_threshold);
        }
        public override LogicalModule_DTO GetDTO()
        {
            var dto = new LM_01_deltaThreshold_DTO
            {
                Threshold = _threshold
            };
            return dto;
        }
    }
    public class LM_01_deltaThreshold_DTO : LogicalModule_DTO
    {
        public double Threshold { get; set; }
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_01_deltaThreshold(Threshold);
        }
    }

    /// <summary>
    /// _deltaData должен содержать value больше, чем _threshold. Иначе возвращается False
    /// </summary>
    public class LM_02_damageThreshold : LogicalModule
    {
        private readonly EDamageType _damageType;
        private readonly double _threshold;
        public LM_02_damageThreshold(EDamageType damageType, double threshold)
        {
            _damageType = damageType; _threshold = threshold;
        }
        public override bool Result()
        {
            return _damageData?.damageType == _damageType && _damageData?.value >= _threshold;
        }
        public override LogicalModule Clone()
        {
            return new LM_02_damageThreshold(_damageType, _threshold);
        }
        public override LogicalModule_DTO GetDTO()
        {
            var dto = new LM_02_damageThreshold_DTO
            {
                DamageType = _damageType,
                Threshold = _threshold
            };
            return dto;
        }
    }
    public class LM_02_damageThreshold_DTO : LogicalModule_DTO
    {
        public EDamageType DamageType { get; set; }
        public double Threshold { get; set; }
        public LM_02_damageThreshold_DTO() { }
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_02_damageThreshold(DamageType, Threshold);
        }
    }

    /// <summary>
    /// Логический модуль, реализующий магический щит и выключающий эффект, только когда щит будет разрушен.
    /// </summary>
    public class LM_03_manaShieldTickModule : LogicalModule
    {
        private readonly ECharacteristic _element;
        private readonly double _costOfMaintenance; 
        private readonly Dictionary<EDamageType, double> _damageMultipliers = new Dictionary<EDamageType, double>();

        /// <param name="element">Элемен магического щита, соответвующий одному из 4х элементов маны.</param>
        /// <param name="costOfMaintenance">Стоимость поддержания щита каждый ход.</param>
        /// <param name="physicalDamageMultiplier"></param>
        /// <param name="fireDamageMultiplier"></param>
        /// <param name="waterDamageMultiplier"></param>
        /// <param name="airDamageMultiplier"></param>
        /// <param name="earthDamageMultiplier"></param>
        public LM_03_manaShieldTickModule(
            ECharacteristic element,
            double costOfMaintenance,
            double physicalDamageMultiplier = 1,
            double fireDamageMultiplier = 1,
            double waterDamageMultiplier = 1,
            double airDamageMultiplier = 1,
            double earthDamageMultiplier = 1)
        {
            if(element != ECharacteristic.Fire &&
                element != ECharacteristic.Water &&
                element != ECharacteristic.Earth &&
                element != ECharacteristic.Air)
            {
                throw new ArgumentException("Невозможный элемент щита.");
            }
            if(costOfMaintenance <= 0) throw new ArgumentException("costOfMaintenance должен быть больше 0");
            _element = element;
            _costOfMaintenance = costOfMaintenance;
            _damageMultipliers.Add(EDamageType.PhysicalDamage, physicalDamageMultiplier);
            _damageMultipliers.Add(EDamageType.FireDamage, fireDamageMultiplier);
            _damageMultipliers.Add(EDamageType.WaterDamage, waterDamageMultiplier);
            _damageMultipliers.Add(EDamageType.AirDamage, airDamageMultiplier);
            _damageMultipliers.Add(EDamageType.EarthDamage, earthDamageMultiplier);
        }
        public LM_03_manaShieldTickModule(
            ECharacteristic element,
            double costOfMaintenance,
            Dictionary<EDamageType, double> damageMultipliers)
        {
            _element = element;
            _costOfMaintenance = costOfMaintenance;
            _damageMultipliers = new Dictionary<EDamageType, double>(damageMultipliers);
        }
        public override bool Result()
        {
            if (_simpleData != null)
            {
                var actualСhange = _owner.ChangeMp(_element, -_costOfMaintenance);
                return actualСhange != -_costOfMaintenance;
            }
            else if (_damageData != null)
            {
                var incomingToShieldDamage = _damageData.Value.value * _damageMultipliers[_damageData.Value.damageType];
                var absorbedByShieldShieldDamage = -_owner.ChangeMp(_element, -incomingToShieldDamage);
                var damageModule = _owner.DamageModule;
                damageModule.DefenderDamageMultiplier = 0;
                if (absorbedByShieldShieldDamage == incomingToShieldDamage)
                {
                    return false;
                }
                else
                {
                    damageModule.DefenderDamageSummand += incomingToShieldDamage - absorbedByShieldShieldDamage;
                    return true;
                }
            }
            else throw new Exception("КАК?");
        }
        public override LogicalModule Clone()
        {
            return new LM_03_manaShieldTickModule(_element, _costOfMaintenance, _damageMultipliers);
        }
        public override LogicalModule_DTO GetDTO()
        {
            var dto = new LM_03_manaShieldTickModule_DTO
            {
                Element = _element,
                CostOfMaintenance = _costOfMaintenance,
                DamageMultipliers = new Dictionary<EDamageType, double>(_damageMultipliers)
            };
            return dto;
        }
    }
    public class LM_03_manaShieldTickModule_DTO : LogicalModule_DTO
    {
        public ECharacteristic Element { get; set; }
        public double CostOfMaintenance { get; set; }
        public Dictionary<EDamageType, double> DamageMultipliers { get; set; }
        public LM_03_manaShieldTickModule_DTO() { }
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_03_manaShieldTickModule(Element, CostOfMaintenance, new Dictionary<EDamageType, double>(DamageMultipliers));
        }
    }
}
