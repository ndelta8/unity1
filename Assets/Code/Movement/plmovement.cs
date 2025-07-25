using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;
using System.Diagnostics.Tracing;
using System.Linq;

public class plmovement : MonoBehaviour
{

    //system variables
    Rigidbody rb;
    CharacterController cc;
    Vector3 playerVelocity;
    Transform cameraTransform;
    Transform camPivotTransform;

    RaycastHit hit;
    float groundCheckDist;

    //game variables
    bool blockstrafe = false;
    bool isSprinting = false;

    float mouseRotationY;
    float maxmouseRotation = 90f;
    int perspective = 0;

    bool focused = true;

    //player variables
    public int speed = 7;
    public int jumpspeed = 16;
    public float sprint = 1.7f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        camPivotTransform = GameObject.Find("Cam Pivot").transform;

        groundCheckDist = cc.height * 0.2f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

    }

    void Update()
    {
        look();
        move();
        Jump();

        resetpos();
        changePerspective();

    }

    void move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * h + transform.forward * v;

        if (dir.magnitude > 1f && blockstrafe)
            dir = dir.normalized;

        if (isSprinting || Input.GetKey(GameSettings.gs.run))
            dir *= sprint;

        cc.Move(dir * speed * Time.deltaTime);
    }

    void look()
    {
        float mouseX = Input.GetAxis("Mouse X") * GameSettings.gs.mouseS * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameSettings.gs.mouseS * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        mouseRotationY -= mouseY;
        mouseRotationY = Mathf.Clamp(mouseRotationY, -maxmouseRotation, maxmouseRotation);


        camPivotTransform.localRotation = Quaternion.Euler(mouseRotationY, 0f, 0f);
    }

    float falltime = 0f;
    public float fallspeed = 1.5f;

    bool prevg = true;
    void Jump()
    {
        bool isGrounded = Physics.SphereCast(transform.position, cc.radius, Vector3.down, out hit, groundCheckDist);
        //bool a = Physics.Raycast(transform.position, Vector3.down, cc.radius + 0.5f);
        //bool b = Physics.Raycast(transform.position, Vector3.down, cc.radius + 0.13f);

        if (isGrounded)
        {
            falltime = 0f;
            if (playerVelocity.y < 0)
            {
                //playerVelocity.y = -1f;
                //if (a && !b)
                //{
                playerVelocity.y = -21f;
                //}
            }
            if (Input.GetKey(GameSettings.gs.jump))
            {
                playerVelocity.y = jumpspeed;
            }
        }
        else
        {
            if (prevg && !isGrounded && playerVelocity.y < -20f)
            {
                playerVelocity.y = -1f;
            }
            falltime += Time.deltaTime * fallspeed;
        }
        prevg = isGrounded;

        if (falltime > 0f)
        {
            playerVelocity.y -= Math.Clamp((0.04f * falltime), 0.1f, 0.3f);
        }
        if (playerVelocity.y < -60f)
        {
            playerVelocity.y = -60f;
        }
        cc.Move(playerVelocity * Time.deltaTime);
    }

    void resetpos()
    {
        if (Input.GetKey(GameSettings.gs.resetpos))
        {
            cc.transform.position = new Vector3(0, 5, 0);
        }
    }

    float camdis = 0f;
    void changePerspective()
    {
        Camera cam = cameraTransform.GetComponent<Camera>();
        int pll = LayerMask.NameToLayer("player");

        if (Input.GetKeyDown(GameSettings.gs.perspective))
        {
            perspective = (perspective + 1) % 2;
            if (perspective == 0)
            {
                cam.cullingMask &= ~(1 << pll);
                camdis = 0f;
                cameraTransform.localPosition = new Vector3(0, 0, -camdis);
            }
            else
            {
                cam.cullingMask |= (1 << pll);
                camdis = 6f;

            }
        }
        if (perspective != 0)
        {
            persfix();
        }
    }

    void persfix()
    {
        //TODO 카메라 벽뚫 방지:
        //매틱마다 while로 카메라가 spherecast시 물체에 안닿아있을때까지 camdis 줄이는거.
        float shr = 0;
        float shrsteps = 0.02f;
        for (int i = 0; i <= (camdis / shrsteps); i++)
        {
            RaycastHit h = new RaycastHit();
            bool check = Physics.SphereCast(cameraTransform.position, 0.2f, new Vector3(0, 0, -1f), out h, 0.1f) && h.collider != cc;
            if (!check)
            {
                cameraTransform.localPosition = new Vector3(0, 0, -shr);
                shr += shrsteps;
            }
            else
            {
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (cc.radius + 0.4f));
        Gizmos.DrawSphere(cameraTransform.position, 0.3f);



    }
}