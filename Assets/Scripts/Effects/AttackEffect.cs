public class AttackEffect : IEffect
{
    public bool CanTargetSelf { get; set; }

    public AttackEffect()
    {
        CanTargetSelf = false;
    }

    public void ApplyEffect(EffectMessage message)
    {
        message.target.RemoveHealth(message.value);
    }
}