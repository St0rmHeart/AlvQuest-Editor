namespace AlvQuest_Editor
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BEO_DTO : BGO_DTO
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BaseEffectDTO> Effects { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<ECharacteristic, int> RequirementsForUse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract override BaseEquippableObject RecreateOriginal();
    }
}
