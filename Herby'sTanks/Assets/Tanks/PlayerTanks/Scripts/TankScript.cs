using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerDetails))]
public class TankScript : MonoBehaviour
{
    [Header("Player Details")]
    public SpawnManager spawnManager;
    public PlayerDetails playerDetails;
    public string controlScheme;
    public int playerNumber;

    //Move
    [Header("Movement")]
    public TankControls tankControls;
    public Rigidbody rb;
    public Vector2 moveValue;
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
    public bool canDie;
    public bool isShot;
    public bool canMove = true;
    public GameObject explosion;
    public GameObject smokeTrail;

    [Header("Controller")]
    public Vector2 lookInput;
    public float rotateSmoothing = 1000f;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDetails = GetComponent<PlayerDetails>();
        tankControls = new TankControls();
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            spawnManager = GameObject.Find("PlayerManager").GetComponent<SpawnManager>();
        }
        if (controlScheme == "Keyboard & Mouse")
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
        smokeTrail.SetActive(false);
        controlScheme = GetComponent<PlayerInput>().currentControlScheme;

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
        if (bulletCount < maxBullets && canShoot && !isShot)
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
        Debug.Log(controlScheme);
        switch (controlScheme)
        {
            case "Keyboard & Mouse":
                screenPos = Mouse.current.position.ReadValue();
                screenPos.z = Camera.main.nearClipPlane + 9;

                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // The mouse position in world space
                    Vector3 mousePosWorld = hit.point;

                    // Calculate the direction from the gameobject to the mouse position
                    Vector3 direction = mousePosWorld - tankHead.position;
                    float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                    direction.y = 0;

                    Quaternion rotation = Quaternion.LookRotation(direction, new Vector3(0,1,0));
                    tankHead.rotation = rotation * Quaternion.Euler(0, -90, 0);
                }
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
        if (canMove)
        {
            float horizontal = moveValue.x;
            float vertical = moveValue.y;
            Vector3 Movement = this.transform.right * horizontal + this.transform.forward * vertical;
            rb.velocity = Movement * moveSpeed;
            RotateBody(moveValue);
            RotateBarrel();
            if (isShot && canDie)
            {
                Dead();
            }

            CastRay(bulletHole.position, bulletHole.forward);
        }
        else
        {

        }

    }

    void Dead()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        canMove = false;
        smokeTrail.SetActive(true);
        spawnManager.checkIfDead(playerDetails.playerID + 1);
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
