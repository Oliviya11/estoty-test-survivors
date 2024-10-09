using UnityEngine;

namespace Installers
{
    public struct PlayerDiedSignal
    {
            
    }

    public struct PlayerGotDamageSignal
    {
        public float Value;
        public PlayerGotDamageSignal(float value)
        {
            Value = value;
        }
    }

    public struct KillEnemySignal
    {
        public int KilledEnemies;
        public KillEnemySignal(int killedEnemies)
        {
            KilledEnemies = killedEnemies;
        }
    }
}