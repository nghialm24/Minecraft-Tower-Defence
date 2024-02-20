using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
	internal class LoseUI : Scene
	{
		[SerializeField] private Button retryButton;
		[SerializeField] private Button retry;

		private void Awake()
		{
			retryButton.onClick.AddListener(Close);
			retry.onClick.AddListener(Close);
		}

		private void Close()
		{
			SceneManager.ShowLoading( () =>
			{
				SceneManager.ReloadScene(SceneID.Gameplay);
				SceneManager.CloseScene(SceneID.LoseUI);
			});
		}
	}
}