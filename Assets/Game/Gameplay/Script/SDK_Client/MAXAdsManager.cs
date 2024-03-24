using System;
using System.Collections;
using UnityEngine;

public class MAXAdsManager : SingletonMonoBehaviour<MAXAdsManager>
{
    public static string maxSdkKey = "NDcrL4E6eTcwHnfZbHRNw-0AoxPCMdQSgFOFkOyJ8D1VJrumRobtyvufr93tccBk3mIya4_CTO_SENV-vluony";
    public static string ad_id_interstitial = "005cc65b3fb8a529";
    public static string ad_id_rewarded = "2dd0adbc569d433c";
    public static string ad_id_banner = "d6de826ff68cdc5c";
    public static string ad_id_appopen = "a734754f0cb078b9";

    private Action InterstitialAd_CloseAction;

    private Action RewardedAd_SucceededAction;

    private int interstitialAdsLocked = 0;

    private int interstitialRetryAttempt;

    private int rewardedRetryAttempt;

    private static bool maxSdkStarted = false;

    private bool isInitialized = false;

    public bool Initialized
    {
        get
        {
            return isInitialized;
        }
    }

    public void Initialize()
    {
        if (maxSdkStarted)
        {
            Debug.LogError("error_max_sdk_already_started");
            return;
        }

        Debug.Log("MaX SDK start: " + Time.realtimeSinceStartup);

        maxSdkStarted = true;

        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            Debug.Log("MAX SDK Initialized: " + Time.realtimeSinceStartup);
            isInitialized = true;

            InitializeAppOpenAds();
            InitializeBannerAds();
            InitializeInterstitialAds();
            InitializeRewardedAds();
        };

        MaxSdk.SetSdkKey(maxSdkKey);
        MaxSdk.InitializeSdk();

        StartCoroutine(WaitAdsStartDelay()); 
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void ImpressionSuccessEvent(MaxSdkBase.AdInfo adInfo, string format, string position)
    {
        if (adInfo != null)
        {
            var revenue = adInfo.Revenue;

            var firebaseController = FirebaseController.Instance;
            firebaseController.LogEventAdsRevenue(position, adInfo.AdUnitIdentifier, adInfo.NetworkName, format, revenue, "usd");
        }
    }

    #region Banner
    public void InitializeBannerAds()
    {      
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

        if (removedAds == false)
        {
            StartCoroutine(CreateBannerAdDelayed(0.5f));

            bannerHeight = Dp2Px(MaxSdkUtils.GetAdaptiveBannerHeight());
            AdsEventManager.NotifyEventBannerResized(bannerHeight);
        }
    }

    private IEnumerator CreateBannerAdDelayed(float delayed)
    {
        yield return new WaitForSecondsRealtime(delayed);

        CreateBannerAd();
    }

    private void CreateBannerAd()
    {
        MaxSdk.CreateBanner(ad_id_banner, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerExtraParameter(ad_id_banner, "adaptive_banner", "true");
        MaxSdk.UpdateBannerPosition(ad_id_banner, MaxSdkBase.BannerPosition.BottomCenter);
        UpdateBannerAdVisibility();

        // Set background or background color for banners to be fully functional.
        MaxSdk.SetBannerBackgroundColor(ad_id_banner, Color.black);

        bannerHeight = Dp2Px(MaxSdkUtils.GetAdaptiveBannerHeight());
        AdsEventManager.NotifyEventBannerResized(bannerHeight);

        Debug.Log("banner height max: " + bannerHeight);
    }

    private float Dp2Px(float dp)
    {
        return dp * (Screen.dpi / 160);
    }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        ImpressionSuccessEvent(adInfo, "banner", AdsPosition.positionBanner);
    }

    public void SetBannerAdVisibility(bool flag)
    {
        isShowingBanner = flag;

        UpdateBannerAdVisibility();
    }

    public float GetBannerHeightInPixels(out bool bannerSizeReady)
    {
        bannerSizeReady = bannerHeight > 0f;
        return bannerHeight;
    }

    public void UpdateBannerAdVisibility()
    {
        if (isShowingBanner && !removedAds && !isShowingAppOpenAds)
        {
            Debug.Log("show banner");
            MaxSdk.ShowBanner(ad_id_banner);
        }
        else
        {
            Debug.Log("hide banner");
            MaxSdk.HideBanner(ad_id_banner);
        }
    }

    private bool isShowingBanner = true;

    private float bannerHeight;
    #endregion

    #region AppOpen
    private bool isShowingAppOpenAds = false;

    private int blockAppOpenAd;

    private DateTime appOpenAdLoadTime;

    private int appOpenRetryAttemp;

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            AdsPosition.positionAppOpen = "app_paused";
        }
        else
        {            
            ShowAppOpenAd();
        }
    }

    public bool IsShowingAppOpenAds
    {
        get { return isShowingAppOpenAds; }
    }  

    private void BlockAppOpenAds_Unite()
    {
        BlockAppOpenAds();
    }

    private void UnlockAppOpenAds_Unite()
    {
        UnlockAppOpenAds();
    }

    public void BlockAppOpenAds()
    {
        blockAppOpenAd++;
    }

    public void UnlockAppOpenAds()
    {
        StartCoroutine(UnlockAppOpenAdsDelayedCoroutine());
    }

    private IEnumerator UnlockAppOpenAdsDelayedCoroutine()
    {
        yield return new WaitForSeconds(5f);

        blockAppOpenAd--;
    }

    public bool IsAppOpenAdAvailable()
    {
        return MaxSdk.IsAppOpenAdReady(ad_id_appopen);
    }

    public bool ShowAppOpenAd()
    {
        if (!removedAds && blockAppOpenAd == 0 && !isShowingAppOpenAds && IsAppOpenAdAvailable() && (DateTime.UtcNow - appOpenAdLoadTime).TotalHours < 2)
        {
            MaxSdk.ShowAppOpenAd(ad_id_appopen);

            AdsEventManager.NotifyEventAppOpenAdReadyToShow();

            return true;
        }

        return false;
    }

    public IEnumerator LoadAppOpenAdDelay(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        LoadAppOpenAd();
    }

    private void OnAppOpenAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        ImpressionSuccessEvent(adInfo, "app_open", AdsPosition.positionAppOpen);
    }

    private void OnAppOpenAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        isShowingAppOpenAds = true;

        UpdateBannerAdVisibility();
    }

    private void OnAppOpenAdDisplayFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        LoadAppOpenAd();

        AdsEventManager.NotifyEventAppOpenAdClosed();
    }

    private void OnAppOpenAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        BlockAppOpenAds_Unite();
        UnlockAppOpenAds_Unite();

        isShowingAppOpenAds = false;
        LoadAppOpenAd();

        UpdateBannerAdVisibility();

        AdsEventManager.NotifyEventAppOpenAdClosed();
    }

    private void OnAppOpenAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        appOpenAdLoadTime = DateTime.UtcNow;

        Debug.LogWarning("Max AppOpen Ads Loaded");
    }

    private void OnAppOpenAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        appOpenRetryAttemp++;

        float reloadFailedDelay = (float)Math.Pow(2, appOpenRetryAttemp);
        float retryDelay = Mathf.Clamp(reloadFailedDelay, RemoteConfigController.interstitialAd_MinReloadFailedDelay, RemoteConfigController.interstitialAd_MaxReloadFailedDelay);

        StartCoroutine(LoadAppOpenAdDelay(retryDelay));
    }

    public void InitializeAppOpenAds()
    {
        MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAppOpenAdRevenuePaidEvent;
        MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += OnAppOpenAdDisplayedEvent;
        MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent += OnAppOpenAdDisplayFailedEvent;
        MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenAdHiddenEvent;
        MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += OnAppOpenAdLoadedEvent;
        MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += OnAppOpenAdLoadFailedEvent;

        LoadAppOpenAd();
    }

    public void LoadAppOpenAd()
    {
        MaxSdk.LoadAppOpenAd(ad_id_appopen);
    }
    #endregion

    #region Interstitial

    private void SetAdsInterval(float interval)
    {
        interstitialAdsLocked++;

        StartCoroutine(WaitInterstitalAdsInterval(interval));
    }

    private IEnumerator WaitAdsStartDelay()
    {
        interstitialAdsLocked++;
        yield return new WaitForSecondsRealtime(10f);
        interstitialAdsLocked--;
    }

    private IEnumerator WaitInterstitalAdsInterval(float interval)
    {
        yield return new WaitForSecondsRealtime(interval);
        interstitialAdsLocked--;
    }

    private IEnumerator LoadInterstitialAdDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        LoadInterstitialAd();
    }

    public void InitializeInterstitialAds()
    {
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialAdRevenuePaidEvent;

        StartCoroutine(LoadInterstitialAdDelay(1f));
    }

    private void OnInterstitialAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        ImpressionSuccessEvent(adInfo, "interstitial", AdsPosition.positionInterstitial);

        BlockAppOpenAds_Unite();  

        AdsEventManager.NotifyEventInterstitialAdRecordImpression();
    }

    void LoadInterstitialAd()
    {
        MaxSdk.LoadInterstitial(ad_id_interstitial);
    }

    public bool IsInterstitialLoaded()
    {
        return MaxSdk.IsInterstitialReady(ad_id_interstitial);
    }

    public void ShowInterstitialAd(string position, Action ClosedEvent, bool ignoreInterval)
    {
        if (removedAds)
        {
            ClosedEvent?.Invoke();
            return;
        }

        if (interstitialAdsLocked == 0 || ignoreInterval)
        {
            if (IsInterstitialLoaded())
            {
                interstitialAdsLocked++;
                InterstitialAd_CloseAction = ClosedEvent;

                MaxSdk.ShowInterstitial(ad_id_interstitial);
            }
            else
            {
                ClosedEvent?.Invoke();
            }
        }
        else
        {
            ClosedEvent?.Invoke();

            if (IsInterstitialLoaded())
            {
                FirebaseController.Instance.LogEvent("ad_failed_interval");
            }
        }
    }

    private void OnInterstitialDisplayedEvent(string adUnitID, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnInterstitialClickedEvent(string adUnitID, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        interstitialRetryAttempt = 0;

        Debug.LogWarning("Max Interstitial Ads Loaded");
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        interstitialRetryAttempt++;

        float reloadFailedDelay = (float)Math.Pow(2, interstitialRetryAttempt);
        float retryDelay = Mathf.Clamp(reloadFailedDelay, RemoteConfigController.interstitialAd_MinReloadFailedDelay, RemoteConfigController.interstitialAd_MaxReloadFailedDelay);

        StartCoroutine(LoadInterstitialAdDelay(retryDelay));
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        InterstitialAd_CloseAction?.Invoke();
        InterstitialAd_CloseAction = null;

        SetAdsInterval(RemoteConfigController.interstitialAd_Interval);

        LoadInterstitialAd();
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        InterstitialAd_CloseAction?.Invoke();
        InterstitialAd_CloseAction = null;

        LoadInterstitialAd();

        SetAdsInterval(RemoteConfigController.interstitialAd_Interval);

        UnlockAppOpenAds_Unite();
    }

    #endregion

    #region Rewarded

    private Coroutine loadRewardedAdsDelayCoroutine;

    private enum RewardedAdsRequestState
    {
        None,
        Requested,
        Succeeded,
        Failed,
    }

    private RewardedAdsRequestState rewardedAdsRequestState = RewardedAdsRequestState.None;

    private IEnumerator LoadRewardedAdDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        LoadRewardedAd();
        loadRewardedAdsDelayCoroutine = null;
    }

    public bool IsRewardedReady()
    {
        return MaxSdk.IsRewardedAdReady(ad_id_rewarded);
    }

    public void InitializeRewardedAds()
    {
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;

        LoadRewardedAd();
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        ImpressionSuccessEvent(adInfo, "rewarded", AdsPosition.positionRewarded);

        BlockAppOpenAds_Unite();

        AdsEventManager.NotifyEventRewardedAdRecordImpression();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(ad_id_rewarded);

        rewardedAdsRequestState = RewardedAdsRequestState.Requested;
    }

    public void ShowRewardedAd(string position, Action succeededEvent)
    {
        if (IsRewardedReady())
        {
            RewardedAd_SucceededAction = succeededEvent;
            MaxSdk.ShowRewardedAd(ad_id_rewarded);

            AdsEventManager.NotifyEventRewardedAdStateChanged(false);
        }
        else
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                AdsEventManager.NotifyEventRewardedAdsNotReadyCauseInternetNotReachable();
            }
            else
            {
                if (loadRewardedAdsDelayCoroutine != null)
                {
                    StopCoroutine(loadRewardedAdsDelayCoroutine);
                    loadRewardedAdsDelayCoroutine = null;
                }

                if (rewardedAdsRequestState != RewardedAdsRequestState.Requested)
                {
                    LoadRewardedAd();
                }

                AdsEventManager.NotifyEventRewardedAdsNotReadyCauseLoading();
            }
        }
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.LogWarning("Max Rewarded Ads Loaded");

        rewardedAdsRequestState = RewardedAdsRequestState.Succeeded;
        rewardedRetryAttempt = 0;

        AdsEventManager.NotifyEventRewardedAdStateChanged(true);
    }

    private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        rewardedAdsRequestState = RewardedAdsRequestState.Failed;
        rewardedRetryAttempt++;

        float reloadFailedDelay = (float)Math.Pow(2, rewardedRetryAttempt);
        float retryDelay = Mathf.Clamp(reloadFailedDelay, RemoteConfigController.rewardedAd_MinReloadFailedDelay, RemoteConfigController.rewardedAd_MaxReloadFailedDelay);

        loadRewardedAdsDelayCoroutine = StartCoroutine(LoadRewardedAdDelay(retryDelay));
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        rewardedAdsRequestState = RewardedAdsRequestState.None;
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        rewardedAdsRequestState = RewardedAdsRequestState.None;

        LoadRewardedAd();
        SetAdsInterval(RemoteConfigController.interstitialAd_Rewarded_Interval);

        UnlockAppOpenAds_Unite();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        StartCoroutine(InvokeRewardedAdsSucceeded_Delayed());

        AdsEventManager.NotifyEventRewardedAdSucceeded();
    }

    IEnumerator InvokeRewardedAdsSucceeded_Delayed()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        RewardedAd_SucceededAction?.Invoke();
        RewardedAd_SucceededAction = null;
    }
    #endregion

    private bool removedAds;

    public void SetRemovedAds(bool flag)
    {
        if (removedAds != flag)
        {
            removedAds = flag;
        }
    }
}
