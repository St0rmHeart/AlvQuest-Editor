namespace AlvQuest_Editor
{
    /// <summary>
    /// Эффект, модифицирующий набор указанных переменных в указанных параметрах в начале сражения. Может либо мофицировать все ссылки одним значением,
    /// либо модифицировать каждую ссылку своим уникальным значением
    /// </summary>
    public partial class PassiveParameterModifier : BaseEffect
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> _links;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="links"></param>
        private PassiveParameterModifier(
            string name,
            string description,
            string iconName,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)> links) : base(name, description, iconName)
        {
            _links = links;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        public override void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            for (int i = 0; i < _links.Count; i++)
            {
                var link = _links[i];
                var target = (link.target == EPlayerType.Self) ? owner : enemy;
                var currentParameter = target.Data[link.characteristic][link.derivative];
                currentParameter.ChangeVariable(link.variable, link.value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void Uninstallation()
        {
            //никаких действий не требуется, так как объект не формирует никаких связей в методе Installation
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override PassiveParameterModifier Clone()
        {
            return new PassiveParameterModifier(
                name: Name,
                description: Description,
                iconName: Icon,
                new List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)>(_links));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override PPM_DTO GetDTO()
        {
            var dto = new PPM_DTO
            {
                BaseData = GetBaseData(),
                Links = AlvQuestStatic.DTOConverter.ToDTOImpactLinkList(_links)
            };
            return dto;
        }
    }
}
