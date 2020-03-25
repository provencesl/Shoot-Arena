using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int playerId;
    public int lives = 3;
    public string playerName = "Veteran";

    [SerializeField] private const int PLAYER_MAX_GRENADES = 3;
    [SerializeField] private const float PLAYER_MAX_HEALTH = 20f;

    [SerializeField] private float playerHealth = 20f;
    [SerializeField] private int playerGrenades = 3;
    [SerializeField] private int bleedingAmount = 5;
    [SerializeField] private int goreAmount = 5;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 250f;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject goreParticle;
    [SerializeField] private GameObject grenadeSpawn;

    [SerializeField] private AudioSource characterAudioSource;
    [SerializeField] private AudioClip deathSplatterSound;
    [SerializeField] private AudioClip deathScreamSound;
    [SerializeField] private float fxSplatterVolume = 0.5f;
    [SerializeField] private float fxScreamVolume = 0.3f;

    private GameObject player;
    private GameObject[] ground;
    private GameObject[] spawnPoints;
    private GameObject previousSpawnPoint;

    private BoxCollider2D playerBoxCollider;

    private bool facingRight = true;
    private bool isGrounded = false;

    Animator animator;

    // Start is called before the first frame update
    void Start() {
        
        player = gameObject;
        animator = GetComponent<Animator>();
        playerBoxCollider = GetComponent<BoxCollider2D>();

        ground = GameObject.FindGameObjectsWithTag("Ground");
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

        characterAudioSource = GameObject.Find("CharacterSoundEffects").GetComponent<AudioSource>();

        previousSpawnPoint = new GameObject();

        Respawn();
     }

    // Update is called once per frame
    void FixedUpdate() {

        float moveHorizontal = Input.GetAxis("Horizontal" + playerId);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * speed, GetComponent<Rigidbody2D>().velocity.y);

        if(moveHorizontal > 0 && !facingRight) {
            Flip();
        } else if(moveHorizontal < 0 && facingRight) {
            Flip();
        }
    }

    void Update() {
        CheckInput();
    }

    void CheckInput() {
        //if(isGrounded && Input.GetButtonDown("Jump" + playerId)) {
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        //}
        if (Input.GetButtonDown("Grenade" + playerId))
        {
            ThrowGrenade();
        }
        if (isGrounded && Input.GetButtonDown("Jump" + playerId))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1f * speed, 0,0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1f * speed, 0, 0);
        }

    }

    void ThrowGrenade() {
        if(playerGrenades > 0) {
            StartCoroutine(grenadeSpawn.GetComponent<GrenadeThrow>().ThrowGrenade());
            playerGrenades--;
        }
    }

    public void ReceiveDamage(float dmg) {
        Bleed();
        playerHealth -= dmg;
        if(playerHealth <= 0) {
            Killed();
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar() {
        healthBar.transform.localScale = new Vector3 (playerHealth, 1f, 1f);
    }
 
    void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.tag == "Ground") {
         isGrounded = true;
        }
        if(collider.gameObject.tag == "OutOfBounds") {
            Killed();
        }
    }
 
    void OnCollisionExit2D(Collision2D collider) {
        if(collider.gameObject.tag == "Ground") {
         isGrounded = false;
        }
    }
    
    void Flip() {
        facingRight = !facingRight;
        player.transform.Rotate(0f, 180f, 0f);
    }

    void Killed() {
        characterAudioSource.PlayOneShot(deathScreamSound, fxScreamVolume);
        characterAudioSource.PlayOneShot(deathSplatterSound, fxSplatterVolume);
        SpawnGore();
        gameObject.SetActive(false);
        lives--;
        if(lives > 0) {
            Respawn();
        }
    }

    void SpawnGore() {
        for(int i = 0; i < goreAmount; i++) {
            Instantiate(goreParticle, player.transform.position, player.transform.rotation);
        }
    }

    void Respawn() {
       GameObject chosenSpawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
       if(chosenSpawnpoint.transform.position == previousSpawnPoint.transform.position) {
           Respawn();
       } else {
           gameObject.transform.position = chosenSpawnpoint.transform.position;
           previousSpawnPoint = chosenSpawnpoint;
           gameObject.SetActive(true);
           ResetProperties();
       }
    }

    void ResetProperties() {
        playerHealth = PLAYER_MAX_HEALTH;
        playerGrenades = PLAYER_MAX_GRENADES;
        UpdateHealthBar();
    }

    void Bleed() {
        for(int i = 0; i < bleedingAmount; i++) {
            Instantiate(bloodParticle, player.transform.position, player.transform.rotation);
        }
    }
}
