using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{

    private BannerView bannerView;

    private float timeLastIntAd;

    public void Start() {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        this.RequestInterstitial();
        timeLastIntAd = 0f;
    }

    private void RequestBanner() {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-8336816360970923/9593059300";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private InterstitialAd interstitial;

    private void RequestInterstitial() {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-8336816360970923/2789484404";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
        
    }

    public void HandleOnAdOpening(object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
    }

    // Update is called once per frame
    void Update()
    {
        timeLastIntAd = timeLastIntAd +  Time.deltaTime;
    }

    public void ShowInterstitial() {
        if (timeLastIntAd > 10f && interstitial.IsLoaded())
        {
            interstitial.Show();
            timeLastIntAd = 0;

        } else if(!interstitial.IsLoaded())
            this.RequestInterstitial();
    }
}
