using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Text scoreText;
    public Text WaveText;

    private PlayAd _ad;
    private int _num1;
    private int _num2;

    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        WaveText.text = PlayerPrefs.GetInt("wave").ToString();
        _ad = GetComponent<PlayAd>();


        _num1 = Random.Range(0, 4);
        _num2 = Random.Range(0, 4);

        Debug.Log(_num1 + "-----" + _num2);
    }

    public void Play()
    {
        if (_num1 == _num2)
        {
            _ad.ShowAd();
            SceneManager.LoadScene(1);
        }

        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void EnemyValues()
    {
        SceneManager.LoadScene(2);
    }
}