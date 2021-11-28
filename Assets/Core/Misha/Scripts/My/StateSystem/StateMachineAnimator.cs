using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using DCFAEngine.StateSystem.Utils;

namespace DCFAEngine.StateSystem
{
    public class StateMachineAnimator : MonoBehaviour
    {
        [SerializeField]
        private StateMachine stateMachine;
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private bool noTransition = false;


        //private SkinController skinController;

        private void OnEnable()
        {
            stateMachine.OnCurrenStateChanged += OnCurrenStateChanged;
            OnCurrenStateChanged(Consts.baseLayer, stateMachine.GetCurrentState());
        }

        private void OnDisable()
        {
            stateMachine.OnCurrenStateChanged -= OnCurrenStateChanged;
        }

        private void OnCurrenStateChanged(string layerName, string state)
        {
            if (noTransition)
                animator.Play(state, 0, 0f);
            else
                animator.SetTrigger(GetStateName(state));
        }

        private string GetStateName(string state) => state.ToString().Split('.').Last();

        public bool IsCorrected
        {
            get => stateMachine != null && animator != null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (stateMachine == null)
                stateMachine = GetComponent<StateMachine>();
            if (animator == null)
                animator = GetComponent<Animator>();
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(StateMachineAnimator), true)]
    [CanEditMultipleObjects]
    public class StateMachineAnimatorEditor : Editor
    {
        private List<StateMachineAnimator> objs = new List<StateMachineAnimator>();
        private void OnEnable()
        {
            objs.Clear();
            foreach (var target in targets)
            {
                objs.Add((StateMachineAnimator)target);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (targets.Length <= 0)
                return;
            if (targets.Length == 1)
            {
                if (objs[0].IsCorrected == false)
                    EditorGUILayout.HelpBox("Не хватает либо StateMachine либо Animator", MessageType.Error);
            }
            else
            {
                if (objs.Any(o => o.IsCorrected == false))
                    EditorGUILayout.HelpBox("У одного из объектов не хватает либо StateMachine либо Animator", MessageType.Error);
            }
        }
    }
#endif
}