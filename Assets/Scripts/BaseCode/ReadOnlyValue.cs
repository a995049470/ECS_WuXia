using Unity.Collections;
public struct ReadOnlyValue<T>
{
    [ReadOnly] public T Value;
    public ReadOnlyValue(T value)
    {
        Value = value;
    }
}
