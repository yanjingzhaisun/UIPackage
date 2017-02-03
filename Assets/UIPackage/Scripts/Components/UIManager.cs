using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YJZUIFrame
{
	public class UIManager : MonoBehaviour, ISubmitHandler, ICancelHandler
	{


		public static UIManager Instance;
		public void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else if (Instance != this)
				Destroy(gameObject);
		}

		public Stack<UISection> focusUIs;
		public void OnCancel(BaseEventData eventData)
		{
			throw new NotImplementedException();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			throw new NotImplementedException();
		}
	}
}