using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    float moveSpeed = 5;
    Vector2 playerPos;
    float BoundX = 9f;
    float BoundY = 4.4f;
    Rigidbody2D rgbd;
    public GameObject fire;
    public bool isFiring;
    float health = 200;
    float inputX, inputY;
    Touch touch;
    string gamePlay;
	// Use this for initialization
	void Start () {
        rgbd = GetComponent<Rigidbody2D>();
        isFiring = false;
        gamePlay = Script.gamePlay;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Movement();
        
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFiring = true;
            
            InvokeRepeating( "startFire",0.001f,0.2f);
        }  if(Input.GetKeyUp(KeyCode.Space))
                {
            CancelInvoke("startFire");
            
        }
	}
    void TakingTouchInput()
    {

    }
    void Movement()
    { playerPos = transform.position;
        playerPos.y = (Mathf.Clamp(playerPos.y, -BoundY, BoundY));
        playerPos.x = (Mathf.Clamp(playerPos.x, -BoundX, BoundX));

        MovementByKeyBoard();
       // MovementByAccelerometer();
    }
    void MovementByAccelerometer()
    {
       // inputX = Input.acceleration.x * Time.deltaTime*15;
      
        inputY = Input.acceleration.y * Time.deltaTime * 15;
        playerPos = transform.position;
        // playerPos.x = inputX  * 30;
        Debug.Log(inputY);
        playerPos.y = inputY  * 130;
        transform.position = playerPos;
    }
    void MovementByKeyBoard()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerPos.y += moveSpeed * Time.fixedDeltaTime;

            transform.position = playerPos;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerPos.y += -moveSpeed * Time.fixedDeltaTime;
            transform.position = playerPos;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerPos.x += -moveSpeed * Time.fixedDeltaTime;
            transform.position = playerPos;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerPos.x += moveSpeed * Time.fixedDeltaTime;
            transform.position = playerPos;
        }
    }

    void MovementByUsingGetAxis()
    {
        float inputX = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * moveSpeed;
        float inputY = Input.GetAxis("Vertical") * Time.fixedDeltaTime * moveSpeed;
        playerPos = transform.position;
        InputHandling();

    }
    void InputHandling()
    {       // Debug.Log(inputX + "x" + inputY + "y");
        if (inputX == 0)
        {
            playerPos = rgbd.position + Vector2.up * inputY;
            if (playerPos.y > BoundY || playerPos.y < -BoundY)
            {
                playerPos.y = Mathf.Clamp(playerPos.y, BoundY, -BoundY);
            }
            // 
            rgbd.MovePosition(playerPos);
        }
    
        else if(inputY == 0)
        {
            playerPos = rgbd.position + Vector2.right * inputX;
            if(playerPos.x>BoundX|| playerPos.x < -BoundX)
            {
                playerPos.x = Mathf.Clamp(playerPos.x, BoundX, -BoundX);
            }
            rgbd.position = playerPos;
        }
    }
   void startFire()
    {
        Vector3 offset = new Vector2(2, 0);
        if (isFiring)
        {
          //  Debug.Log("firung");
          
          GameObject obj =   Instantiate(fire, transform.position +offset, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().color = Color.yellow;
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);

            //obj.transform.parent = transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Fire missile = collision.gameObject.GetComponent<Fire>();
        if (missile)
        {
          //  Debug.Log("hit by missile " + health);
            health -= missile.GetDamage();
          
            missile.Hit();
            if (health <= 0)
            {
                
                Destroy(gameObject);
            }
        }
    }
}
