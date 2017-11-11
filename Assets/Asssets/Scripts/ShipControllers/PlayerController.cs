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
    public GameObject Shield;

    [Space]
    [Header("All Shot Spawns")]
    public Transform[] ShotSpawns;

    public AudioSource PowerUpSound;

    public bool _doubleShots;
    public bool _moreShots;

    [HideInInspector]
    public bool canShoot = true;
    private Quaternion calibrationQuaternion;
    private Rigidbody rb;
    private Vector3 pos;

    void Start()
    {
        Shield.SetActive(value: false);
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        CalibrateAccelerometer();
        Player = this;
        _doubleShots = false;
        _moreShots = false;
        MissileLauncher.SetActive(value: false);
        Physics.IgnoreLayerCollision(8, 13);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Shoot();
            MoreShots();
        }

        //else if (HaveShield)
        //{
        //    GivePlayerShield();
        //}

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
            Instantiate(shot, ShotSpawns[1].position, ShotSpawns[1].rotation);
            Instantiate(shot, ShotSpawns[2].position, ShotSpawns[2].rotation);
        }

        if (_moreShots && canShoot)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, ShotSpawns[3].position, ShotSpawns[3].rotation);
            Instantiate(shot, ShotSpawns[4].position, ShotSpawns[4].rotation);
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
            Instantiate(shot, ShotSpawns[0].position, ShotSpawns[0].rotation);
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

    public IEnumerator TakeLauncherAway(float time)
    {
        yield return new WaitForSeconds(time);
        MissileLauncher.SetActive(value: false);
    }

    public void GivePlayerShield()
    {
        Shield.SetActive(value: true);
        Debug.Log("Player has Shield");
    }
}