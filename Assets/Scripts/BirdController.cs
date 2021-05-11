using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpPower = 3f;
    public float moveSpeed = 0.5f;

    public bool isFirstFire = false;

    public int ceilingPoint = 5;
    public int basePoint = -3;

    int count;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("Bird's Rigidbody not found!");

        rb.useGravity = false;
    }
    private void Start()
    {
        count = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 5 || gameObject.transform.position.y < -3)
        {
            GamePlayManager.Instance.GameOver();
        }
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            if(isFirstFire == false)
            {
                rb.useGravity = true;
                isFirstFire = true;
            }
            else
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
        count++;
        Debug.Log(count);
        if (isFirstFire == true && count < 3000)
            rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipes"))
        {
            GamePlayManager.Instance.GameOver();
        }
    }
}
