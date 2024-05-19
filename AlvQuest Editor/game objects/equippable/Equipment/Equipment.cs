namespace AlvQuest_Editor
{
    /// <summary>
    /// Предмет снаряжения, который можно одеть в соответсвующий ему слот
    /// </summary>
    public partial class Equipment : BaseEquippableObject
    { 
        /// <summary>
        /// 
        /// </summary>
        public EBodyPart BodyPart { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="effects"></param>
        /// <param name="requirementsForUse"></param>
        /// <param name="bodyPart"></param>
        private Equipment(
            string name,
            string description,
            string iconName,
            List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse,
            EBodyPart bodyPart) : base(name, description, iconName, effects, requirementsForUse)
        {
            BodyPart = bodyPart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Equipment Clone()
        {
            return new Equipment(
                name: Name,
                description: Description,
                iconName: Icon,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                bodyPart: BodyPart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override EquipmentDTO GetDTO()
        {
            var dto = new EquipmentDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                RequirementsForUse = new Dictionary<ECharacteristic, int>(RequirementsForUse),
                BodyPart = BodyPart
            };
            return dto;
        } 
    }
}
