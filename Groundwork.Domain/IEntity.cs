namespace Groundwork.Domain
{
    using System.ComponentModel;

    public interface IEntity<TopLevelDomain> : INotifyPropertyChanged, INotifyPropertyChanging
    {
        TopLevelDomain Id { get; set; }

        string Name { get; set; }
    }
}
