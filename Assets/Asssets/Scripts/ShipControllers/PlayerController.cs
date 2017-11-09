using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player;

    [Header("Ship Details")]
    public float speed;
    public float tilt;
    public float fireRate;
    public float StartWait;
    public float TimeToBegin;
    private float nextFire;
    [Space]
    public GameObject playerExplosion;
    [Space]
    //  public TouchPad TouchPad;
    [Space]
    public Boundary boundary;
    [Space]
    [Header("Bullets")]
    public GameObject MissileLauncher;
    public GameObject shot;
    [Space]
    [Header("All Shot Spawns")]
    public Transform shotSpawns;
    public Transform shotSpawns2;
    public Transform shotSpawns3;
    public Transform shotSpawns4;
    public Transform shotSpawns5;

    public AudioSource PowerUpSound;

    public bool _doubleShots;
    public bool _moreShots;

    [HideInInspector]
    public bool canShoot = true;
    private Quaternion calibrationQuaternion;
    Rigidbody rb;
    Vector3 pos;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        CalibrateAccelerometer();
        Player = this;
        _doubleShots = false;
        _moreShots = false;
        MissileLauncher.SetActive(value: false);
        Physics.IgnoreLayerCollision(8, 13);
        //TouchPad = FindObjectOfType<TouchPad>();
        //    InvokeRepeating("UpdateShotSpeed", 0, 15);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Shoot();
            MoreShots();
        }

    }

    void MoreShots()
    {

        //if (PowerUp.powerUp.gameObject == null)
        //{
        //    return;
        //}

        if (_doubleShots && canShoot)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawns2.position, shotSpawns2.rotation);
            Instantiate(shot, shotSpawns3.position, shotSpawns3.rotation);
        }

        if (_moreShots && canShoot)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawns4.position, shotSpawns4.rotation);
            Instantiate(shot, shotSpawns5.position, shotSpawns5.rotation);
        }


        #region SlowMotion
        //if (SlowMotion.instance.IsSlowMotion)
        //{
        //    speed = 180;
        //    tilt = 0.25f;
        //    fireRate = 0.006f;
        //}

        //else
        //{
        //    speed = 12;
        //    tilt = 5;
        //    fireRate = 0.15f;
        //} 
        #endregion
    }

    void Shoot()
    {
        if (canShoot)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawns.position, shotSpawns.rotation);
            //foreach (Transform shotSpawn in shotSpawns)
            //{
            //    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //}
            GetComponent<AudioSource>().Play();
        }
        #region PowerUp
        //if (PowerUp.powerUp.gameObject != null)
        //{
        //    if (PowerUp.powerUp.HavePowerUp == true)
        //    {
        //        SlowMotion.instance.IsSlowMotion = true;
        //        SlowMotion.instance.DoSlowMotion();
        //    }
        //}
        #endregion
    }
    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
        }

        else
        {
            rb.rotation = Quaternion.Euler(0f, 0f, 0f);
            //pos = Vector3.zero;
        }

        //Not sure which one is which for windows
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        }

        Vector3 direction = new Vector3(pos.x, pos.y, pos.z);
        //Vector3 direction = TouchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0f, direction.z);
        rb.position = movement + Vector3.forward;
        // rb.velocity = movement;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0f, 0f, rb.position.x * -tilt);
    }

    //Used to calibrate the Input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the calibrated value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    IEnumerator TakeLauncherAway(float time)
    {
        yield return new WaitForSeconds(time);
        MissileLauncher.SetActive(value: false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            PowerUpSound.Play();

            if (_moreShots == true)
            {
                GameManager.instance.AddScore(2);
            }

            if (_doubleShots == true)
            {
                _moreShots = true;
                Destroy(other.gameObject);
                return;
            }

            _doubleShots = true;

            Destroy(other.gameObject);

            //PowerUpSound.Play();
        }

        if (other.CompareTag("MissilePowerUp"))
        {
            PowerUpSound.Play();
            MissileLauncher.SetActive(value: true);
            StartCoroutine(TakeLauncherAway(18));
            Destroy(other.gameObject);
        }

        //void UpdateShotSpeed()
        //{
        //    fireRate -= .01f;
        //}
    }
}