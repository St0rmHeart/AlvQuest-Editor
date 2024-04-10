namespace AlvQuest_Editor
{
    public abstract class BasicEquippableEntity
    {
        public BaseData BaseData { get; set; } = new BaseData();
        //список эффектов, реализующий действие снаряжения
        public List<BaseEffect> Effects { get; set; } = [];
    }
}
