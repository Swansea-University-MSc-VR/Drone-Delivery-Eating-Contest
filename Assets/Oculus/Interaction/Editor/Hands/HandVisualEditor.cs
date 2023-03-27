/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Oculus.Interaction.Input;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Oculus.Interaction.Hands.Editor
{
    [CustomEditor(typeof(HandVisual))]
    public class HandVisualEditor : UnityEditor.Editor
    {
        private SerializedProperty _handProperty;
        private SerializedProperty _rootProperty;

        private IHand Hand => _handProperty.objectReferenceValue as IHand;

        private void OnEnable()
        {
            _handProperty = serializedObject.FindProperty("_hand");
            _rootProperty = serializedObject.FindProperty("_root");
        }

        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject);
            serializedObject.ApplyModifiedProperties();

            HandVisual visual = (HandVisual)target;
            InitializeSkeleton(visual);

            if (Hand == null)
            {
                return;
            }

            if (GUILayout.Button("Auto Map Joints"))
            {
                AutoMapJoints(visual);
                EditorUtility.SetDirty(visual);
                EditorSceneManager.MarkSceneDirty(visual.gameObject.scene);
            }

            EditorGUILayout.LabelField("Joints", EditorStyles.boldLabel);
            HandJointId start = HandJointId.HandStart;
            HandJointId end = HandJointId.HandEnd;

            for (int i = (int)start; i < (int)end; ++i)
            {
                string jointName = HandJointLabelFromJointId((HandJointId)i);
                visual.Joints[i] = (Transform)EditorGUILayout.ObjectField(jointName,
                    visual.Joints[i], typeof(Transform), true);
            }
        }

        private static readonly string[] _fbxHandSidePrefix = { "l_", "r_" };
        private static readonly string _fbxHandBonePrefix = "b_";

        private static readonly string[] _fbxHandBoneNames =
        {
            "wrist",
            "forearm_stub",
            "thumb0",
            "thumb1",
            "thumb2",
            "thumb3",
            "index1",
            "index2",
            "index3",
            "middle1",
            "middle2",
            "middle3",
            "ring1",
            "ring2",
            "ring3",
            "pinky0",
            "pinky1",
            "pinky2",
            "pinky3"
        };

        private static readonly string[] _fbxHandFingerNames =
        {
            "thumb",
            "index",
            "middle",
            "ring",
            "pinky"
        };

        private void InitializeSkeleton(HandVisual visual)
        {
            if (visual.Joints.Count == 0)
            {
                for (int i = (int)HandJointId.HandStart; i < (int)HandJointId.HandEnd; ++i)
                {
                    visual.Joints.Add(null);
                }
            }
        }

        private void AutoMapJoints(HandVisual visual)
        {
            if (Hand == null)
            {
                InitializeSkeleton(visual);
                return;
            }

            Transform rootTransform = visual.transform;
            if (_rootProperty.objectReferenceValue != null)
            {
                rootTransform = _rootProperty.objectReferenceValue as Transform;
            }

            for (int i = (int)HandJointId.HandStart; i < (int)HandJointId.HandEnd; ++i)
            {
                string fbxBoneName = FbxBoneNameFromHandJointId(visual, (HandJointId)i);
                Transform t = rootTransform.FindChildRecursive(fbxBoneName);
                visual.Joints[i] = t;
            }
        }

        private string FbxBoneNameFromHandJointId(HandVisual visual, HandJointId handJointId)
        {
            if (handJointId >= HandJointId.HandThumbTip && handJointId <= HandJointId.HandPinkyTip)
            {
                return _fbxHandSidePrefix[(int)Hand.Handedness] + _fbxHandFingerNames[(int)handJointId - (int)HandJointId.HandThumbTip] + "_finger_tip_marker";
            }
            else
            {
                return _fbxHandBonePrefix + _fbxHandSidePrefix[(int)Hand.Handedness] + _fbxHandBoneNames[(int)handJointId];
            }
        }

        // force aliased enum values to the more appropriate value
        private static string HandJointLabelFromJointId(HandJointId handJointId)
        {
            switch (handJointId)
            {
                case HandJointId.HandWristRoot:
                    return "HandWristRoot";
                case HandJointId.HandForearmStub:
                    return "HandForearmStub";
                case HandJointId.HandThumb0:
                    return "HandThumb0";
                case HandJointId.HandThumb1:
                    return "HandThumb1";
                case HandJointId.HandThumb2:
                    return "HandThumb2";
                case HandJointId.HandThumb3:
                    return "HandThumb3";
                case HandJointId.HandIndex1:
                    return "HandIndex1";
                case HandJointId.HandIndex2:
                    return "HandIndex2";
                case HandJointId.HandIndex3:
                    return "HandIndex3";
                case HandJointId.HandMiddle1:
                    return "HandMiddle1";
                case HandJointId.HandMiddle2:
                    return "HandMiddle2";
                case HandJointId.HandMiddle3:
                    return "HandMiddle3";
                case HandJointId.HandRing1:
                    return "HandRing1";
                case HandJointId.HandRing2:
                    return "HandRing2";
                case HandJointId.HandRing3:
                    return "HandRing3";
                case HandJointId.HandPinky0:
                    return "HandPinky0";
                case HandJointId.HandPinky1:
                    return "HandPinky1";
                case HandJointId.HandPinky2:
                    return "HandPinky2";
                case HandJointId.HandPinky3:
                    return "HandPinky3";
                case HandJointId.HandThumbTip:
                    return "HandThumbTip";
                case HandJointId.HandIndexTip:
                    return "HandIndexTip";
                case HandJointId.HandMiddleTip:
                    return "HandMiddleTip";
                case HandJointId.HandRingTip:
                    return "HandRingTip";
                case HandJointId.HandPinkyTip:
                    return "HandPinkyTip";
                default:
                    return "HandUnknown";
            }
        }

    }
}
