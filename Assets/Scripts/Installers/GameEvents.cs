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
}