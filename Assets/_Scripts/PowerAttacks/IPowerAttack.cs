public interface IPowerAttack
{
    float MaxAttackTime { get; set; }
    float AttactTime { get; set; }
    float MaxRechargeTime { get; set; }
    float RechargeTime { get; set; }
    bool CanAttack { get; set; }

    public void OnAttack();
    bool IsRecharged();
}
