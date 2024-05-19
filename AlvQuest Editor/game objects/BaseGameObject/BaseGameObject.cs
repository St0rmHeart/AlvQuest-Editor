namespace AlvQuest_Editor
{
    /// <summary>
    /// Базовый класс для всех игровых объектов.
    /// </summary>
    public abstract class BaseGameObject
    {
        /// <summary>
        /// Название игрового объекта.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Описание игрового объекта.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Название файла - иконки.
        /// </summary>
        public string Icon { get; }

        /// <summary>
        /// Базовый конструктор всех игровых объектов.
        /// </summary>
        /// <param name="name"> Название</param>
        /// <param name="description"> Описание</param>
        /// <param name="icon"> Название файла - иконки </param>
        public BaseGameObject(string name, string description, string icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }

        /// <summary>
        /// Внутренний метод для удобного копирования основных полей в DTO
        /// </summary>
        /// <returns></returns> 
        protected BaseData GetBaseData()
        {
            return new BaseData
            {
                Name = Name,
                Description = Description,
                Icon = Icon
            };
        }

        /// <summary>
        /// Установка объекта. Вызывается во время создания арены перед началом сражения.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        public abstract void Installation(CharacterSlot owner, CharacterSlot enemy);

        /// <summary>
        /// Деинсталляция объекта. Вызывается после завершения сражения.
        /// </summary>
        public abstract void Uninstallation();

        /// <summary>
        /// Возвращает глубокую копию объекта
        /// </summary>
        public abstract BaseGameObject Clone();

        /// <summary>
        /// Возвращает DTO версию объекта.
        /// </summary>
        /// <returns></returns>
        public abstract BGO_DTO GetDTO();
    }
}
