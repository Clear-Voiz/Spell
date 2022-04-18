public enum modiType
{
    Flat = 100,
    Percent = 200
}

public class StatModifier
{
    public readonly float value;
    public readonly modiType Type;

    public StatModifier(float Value, modiType type)
    {
        value = Value;
        Type = type;

    }
}
