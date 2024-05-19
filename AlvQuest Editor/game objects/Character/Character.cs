using System.Xml.Linq;

namespace AlvQuest_Editor
{
    /// <summary>
    /// Класс, в хором хранится вся информация, определющая персонажа
    /// </summary>
    public partial class Character : BaseGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        public int Level { get; private set; } = 1;
        
        /// <summary>
        /// 
        /// </summary>
        public int Xp { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int CharPoints { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int Gold { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characteristic"></param>
        /// <returns></returns>
        public int this[ECharacteristic characteristic]
        {
            get { return Characteristics[characteristic]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Perk> Perks { get; private set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<EBodyPart, Equipment> Equipment { get; private set; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyPart"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public List<Spell> Spells { get; private set; } = new();

        /// <summary>
        /// Базовый конструктор персонажа.
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        private Character(string name, string description, string iconName) : base(name, description, iconName)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Uninstallation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Character Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override CharacterDTO GetDTO()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddXP(int value)
        {
            throw new NotImplementedException();
        }
    }
}
