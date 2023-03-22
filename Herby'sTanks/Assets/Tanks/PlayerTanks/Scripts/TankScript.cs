using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankScript : MonoBehaviour
{
    //Move
    [Header("Movement")]
    private TankControls tankControls;
    private Rigidbody rb;
    private Vector2 moveValue;
    public float moveSpeed;

    //Move Rotation
    [Header("Move Rotation")]
    public Transform tankBody;
    public float turnSpeed;
    public Quaternion targetRot;

    //Shooting
    [Header("Shooting")]
    public Transform crosshair;
    public Transform tankHead;
    public Vector3 screenPos;
    public Vector3 crosshairPos;
    public Transform bulletHole;
    public GameObject bullet;
    public int bulletCount;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tankControls = new TankControls();


    }

    private void OnEnable()
    {
        tankControls.Enable();
    }

    private void OnDisable()
    {
        tankControls.Disable();
    }


    private void Start()
    {
    }

    public void GetMoveInput(InputAction.CallbackContext ctx)
    {
        moveValue = ctx.ReadValue<Vector2>();
    }

    private void Move(Vector2 moveInput)
    {

    }

    private void RotateBody(Vector2 context)
    {
        Vector2 inputVal = context;
        switch (inputVal.x)
        {
            case 1:
                targetRot = Quaternion.Euler(0f, 360f, 0f);
                break;
            case -1:
                targetRot = Quaternion.Euler(0f, 180f, 0f);
                break;
        }
        switch (inputVal.y)
        {
            case 1:
                targetRot = Quaternion.Euler(0f, 270f, 0f);
                break;
            case -1:
                targetRot = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
        /*
        if (inputVal.x == 1)
        {
            //is Moving Right
            targetRot = Quaternion.Euler(0f, 360f, 0f);
        }
        else if (inputVal.x == -1)
        {
            //is Moving Left
            targetRot = Quaternion.Euler(0f, 180f, 0f);

        }

        else if (inputVal.y == 1)
        {
            //is Moving Up
            targetRot = Quaternion.Euler(0f, 270f, 0f);


        }
        else if (inputVal.y == -1)
        {
            //is Moving Down
            targetRot = Quaternion.Euler(0f, 90f, 0f);

        }
        */
        if (inputVal.x > 0 && inputVal.x != 1 && inputVal.y > 0 && inputVal.y != 1)
        {
            // is moving top right

            targetRot = Quaternion.Euler(0f, 315f, 0f);

        }
        else if (inputVal.x > 0 && inputVal.x != 1 && inputVal.y < 0 && inputVal.y != -1)
        {
            //is moving bottom right
            targetRot = Quaternion.Euler(0f, 45f, 0f);

        }
        else if (inputVal.x < 0 && inputVal.x != -1 && inputVal.y < 0 && inputVal.y != -1)
        {
            //is moving bottom left
            targetRot = Quaternion.Euler(0f, 135f, 0f);

        }
        else if (inputVal.x < 0 && inputVal.x != -1 && inputVal.y > 0 && inputVal.y != 1)
        {
            // is moving top left
            targetRot = Quaternion.Euler(0, 225f, 0);

        }

        tankBody.rotation = targetRot;

    }

    private void RotateBarrel()
    {
        
        screenPos = Mouse.current.position.ReadValue();
        screenPos.z = Camera.main.nearClipPlane + 9;

        crosshairPos = Camera.main.ScreenToWorldPoint(screenPos);

        crosshairPos.y = 0.45f;
        crosshair.position = crosshairPos;
        
        
        Vector3 relativePos = crosshairPos - tankHead.position;
        
        Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
        rotation.z = 0;
        tankHead.rotation = rotation * Quaternion.Euler(0, -90, 0);
        
        //tankHead.LookAt(crosshairPos);

    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (bulletCount < 5)
        {
            var bulletObj = Instantiate(bullet, bulletHole.position, bulletHole.rotation);
            //bulletCount++;

            //bulletObj.AddComponent<Rigidbody>().AddForce(bulletObj.transform.forward * 500);
            //bulletObj.GetComponent<Rigidbody>().useGravity = false;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = moveValue.x;
        float vertical = moveValue.y;
        Vector3 Movement = this.transform.right * horizontal + this.transform.forward * vertical;
        rb.velocity = Movement * moveSpeed;
        RotateBody(moveValue);
        RotateBarrel();

    }

    private void Update()
    {

    }
}
