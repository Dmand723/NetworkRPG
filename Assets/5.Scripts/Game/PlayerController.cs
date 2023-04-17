using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rig;
    public LayerMask layerMaskinteract;
    public CameraController Camera;
    public AudioSource audioSource;

    [Header("movemnet stats")]
    public float moveSpeed;
    public float jumpForce;

    [Header("player stats")]
    public int health;
    public int lives;
    public int ammo;
    public int score;

    [Header("End game stats")]
    public int damageTaken;
    public int kills;
    public int deaths;
    public int damageDelt;
    public int shotsFired;
    public int shotsHit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        Camera = GetComponentInChildren<CameraController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tryJump();  
        }
    }

    private void move()
    {
        //get inputs 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;
    }

    private void tryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, 1.5f))
        {
            rig.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }
    }

    private void tryShoot()
    {

    }

    public void heal()
    {

    }

    public void addAmmo()
    {

    }

    public void takeDammage()
    {

    }

    public void die()
    {

    }

    public void Tryinteract()
    {
        Ray hit = new Ray(Camera.transform.position, Camera.transform.forward);
        if (Physics.Raycast(hit, 1.5f, layerMaskinteract))
        {
            
        }


    }
}
