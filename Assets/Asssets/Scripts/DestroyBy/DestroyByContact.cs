using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [Header("Explosions")]
    public GameObject explosion;
    public GameObject playerExplosion;
    [Space]
    [Header("PowerUps")]
    public GameObject[] PowerUps;
    [Space]
    public int scoreValue;
    public int MaxPowerUps = 3;
    private GameManager gameController;
    //private bool canInsantiate = false;
    public int RandomPowerUp;
    public int RandomPowerUpNum;
    public int RandomPowerUpNum2;
    //private bool isDead = false;

    void Start()
    {
        GameObject gameControllerObj = GameObject.FindWithTag("GameManager");
        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameManager>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        Physics.IgnoreLayerCollision(10, 10);

        RandomPowerUp = Random.Range(0, PowerUps.Length/* - 1*/);
    }

    void Update()
    {
        Physics.IgnoreLayerCollision(10, 10);

        RandomPowerUpNum = Random.Range(0, 4);
        RandomPowerUpNum2 = Random.Range(0, 4);

        //if (MaxPowerUps <= 0)
        //{
        //    canInsantiate = false;
        //}
        //if (MaxPowerUps >= 3)
        //{
        //    canInsantiate = true;
        //}
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("PowerUp") || other.CompareTag("MissilePowerUp"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (PowerUps != null)
            {
                if (RandomPowerUpNum == RandomPowerUpNum2/*gameController._generatePowerUp < RandomPowerUpNum && gameController._generatePowerUp > RandomPowerUpNum2 *//*gameController.hazards.Length*/)
                {
                    //MaxPowerUps -= 1;
                    Instantiate(PowerUps[RandomPowerUp], other.gameObject.transform.position, Quaternion.identity);
                }
                //if (canInsantiate == true)
                //{
                //    //if (other.CompareTag("Bolt"))
                //    //    return;
                //}
            }
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);
            //gameController.GameOver();
        }

        else
        {
            gameController.AddScore(scoreValue);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
