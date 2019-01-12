public interface IEffect
{
    bool CanTargetSelf { get; set; }

    void ApplyEffect(EffectMessage message);
}