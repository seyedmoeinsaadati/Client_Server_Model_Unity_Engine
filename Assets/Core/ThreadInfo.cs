using System;

public struct ThreadInfo<T>
{
    public T parameter;
    public Action<T> callback;

    public ThreadInfo(T parameter, Action<T> callback)
    {
        this.parameter = parameter;
        this.callback = callback;
    }
}