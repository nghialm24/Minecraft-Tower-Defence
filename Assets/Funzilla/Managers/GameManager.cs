
using System;
using System.Collections.Generic;
//using Facebook.Unity;
using UnityEngine;
// using Firebase;
// using Firebase.Analytics;
//using GameAnalyticsSDK;

namespace Funzilla
{
	internal class GameManager : Singleton<GameManager>
	{
		//internal static bool FirebaseOk { get; private set; }

		private enum State
		{
			None,
			InitializingFirebase,
			InitializingConfig,
			Initialized
		}

		private State _state = State.None;
		private readonly Queue<Action> _queue = new Queue<Action>();

		private void Start()
		{
			if (_state != State.Initialized && _queue.Count <= 0)
			{
				Init(() =>
				{
					SceneManager.OpenScene(SceneID.Gameplay);
				});
			}
		}

		internal static void Init(Action onComplete)
		{
			switch (Instance._state)
			{
				case State.None:
					Instance._state = State.InitializingFirebase;
					Application.targetFrameRate = 60;
					//GameAnalytics.Initialize();
					//FB.Init();
					AdsUtility.Initiate(false);
#if UNITY_EDITOR
					
#else
					if (FirebaseController.Instance.IsReady())
					{
						OnFirebaseReady();
					}
					else
					{
						FirebaseController.Instance.OnFirebaseReady += OnFirebaseReady;
					}
#endif
					if (onComplete != null) Instance._queue.Enqueue(onComplete);
					break;
				case State.InitializingFirebase:
				case State.InitializingConfig:
					if (onComplete != null) Instance._queue.Enqueue(onComplete);
					break;
				case State.Initialized:
					onComplete?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Update()
		{
			switch (_state)
			{
				case State.None:
					break;
				case State.InitializingFirebase:
					// _state = State.InitializingConfig;
					// Config.Init();
					if (AdsUtility.Initiated())
					{
						_state = State.InitializingConfig;
						Config.Init();
					}
					break;
				case State.InitializingConfig:
					if (Config.Initialized)
					{
						_state = State.Initialized;
						enabled = false;
						while (_queue.Count > 0)
						{
							var onComplete = _queue.Dequeue();
							onComplete?.Invoke();
						}
					}
					break;
				case State.Initialized:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private static void OnFirebaseReady()
		{
			var firebaseController = FirebaseController.Instance;

			RemoteConfigController.Instance.OnFetchCompleted = OnRemoteConfigFetched;
			RemoteConfigController.Instance.Initialize();

			firebaseController.SetUserId(SystemInfo.deviceUniqueIdentifier);
		}

		private static void OnRemoteConfigFetched(bool result)
		{
		}
	}
}