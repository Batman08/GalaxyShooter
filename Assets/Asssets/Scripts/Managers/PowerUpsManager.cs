using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject MissileLauncher;
    //private PlayerController _player;

    void Start()
    {
     //   _player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "PowerUp")
        //{
        //    _player._doubleShots = true;
        //    if (_player._doubleShots == true)
        //    {
        //        _player._moreShots = true;
        //    }
        //}

        if (other.tag == "MissilePowerUp")
        {
            //   MissileLauncher.SetActive(value: true);
        }
        Destroy(other.gameObject);
    }
}
