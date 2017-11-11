using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class PlayAd : MonoBehaviour
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("Player failed to launch ad");
                break;
            case ShowResult.Skipped:
                GameManager.instance.score = 100;
                GameManager.instance.scoreText.text = "" + 100;
                Debug.Log("Player skipped ad");
                break;
            case ShowResult.Finished:
                PlayerController.Player.DoubleShots = true;
                break;
            default:
                break;
        }
    }
}
