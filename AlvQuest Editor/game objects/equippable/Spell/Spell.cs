using System.Xml.Linq;

namespace AlvQuest_Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Spell : BaseEquippableObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<EManaType, double> ManaCost {  get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="effects"></param>
        /// <param name="requirementsForUse"></param>
        /// <param name="manaCost"></param>
        public Spell(string name,
            string description,
            string iconName, List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse,
            Dictionary<EManaType, double> manaCost)
            : base(name, description, iconName, effects, requirementsForUse)
        {
            ManaCost = manaCost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Spell Clone()
        {
            return new Spell(
                name: Name,
                description: Description,
                iconName: Icon,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                manaCost: new Dictionary<EManaType, double>(ManaCost));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
    }
}