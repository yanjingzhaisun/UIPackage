using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YJZUIFrame
{
	[CustomEditor(typeof(UISection))]
	public class UISectionEditor : Editor
	{

		public override void OnInspectorGUI()
		{
			
			UISection uisection = target as UISection;
			EditorGUILayout.LabelField("UI Monitor Space", EditorStyles.boldLabel);
			uisection.UIMonitorSpace = (UIMonitorSpace)EditorGUILayout.EnumPopup("Monitor Space", uisection.UIMonitorSpace);
			EditorGUILayout.LabelField("");

			EditorGUILayout.LabelField("UI Animation",EditorStyles.boldLabel);
			uisection.AnimationDuration = EditorGUILayout.FloatField("Animation Duration", uisection.AnimationDuration);
			uisection.UISectionAnimationType = (UISectionAnimationType)EditorGUILayout.EnumPopup("Animation Type", uisection.UISectionAnimationType);


			switch (uisection.UISectionAnimationType) {
				case UISectionAnimationType.FadeIn:
					if (uisection.UIMonitorSpace == UIMonitorSpace.Canvas)
						uisection.InitialPosition = EditorGUILayout.Vector2Field("Initial Position", uisection.InitialPosition);
					else
						uisection.InitialPosition = EditorGUILayout.Vector3Field("Initial Position", uisection.InitialPosition);
					break;
				case UISectionAnimationType.ScaleIn:
					if (uisection.UIMonitorSpace == UIMonitorSpace.Canvas)
						uisection.InitialPosition = EditorGUILayout.Vector2Field("Initial Position", uisection.InitialPosition);
					else
						uisection.InitialPosition = EditorGUILayout.Vector3Field("Initial Position", uisection.InitialPosition);
					uisection.TargetScale = EditorGUILayout.Vector2Field("Target Scale", uisection.TargetScale);
					break;
				case UISectionAnimationType.SlideIn:
					uisection.SlideInPosition = EditorGUILayout.Vector2Field("Target Position", uisection.SlideInPosition);
					uisection.SlideOutPosition = EditorGUILayout.Vector2Field("Initial Position", uisection.SlideOutPosition);
					break;
				default:
					break;
			}
			EditorGUILayout.LabelField("");
			EditorGUILayout.LabelField("UI Functionning Status -> ", uisection.UIFunctionStatus.ToString());
			EditorGUILayout.LabelField("");
			//EditorGUILayout.field("UI Functionning Status -> ", uisection.UIFunctionStatus.ToString());

			//base.OnInspectorGUI();
		}

	}
}
