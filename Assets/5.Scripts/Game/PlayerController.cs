using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun 
{
    #region Componets
    [Header("Components")]
    public Rigidbody rig;
    public LayerMask layerMaskinteract;
    public CameraController Camera;
    public AudioSource audioSource;
    public GameObject Inventory;
    public int punID;
    public Player photonPlayer;

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

    [Header("Options")]
    public ValueAttribute InteractButton;

    [Header("Debug")]
    public bool debug;
    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        Camera = GetComponentInChildren<CameraController>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tryJump();  
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Tryinteract();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            Inventory.SetActive(false);
        }
    }

    [PunRPC]
    public void Initialize(Player player)
    {
        punID = player.ActorNumber;
        photonPlayer = player;
        GameManager.Instance.players[punID - 1] = this;

        if(!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            rig.isKinematic = true;
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

        Ray ray = new Ray(new Vector3(this.transform.position.x, this.transform.position.y+1, this.transform.position.z), Camera.transform.forward*1);
        RaycastHit hit;
        if(Physics.SphereCast(ray,5, out hit, 10))
        {
            if(hit.transform.gameObject.CompareTag("Interactable"))
            {
                 hit.transform.gameObject.GetComponent<Interactable>().Interact.Invoke();
            }
            
        }

        if (debug)
        {
            print("draw?");
            Debug.DrawRay(this.transform.position, Camera.transform.forward * 3, Color.red, 2);
        }



    }
   
}
