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
    public int RandomPowerUp;
    public int RandomPowerUpNum;
    public int RandomPowerUpNum2;

    private GameObject gameControllerObj;
    //private GameObject sheildObj;

    private GameManager gameController;
    private ForceField _sheild;

    void Start()
    {
        //  sheildObj = GameObject.FindWithTag("ForceField");
        gameControllerObj = GameObject.FindWithTag("GameManager");

        //Game Manager instance
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
        bool Boundary = (other.CompareTag("Boundary"));
        bool Enemy = (other.CompareTag("Enemy"));
        bool PowerUp = (other.CompareTag("PowerUp"));
        bool MissilePowerUp = (other.CompareTag("MissilePowerUp"));
        bool Player = (other.tag == "Player");
        //bool Shield = (other.tag == "ForceField");

        if (Boundary || Enemy || PowerUp || MissilePowerUp)
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (PowerUps != null)
            {
                Destroy(other.gameObject);
                if (/*RandomPowerUpNum == RandomPowerUpNum2*/gameController._generatePowerUp < RandomPowerUpNum && gameController._generatePowerUp > RandomPowerUpNum2 /*gameController.hazards.Length*/)
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

        //if (Shield)
        //    ForceField.instnace.Health--;

        if (Player)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }

        else
        {
            gameController.AddScore(scoreValue);
        }

        //if (sheildObj == null)
        //    Destroy(other.gameObject);

        Destroy(gameObject);
    }
}
