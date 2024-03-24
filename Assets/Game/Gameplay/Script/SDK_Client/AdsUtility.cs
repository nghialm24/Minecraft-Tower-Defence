using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class AdsUtility
{
    public static bool useMaxBanner;

    public static bool adsEnabled = true;

    public static void Initiate(bool removeAds)
    {
        MAXAdsManager.Instance.Initialize();
        MAXAdsManager.Instance.SetRemovedAds(removeAds);
    }

    public static bool Initiated()
    {
        return MAXAdsManager.Instance.Initialized;
    }

    public static bool IsGDPRRegion()
    {
        return false;
    }
    
    public static void LoadAndShowCMPFlow()
    {

    }

    public static void ShowRewardedAds(string position, Action SucceededAction)
    {
        if (adsEnabled)
        {
            AdsPosition.positionRewarded = position;
            MAXAdsManager.Instance.ShowRewardedAd(position, SucceededAction);
        }
        else
        {
            SucceededAction?.Invoke();
        }       
    }

    public static void ShowInterstitialAds(string position, Action ClosedAction, bool ignoreInterval = false)
    {
        if (adsEnabled)
        {
            AdsPosition.positionInterstitial = position;
            MAXAdsManager.Instance.ShowInterstitialAd(position, ClosedAction, ignoreInterval);
        }
        else
        {
            ClosedAction?.Invoke();
        }       
    }

    public static bool IsInterstitialAdsReady()
    {
        if (adsEnabled)
        {
            return MAXAdsManager.Instance.IsInterstitialLoaded();
        }
        else
        {
            return false;
        }        
    }

    public static bool IsRewardedAdsReady()
    {
        return MAXAdsManager.Instance.IsRewardedReady();
    }

    public static void LockAppOpenAds()
    {
        MAXAdsManager.Instance.BlockAppOpenAds();
    }

    public static void UnlockAppOpenAds()
    {
        MAXAdsManager.Instance.UnlockAppOpenAds();
    }

    public static void SetBannerVisibility(bool flag)
    {
        if (adsEnabled)
        {
            MAXAdsManager.Instance.SetBannerAdVisibility(flag);
        }
        else
        {
            MAXAdsManager.Instance.SetBannerAdVisibility(false);
        }       
    }

    public static bool ShowAppOpenAds()
    {
        if (adsEnabled)
        {
            return MAXAdsManager.Instance.ShowAppOpenAd();
        }
        else
        {
            return false;
        }       
    }

    public static void SetRemovedAds(bool flag)
    {
        MAXAdsManager.Instance.SetRemovedAds(flag);
    }

    public static float GetBannerHeightInPixels(out bool bannerSizeReady)
    {
        return MAXAdsManager.Instance.GetBannerHeightInPixels(out bannerSizeReady);
    }
}

public static class AdsPosition
{
    public const string none = "none";

    public static string positionBanner;

    public static string positionAppOpen;

    public static string positionInterstitial;

    public static string positionRewarded;
}


public static class AdsEventManager
{
    public static event Action<float> OnBannerResized;

    public static event Action OnAppOpenAdReadyToShow;

    public static event Action OnAppOpenAdClosed;

    public static event Action OnInterstitialAdRecordImpression;

    public static event Action OnRewardedAdRecordImpression;

    public static event Action OnRewardedAdRewardedAdSucceeded;

    public static event Action<bool> OnRewardedAdStateChanged;

    public static event Action OnRewardedAdsNotReadyCauseNoInternet;

    public static event Action OnRewardedAdsNotReadyCauseLoading;

    public static void NotifyEventBannerResized(float size)
    {
        OnBannerResized?.Invoke(size);
    }

    public static void NotifyEventAppOpenAdReadyToShow()
    {
        OnAppOpenAdReadyToShow?.Invoke();
    }

    public static void NotifyEventAppOpenAdClosed()
    {
        OnAppOpenAdClosed?.Invoke();
    }

    public static void NotifyEventRewardedAdRecordImpression()
    {
        OnRewardedAdRecordImpression?.Invoke();
    }

    public static void NotifyEventRewardedAdSucceeded()
    {
        OnRewardedAdRewardedAdSucceeded?.Invoke();
    }

    public static void NotifyEventInterstitialAdRecordImpression()
    {
        OnInterstitialAdRecordImpression?.Invoke();
    }

    public static void NotifyEventRewardedAdStateChanged(bool state)
    {
        OnRewardedAdStateChanged?.Invoke(state);
    }

    public static void NotifyEventRewardedAdsNotReadyCauseInternetNotReachable()
    {
        OnRewardedAdsNotReadyCauseNoInternet?.Invoke();
    }

    public static void NotifyEventRewardedAdsNotReadyCauseLoading()
    {
        OnRewardedAdsNotReadyCauseLoading?.Invoke();
    }
}
