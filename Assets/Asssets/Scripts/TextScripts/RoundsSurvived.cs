using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class RoundsSurvived : MonoBehaviour
{
    public Text WaveText;

    private GameManager gameManager;

    void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        WaveText.text = "0";
        int Wave = 0;

        yield return new WaitForSeconds(0.7f);

        while (Wave < gameManager.waveCount)
        {
            Wave++;
            WaveText.text = Wave.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }

}
