using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFirebaseExample : MonoBehaviour
{
    void Start()
    {
        if (FirebaseController.Instance.IsReady())
        {
            OnFirebaseReady();
        }
        else
        {
            FirebaseController.Instance.OnFirebaseReady += OnFirebaseReady;
        }

        AdsUtility.Initiate(false);
    }

    private void OnFirebaseReady()
    {
        var firebaseController = FirebaseController.Instance;

        RemoteConfigController.Instance.OnFetchCompleted = OnRemoteConfigFetched;
        RemoteConfigController.Instance.Initialize();

        firebaseController.SetUserId(SystemInfo.deviceUniqueIdentifier);
    }

    private void OnRemoteConfigFetched(bool result)
    {
    }
}
