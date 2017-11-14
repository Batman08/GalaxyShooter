using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Text LivesText;

    private GameObject _playerInstance;
    private float _respawnTimer = 1f;
    [HideInInspector]
    public int numLives = 4;
    private bool _isDead = false;


    void Start()
    {
        SpawnPlayer();
    }

    void Update()
    {
        LivesText.text = "Lives: " + numLives;

        if (numLives <= 0)
        {
            _isDead = true;
            GameManager.instance.GameOver();
        }

        if (_playerInstance == null && _isDead == false)
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0 && numLives >= 1)
            {
                SpawnPlayer();
                StartCoroutine(Spawn());
            }
        }


    }

    public void SpawnPlayer()
    {
        numLives--;
        _respawnTimer = 2;
        if (numLives >= 1)
        {
            _playerInstance = Instantiate(PlayerPrefab, PlayerPrefab.transform.position, Quaternion.identity);
        }
    }

    IEnumerator Spawn() /*TODO: Make  player able to interact with powerups but not the enemies*/
    {
        if (_playerInstance != null)
        {
            Physics.IgnoreLayerCollision(8, 9, true);
            //PlayerController.Player.GetComponent<MeshCollider>().enabled = false;
        }
        yield return new WaitForSeconds(3);
        if (_playerInstance != null)
        {
            Physics.IgnoreLayerCollision(8, 9, false);
            //PlayerController.Player.GetComponent<MeshCollider>().enabled = true;
        }
    }
}
