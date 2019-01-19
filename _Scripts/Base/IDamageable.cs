namespace MyShooter
{
    public interface IDamageable
    {
        float Hp { get; }
        float MaxHp { get; }
        void GetDamage(DamageInfo damageInfo);
    }
}