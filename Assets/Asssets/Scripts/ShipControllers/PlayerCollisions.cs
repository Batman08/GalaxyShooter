using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController _player;
    private MissileLauncher _launcher;

    void Start()
    {
        _player = GetComponent<PlayerController>();
        _launcher = GetComponent<MissileLauncher>();
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
            //    _player.LaserBeam.SetActive(value: true);
                StartCoroutine(_player.TakeLauncherAway(18));
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
            _player.PowerUpSound.Play();
            _player.MissileLauncher.SetActive(value: true);
            StartCoroutine(_player.TakeLauncherAway(18));
            Destroy(other.gameObject);
        }

        //void UpdateShotSpeed()
        //{
        //    fireRate -= .01f;
        //}
    }
}
