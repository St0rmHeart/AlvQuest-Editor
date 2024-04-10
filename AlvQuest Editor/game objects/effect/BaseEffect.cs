namespace AlvQuest_Editor
{
    /// <summary>
    /// Общие поля всех эффектов 
    /// </summary>
    public class BaseData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
    /// <summary>
    /// Базовый Data Transfer Object класс для всех эффектов 
    /// </summary>
    public abstract class BaseEffect_DTO
    {
        public BaseData BaseData { get; set; } = new BaseData();
    }

    /// <summary>
    /// Общие свой ства всех эффектов
    /// </summary>
    public abstract class BaseEffect
    {
        public BaseEffect(string name, string description, string iconName)
        {
            Name = name;
            Description = description;
            IconName = iconName;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string IconName { get; private set; }
        protected BaseData GetBaseData()
        {
            return new BaseData { Name = Name, Description = Description, IconName = IconName };
        }
        public abstract BaseEffect_DTO GetDTO();
        public abstract void Installation(CharacterSlot owner, CharacterSlot enemy);
        public abstract BaseEffect Clone();
    }
    /// <summary>
    /// Общий функционал для всех строителей эффектов
    /// </summary>
    public abstract class BaseEffect_Builder<TBuilder, TProduct, TDTO>
        where TBuilder : BaseEffect_Builder<TBuilder, TProduct, TDTO>
        where TProduct : BaseEffect
        where TDTO : BaseEffect_DTO, new()
    {
        protected TDTO _effectData = new();
        public TBuilder Reset()
        {
            _effectData = new();
            return this as TBuilder;
        }
        public TBuilder InstallDTO(TDTO obj)
        {
            _effectData = obj;
            return this as TBuilder;
        }
        public TBuilder Name(string name)
        {
            _effectData.BaseData.Name = name;
            return this as TBuilder;
        }
        public TBuilder Description(string Description)
        {
            _effectData.BaseData.Description = Description;
            return this as TBuilder;
        }
        public TBuilder Icon(string iconName)
        {
            _effectData.BaseData.IconName = iconName;
            return this as TBuilder;
        }
        public TProduct Build()
        {
            ValidateBaseContent();
            ValidateAdditionalContent();
            return Construct();
        }
        private void ValidateBaseContent()
        {
            var baseData = _effectData.BaseData;
            if (baseData.Name == null || baseData.Name == "") throw new ArgumentException("Отсутствует название эффекта.");
            if (baseData.Description == null || baseData.Description == "") throw new ArgumentException("Отсутствует описание эффекта.");
            if (baseData.IconName == null) throw new ArgumentException("Отсутствует иконка эффекта.");
        }
        protected abstract void ValidateAdditionalContent();
        protected abstract TProduct Construct();
    }
}
