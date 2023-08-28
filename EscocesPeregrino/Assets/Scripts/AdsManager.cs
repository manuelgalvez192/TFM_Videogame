using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{

    static AdsManager Instance;
    public static AdsManager instance
    {
        get { return Instance; }
    }



    BannerView bannerAd;
    InterstitialAd interAd;
    RewardedAd rewardVideoAd;

    #region NO TOCAR
    const string bannerId = "ca-app-pub-3940256099942544/6300978111";
    const string interId = "ca-app-pub-3940256099942544/1033173712";
    const string rewardVideoId = "ca-app-pub-3940256099942544/5224354917";
    #endregion

    void Awake()
    {
        if (instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);

        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        MobileAds.Initialize((InitializationStatus initStatus) => { });
    }

    #region Banner
    /// <summary>
    /// Lanza un anuncio horizontal chiquitin en la parte de abajo de la pantalla
    /// </summary>
    public void ThrowBannerAdd()
    {
        CloseBannerAdd();
        //crear banner
        bannerAd = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        //ejecutar eventos
        ListenToBannerEvents();

        //Cargar el banner
        if (bannerAd == null)
        {
            if (bannerAd != null)
            {
                CloseBannerAdd();
            }
            bannerAd = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        }

        AdRequest request = new AdRequest();
        request.Keywords.Add("unity-admob-sample");
        print("LoadingBannerAd!!");
        bannerAd.LoadAd(request);//show add
    }
    /// <summary>
    /// Cierra si hay algun banner
    /// </summary>
    public void CloseBannerAdd()
    {
        if (bannerAd != null)
        {
            bannerAd.Destroy();
            bannerAd = null;

        }

    }
    void ListenToBannerEvents()
    {
        // Raised when an ad is loaded into the banner view.
        bannerAd.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerAd.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerAd.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Banner view paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        bannerAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerAd.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    #endregion

    #region intersticial
    /// <summary>
    /// Lanza un anuncio en formato imagen que ocupa toda la pantalla y se puede cerrar
    /// </summary>
    public void ThrowInterstitialAd()
    {
        StartCoroutine(ThorwingInterstitialAD());
    }
    IEnumerator ThorwingInterstitialAD()
    {
        bool isLoad = false;

        DestroyIntersticialAd();
        //creating
        AdRequest request = new AdRequest();
        request.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(interId, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Failed to load intersticial ad");
                return;
            }
            else
            {
                print("intersticial ad loaded succesfull");
                interAd = ad;
                InterstitialAdEvent(interAd);
                isLoad = true;
            }
        });

        while(!isLoad)
        {
            yield return null;
        }

        //showing
        if (interAd != null && interAd.CanShowAd())
        {
            interAd.Show();
        }

        yield break;
    }

    void DestroyIntersticialAd()
    {
        if (interAd != null)
        {
            interAd.Destroy();
            interAd = null;
        }
    }
    public void InterstitialAdEvent(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Interstitial ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion

    #region RewardVideo
    /// <summary>
    /// Lanza un anuncio en formato video que se puede cerrar cuando acaba, lanza un evento al final para obtenmer recompensas
    /// </summary>
    public void ThrowVideoRewardAd()
    {
        StartCoroutine(ThrowingRewardVideo());
    }
    IEnumerator ThrowingRewardVideo()
    {
        bool isLoad = false;
        if (rewardVideoAd != null)
        {
            rewardVideoAd.Destroy();
            rewardVideoAd = null;
        }
        //loading ad
        AdRequest request = new AdRequest();
        request.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardVideoId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                //print("Failed to load Rewarded ad");
                return;
            }
            else
            {
                //print("loading Rewarded ad Successfull");
                rewardVideoAd = ad;
                RewardedAdEvents(ad);
                isLoad = true;
            }
        });
        while (!isLoad)
        {
            yield return null;

        }

        ShowVideoRewardAd();
        yield break;
    }
    void ShowVideoRewardAd()
    {
        //showing
        if (rewardVideoAd != null)
        {
            rewardVideoAd.Show((Reward reward) =>
            {
                GetRewarded();
            });
        }
        else
            print("AD is nopt ready");
        //print("Showing");
    }
    public void RewardedAdEvents(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Rewarded ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion

    #region RewardEvents

    void GetRewarded()
    {
        print("HAS OBTENIDO UN PONI AMARILLO");
    }

    #endregion
}
