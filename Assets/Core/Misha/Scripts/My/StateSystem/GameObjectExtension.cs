using UnityEngine;

namespace DCFAEngine.StateSystem
{
    public static class GameObjectExtension
    {
        public static string GetCurrentState(this GameObject go)
        {
            StateMachine stateMachine = go.GetComponent<StateMachine>();
            if (stateMachine != null)
            {
                return stateMachine.GetCurrentState();
            }
            else
            {
                return DefaultStates.NULL;
            }

        }
    }
}
