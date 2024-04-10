using System.Runtime.CompilerServices;

namespace AlvQuest_Editor
{
    internal class EffectPanel : GameEntityPanel
    {
        public BaseEffect Effect { get; set; }
        public EffectPanel(BaseEffect effect)
        {
            Effect = effect;
            EffectName.Text = effect.Name;
        }
    }
}
