namespace AlvQuest_Editor
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseEquippableEntityDTO : BaseDTO
    {
        public List<BaseEffectDTO> Effects { get; set; } = [];
        public Dictionary<ECharacteristic, int> RequirementsForUse { get; set; } = [];
        public abstract override BaseEquippableEntity RecreateOriginal();
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseEquippableEntity : BaseGameEntity
    {
        public List<BaseEffect> Effects { get; }
        public Dictionary<ECharacteristic, int> RequirementsForUse { get; }
        protected BaseEquippableEntity(
            string name,
            string description,
            string iconName,
            List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse) : base(name, description, iconName)
        {
            Effects = effects;
            RequirementsForUse = requirementsForUse;
        }
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            Effects.ForEach(effect => effect.Installation(owner, enemy));
        }
        public override void Uninstallation()
        {
            Effects.ForEach(effect => effect.Uninstallation());
        }
        public abstract override BaseEquippableEntity Clone();
        public abstract override BaseEquippableEntityDTO GetDTO();
    }
}
