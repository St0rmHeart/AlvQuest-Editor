using System.Text.Json.Serialization;

namespace AlvQuest_Editor
{
    /// <summary>
    /// Базовый DTO класс для всех DTO версий эффектов
    /// </summary>
    [JsonDerivedType(typeof(PassiveParameterModifier.PPM_DTO), typeDiscriminator: "PPM_DTO")]
    [JsonDerivedType(typeof(TriggerParameterModifier.TPM_DTO), typeDiscriminator: "TPM_DTO")]
    public abstract class BaseEffectDTO : BGO_DTO
    {
        /// <summary>
        /// Вычисляет хэш код в зависимости от содержимых данных.
        /// </summary>
        /// <returns><see cref="int"/> Хэш код конкретного объекта. </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Воссоздаёт оригинальную версию объекта.
        /// </summary>
        /// <returns></returns>
        public abstract override BaseEffect RecreateOriginal();
    }
}
