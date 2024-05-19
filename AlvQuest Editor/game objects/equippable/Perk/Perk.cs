namespace AlvQuest_Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Perk : BaseEquippableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="effects"></param>
        /// <param name="requirementsForUse"></param>
        private Perk(string name,
            string description,
            string iconName, List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse)
            : base(name, description, iconName, effects, requirementsForUse)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Perk Clone()
        {
            return new Perk(
                name: Name,
                description: Description,
                iconName: Icon,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
    }
}
