using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerDetails))]
public class TankScript : MonoBehaviour
{
    [Header("Player Details")]
    private PlayerDetails playerDetails;
    private string controlScheme;
    private int playerNumber;

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
    public bool hasShot;
    public int BounceCount;
    public int maxBullets;
    public bool canShoot;

    [Header("Health")]
    public bool isShot;

    [Header("Controller")]
    public Vector2 lookInput;
    public float rotateSmoothing = 1000f;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDetails = GetComponent<PlayerDetails>();
        controlScheme = playerDetails.playerDevice;
        tankControls = new TankControls();
        if(controlScheme == "Keyboard & Mouse")
        {
            crosshair.gameObject.SetActive(true);
        }
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
        canShoot = true;
    }

    public void GetLookInput(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();  
    }


    public void GetMoveInput(InputAction.CallbackContext ctx)
    {
        moveValue = ctx.ReadValue<Vector2>();
    }

    public void HasShot(InputAction.CallbackContext ctx)
    {
        if (bulletCount < maxBullets && canShoot)
        {
            StartCoroutine(FireRate());
        }                 
    }


    IEnumerator FireRate()
    {
        Debug.Log("hasShot");
        var bulletObj = Instantiate(bullet, bulletHole.position, bulletHole.rotation);
        bulletCount++;
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private void RotateBody(Vector2 inputVal)
    {
        switch (controlScheme)
        {
            case "Keyboard & Mouse":
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
                if (inputVal.x == 1 && inputVal.y == 1)
                {
                    // is moving top right

                    targetRot = Quaternion.Euler(0f, 315f, 0f);

                }
                else if (inputVal.x == 1 && inputVal.y == -1)
                {
                    //is moving bottom right
                    targetRot = Quaternion.Euler(0f, 45f, 0f);

                }
                else if (inputVal.x == -1 && inputVal.y == -1)
                {
                    //is moving bottom left
                    targetRot = Quaternion.Euler(0f, 135f, 0f);

                }
                else if (inputVal.x == -1 && inputVal.y == 1 )
                {
                    // is moving top left
                    targetRot = Quaternion.Euler(0, 225f, 0);

                }

                tankBody.rotation = Quaternion.RotateTowards(tankBody.rotation, targetRot, rotateSmoothing * Time.deltaTime);

                break;
            case "Controller":
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
                if (inputVal.x < 1 && inputVal.x > 0.5 && inputVal.y > 0.5 && inputVal.y < 1)
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

                tankBody.rotation = Quaternion.RotateTowards(tankBody.rotation, targetRot, rotateSmoothing * Time.deltaTime);
                break;
        }

    }

    private void RotateBarrel()
    {
        switch (controlScheme)
        {
            case "Keyboard & Mouse":
                screenPos = Mouse.current.position.ReadValue();
                screenPos.z = Camera.main.nearClipPlane + 9;

                crosshairPos = Camera.main.ScreenToWorldPoint(screenPos);

                crosshairPos.y = 0.45f;
                crosshair.position = crosshairPos;


                Vector3 relativePos = crosshairPos - tankHead.position;

                Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
                rotation.z = 0;
                tankHead.rotation = rotation * Quaternion.Euler(0, -90, 0);
                break;
            case "Controller":
                Vector3 rotateDir = Vector3.right * lookInput.x + Vector3.forward * lookInput.y;
                if(rotateDir.sqrMagnitude> 0)
                {
                    Quaternion newRot = Quaternion.LookRotation(rotateDir, Vector3.up);
                    tankHead.rotation = newRot * Quaternion.Euler(0,-90,0);
                }
                break;
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
        if (isShot)
        {
            Debug.Log("isDead");
        }

        CastRay(bulletHole.position, bulletHole.forward);

    }

    void CastRay(Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < BounceCount; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, 1))
            {
                Debug.DrawLine(position, hit.point, Color.red);
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else
            {
                Debug.DrawRay(position, direction * 100, Color.blue);
            }
        }
    }

    private void Update()
    {

    }
}
