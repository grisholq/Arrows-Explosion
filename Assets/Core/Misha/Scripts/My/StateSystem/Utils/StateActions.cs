using System;

namespace DCFAEngine.StateSystem.Utils
{

    [Serializable]
    class StateActions
    {
        public StateActions(Action pre, Action update, Action post)
        {
            this.pre = pre;
            this.update = update;
            this.post = post;
        }
        public Action pre;
        public Action update;
        public Action post;
    }
}
