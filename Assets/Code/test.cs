using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class test : MonoBehaviour
{
    public float player_speed = 15;
    public bool shouldmove = false;

    Rigidbody rb;

    private void moveplayer1(float speed)
    {
        float dt = Time.deltaTime;
        float h = Input.GetAxisRaw("Horizontal"); //ad
        float v = Input.GetAxisRaw("Vertical"); //ws

        transform.position += (Vector3.forward * v + Vector3.right * h) * speed * dt;
    }

    private void moveplayer2(float speed)
    {
        float dt = Time.deltaTime;
        float h = Input.GetAxisRaw("Horizontal"); //ad
        float v = Input.GetAxisRaw("Vertical"); //ws    
        rb.AddForce((Vector3.forward * v + Vector3.right * h) * speed * dt, ForceMode.Impulse);
    }

    private void repose()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;

            transform.position = new Vector3(0, 3, 0);

            rb.constraints = RigidbodyConstraints.None;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldmove)
        {
            moveplayer2(player_speed);
            repose();
        }
    }
}

