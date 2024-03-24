using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;

public class RemoteConfigController : SingletonMonoBehaviour<RemoteConfigController>
{
    public Action<bool> OnFetchCompleted;

    public bool isDataFetched = false;

    public static float interstitialAd_SessionDelayed = 90;

    public static float interstitialAd_LevelDelayed = 30;

    public static float interstitialAd_FirstLevelDelayed = 120;

    public static float interstitialAd_Interval = 60f;

    public static float interstitialAd_Rewarded_Interval = 20f;

    public static float interstitialAd_PopupCloseDelayed = 5f;

    public static float interstitialAd_MinReloadFailedDelay = 2f;

    public static float interstitialAd_MaxReloadFailedDelay = 128f;

    public static float rewardedAd_MinReloadFailedDelay = 2f;

    public static float rewardedAd_MaxReloadFailedDelay = 128f;

    private static string showRateLevelData = "3|6|14|25";

    public static List<int> showRateLevels = new List<int>();

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

    public void Initialize()
    {
        LoadData();

        Dictionary<string, object> defaults =
          new Dictionary<string, object>
          {
              {"interstitialAd_MinReloadFailedDelay", interstitialAd_MinReloadFailedDelay },
              {"interstitialAd_MaxReloadFailedDelay", interstitialAd_MaxReloadFailedDelay },
              {"rewardedAd_MinReloadFailedDelay", rewardedAd_MinReloadFailedDelay },
              {"rewardedAd_MaxReloadFailedDelay", rewardedAd_MaxReloadFailedDelay },
              {"showRateLevelData", showRateLevelData },

              {"interstitialAd_SessionDelayed", interstitialAd_SessionDelayed },
              {"interstitialAd_LevelDelayed", interstitialAd_LevelDelayed },
              {"interstitialAd_FirstLevelDelayed", interstitialAd_FirstLevelDelayed },
              {"interstitialAd_Interval", interstitialAd_Interval },
              {"interstitialAd_Rewarded_Interval", interstitialAd_Rewarded_Interval },
              {"interstitialAd_PopupCloseDelayed", interstitialAd_PopupCloseDelayed }
          };

        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);

        Debug.Log("RemoteConfig configured and ready!");

        FetchDataAsync();
    }

    public void FetchDataAsync()
    {
        Debug.Log("Fetching data...");

        System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (fetchTask.IsCanceled)
        {
            Debug.Log("Fetch canceled.");
        }
        else if (fetchTask.IsFaulted)
        {
            Debug.Log("Fetch encountered an error.");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.Log("Fetch completed successfully!");
        }

        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus)
        {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                       info.FetchTime) + Time.frameCount);
                RefrectProperties();

                OnFetchCompleted?.Invoke(true);
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason" + Time.frameCount);
                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime + Time.frameCount);
                        break;
                }
                OnFetchCompleted?.Invoke(false);
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending." + Time.frameCount);
                OnFetchCompleted?.Invoke(false);
                break;
        }
    }

    private void RefrectProperties()
    {
        interstitialAd_MinReloadFailedDelay = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_MinReloadFailedDelay").DoubleValue;
        interstitialAd_MaxReloadFailedDelay = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_MaxReloadFailedDelay").DoubleValue;
        rewardedAd_MinReloadFailedDelay = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("rewardedAd_MinReloadFailedDelay").DoubleValue;
        rewardedAd_MaxReloadFailedDelay = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("rewardedAd_MaxReloadFailedDelay").DoubleValue;
        showRateLevelData = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("showRateLevelData").StringValue;

        interstitialAd_SessionDelayed = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_SessionDelayed").DoubleValue;
        interstitialAd_LevelDelayed = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_LevelDelayed").DoubleValue;
        interstitialAd_FirstLevelDelayed = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_FirstLevelDelayed").DoubleValue;
        interstitialAd_Interval = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_Interval").DoubleValue;
        interstitialAd_Rewarded_Interval = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_Rewarded_Interval").DoubleValue;
        interstitialAd_PopupCloseDelayed = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("interstitialAd_PopupCloseDelayed").DoubleValue;

        GetShowRateLevels();

        Debug.LogWarning(showRateLevelData);

        SaveData();
        isDataFetched = true;
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat("interstitialAd_MinReloadFailedDelay", interstitialAd_MinReloadFailedDelay);
        PlayerPrefs.SetFloat("interstitialAd_MaxReloadFailedDelay", interstitialAd_MaxReloadFailedDelay);
        PlayerPrefs.SetFloat("rewardedAd_MinReloadFailedDelay", rewardedAd_MinReloadFailedDelay);
        PlayerPrefs.SetFloat("rewardedAd_MaxReloadFailedDelay", rewardedAd_MaxReloadFailedDelay);
        PlayerPrefs.SetString("showRateLevelData", showRateLevelData);

        PlayerPrefs.SetFloat("interstitialAd_SessionDelayed", interstitialAd_SessionDelayed);
        PlayerPrefs.SetFloat("interstitialAd_LevelDelayed", interstitialAd_LevelDelayed);
        PlayerPrefs.SetFloat("interstitialAd_FirstLevelDelayed", interstitialAd_FirstLevelDelayed);
        PlayerPrefs.SetFloat("interstitialAd_Interval", interstitialAd_Interval);
        PlayerPrefs.SetFloat("interstitialAd_Rewarded_Interval", interstitialAd_Rewarded_Interval);
        PlayerPrefs.SetFloat("interstitialAd_PopupCloseDelayed", interstitialAd_PopupCloseDelayed);
    }

    private void LoadData()
    {
        interstitialAd_MinReloadFailedDelay = PlayerPrefs.GetFloat("interstitialAd_MinReloadFailedDelay", interstitialAd_MinReloadFailedDelay);
        interstitialAd_MaxReloadFailedDelay = PlayerPrefs.GetFloat("interstitialAd_MaxReloadFailedDelay", interstitialAd_MaxReloadFailedDelay);
        rewardedAd_MinReloadFailedDelay = PlayerPrefs.GetFloat("rewardedAd_MinReloadFailedDelay", rewardedAd_MinReloadFailedDelay);
        rewardedAd_MaxReloadFailedDelay = PlayerPrefs.GetFloat("rewardedAd_MaxReloadFailedDelay", rewardedAd_MaxReloadFailedDelay);
        showRateLevelData = PlayerPrefs.GetString("showRateLevelData", showRateLevelData);

        interstitialAd_SessionDelayed = PlayerPrefs.GetFloat("interstitialAd_SessionDelayed", interstitialAd_SessionDelayed);
        interstitialAd_LevelDelayed = PlayerPrefs.GetFloat("interstitialAd_LevelDelayed", interstitialAd_LevelDelayed);
        interstitialAd_FirstLevelDelayed = PlayerPrefs.GetFloat("interstitialAd_FirstLevelDelayed", interstitialAd_FirstLevelDelayed);
        interstitialAd_Interval = PlayerPrefs.GetFloat("interstitialAd_Interval", interstitialAd_Interval);
        interstitialAd_Rewarded_Interval = PlayerPrefs.GetFloat("interstitialAd_Rewarded_Interval", interstitialAd_Rewarded_Interval);
        interstitialAd_PopupCloseDelayed = PlayerPrefs.GetFloat("interstitialAd_PopupCloseDelayed", interstitialAd_PopupCloseDelayed);

        GetShowRateLevels();
    }

    private void GetShowRateLevels()
    {
        showRateLevels.Clear();

        var elements = showRateLevelData.Split('|');
        for (int i = 0; i < elements.Length; i++)
        {
            if (int.TryParse(elements[i], out int level))
                showRateLevels.Add(level);
        }
    }
}
