namespace AlvQuest_Editor
{
    public class Perk : BaseEquippableEntity
    {
        public Perk(string name,
            string description,
            string iconName, List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse)
            : base(name, description, iconName, effects, requirementsForUse)
        {

        }

        public override Perk Clone()
        {
            return new Perk(
                name: Name,
                description: Description,
                iconName: IconName,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse));
        }

        public override PerkDTO GetDTO()
        {
            var dto = new PerkDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                RequirementsForUse = new Dictionary<ECharacteristic, int>(RequirementsForUse),
            };
            return dto;
        }

        public class PerkDTO : BaseEquippableEntityDTO
        {
            public override Perk RecreateOriginal()
            {
                return new Perk(
                name: BaseData.Name,
                description: BaseData.Description,
                iconName: BaseData.Icon,
                effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse));
            }
        }
    }
}
