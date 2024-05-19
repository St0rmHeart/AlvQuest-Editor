namespace AlvQuest_Editor
{
    public partial class Equipment
    {
        //надо переписать
        
        /*public class EquipmentBuilder : BaseBuilder<EquipmentBuilder, Equipment, EquipmentDTO>
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
        }*/
    }
}
