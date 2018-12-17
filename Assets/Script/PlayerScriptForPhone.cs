using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptForPhone : MonoBehaviour
{
    [Range(0, 10)]
    public float moveSpeed = 5;
    // to delay fire
    public float shootDelay = 0;
    private float fireRate = 0.1f;
    //To delay gameOverScreen
    float gameOverDelay = 1;

    public bool forMobile ;
    public GameObject fire;
    [Range(0, 20)]
    [Header("Up")]
    public int upwardForce=7;
    public bool isFiring;
    float health = 100;
    float currentHealth;
    float inputX, inputY;
    Touch touch;
    public static bool isRunning = false;
    public GameObject gameoverPrefab;
    // input from main screen
    [SerializeField]
    bool hard;
    bool runningOnPhone;
    private Vector2 playerPos;
    private float BoundX = 9f;
    private float BoundY = 4.4f;
    private Rigidbody2D rgbd;
    private Rect bottomLeftRect, TopLeftRect, RightHalf,Lefthalf;
    public GameObject StartInstructionDesktopPrefab, StartInstructionPhonePrefab;
    GameObject StartInstructionDeskTopPrefabObj, StartInstructionPhonePrefabObj;
    public GameObject deathParticle;
    public Color deathAlphaZeroColor;
    public AudioClip fireSound, deathSound;

    public Image healthBar;
    // Use this for initialization
    void OnAwake()
    {
        
    }
    void Start()
    {
        currentHealth = health;
        upwardForce = 7;
        Screen.orientation = ScreenOrientation.Landscape;
        
        forMobile = SecMenuScene.forMobile;
        hard = OnButtonClick.hard;
        if (!forMobile)
        {
            StartInstructionDeskTopPrefabObj = Instantiate(StartInstructionDesktopPrefab, transform.position, Quaternion.identity);

        }
        else
        {
            StartInstructionPhonePrefab = Instantiate(StartInstructionPhonePrefabObj, transform.position, Quaternion.identity);
        }
        rgbd = GetComponent<Rigidbody2D>();
        isFiring = false;
        Input.multiTouchEnabled = true;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
          if (forMobile)
          {
              Debug.Log(forMobile);
              Touch touch = Input.GetTouch(0);
              if (touch.tapCount > 1)
              {
                  isRunning = true;
                Destroy(StartInstructionPhonePrefabObj);
              }

          }

          if (Input.GetKey(KeyCode.S))
          {
            Destroy(StartInstructionDeskTopPrefabObj);
            
              Debug.Log("s pressed");
              isRunning = true;
          }
          if (isRunning)
          {
              if (hard)
              {
                  Invoke("HardPlay", 0f);
              }
            else
            {
                Invoke("NormalPlay", 0f);
              //  Debug.Log("normal");

            }
        }
       
    }

    void NormalPlay()
    {
        Vector2 touchPos = Vector2.zero;
        if (forMobile)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;

            // if (Input.touchCount > 0)
            {

                bottomLeftRect = new Rect(0, 0, Screen.width / 2, Screen.height / 2);
                TopLeftRect = new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2);
                //check this
                RightHalf = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
                Lefthalf = new Rect(0, 0, Screen.width / 2, Screen.height);
                 Debug.Log(Lefthalf + "+" + RightHalf);
            }

        }


        Movement();




        if (Input.GetKey(KeyCode.Space) || RightHalf.Contains(touchPos))
        {
            //Debug.Log("fire");
            isFiring = true;

            startFire();
            // InvokeRepeating("startFire", 0.003f, 0.4f);

            //
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFiring = false;

        }
    }
    // In hard play space will fire and up arrow will make player jump control using both hand
    void HardPlay()
    {
        Debug.Log("HardPlay 1");
        Vector2 touchPos=Vector2.zero;
        if (forMobile)
        {
            Debug.Log("HardPlay 2");
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position; 
            Debug.Log(touch);
        }

        rgbd.bodyType = RigidbodyType2D.Dynamic;
        rgbd.mass = 1f;
       // Debug.Log("Lefthalf.Contains(touchPos)" + Lefthalf.Contains(touchPos));
       // Debug.Log("HardPlay 3");
        if (forMobile && Lefthalf.Contains(touchPos) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            Debug.Log("HardPlay 4");
            // hacve to change something
           
             rgbd.velocity = new Vector2(0,7);
         //   Debug.Log(rgbd.velocity);
            

            rgbd.gravityScale = 1f;
            rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;

           
        }
        if (forMobile && RightHalf.Contains(touchPos) || Input.GetKey(KeyCode.Space))
        {

            isFiring = true;

            startFire();
            //  Debug.Log("RightClick");
        }

    }

    void Movement()
    {
        playerPos = transform.position;
        playerPos.y = (Mathf.Clamp(playerPos.y, -BoundY, BoundY));
        playerPos.x = (Mathf.Clamp(playerPos.x, -BoundX, BoundX));

        MovementByKeyBoardOrTouch();

    }

    void MovementByKeyBoardOrTouch()
    {
        Vector2 touchPos = Vector2.zero;
        if (forMobile)
        {
            //for touch
            Vector2 topLeft = Vector2.zero;
            Vector2 bottomleft = Vector2.zero;
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;
            //to check if touch bottom left or top Left

        }




        if (Input.GetKey(KeyCode.UpArrow) || TopLeftRect.Contains(touchPos))
        {
            playerPos.y += moveSpeed * Time.fixedDeltaTime;

            transform.position = playerPos;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || bottomLeftRect.Contains(touchPos))
        {
            playerPos.y += -moveSpeed * Time.fixedDeltaTime;
            transform.position = playerPos;
        }



    }


    void startFire()
    {

        if (isFiring && Time.time > shootDelay)
        {
            ///Debug.Log("firung");
            shootDelay = Time.time + fireRate;
            Vector3 offset = new Vector2(1, 0);

            GameObject obj = Instantiate(fire, transform.position + offset, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().color = Color.yellow;
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);


            //sound
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            // StartCoroutine(fireCouroutine());

            //obj.transform.parent = transform;
        }
    }

    /* IEnumerator fireCouroutine()
      {
          Vector3 offset = new Vector2(1, 0);
          GameObject obj = Instantiate(fire, transform.position + offset, Quaternion.identity);
          obj.GetComponent<SpriteRenderer>().color = Color.yellow;
          obj.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);

          isFiring = false;
          Debug.Log("1" +isFiring);
          yield return new WaitForSeconds(shootDelay);

          isFiring = false;
          Debug.Log("2" + isFiring);

      }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "villan")
        {
            //Debug.Log("player dead get high score and display to gameover screen and restart the game");
            
            Destroy(gameObject);
            Instantiate(gameoverPrefab, transform.position, Quaternion.identity);
        }

        Fire missile = collision.gameObject.GetComponent<Fire>();
        if (missile)
        {
            //  Debug.Log("hit by missile " + health);
            health -= missile.GetDamage();
            healthBar.fillAmount =  health/currentHealth;

            missile.Hit();
            if (health <= 0)
            {
                StartCoroutine(death());
               
            }
        }
    }
    IEnumerator death()
    {   
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        rgbd.velocity = Vector2.down*2;
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = deathAlphaZeroColor;
        yield return new WaitForSeconds(1.5f);
        
        Instantiate(gameoverPrefab, transform.position, Quaternion.identity);
        
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("insidfe colliision");
        if (collision.collider.tag == "villan")
        {   
            Destroy(gameObject);
        }

    }
}

