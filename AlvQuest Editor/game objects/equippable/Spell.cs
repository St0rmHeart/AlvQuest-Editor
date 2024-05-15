using System.Xml.Linq;

namespace AlvQuest_Editor
{
    public class Spell : BaseEquippableEntity
    {
        public Dictionary<EManaType, double> ManaCost {  get; private set; }

        public Spell(string name,
            string description,
            string iconName, List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse,
            Dictionary<EManaType, double> manaCost)
            : base(name, description, iconName, effects, requirementsForUse)
        {
            ManaCost = manaCost;
        }

        public override Spell Clone()
        {
            return new Spell(
                name: Name,
                description: Description,
                iconName: IconName,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                manaCost: new Dictionary<EManaType, double>(ManaCost));
        }

        public override SpellDTO GetDTO()
        {
            var dto = new SpellDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                RequirementsForUse = new Dictionary<ECharacteristic, int>(RequirementsForUse),
            };
            return dto;
        }

        public class SpellDTO : BaseEquippableEntityDTO
        {
            public Dictionary<EManaType, double> ManaCost { get; set; }
            public override Spell RecreateOriginal()
            {
                return new Spell(
                name: BaseData.Name,
                description: BaseData.Description,
                iconName: BaseData.Icon,
                effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                manaCost: new Dictionary<EManaType, double>(ManaCost));
            }
        }
    }
}