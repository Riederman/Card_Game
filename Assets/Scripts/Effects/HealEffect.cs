public class HealEffect : IEffect
{
    public bool CanTargetSelf { get; set; }

    public HealEffect()
    {
        CanTargetSelf = true;
    }

    public void ApplyEffect(EffectMessage message)
    {
        message.target.AddHealth(message.value);
    }
}