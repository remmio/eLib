using System.Collections.Generic;
using eLib.Entity;

namespace eLib.Collections
{
    public class Card : BindableBase
    {
        #region Fields

        private object _item5;
        private object _item4;
        private object _item3;
        private object _item2;
        private object _item1;
        private object _item6;
        private List<Child> _children;

        #endregion

        public object Item1
        {
            get { return _item1; }
            set { SetProperty(ref _item1, value); }
        }

        public object Item2
        {
            get { return _item2; }
            set { SetProperty(ref _item2, value); }
        }

        public object Item3
        {
            get { return _item3; }
            set { SetProperty(ref _item3, value); }
        }

        public object Item4
        {
            get { return _item4; }
            set { SetProperty(ref _item4, value); }
        }

        public object Item5
        {
            get { return _item5; }
            set { SetProperty(ref _item5, value); }
        }

        public object Item6
        {
            get { return _item6; }
            set
            {
                SetProperty(ref _item6, value);
            }
        }

        public List<Child> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }
    }

    public class Child : Card
    {
        private List<GrandChild> _grandChild;

        public List<GrandChild> GrandChild
        {
            get { return _grandChild; }
            set { SetProperty(ref _grandChild, value); }
        }
    }

    public class GrandChild : Card
    {

    }
}
