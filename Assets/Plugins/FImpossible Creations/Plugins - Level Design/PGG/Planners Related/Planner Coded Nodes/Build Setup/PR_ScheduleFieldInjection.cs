﻿using FIMSpace.Graph;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FIMSpace.Generating.Planning.PlannerNodes.BuildSetup
{

    public class PR_ScheduleFieldInjection : PlannerRuleBase
    {
        public override string GetDisplayName(float maxWidth = 120) { return wasCreated ? "Schedule Field Injection" : "Schedule Field Setup Injection"; }
        public override string GetNodeTooltipDescription { get { return "Schedules change of some Field Setup variable when being generated by Field Planner Executor.\nIt is not working yet with flexible painters!"; } }
        public override Color GetNodeColor() { return new Color(1.0f, 0.4f, 0.4f, 0.9f); }
        public override Vector2 NodeSize { get { return new Vector2(252, 122); } }
        public override bool DrawInputConnector { get { return true; } }
        public override bool DrawOutputConnector { get { return false; } }

        public override EPlannerNodeType NodeType { get { return EPlannerNodeType.ReadData; } }
        public override EPlannerNodeVisibility NodeVisibility { get { return EPlannerNodeVisibility.JustPlanner; } }

        public enum ESetVariableBy
        {
            IndexID, NameID
        }

        [Tooltip("Basically first (0), second (1) field variable in Field Setup list or selection by name. If variable won't be found nothing will change.")]
        public ESetVariableBy GetFieldVariableBy = ESetVariableBy.IndexID;

        [Tooltip("First (0), second (1) field variable in Field Setup list.")]
        [HideInInspector] public int VariableID = 0;

        [Tooltip("Searching for field variable in Field Setup list using name.")]
        [HideInInspector] public string VariableName = "";

        [Tooltip("The variable type must match in order to make node work correctly.")]
        [HideInInspector][Port(EPortPinType.Input)] public PGGUniversalPort SetValue;
        //[HideInInspector][Port(EPortPinType.Output, EPortPinDisplay.JustPort)] public FloatPort Random;

        public override void Execute(PlanGenerationPrint print, PlannerResult newResult)
        {
            if (CurrentExecutingPlanner == null) return;

            SetValue.TriggerReadPort(true);
            //ParentPlanner.ScheduleEvent()
            var tValue = SetValue.GetPortValueSafe;

            ESetVariableBy getBy = GetFieldVariableBy;
            int id = VariableID;
            string name = VariableName;

            System.Action<object> variableApply =
            (o) =>
            {
                GridPainter painter = o as GridPainter;
                if (painter)
                {
                    FieldSetup fs = painter.FieldPreset;
                    FieldVariable toChange = null;

                    // Get Variable
                    if (getBy == ESetVariableBy.IndexID)
                    {
                        if (id < fs.Variables.Count)
                        {
                            toChange = fs.Variables[id];
                        }
                    }
                    else if (getBy == ESetVariableBy.NameID)
                    {
                        toChange = fs.GetVariable(name);
                    }

                    if (toChange != null)
                    {
                        FieldSetup duplFS = painter.GetTargetGeneratingSetup();
                        var fVar = duplFS.GetVariable(toChange.Name);
                        fVar.SetValue(tValue);

                        // Apply injection
                        //FieldVariable fVar = vv.Copy();

                        //InjectionSetup inj = new InjectionSetup(null, InjectionSetup.EGridCall.Pre);
                        //inj.Inject = InjectionSetup.EInjectTarget.ModOnlyForAccessingVariables;
                        //inj.Modificator = painter.PGG_Setup.RootPack.FieldModificators[0];
                        //inj.OverrideVariables = true;

                        //fVar.Name = name;
                        //inj.AddOverride(fVar);

                        //if (painter.Injections == null) painter.Injections = new System.Collections.Generic.List<InjectionSetup>();
                        //painter.Injections.Add(inj);
                    }
                    else
                    {
                        if (getBy == ESetVariableBy.NameID)
                        {
                            UnityEngine.Debug.Log("[PGG - Schedule Field Injection Node] Not found variable with name '" + name + "!");
                        }
                    }
                }
            };

            CurrentExecutingPlanner.AddOnGeneratingEvent(variableApply);
        }



#if UNITY_EDITOR
        SerializedProperty sp = null;
        public override void Editor_OnNodeBodyGUI(ScriptableObject setup)
        {
            base.Editor_OnNodeBodyGUI(setup);

            if (sp == null) sp = baseSerializedObject.FindProperty("VariableID");
            SerializedProperty s = sp.Copy();

            baseSerializedObject.Update();

            if (GetFieldVariableBy == ESetVariableBy.IndexID)
            {
                EditorGUILayout.PropertyField(s);
                s.Next(false);
            }
            else
            {
                s.Next(false);
                EditorGUILayout.PropertyField(s);
            }

            s.Next(false);
            EditorGUILayout.PropertyField(s);

            baseSerializedObject.ApplyModifiedProperties();
        }
#endif

    }
}