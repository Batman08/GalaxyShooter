//using UnityEngine;

//public class PlayGameAd : MonoBehaviour
//{

//    public void ShowNormalAd()
//    {
//        Advertisement.Show("video", new ShowOptions() { resultCallback = HandleGameAdResult });
//    }

//    private void HandleGameAdResult(ShowResult result)
//    {
//        switch (result)
//        {
//            case ShowResult.Failed:
//                Debug.Log("Player failed to launch ad");
//                break;
//            case ShowResult.Skipped:
//                Debug.Log("Player skipped ad");
//                break;
//            case ShowResult.Finished:
//                Debug.Log("Player Watched the full ad");
//                break;
//            default:
//                break;
//        }
//    }
//}
