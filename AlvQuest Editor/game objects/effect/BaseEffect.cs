using System.Text.Json.Serialization;

namespace AlvQuest_Editor
{
    [JsonDerivedType(typeof(PassiveParameterModifier.PPM_DTO), typeDiscriminator: "PPM_DTO")]
    [JsonDerivedType(typeof(TriggerParameterModifier.TPM_DTO), typeDiscriminator: "TPM_DTO")]
    public abstract class BaseEffectDTO : BaseDTO
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public abstract override BaseEffect RecreateOriginal();
    }
    public abstract class BaseEffect : BaseGameEntity
    {
        protected BaseEffect(string name, string description, string iconName) : base(name, description, iconName) { }
        public abstract override BaseEffect Clone();
        public abstract override BaseEffectDTO GetDTO();
    }
}