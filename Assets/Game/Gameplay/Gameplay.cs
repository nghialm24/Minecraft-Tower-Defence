using System;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
	internal class Gameplay : Scene
	{
		[SerializeField] private Text levelText;
		[SerializeField] private Button settingButton;
		[SerializeField] private Button winButton;
		[SerializeField] private Button loseButton;
		[SerializeField] bool needLoad;
		[SerializeField] bool isTest;
		[SerializeField] string levelTest;
		private enum State
		{
			Init,
			Play,
			Win,
			Lose
		}

		private State _state;

		internal static Gameplay Instance { get; private set; }
		internal Level Level { get; private set; }
		internal static int CoinsEarned;
		public DataConfigSO dataConfigSO;
		public ItemCollection itemCollection;
		private void Init()
		{
			if (!needLoad)
			{
				Level = GetComponentInChildren<Level>();
			}
			else
			{
				if (!isTest)
				{
					// Load level
					var levelIndex = (Profile.Level - 1) % LevelManager.Levels.Count;
					var levelAsset = LevelManager.Levels[levelIndex];
					var levelPrefab = Resources.Load<Level>($"Levels/{levelAsset}");
					Level = Instantiate(levelPrefab, transform);
				}
				else
				{
					var levelPrefab = Resources.Load<Level>($"Levels/{levelTest}");
					Level = Instantiate(levelPrefab, transform);
				}
			}
			
			// Hide splash screen after game is initialized
			levelText.text = $"Level {Profile.Level}";
			SceneManager.HideLoading();
		}

		internal void Play()
		{
			ChangeState(State.Play);
		}

		internal void Win()
		{
			ChangeState(State.Win);
		}

		internal void Lose()
		{
			ChangeState(State.Lose);
		}

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			GameManager.Init(Init);
			settingButton.onClick.AddListener(()=>SceneManager.OpenPopup(SceneID.SettingUI));
			winButton.onClick.AddListener(Win);
			loseButton.onClick.AddListener(Lose);
		}

		private void ChangeState(State newState)
		{
			if (_state == newState) return;
			ExitOldState();
			_state = newState;
			EnterNewState();
		}

		private void EnterNewState()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					Analytics.LogLevelStartEvent();
					break;
				case State.Win:
					Analytics.LogLevelCompleteEvent();
					SceneManager.OpenScene(SceneID.WinUI);
					Profile.Level++;
					break;
				case State.Lose:
					Analytics.LogLevelFailEvent();
					SceneManager.OpenScene(SceneID.LoseUI);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void ExitOldState()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					break;
				case State.Win:
					break;
				case State.Lose:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Update()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					break;
				case State.Win:
					break;
				case State.Lose:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}