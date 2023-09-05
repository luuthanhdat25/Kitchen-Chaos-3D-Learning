using System;

namespace KitchenObjects.Counter
{
    public interface IHasProgess
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProcessChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
    }
}