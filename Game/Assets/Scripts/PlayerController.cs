using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rigidbody;
    AudioSource primaryFireSound;
    AudioSource secondaryFireSound;

    #region Public Variables
    public float speed;
    public float tilt;
    public float primaryFireRate;
    public float secondaryFireRate;

    public Boundary boundary;
    public GameObject primaryShot;
    public GameObject secondaryShot;
    public Transform shotSpawn;
    #endregion

    #region Private Variables
    private float nextPrimaryFire;
    private float nextSecondaryFire;
    #endregion
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        primaryFireSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextPrimaryFire)
        {
            nextPrimaryFire = Time.time + primaryFireRate;
            Instantiate(primaryShot, shotSpawn.position, shotSpawn.rotation);

            primaryFireSound.Play();
        }

        if (Input.GetMouseButton(1) && Time.time > nextSecondaryFire)
        {
            nextSecondaryFire = Time.time + secondaryFireRate;
            Instantiate(secondaryShot, shotSpawn.position, shotSpawn.rotation);

        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);


    }


}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
