namespace Groundwork.Domain
{
    using System;

    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
