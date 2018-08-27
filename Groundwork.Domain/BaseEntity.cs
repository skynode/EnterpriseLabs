namespace Groundwork.Domain
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class BaseEntity<TopLevelDomain> : IEntity<TopLevelDomain>
    {
        private TopLevelDomain _id;
        private string _name;

        protected BaseEntity(TopLevelDomain id, string name)
        {
            _id = id;
            _name = name;
        }

        public bool IsModified { get; private set; }

        public void SetModified(bool modified)
        {
            if(IsModified != modified)
            {
                IsModified = modified;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual void OnPropertyChanging(string propertyName)
            => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [DataMember]
        public virtual string Name
        {
            get => _name;
            set
            {
                if(!Equals(_name, value))
                {
                    OnPropertyChanging(nameof(Name));
                    _name = value;
                    SetModified(true);
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [DataMember]
        public TopLevelDomain Id
        {
            get => _id;
            set
            {
                if(!Equals(_id, value))
                {
                    OnPropertyChanging(nameof(Id));
                    _id = value;
                    SetModified(true);
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
    }
}
