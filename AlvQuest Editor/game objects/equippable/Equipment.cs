namespace AlvQuest_Editor
{
    /// <summary>
    /// Предмет снаряжения, который можно одеть в соответсвующий ему слот
    /// </summary>
    public class Equipment : BaseEquippableEntity
    {
        //ячейка снаряжения
        public EBodyPart BodyPart { get; }
        private Equipment(string name, string description, string iconName, List<BaseEffect> effects, EBodyPart bodyPart) : base(name, description, iconName, effects)
        {
            BodyPart = bodyPart;
        }
        public override Equipment Clone()
        {
            return new Equipment(
                name: Name,
                description: Description,
                iconName: IconName,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                bodyPart: BodyPart);
        }
        public override EquipmentDTO GetDTO()
        {
            var dto = new EquipmentDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                BodyPart = BodyPart
            };
            return dto;
        }
        public class EquipmentDTO : BaseEquippableEntityDTO
        {
            public EBodyPart BodyPart { get; set; }
            public override Equipment RecreateOriginal()
            {
                return new Equipment(
                        name: BaseData.Name,
                        description: BaseData.Description,
                        iconName: BaseData.Icon,
                        effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                        bodyPart: BodyPart);

            }
        }
        public class EquipmentBuilder : BaseBuilder<EquipmentBuilder, Equipment, EquipmentDTO>
        {
            public EquipmentBuilder SetEffect(BaseEffectDTO newEffect)
            {
                int newEffectHashCode = newEffect.GetHashCode();
                foreach (var effect in _entityData.Effects)
                {
                    if (newEffectHashCode == effect.GetHashCode()) throw new Exception("Такой эффект уже существует!");
                }
                _entityData.Effects.Add(newEffect);
                return this;
            }
            public EquipmentBuilder SetBodypart(EBodyPart bodyPart)
            {
                if (bodyPart == EBodyPart.None) throw new ArgumentException("None в свойстве bodyPart недопутим");
                _entityData.BodyPart = bodyPart;
                return this;
            }
            protected override void ValidateAdditionalContent()
            {
                if (_entityData.Effects?.Count == 0) throw new ArgumentException("Не добавлено ниодногоэффекта");
            }
        }
    }
}
