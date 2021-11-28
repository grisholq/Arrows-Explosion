using DCFAEngine.StateSystem.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DCFAEngine.StateSystem
{
    public class AggregateStateMachine : MonoBehaviour
    {
        public static readonly string baseState = "Base";
        public static readonly string baseLayer = "Base";

        private readonly Dictionary<string, LayerData> layers = new Dictionary<string, LayerData>();
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
        public void SetNextState(string state) => SetNextState(baseLayer, state);
        public void SetNextState(string layerName, string state)
        {
            OnCurrenStateChanged?.Invoke(layerName, state);

            LayerData layerData;
            if (layers.ContainsKey(layerName))
            {
                layerData = layers[layerName];
            }
            else
            {
                layerData = new LayerData();
                layers.Add(layerName, layerData);
            }

            if (!layerData.CurrentState.Equals(state))
            {
                layerData.NextState = state;

                states.TryGetValue(layerData.CurrentState, out StateActions actions);
                actions?.post?.Invoke();

                layerData.OldState = layerData.CurrentState;
                layerData.CurrentState = layerData.NextState;

                states.TryGetValue(layerData.CurrentState, out actions);
                actions?.pre?.Invoke();

                layerData.NextState = null;
            }
        }
        #endregion

        #region Reset
        public void ResetStates()
        {
            ResetStates(baseLayer);
        }
        public void ResetStates(string layerName)
        {
            LayerData layer = layers[layerName];
            layer.NextState = null;
            layer.CurrentState = null;
            layer.OldState = null;
        }
        public void ResetAllStates()
        {
            foreach (LayerData layer in layers.Values)
            {
                layer.NextState = null;
                layer.CurrentState = null;
                layer.OldState = null;
            }
        }
        #endregion

        #region Getters
        public string GetNextState()
        {
            return GetNextState(baseLayer);
        }
        public string GetNextState(string layerName)
        {
            layers.TryGetValue(layerName, out LayerData layer);
            return layer?.NextState ?? Consts.nullState;
        }
        public string GetCurrentState()
        {
            return GetCurrentState(baseLayer);
        }
        public string GetCurrentState(string layerName)
        {
            layers.TryGetValue(layerName, out LayerData layer);
            return layer?.CurrentState ?? Consts.nullState;
        }
        public string GetOldState()
        {
            return GetOldState(baseLayer);
        }
        public string GetOldState(string layerName)
        {
            layers.TryGetValue(layerName, out LayerData layer);
            return layer?.OldState ?? Consts.nullState;
        }
        #endregion

        #region Update
        //private void Update()
        //{
        //    foreach (LayerData layer in layers.Values)
        //    {
        //        states.TryGetValue(layer.CurrentState, out StateActions actions);
        //        actions?.update?.Invoke();
        //    }
        //}

        private void Update()
        {
            UpdateAllLayers();
        }

        public void UpdateAllLayers()
        {
            foreach (LayerData layer in layers.Values)
            {
                UpdateLayer(layer);
            }
        }
        public void UpdateLayer()
        {
            UpdateLayer(baseLayer);
        }
        public void UpdateLayer(string layerName)
        {
            if (layers.ContainsKey(layerName))
                UpdateLayer(layers[layerName]);
        }

        private void UpdateLayer(LayerData layer)
        {
            states.TryGetValue(layer.CurrentState, out StateActions actions);
            actions?.update?.Invoke();
        }
        #endregion

        public delegate void StateChangedHandler(string layerName, string state);
        public event StateChangedHandler OnCurrenStateChanged;

#if UNITY_EDITOR
        public List<Dictionary<string, string>> GetInfo()
        {
            var result = new List<Dictionary<string, string>>();
            foreach (string key in layers.Keys)
            {
                LayerData layer = layers[key];
                var layerdata = new Dictionary<string, string>
            {
                { "KeyName", key.ToString() },
                { "OldState", ConvertInfo(layer.OldState) },
                { "CurrentState", ConvertInfo(layer.CurrentState) },
                { "NextState", ConvertInfo(layer.NextState) },
            };
                result.Add(layerdata);
            }
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
    [CustomEditor(typeof(AggregateStateMachine))]
    public class AggregateStateMachineEditor : Editor
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

