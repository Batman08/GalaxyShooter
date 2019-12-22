using System.Collections;
using admob;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }
    private string InterstialId = "ca-app-pub-1211888741379414/2048543145";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //For testing -- CODE( Admob.Instance().setTesting(true);)
        Instance = this;

#if UNITY_EDITOR
#elif UNITY_ANDROID
        Admob.Instance().initAdmob("", InterstialId);
        Admob.Instance().loadInterstitial();
#endif
    }


    void ShowBannerAd()
    {
#if UNITY_EDITOR
        Debug.Log("Unable to play ad from editor");
#elif UNITY_ANDROID
        Debug.Log("Banner ad has been played");
#endif
    }

    public void ShowVideo()
    {

#if UNITY_EDITOR
        Debug.Log("Unable to play ad from editor");
#elif UNITY_ANDROID
        if (Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
        Debug.Log("Ad is playing");
        }
        
        else
	    {
             Debug.Log("Ad is not ready at the moment");
	    }
#endif
    }
}
