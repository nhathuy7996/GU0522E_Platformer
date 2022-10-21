using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{

    private static AdManager _instant;
    public static AdManager instant => _instant;

    #region BANNER
    [SerializeField] bool _isBannerShow = false;
    [SerializeField]
    string _bannerID = "ca-app-pub-2623284640924516/1714032783";
    private BannerView _bannerView;

    float _reLoadBannerTime = 1;
    #endregion

    #region INTER
    [SerializeField]
    private string _interID = "ca-app-pub-2623284640924516/6032316783";
    private InterstitialAd _interstitialAd;
    private float _interTime = 1;

    public Action onInterRewared;

    float _timerDelay = 0;
    #endregion


    #region Rewarded
    [SerializeField]
    private string _rewaredID = "ca-app-pub-2623284640924516/9376900388";
    private List<RewardedAd> _rewardedAds = new List<RewardedAd>();
    public Action onVideoRewarded;
    private float _rewardTime = 1;
    [SerializeField] int _maxReward = 3;
    #endregion

    private void Awake()
    {
        _instant = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => {
            Debug.Log(initStatus);
            RequestBanner();
            ReQuestInter();


            for (int  i =0; i< _maxReward; i++)
            {
                _rewardedAds.Add(RequestRewardAd());
            }

        });

       
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerDelay > 0)
            _timerDelay -= Time.deltaTime;
    }

    #region BANNER HANDLE
    void RequestBanner()
    {
        this._bannerView = new BannerView(_bannerID, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

       

        this._bannerView.OnAdLoaded += _bannerView_OnAdLoaded;
        this._bannerView.OnAdFailedToLoad += _bannerView_OnAdFailedToLoad;

        // Load the banner with the request.
        this._bannerView.LoadAd(request);
    }

    private void _bannerView_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Invoke("RequestBanner", _reLoadBannerTime);
        _reLoadBannerTime *= 2;
    }

    private void _bannerView_OnAdLoaded(object sender, System.EventArgs e)
    {
        _isBannerShow = true;
        _reLoadBannerTime = 1;

    }
    #endregion

    #region INTER HANDLE
    void ReQuestInter()
    {
        this._interstitialAd = new InterstitialAd(this._interID);

        AdRequest request = new AdRequest.Builder().Build();


        this._interstitialAd.OnAdLoaded += _interstitialAd_OnAdLoaded;
        this._interstitialAd.OnAdFailedToLoad += _interstitialAd_OnAdFailedToLoad;
        this._interstitialAd.OnAdFailedToShow += _interstitialAd_OnAdFailedToShow;
        this._interstitialAd.OnAdClosed += _interstitialAd_OnAdClosed;

        this._interstitialAd.OnAdDidRecordImpression += _interstitialAd_OnAdDidRecordImpression;

        this._interstitialAd.LoadAd(request);
    }

    private void _interstitialAd_OnAdDidRecordImpression(object sender, System.EventArgs e)
    {
        onInterRewared?.Invoke();
        onInterRewared = null;
    }

    public void ShowInter()
    {
        if (_timerDelay > 0)
            return;
        if(this._interstitialAd.IsLoaded())
            this._interstitialAd.Show();
    }

    private void _interstitialAd_OnAdClosed(object sender, System.EventArgs e)
    {
        ReQuestInter();
        _timerDelay = 60;
    }

    private void _interstitialAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        Invoke("ReQuestInter", _interTime);

        _interTime *= 2;
    }

    private void _interstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Invoke("ReQuestInter", _interTime);

        _interTime *= 2;
    }

    private void _interstitialAd_OnAdLoaded(object sender, System.EventArgs e)
    {
        Debug.LogError("Inter load Done!");
    }

    #endregion

    #region REWARD HANDLE
    RewardedAd RequestRewardAd()
    {
        RewardedAd _rewardedAd = new RewardedAd(_rewaredID);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        _rewardedAd.OnUserEarnedReward += _rewardedAd_OnUserEarnedReward;
        _rewardedAd.OnAdFailedToLoad += _rewardedAd_OnAdFailedToLoad;
        _rewardedAd.OnAdFailedToShow += _rewardedAd_OnAdFailedToShow;
        _rewardedAd.OnAdLoaded += _rewardedAd_OnAdLoaded;

        // Load the rewarded ad with the request.
        _rewardedAd.LoadAd(request);

        return _rewardedAd;
    }

    private void _rewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        _rewardTime = 1;
    }

    private void _rewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        onVideoRewarded = null;
        StartCoroutine(waitForTime(_rewardTime, () => {
            this._rewardedAds.Remove((RewardedAd)sender);
            this._rewardedAds.Add(RequestRewardAd());


        }));
        _rewardTime *= 2;
    }

    private void _rewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        StartCoroutine(waitForTime(_rewardTime, () => {
            this._rewardedAds.Remove((RewardedAd)sender);
            this._rewardedAds.Add(RequestRewardAd());

            
        }));
        _rewardTime *= 2;
    }

    private void _rewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        onVideoRewarded?.Invoke();
        onVideoRewarded = null;

        this._rewardedAds.Remove((RewardedAd)sender);
        this._rewardedAds.Add(RequestRewardAd());
    }

    IEnumerator waitForTime(float timeWait, Action onEndTime)
    {
        yield return new WaitForSeconds(timeWait);
        onEndTime.Invoke();
    }

    public void ShowRewared()
    {
        foreach (RewardedAd r in _rewardedAds)
        {
            if (r.IsLoaded())
            {
                r.Show();
            }
        }
    }

#endregion

}
