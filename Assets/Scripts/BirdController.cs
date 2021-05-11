using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpPower = 3f;
    public float moveSpeed = 0f;

    public bool isFirstFire = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("Bird's Rigidbody not found!");

        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            if(isFirstFire == false)
            {
                rb.useGravity = true;
                isFirstFire = true;
            }
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (isFirstFire == true)
            rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }
    public void AddSpeed()
    {
        rb.AddForce(Vector3.right * moveSpeed, ForceMode.VelocityChange);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipes"))
        {
            GamePlayManager.Instance.GameOver();
        }
    }
}
