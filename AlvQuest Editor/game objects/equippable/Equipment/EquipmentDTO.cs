namespace AlvQuest_Editor
{
    public partial class Equipment
    {
        /// <summary>
        /// 
        /// </summary>
        public class EquipmentDTO : BEO_DTO
        {
            /// <summary>
            /// 
            /// </summary>
            public EBodyPart BodyPart { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override Equipment RecreateOriginal()
            {
                return new Equipment(
                        name: BaseData.Name,
                        description: BaseData.Description,
                        iconName: BaseData.Icon,
                        effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                        requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                        bodyPart: BodyPart);
            }
        }
    }
}
