namespace AlvQuest_Editor
{
    public abstract class BaseEquippableEntityDTO : BaseDTO
    {
        public List<BaseEffectDTO> Effects { get; set; } = [];
        public abstract override BaseEquippableEntity RecreateOriginal();
    }
    public abstract class BaseEquippableEntity : BaseGameEntity
    {
        public List<BaseEffect> Effects { get; }
        protected BaseEquippableEntity(string name, string description, string iconName, List<BaseEffect> effects) : base(name, description, iconName)
        {
            Effects = effects;
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
