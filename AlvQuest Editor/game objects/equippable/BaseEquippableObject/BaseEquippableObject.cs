namespace AlvQuest_Editor
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseEquippableObject : BaseGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BaseEffect> Effects { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<ECharacteristic, int> RequirementsForUse { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="effects"></param>
        /// <param name="requirementsForUse"></param>
        protected BaseEquippableObject(
            string name,
            string description,
            string iconName,
            List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse) : base(name, description, iconName)
        {
            Effects = effects;
            RequirementsForUse = requirementsForUse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            Effects.ForEach(effect => effect.Installation(owner, enemy));
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Uninstallation()
        {
            Effects.ForEach(effect => effect.Uninstallation());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract override BaseEquippableObject Clone();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract override BEO_DTO GetDTO();
    }
}
