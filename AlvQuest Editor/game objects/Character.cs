using System.Xml.Linq;

namespace AlvQuest_Editor
{
    /// <summary>
    /// Класс, в хором хранится вся информация, определющая персонажа
    /// </summary>
    public partial class Character : BaseGameEntity
    {
        #region _____________________ПОЛЯ_____________________

        // Границы перехода на новый уровень
        public static readonly int[] levelBoundaries = [0, 100, 150, 250, 400, 600, 900, 1400, 2000, 2800, 3700];

        // Уровень 
        public int Level { get; private set; } = 1;
        
        // Накопленный опыт на уровне 
        public int Xp { get; set; } = 0;

        // Количество очков характеристик (за каждый уровень даётся 4 очка)
        private int _charPoints = 0;

        // Накопленное золото
        public int Gold { get; set; } = 0;

        // Базовые характеристики
        public Dictionary<ECharacteristic, int> Characteristics { get; private set; } = new()
        {
            {ECharacteristic.Strength, 0},
            {ECharacteristic.Endurance, 0},
            {ECharacteristic.Dexterity, 0},
            {ECharacteristic.Fire, 0},
            {ECharacteristic.Water, 0},
            {ECharacteristic.Air, 0},
            {ECharacteristic.Earth, 0},
        };

        // Итератор по характеристикам
        public int this[ECharacteristic characteristic]
        {
            get { return Characteristics[characteristic]; }
        }

        // Используемые перки
        public List<Perk> Perks { get; private set; } = [];

        // Носимое снаряжение
        public Dictionary<EBodyPart, Equipment> Equipment { get; private set; } = [];

        // Итератор по снаряжению
        public Equipment this[EBodyPart bodyPart]
        {
            //возвращает предмет саряжения, либо null если в указанной ячейке ничего не одето
            get
            {
                if (Equipment.TryGetValue(bodyPart, out Equipment value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            //если в указанной ячейку уже что-то одето - устанавливается ссылка на новый объект снаряжения
            set
            {
                Equipment[bodyPart] = value;
            }
        }

        //используемые заклинания
        public List<Spell> Spells { get; private set; } = [];
        #endregion

        #region _____________________КОНСТРУКТОР_____________________
        /// <summary>
        /// Базовый конструктор персонажа.
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        private Character(string name, string description, string iconName) : base(name, description, iconName)
        {
            
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________

        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            throw new NotImplementedException();
        }

        public override void Uninstallation()
        {
            throw new NotImplementedException();
        }

        public override Character Clone()
        {
            throw new NotImplementedException();
        }

        public override CharacterDTO GetDTO()
        {
            throw new NotImplementedException();
        }
        public void AddXP(int value)
        {
            throw new NotImplementedException();
        }
        #endregion






        #region СТРОИТЕЛЬ
        /// <summary>
        /// Строитель персонажа. Строитель кеширует настройки, устанавливаемые через его методы и может создавать персонажа по указанным настройкам.
        /// </summary>
        public class CharacterBuilder : BaseBuilder<CharacterBuilder, Character, CharacterDTO>
        {
            protected override void ValidateAdditionalContent()
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        public class CharacterDTO : BaseDTO
        {
            public int Level { get; set; }
            public int Xp { get; set; }
            public int Gold { get; set; }
            public int CharPoints { get; set; }
            public Dictionary<ECharacteristic, int> Characteristics { get; set; }
            public List<Perk.PerkDTO> Perks { get; set; }
            public Dictionary<EBodyPart, Equipment.EquipmentDTO> Equipment { get; set; }
            public List<Spell.SpellDTO> Spells { get; set; }

            public override Character RecreateOriginal()
            {
                throw new NotImplementedException();
            }
        }
    }
}
