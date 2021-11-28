using DCFAEngine.StateSystem.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DCFAEngine.StateSystem
{
    public class StateMachine : MonoBehaviour
    {
        public static readonly string baseState = "Base";

        private readonly LayerData mainLayer = new LayerData();
        private readonly Dictionary<string, StateActions> states = new Dictionary<string, StateActions>();

        #region Add/Remove Listeners
        public void AddStateListeners(string state, Action pre = null, Action update = null, Action post = null)
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
        public void RemoveStateListeners(string state, Action pre = null, Action update = null, Action post = null)
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
        public void SetNextState(string state)
        {
            OnCurrenStateChanged?.Invoke(Consts.baseLayer, state);
;
            if (!mainLayer.CurrentState.Equals(state))
            {
                mainLayer.NextState = state;

                states.TryGetValue(mainLayer.CurrentState, out StateActions actions);
                actions?.post?.Invoke();

                mainLayer.OldState = mainLayer.CurrentState;
                mainLayer.CurrentState = mainLayer.NextState;

                states.TryGetValue(mainLayer.CurrentState, out actions);
                actions?.pre?.Invoke();

                mainLayer.NextState = null;
            }
        }
        #endregion

        #region Reset
        public void ResetStates()
        {
            mainLayer.NextState = null;
            mainLayer.CurrentState = null;
            mainLayer.OldState = null;
        }
        #endregion

        #region Getters
        public string GetNextState()
        {
            return mainLayer.NextState ?? Consts.nullState;
        }
        public string GetCurrentState()
        {
            return mainLayer.CurrentState ?? Consts.nullState;
        }
        public string GetOldState()
        {
            return mainLayer.OldState ?? Consts.nullState;
        }
        #endregion

        #region Update

        private void Update()
        {
            UpdateLayer();
        }

        private void UpdateLayer()
        {
            states.TryGetValue(mainLayer.CurrentState, out StateActions actions);
            actions?.update?.Invoke();
        }
        #endregion

        public delegate void StateChangedHandler(string layerName, string state);
        public event StateChangedHandler OnCurrenStateChanged;

#if UNITY_EDITOR
        public List<Dictionary<string, string>> GetInfo()
        {
            var result = new List<Dictionary<string, string>>();

            var layerdata = new Dictionary<string, string>
            {
                { "KeyName", Consts.baseLayer},
                { "OldState", ConvertInfo(mainLayer.OldState) },
                { "CurrentState", ConvertInfo(mainLayer.CurrentState) },
                { "NextState", ConvertInfo(mainLayer.NextState) },
            };
            result.Add(layerdata);

            return result;
        }
        private string ConvertInfo(string state) => state == Consts.nullState ? "" : (state);
#endif
    }
}

#region Editor
#if UNITY_EDITOR
namespace DCFAEngine.StateSystem.CustomEditors
{
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineEditor : Editor
    {
        private static Color addColor = new Color(0.5f, 1f, 1f);

        private static List<bool> toggles = new List<bool>();

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            
            StateMachine obj = target as StateMachine;
            var datas = obj.GetInfo();
            for (int i = 0; i < datas.Count; i++)
            {
                var layerdata = datas[i];
                if (toggles.Count <= i)
                {
                    toggles.Add(true);
                }
                foreach (string key in layerdata.Keys)
                {
                    string data = layerdata[key];
                    switch (key)
                    {
                        case "KeyName":
                            toggles[i] = EditorGUILayout.Foldout(toggles[i], data);
                            if (toggles[i])
                            {
                                Color color = GUI.backgroundColor;
                                GUI.backgroundColor = addColor;
                                GUILayout.TextField(data, GUILayout.ExpandWidth(true));
                                GUI.backgroundColor = color;
                            }
                            break;
                        case "History":
                            toggles[i] = EditorGUILayout.Foldout(toggles[i], data);
                            if (toggles[i])
                            {
                                EditorGUILayout.TextArea(key, data, GUILayout.Height(100f));
                            }
                            break;
                        default:
                            if (toggles[i])
                            {
                                EditorGUILayout.TextField(key, data);
                            }
                            break;
                    }
                }
            }
            
        }
    }
}
#endif
#endregion

