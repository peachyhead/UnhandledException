﻿using UnityEngine;
using System.Collections.Generic;
using FIMSpace.Generating.Planning.PlannerNodes.Field;
using System;

namespace FIMSpace.Generating.Planning
{
    public partial class FieldPlanner
    {
        //[System.Serializable]
        public class LocalVariables
        {
            // Local Variables
            public bool ForceRefresh = true;
            private IPlanNodesContainer parent = null;
            private List<PR_SetLocalVariable> localVars = new List<PR_SetLocalVariable>();

            public LocalVariables(IPlanNodesContainer parent)
            {
                this.parent = parent;
                ForceRefresh = true;
            }

            public void RefreshList()
            {
                localVars.Clear();

                if (parent == null) return;
                if (parent.Procedures == null) return;

                for (int i = 0; i < parent.Procedures.Count; i++)
                {
                    var prc = parent.Procedures[i];
                    if (prc is PR_SetLocalVariable)
                    {
                        localVars.Add(prc as PR_SetLocalVariable);
                    }
                }

                if (parent.PostProcedures == null) return;

                for (int i = 0; i < parent.PostProcedures.Count; i++)
                {
                    var prc = parent.PostProcedures[i];
                    if (prc is PR_SetLocalVariable)
                    {
                        localVars.Add(prc as PR_SetLocalVariable);
                    }
                }
            }


            public int GetVariablesCount()
            {
                return localVars.Count;
            }

            public PR_SetLocalVariable GetLocalVar(int id)
            {
                if (localVars.Count == 0) RefreshList();
                if (id < 0 || id >= localVars.Count) return null;
                return localVars[id];
            }

            public PR_SetLocalVariable GetLocalVar(string name)
            {
                if (localVars.Count == 0) RefreshList();
                for (int i = 0; i < localVars.Count; i++)
                {
                    if (localVars[i].NameID == name) return localVars[i];
                }

                //if ( generateIfNone)
                //{
                //    PR_SetLocalVariable localVar = CreateInstance<PR_SetLocalVariable>();
                //    localVar.NameID = name;
                //    localVar.VName = name;
                //    localVars.Add(localVar);
                //    return localVar;
                //}

                return null;
            }

            private int[] _LocalVarIds = null;
            public int[] GetLocalVarIDList(bool forceRefresh = false)
            {
                if (parent == null) return _errorI;

                if (Event.current != null) if (Event.current.type == EventType.MouseDown) forceRefresh = true;

                if (ForceRefresh) { RefreshList(); forceRefresh = true; ForceRefresh = false; }


                if (forceRefresh || _LocalVarIds == null || _LocalVarIds.Length != localVars.Count)
                {
                    _LocalVarIds = new int[localVars.Count];

                    for (int i = 0; i < localVars.Count; i++)
                    {
                        _LocalVarIds[i] = i;
                    }
                }

                return _LocalVarIds;
            }


            private GUIContent[] _locVarNames = null;
            public GUIContent[] GetLocalVarsNameList(bool forceRefresh = false)
            {
                if (parent == null) return _errorIN;

                if (Event.current != null) if (Event.current.type == EventType.MouseDown)
                    {
                        //UnityEngine.Debug.Log("local vars refresh");
                        forceRefresh = true;
                        ForceRefresh = true;
                    }

                if (ForceRefresh) { RefreshList(); forceRefresh = true; ForceRefresh = false; }


                if (forceRefresh || _locVarNames == null || _locVarNames.Length != localVars.Count)
                {
                    _locVarNames = new GUIContent[localVars.Count];
                    for (int i = 0; i < localVars.Count; i++)
                    {
                        _locVarNames[i] = new GUIContent(localVars[i].VName);
                    }
                }

                return _locVarNames;
            }

            static int[] _errorI = new int[1] { -1 };
            static GUIContent[] _errorIN = new GUIContent[1] { new GUIContent("Error") };

        }
    }
}