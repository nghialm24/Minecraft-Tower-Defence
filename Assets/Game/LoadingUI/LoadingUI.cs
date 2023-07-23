using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
	internal class LoadingUI : MonoBehaviour
	{
		[SerializeField] private Image background;
		[SerializeField] private RectTransform progressBar;
		[SerializeField] private RectTransform progressBg;
		[SerializeField] private float fakeLoadTime = 1.0f;
		[SerializeField] private Text text;
		private void Awake()
		{
			var width = progressBg.sizeDelta.x;
			var size = progressBar.sizeDelta;
			DOVirtual
				.Float(0, 1, fakeLoadTime, t =>
				{
					size.x = width * t;
					progressBar.sizeDelta = size;
					text.text = ((int)(size.x*0.2f)) + " %";
				})
				.OnComplete(() =>
				{
					size.x = width;
					progressBar.sizeDelta = size;
					Destroy(gameObject);
				});
		}
	}
}