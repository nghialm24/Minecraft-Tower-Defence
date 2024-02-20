using System;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
	[RequireComponent(typeof(Button))]
	internal class ToggleButton : MonoBehaviour
	{
		[SerializeField] private Button button;
		[SerializeField] private Image image;
		[SerializeField] private Material grayscaleMaterial;
		[SerializeField] private bool bgm;	
		private bool _active;
		private Action<bool> _toggle;
		private Material _normalMaterial;
		internal void Init(bool active, Action<bool> toggle)
		{
			_toggle = toggle;
			_normalMaterial = image.material;
			UpdateState(active);
			button.onClick.AddListener(() =>
			{
				UpdateState(!_active);
				if(bgm)
					SoundManager.PlayMusic("Medieval_with_Nature_Sounds_music_only_-_AllenGrey");
			});
		}

		private void UpdateState(bool active)
		{
			_active = active;
			_toggle?.Invoke(active);
			image.material = active ? _normalMaterial : grayscaleMaterial;
		}
	}
}


