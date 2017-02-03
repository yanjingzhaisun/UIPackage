using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YJZUIFrame
{
	public enum UISectionAnimationType
	{
		FadeIn, SlideIn, ScaleIn
	}
	public enum UIMonitorSpace
	{
		Canvas, World
	}

	public enum UIFunctionStatus { 
		None,
		Ready,
		AnimationIn,
		Functioning,
		AnimationOut
	}

	public delegate void UITransitionDel();
	[RequireComponent(typeof(CanvasGroup))]
	public class UISection : MonoBehaviour
	{
		private CanvasGroup _canvasGroup;
		public CanvasGroup CanvasGroup
		{
			get
			{
				if (_canvasGroup == null)
					_canvasGroup = GetComponent<CanvasGroup>();
				return _canvasGroup;
			}
		}
		public UIMonitorSpace UIMonitorSpace;
		public UISectionAnimationType UISectionAnimationType;
		public UIFunctionStatus UIFunctionStatus;
		public float AnimationDuration;
		public Vector3 SlideInPosition, SlideOutPosition;
		public Vector3 InitialPosition;
		public Vector3 TargetScale;
		private RectTransform _rectTransform;
		public RectTransform RectTransform
		{
			get
			{
				if (_rectTransform == null)
					_rectTransform = GetComponent<RectTransform>();
				return _rectTransform;
			}
		}

		public UITransitionDel OnFocusDel, OnFocusCompleteDel, OnClearDel, OnClearCompleteDel;
		public bool IsDestroyAfterFinished = false;
		public bool IsInteractable = true;
		public bool IsIgnoreParentGroup = true;


		UIAnimation _uiAnimation;
		UIAnimation uiAnimation {
			get {
				if (_uiAnimation == null)
					_uiAnimation = new UIAnimation(this);
				return _uiAnimation;
				
			}
		}


		// Use this for initialization
		public virtual void Start()
		{

		}

		// Update is called once per frame
		public virtual void Update()
		{

		}

		public virtual bool Init(params object[] information) {
			if (UIFunctionStatus == UIFunctionStatus.None)
				UIFunctionStatus = UIFunctionStatus.Ready;
			if (UIFunctionStatus != UIFunctionStatus.Ready)
				return false;

			return true;
		}

		public virtual bool OnFocus() {
			switch (UIFunctionStatus) {
				case UIFunctionStatus.None:
					return false;
				case UIFunctionStatus.Ready:
					break;
				case UIFunctionStatus.AnimationIn:
					return false;
				case UIFunctionStatus.Functioning:
					return false;
				case UIFunctionStatus.AnimationOut:
					return false;
			}
			CanvasGroup.interactable = IsInteractable;
			CanvasGroup.ignoreParentGroups = IsIgnoreParentGroup;
			CanvasGroup.blocksRaycasts = true;
			UIFunctionStatus = UIFunctionStatus.AnimationIn;
			uiAnimation.AnimationIn();
			if (OnFocusDel != null)
				OnFocusDel();
			return true;
		}
		public virtual bool OnFocusComplete() {
			switch (UIFunctionStatus)
			{
				case UIFunctionStatus.None:
					return false;
				case UIFunctionStatus.Ready:
					return false;
				case UIFunctionStatus.AnimationIn:
					break;
				case UIFunctionStatus.Functioning:
					return false;
				case UIFunctionStatus.AnimationOut:
					return false;
			}
			UIFunctionStatus = UIFunctionStatus.Functioning;
			if (OnFocusCompleteDel != null)
				OnFocusCompleteDel();
			return true;
		}

		public virtual bool OnClear() {
			switch (UIFunctionStatus)
			{
				case UIFunctionStatus.None:
					return false;
				case UIFunctionStatus.Ready:
					return false;
				case UIFunctionStatus.AnimationIn:
					uiAnimation.tweener.Kill();
					break;
				case UIFunctionStatus.Functioning:
					break;
				case UIFunctionStatus.AnimationOut:
					return false;
			}
			UIFunctionStatus = UIFunctionStatus.AnimationOut;
			uiAnimation.AnimationOut();
			if (OnClearDel != null)
				OnClearDel();
			return true;
		}
		public virtual bool OnClearComplete() {
			switch (UIFunctionStatus)
			{
				case UIFunctionStatus.None:
					return false;
				case UIFunctionStatus.Ready:
					return false;
				case UIFunctionStatus.AnimationIn:
					return false;
				case UIFunctionStatus.Functioning:
					return false;
				case UIFunctionStatus.AnimationOut:
					break;
			}
			UIFunctionStatus = UIFunctionStatus.Ready;
			CanvasGroup.interactable = false;
			CanvasGroup.ignoreParentGroups = IsIgnoreParentGroup;
			CanvasGroup.blocksRaycasts = false;
			if (OnClearCompleteDel != null)
				OnClearCompleteDel();
			if (IsDestroyAfterFinished)
				Destroy(gameObject);
			return true;
		}
	}
}
