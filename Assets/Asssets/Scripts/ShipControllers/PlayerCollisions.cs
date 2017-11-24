using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController _player;
    private float takeAwayTime = 35f;

    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        bool BulletPowerUp = (other.CompareTag("PowerUp"));
        bool MissilePowerUp = (other.CompareTag("MissilePowerUp"));
        if (BulletPowerUp)
        {
            _player.PowerUpSound.Play();

            if (_player.MaxShots == true)
            {
                GameManager.instance.AddScore(5);
            }

            if (_player.DoubleShots == true)
            {
                _player.MaxShots = true;
                Destroy(other.gameObject);
                return;
            }

            _player.DoubleShots = true;

            Destroy(other.gameObject);

            //PowerUpSound.Play();
        }

        else if (MissilePowerUp)
        {
            if (_player.MissileLauncher != null)
            {
                _player.PowerUpSound.Play();
                _player.MissileLauncher.SetActive(value: true);
                StartCoroutine(_player.TakeLauncherAway(takeAwayTime));
                Destroy(other.gameObject);
            }
        }

        //void UpdateShotSpeed()
        //{
        //    fireRate -= .01f;
        //}
    }
}
