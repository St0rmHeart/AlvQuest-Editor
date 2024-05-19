namespace AlvQuest_Editor
{
    public partial class Character
    {
        /// <summary>
        /// 
        /// </summary>
        public class CharacterDTO : BGO_DTO
        {
            /// <summary>
            /// 
            /// </summary>
            public int Level { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int Xp { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int Gold { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int CharPoints { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Dictionary<ECharacteristic, int> Characteristics { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<Perk.PerkDTO> Perks { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Dictionary<EBodyPart, Equipment.EquipmentDTO> Equipment { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<Spell.SpellDTO> Spells { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public override Character RecreateOriginal()
            {
                throw new NotImplementedException();
            }
        }
    }
}
