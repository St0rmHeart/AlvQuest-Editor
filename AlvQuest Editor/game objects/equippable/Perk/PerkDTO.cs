namespace AlvQuest_Editor
{
    public partial class Perk
    {
        /// <summary>
        /// 
        /// </summary>
        public class PerkDTO : BEO_DTO
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
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
