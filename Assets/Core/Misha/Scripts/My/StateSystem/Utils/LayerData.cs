
using System;

namespace DCFAEngine.StateSystem.Utils
{
    [Serializable]
    class LayerData
    {
        private string currentState = Consts.nullState;
        private string nextState = Consts.nullState;
        private string oldState = Consts.nullState;

        public string CurrentState { get => currentState; set => currentState = value ?? Consts.nullState; }
        public string NextState { get => nextState; set => nextState = value ?? Consts.nullState; }
        public string OldState { get => oldState; set => oldState = value ?? Consts.nullState; }
    }
}
