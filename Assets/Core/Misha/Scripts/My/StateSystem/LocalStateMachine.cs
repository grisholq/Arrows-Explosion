using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCFAEngine.StateSystem.Utils;
using UnityEditor;
using UnityEngine;

namespace DCFAEngine.StateSystem.Utils
{
    public abstract class LocalStateMachine
    {

    }
}
namespace DCFAEngine.StateSystem
{
    [Serializable]
    public class LocalStateMachine<T> : LocalStateMachine where T : Enum
    {
        private T nextState = default;
        private T currentState = default;
        private T oldState = default;

        public T NextState { get => nextState; private set => nextState = value ?? default; }
        public T CurrentState { get => currentState; private set => currentState = value ?? default; }
        public T OldState { get => oldState; private set => oldState = value ?? default; }

        private Dictionary<T, StateActions> states = new Dictionary<T, StateActions>();

        #region Add/Remove Listeners
        public void AddStateListeners(T state, Action pre = null, Action update = null, Action post = null)
        {
            if (states.ContainsKey(state))
            {
                StateActions actions = states[state];
                actions.pre += pre;
                actions.update += update;
                actions.post += post;
            }
            else
            {
                StateActions result = new StateActions(pre, update, post);
                states.Add(state, result);
            }
        }
        public void RemoveStateListeners(T state, Action pre = null, Action update = null, Action post = null)
        {
            if (states.ContainsKey(state))
            {
                StateActions actions = states[state];
                actions.pre -= pre;
                actions.update -= update;
                actions.post -= post;
            }
        }
        #endregion

        #region SetNextState
        public void SetNextState(T state)
        {
            OnCurrenStateChanged?.Invoke(state);

            NextState = state;

            states.TryGetValue(CurrentState, out StateActions actions);
            actions?.post?.Invoke();

            OldState = CurrentState;
            CurrentState = NextState;

            states.TryGetValue(CurrentState, out actions);
            actions?.pre?.Invoke();

            NextState = default;
        }
        #endregion

        #region Update
        public void Update()
        {
            if (states.TryGetValue(CurrentState, out StateActions actions))
                actions.update?.Invoke();
        }
        #endregion

        public void Reset()
        {
            NextState = default;
            CurrentState = default;
            OldState = default;
        }

        public delegate void StateChangedHandler(T state);
        public event StateChangedHandler OnCurrenStateChanged;
    }
}

/*
#if UNITY_EDITOR
namespace DCFAEngine.StateSystem.CustomEditors
{
    [CustomPropertyDrawer(typeof(LocalStateMachine))]
    public class LocalStateMachineEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Debug.Log("OnGUI");
            SerializedProperty nextStateProperty = property.FindPropertyRelative("nextState");
            SerializedProperty currentStateProperty = property.FindPropertyRelative("currentState");
            SerializedProperty oldStateProperty = property.FindPropertyRelative("oldState");

            Rect rect1 = position;
            rect1.height = EditorGUIUtility.singleLineHeight;

            Rect rect2 = position;
            rect2.height = EditorGUIUtility.singleLineHeight;
            rect2.y = rect1.yMax;

            Rect rect3 = position;
            rect3.height = EditorGUIUtility.singleLineHeight;
            rect3.y = rect2.yMax;

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginProperty(position, label, property);
            GUI.enabled = false;

            GUIContent gUIContent1 = new GUIContent(text: "NextState");
            GUIContent gUIContent2 = new GUIContent(text: "CurrentState");
            GUIContent gUIContent3 = new GUIContent(text: "OldState");

            EditorGUI.PropertyField(rect1, nextStateProperty, gUIContent1);
            EditorGUI.PropertyField(rect2, currentStateProperty, gUIContent2);
            EditorGUI.PropertyField(rect3, oldStateProperty, gUIContent3);

            GUI.enabled = true;
            EditorGUI.EndProperty();
            EditorGUI.EndChangeCheck();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //if (!EditorApplication.isPlaying)
            //{
            //    return EditorGUIUtility.singleLineHeight;
            //}
            return EditorGUIUtility.singleLineHeight * 3f;
        }
    }
}
#endif
*/