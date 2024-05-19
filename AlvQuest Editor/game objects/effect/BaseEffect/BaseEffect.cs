namespace AlvQuest_Editor
{
    /// <summary>
    /// Базовый класс для всех эффектов
    /// </summary>
    public abstract class BaseEffect : BaseGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        protected BaseEffect(string name, string description, string iconName) : base(name, description, iconName) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract override BaseEffect Clone();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract override BaseEffectDTO GetDTO();
    }
}