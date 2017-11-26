using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public Text ScoreText;

    private GameManager gameManager;

    void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        ScoreText.text = gameManager.score.ToString();
        //StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        ScoreText.text = "0";
        int Score = 0;

        yield return new WaitForSeconds(0.7f);

        while (Score < gameManager.score)
        {
            Score++;
            ScoreText.text = Score.ToString();

            yield return new WaitForSeconds(0.00000000000000000000000f);
        }
    }
}
