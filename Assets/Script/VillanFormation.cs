using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillanFormation : MonoBehaviour {
    
    public GameObject VillanPrefab;
    float moveSpeed = 3;
    Vector3 bottomEdge, topEdge,rightEdge,rightEdgeOffset;
    public float width = 5;
    public float height = 7;
    bool moveUp = true;
    public GameObject firePrefab;
    public bool villanStillAlive = true;
    int formationRespawningCount = 0;
    int formationRespawningCountSetter = 10;// chnage this to amke game easy or hard
    float timeToMoveColserToPlayer = 2;
    float startingTime;
    public float speed = 10;
    //exp
    float SpawnResetTime = 0;
    int spawnResetCount = 10;
    int functionRunningFirstTime = 0;
    Vector2 localPosition;
    //
   ScoreKeeper2 scoreKeeper2;
    public GameObject gotBonusParticle;
    // Use this for initialization
    void Start()
    {
        scoreKeeper2 = FindObjectOfType<ScoreKeeper2>();
        float distance = transform.position.z - Camera.main.transform.position.z;
        bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,distance));
        topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1,distance));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        rightEdgeOffset = new Vector3(2.5f, 0, 0);
        startingTime = 1;
        //spawnVillan();
        SpawnOneByOne();

    }
    void spawnVillan()
    {
        Vector3 offset = new Vector3(Random.Range(-2, 2), 0);
        formationRespawningCount += 1;
        foreach (Transform child in transform)
        {
            Quaternion rotation = child.parent.rotation;
            GameObject obj = Instantiate(VillanPrefab, child.transform.position+offset, rotation) as GameObject;
            obj.transform.parent = child;
        }
        
    }
    void SpawnOneByOne()
    {
        Vector3 offset = new Vector3(Random.Range(-2, 2), 0);
        formationRespawningCount += 1;
        
        Transform freePos = NextFreePosition();
        if (freePos)
        {
            Quaternion rotation = freePos.parent.rotation;
            GameObject obj = Instantiate(VillanPrefab, freePos.position + offset, rotation) as GameObject;
            obj.transform.parent = freePos;

        }
        if (NextFreePosition())
        {
            Invoke("SpawnOneByOne", 0.6f);
        }
       
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerScriptForPhone.isRunning)
        {
            localPosition = transform.localPosition;
            if (Time.time >= startingTime)
            {
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                // Debug.Log("Villan is closed=r now");
                startingTime = Time.time + timeToMoveColserToPlayer;
                //  Debug.Log(startingTime + "=" + Time.time + "+" + timeToMoveColserToPlayer);
                // Debug.Log("VillanBehaviour>update>StartingTime" + startingTime);
            }


            moveLeftOrRight();
            if (villanIsDead())
            {
                SpawnOneByOne();

            }
            MakeLevelHarder();
        }
        

   
       
        //SpawnOneByOne(); // different effect can use

    }
    void MakeLevelHarder()
    {
        if (formationRespawningCount > formationRespawningCountSetter)
        {

            if (functionRunningFirstTime == 0)
            {
                SpawnResetTime = Time.time;
                functionRunningFirstTime = 1;
            }


           // Debug.Log("Spt " + SpawnResetTime + " ++" + Time.time);

          //  Debug.Log("kro rese3t");
            formationRespawningCount = 0;

            if (Time.time - SpawnResetTime > spawnResetCount) // change this to make game easy and hard
            {
                formationRespawningCountSetter += 5;
                spawnResetCount += 3;
                // nice done the trick
                // transform.position = new Vector3(8, 0, 0); //worked
                StartCoroutine(DelaySomeTime());
                // level 2 scene transition change BG and inc. //change this thing in next scene to higher.
              //  Debug.Log("formationRespawningCountSetter " + formationRespawningCountSetter);
            }

        }
    }

    IEnumerator DelaySomeTime()
    {
       GameObject particleObj= Instantiate(gotBonusParticle,rightEdge, Quaternion.identity)as GameObject;
        transform.position = rightEdge - rightEdgeOffset;
        yield return new WaitForSeconds(1f);
        Debug.Log("Score" +ScoreKeeper2.score);
        scoreKeeper2.Bonus(50);
        Debug.Log("Score" + ScoreKeeper2.score);
        Destroy(particleObj);


    }
    void moveUsingSin()
    {
        Vector2 pos = transform.position;
        pos.y += 2 * Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = pos;
    }
    void moveLeftOrRight()
    {   
        float bottomHalf = transform.position.y - (0.5f * height);
        float topHalf = transform.position.y + (0.5f * height);
        if (bottomHalf < bottomEdge.y)
        {
            moveUp = true;
        }
        else if (topHalf > topEdge.y)
        {
            moveUp = false;
        }
        if (moveUp)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));

    }
    bool villanIsDead()
    {
        foreach (Transform villanChild in transform)
        {
           if(villanChild.childCount>0)
            {
                //Debug.Log("GetChildCount" +v );
                return false;
            }
        }
         return true;
    }
    Transform NextFreePosition()
    {
        foreach (Transform childPos in transform)
        {
            if (childPos.childCount==0)
            {
                return childPos;
            }
        }
        return null;
    }

}
