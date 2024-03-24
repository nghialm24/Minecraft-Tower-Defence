using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using Firebase.Extensions;

public class FirebaseController : SingletonMonoBehaviour<FirebaseController>
{
    private bool firebaseIsReady = false;

    public Action OnFirebaseReady;

    public bool IsReady() { return firebaseIsReady; }

    public override void Awake()
    {
        base.Awake();

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase was ready");

                firebaseIsReady = true;
                OnFirebaseReady?.Invoke();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public void SetUserId(string userId)
    {
        if (!firebaseIsReady) return;

        FirebaseAnalytics.SetUserId(userId);
    }

    public void SetUserProperty(string key, string value)
    {
        if (!firebaseIsReady) return;

        FirebaseAnalytics.SetUserProperty(key, value);
    }

    public void LogEventStageComplete(string name)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] parameters = {
          new Firebase.Analytics.Parameter("name", name)
        };

        FirebaseAnalytics.LogEvent("stage_complete", parameters);
    }  

    public void LogEventLevelComplete(int level, float duration, bool start)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] parameters = {
          new Firebase.Analytics.Parameter("level", level.ToString()),
          new Firebase.Analytics.Parameter("action", start ? "start" : "finish"),
          new Firebase.Analytics.Parameter("duration", duration)
        };

        FirebaseAnalytics.LogEvent("level_completed", parameters);
    }

    public void LogEventInterstitialAd(string position)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] parameters = {
          new Firebase.Analytics.Parameter("position", position)
        };

        FirebaseAnalytics.LogEvent("ad_impression_interstitial", parameters);
    }

    public void LogEventRewardedAd(string position)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] parameters = {
          new Firebase.Analytics.Parameter("position", position)
        };

        FirebaseAnalytics.LogEvent("ad_impression_rewarded", parameters);
    }

    public void LogEventUnlock(string type, string id, string currency)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] parameters = {
          new Firebase.Analytics.Parameter("type", type),
          new Firebase.Analytics.Parameter("id", id),
          new Firebase.Analytics.Parameter("currency", currency)
        };

        FirebaseAnalytics.LogEvent("unlock", parameters);
    }

    public void LogEventPurchase(string productId, string price)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] parameters = {
          new Firebase.Analytics.Parameter("product_id", productId),
          new Firebase.Analytics.Parameter("price", price)
        };

        FirebaseAnalytics.LogEvent("iap", parameters);
    }

    public bool LogEvent(string name)
    {
        if (!firebaseIsReady) return false;

        FirebaseAnalytics.LogEvent(name);

        Debug.LogWarning(name);

        return true;
    }

    public void LogEventError(string name, string message)
    {
        if (!firebaseIsReady) return ;

        Firebase.Analytics.Parameter[] pr =
            {
                new Firebase.Analytics.Parameter("message", message)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent(name, pr);
    }

    public void LogEventAdsRevenue(string position, string ad_unit, string ad_network, string ad_format, double revenue, string currency)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] AdParameters = {
                new Firebase.Analytics.Parameter("position", position),
                new Firebase.Analytics.Parameter("ad_unit", ad_unit),
                new Firebase.Analytics.Parameter("ad_format", ad_format),
                new Firebase.Analytics.Parameter("ad_network", ad_network),
                new Firebase.Analytics.Parameter("currency", currency),
                new Firebase.Analytics.Parameter("value", revenue)
                    };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_paid", AdParameters);

        Debug.LogWarning(position + "|" + ad_unit + "|" + ad_network + "|" + ad_format + "|" + revenue);
    }

    public void LogEventAdsImpression(string position, string ad_format, double revenue, string currency)
    {
        if (!firebaseIsReady) return;

        Firebase.Analytics.Parameter[] AdParameters = {
                new Firebase.Analytics.Parameter("position", position),
                new Firebase.Analytics.Parameter("ad_format", ad_format),
                new Firebase.Analytics.Parameter("currency", currency),
                new Firebase.Analytics.Parameter("value", revenue)
                    };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impr", AdParameters);

        Debug.LogWarning(position + "|" + ad_format + "|" + revenue);
    }
}
