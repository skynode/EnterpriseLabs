namespace Groundwork.Domain
{
    using System.ComponentModel;

    public class CancelEventArgs<T> : CancelEventArgs
    {
        public CancelEventArgs(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
