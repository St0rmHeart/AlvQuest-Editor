namespace AlvQuest_Editor
{
    /// <summary>
    /// Базовый Data Transfer Object класс для всех игровых объектов. 
    /// </summary>
    public abstract class BaseDTO
    {
        public BaseData BaseData { get; set; } = new BaseData();
        public override int GetHashCode()
        {
            return BaseData.GetHashCode();
        }
        public abstract BaseGameEntity RecreateOriginal();
    }
    /// <summary>
    /// Базовый класс для всех игровых объектов
    /// </summary>
    public abstract class BaseGameEntity
    {
        public BaseGameEntity(string name, string description, string iconName)
        {
            Name = name;
            Description = description;
            IconName = iconName;
        }
        public string Name { get; }
        public string Description { get; }
        public string IconName { get; }
        protected BaseData GetBaseData()
        {
            return new BaseData { Name = Name, Description = Description, Icon = IconName };
        }
        public abstract void Installation(CharacterSlot owner, CharacterSlot enemy);
        public abstract void Uninstallation();
        public abstract BaseGameEntity Clone();
        public abstract BaseDTO GetDTO();
    }
    /// <summary>
    /// Общий функционал для всех строителей эффектов
    /// </summary>
    public abstract class BaseBuilder<TBuilder, TProduct, TDTO>
        where TBuilder : BaseBuilder<TBuilder, TProduct, TDTO>
        where TProduct : BaseGameEntity
        where TDTO : BaseDTO, new()
    {
        protected TDTO _entityData = new();
        public TBuilder Reset()
        {
            _entityData = new();
            return this as TBuilder;
        }
        public TBuilder InstallDTO(TDTO dto)
        {
            _entityData = dto;
            return this as TBuilder;
        }
        public TBuilder SetName(string name)
        {
            _entityData.BaseData.Name = name;
            return this as TBuilder;
        }
        public TBuilder SetDescription(string Description)
        {
            _entityData.BaseData.Description = Description;
            return this as TBuilder;
        }
        public TBuilder SetIcon(string iconName)
        {
            _entityData.BaseData.Icon = iconName;
            return this as TBuilder;
        }
        protected void ValidateBaseContent()
        {
            var baseData = _entityData.BaseData;
            if (baseData.Name == null || baseData.Name == "") throw new ArgumentException("Отсутствует название.");
            if (baseData.Description == null || baseData.Description == "") throw new ArgumentException("Отсутствует описание.");
            if (baseData.Icon == null) throw new ArgumentException("Отсутствует иконка.");
        }
        public TProduct BuildEntity()
        {

            ValidateBaseContent();
            ValidateAdditionalContent();
            return _entityData.RecreateOriginal() as TProduct;
        }
        public TDTO ExtractDTO()
        {
            ValidateBaseContent();
            ValidateAdditionalContent();
            return _entityData;
        }
        /// <summary>
        /// Метод, верифицирующий все данные, которые строятся в конструкторе-потомке
        /// </summary>
        protected abstract void ValidateAdditionalContent();
    }
}
