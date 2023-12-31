﻿using FIMSpace.Graph;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Generating.Planning.PlannerNodes.Field.Special
{

    public class PR_PushOutAway : PlannerRuleBase
    {
        public override string GetDisplayName(float maxWidth = 120) { return wasCreated ? "Push Out Away" : "Push Out Of Collision Away"; }
        public override string GetNodeTooltipDescription { get { return "Trying to push field out of collision with other fields cells."; } }
        public override EPlannerNodeType NodeType { get { return EPlannerNodeType.WholeFieldPlacement; } }
        public override Vector2 NodeSize { get { return new Vector2(222, _EditorFoldout ? 144 : 92); } }
        public override bool IsFoldable { get { return true; } }
        public override Color GetNodeColor() { return new Color(0.1f, 0.7f, 1f, 0.95f); }

        [Tooltip("If 'Collision With' left empty or -1 then colliding with every field in the current plan stage")]
        [Port(EPortPinType.Input, 1)] public PGGPlannerPort CollisionWith;
        [HideInInspector] [Port(EPortPinType.Input, 1)] public PGGPlannerPort ToPush;
        [HideInInspector] [Range(1f, 4f)] public float PushPowerMultiply = 1f;
        [HideInInspector] public bool RoundAccordingly = true;
        public override void Execute(PlanGenerationPrint print, PlannerResult newResult)
        {
            FieldPlanner planner = GetPlannerFromPort(ToPush);
            CollisionWith.Editor_DefaultValueInfo = "(All)";

            planner.LatestChecker._IsCollidingWith_MyFirstCollisionCell = null;

            bool collideWithAll = false;
            if (CollisionWith.PortState() != EPortPinState.Connected)
            {
                if (CollisionWith.UniquePlannerID < 0)
                {
                    collideWithAll = true;
                }
            }


            if (collideWithAll)
            {
                if (ParentPlanner)
                    if (ParentPlanner.ParentBuildPlanner)
                    {
                        var bp = ParentPlanner.ParentBuildPlanner;

                        var all = bp.CollectAllAvailablePlanners(true, true);
                        all.Remove(planner);

                        for (int i = 0; i < all.Count; i++)
                        {
                            FieldPlanner pl = all[i];
                            planner.LatestChecker.PushOutOfCollisionAway(pl.LatestChecker, RoundAccordingly, PushPowerMultiply);
                        }
                    }
            }
            else
            {
                List<FieldPlanner> checkCollWith = GetPlannersFromPort(CollisionWith, false);

                for (int i = 0; i < checkCollWith.Count; i++)
                {
                    FieldPlanner pl = checkCollWith[i];
                    planner.LatestChecker.PushOutOfCollisionAway(pl.LatestChecker, RoundAccordingly, PushPowerMultiply);
                }
            }

            if (Debugging)
            {
                if (planner.LatestChecker._IsCollidingWith_MyFirstCollisionCell == null)
                {
                    DebuggingInfo = "Collision of " + planner.name + planner.ArrayNameString + " not detected";
                }
                else
                {
                    DebuggingInfo = "Collision of " + planner.name + planner.ArrayNameString + " DETECTED";
                }
            }
        }



#if UNITY_EDITOR

        private UnityEditor.SerializedProperty sp = null;
        public override void Editor_OnNodeBodyGUI(ScriptableObject setup)
        {
            CollisionWith.Editor_DefaultValueInfo = "(All)";
            base.Editor_OnNodeBodyGUI(setup);

            if (_EditorFoldout)
            {
                GUILayout.Space(1);

                ToPush.AllowDragWire = true;
                baseSerializedObject.Update();
                if (sp == null) sp = baseSerializedObject.FindProperty("ToPush");
                UnityEditor.SerializedProperty scp = sp.Copy();
                UnityEditor.EditorGUILayout.PropertyField(scp);
                scp.Next(false); UnityEditor.EditorGUILayout.PropertyField(scp);
                scp.Next(false); UnityEditor.EditorGUILayout.PropertyField(scp);
                //scp.Next(false); UnityEditor.EditorGUILayout.PropertyField(scp);
                baseSerializedObject.ApplyModifiedProperties();
            }
            else
            {
                ToPush.AllowDragWire = false;

                if (CollisionWith.PortState() != EPortPinState.Connected)
                    if (CollisionWith.UniquePlannerID < 0)
                    {
                        GUILayout.Space(-2);
                        UnityEditor.EditorGUILayout.HelpBox("Collide with all", UnityEditor.MessageType.None);
                    }
            }
        }

#endif

    }
}