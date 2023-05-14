public class PerMileMeter
{
    public float Value { get; set; }

    public PerMileMeter(float startValue)
    {
        Value = startValue;
    }

    public void Add(float value) => Value += value;
}