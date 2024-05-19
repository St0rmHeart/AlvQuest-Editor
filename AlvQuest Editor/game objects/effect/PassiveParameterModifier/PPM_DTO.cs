namespace AlvQuest_Editor
{
    public partial class PassiveParameterModifier
    {
        /// <summary>
        /// DTO версия класса <see cref="PassiveParameterModifier"/>.
        /// </summary>
        public class PPM_DTO : BaseEffectDTO
        {
            /// <summary>
            /// Список коретежей, преобразованных в словари, описывающий все модификации, производимыe объектом
            /// </summary>
            public List<Dictionary<string, string>> Links { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = base.GetHashCode();
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(Links);
                    return hashCode;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override PassiveParameterModifier RecreateOriginal()
            {
                return new PassiveParameterModifier(
                        name: BaseData.Name,
                        description: BaseData.Description,
                        iconName: BaseData.Icon,
                        links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(Links));
            }
        }
    }
}
