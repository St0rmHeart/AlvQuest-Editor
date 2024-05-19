namespace AlvQuest_Editor
{
    public partial class TriggerParameterModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public class TPM_DTO : BaseEffectDTO
        {
            /// <summary>
            /// 
            /// </summary>
            public LogicalModule_DTO TriggerLogicalModule_DTO { get; set; }
            
            /// <summary>
            /// 
            /// </summary>
            public LogicalModule_DTO TickLogicalModule_DTO { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int Duration { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int MaxStack { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<Dictionary<string, string>> TriggerEvents { get; set; } = new();

            /// <summary>
            /// 
            /// </summary>
            public List<Dictionary<string, string>> TickEvents { get; set; } = new();

            /// <summary>
            /// 
            /// </summary>
            public List<Dictionary<string, string>> Links { get; set; } = new();

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = base.GetHashCode();
                    hashCode ^= TriggerLogicalModule_DTO.GetHashCode();
                    hashCode ^= TickLogicalModule_DTO.GetHashCode();
                    hashCode ^= Duration.GetHashCode();
                    hashCode ^= MaxStack.GetHashCode();
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(TriggerEvents);
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(TickEvents);
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(Links);
                    return hashCode;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override TriggerParameterModifier RecreateOriginal()
            {
                return new TriggerParameterModifier(
                    name: BaseData.Name,
                    description: BaseData.Description,
                    iconName: BaseData.Icon,
                    triggerlogicalModule: TriggerLogicalModule_DTO.RecreateLogicalModule(),
                    ticklogicalModule: TickLogicalModule_DTO.RecreateLogicalModule(),
                    duration: Duration,
                    maxStack: MaxStack,
                    links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(Links),
                    triggerEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(TriggerEvents),
                    tickEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(TickEvents));
            }
        }
    }
}
