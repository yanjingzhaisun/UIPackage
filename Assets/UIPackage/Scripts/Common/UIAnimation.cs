using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace YJZUIFrame
{

	public class UIAnimation
	{
		public Tweener tweener;
		protected UISection targetUISection;
		public UIAnimation(UISection targetUISection) {
			this.targetUISection = targetUISection;
		}

		public void AnimationIn() {
			
			switch (targetUISection.UISectionAnimationType) {
				case UISectionAnimationType.FadeIn:
					if (targetUISection.UIMonitorSpace == UIMonitorSpace.Canvas)
						targetUISection.RectTransform.anchoredPosition = targetUISection.InitialPosition;
					else
						targetUISection.transform.position = targetUISection.InitialPosition;
					targetUISection.CanvasGroup.alpha = 0f;
					tweener = targetUISection.CanvasGroup.DOFade(1f, targetUISection.AnimationDuration).SetUpdate(true).OnComplete(() => targetUISection.OnFocusComplete());
					break;
				case UISectionAnimationType.ScaleIn:
					if (targetUISection.UIMonitorSpace == UIMonitorSpace.Canvas)
						targetUISection.RectTransform.anchoredPosition = targetUISection.InitialPosition;
					else
						targetUISection.transform.position = targetUISection.InitialPosition;
					targetUISection.RectTransform.localScale = Vector3.zero;
					tweener = targetUISection.RectTransform.DOPunchScale(targetUISection.TargetScale, targetUISection.AnimationDuration).SetUpdate(true).OnComplete(() => targetUISection.OnFocusComplete());
					break;
				case UISectionAnimationType.SlideIn:
					tweener = targetUISection.RectTransform.DOMove(targetUISection.SlideInPosition, targetUISection.AnimationDuration).SetUpdate(true).OnComplete(() => targetUISection.OnFocusComplete());
					break;
				default:
					break;
			}
		}
		public void AnimationOut() {
			switch (targetUISection.UISectionAnimationType)
			{
				case UISectionAnimationType.FadeIn:
					targetUISection.CanvasGroup.alpha = 1f;
					tweener = targetUISection.CanvasGroup.DOFade(0f, targetUISection.AnimationDuration).SetUpdate(true).OnComplete(() => targetUISection.OnClearComplete());
					break;
				case UISectionAnimationType.ScaleIn:
					
					targetUISection.RectTransform.localScale = targetUISection.TargetScale;
					tweener = targetUISection.RectTransform.DOPunchScale(Vector3.zero, targetUISection.AnimationDuration).SetUpdate(true).OnComplete(() => targetUISection.OnClearComplete());
					break;
				case UISectionAnimationType.SlideIn:
					tweener = targetUISection.RectTransform.DOMove(targetUISection.SlideOutPosition, targetUISection.AnimationDuration).SetUpdate(true).OnComplete(() => targetUISection.OnClearComplete());
					break;
				default:
					break;
			}
		}
	}


}