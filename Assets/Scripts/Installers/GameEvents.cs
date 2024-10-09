namespace Installers
{
    public class GameEvents
    {
        public struct EnemyHitSignal
        {
            public float Damage;

            public EnemyHitSignal(float damage)
            {
                Damage = damage;
            }
        }
    }
}