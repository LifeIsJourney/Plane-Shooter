using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillanBehaviour : MonoBehaviour {
    public GameObject firePrefab;
    float frequency = 0.5f;
    float health = 30f;
    ScoreKeeper2 scoreKeeper2;
    int point = 10;
    public Animator deathAnimation;
    public GameObject deathParticle;
    private ParticleSystem ps;
    public AudioClip fireSound, deathSound;
    // Use this for initialization
    void Start () {
        scoreKeeper2 = GameObject.Find("Score").GetComponent<ScoreKeeper2>();
        ps = GetComponent<ParticleSystem>();   
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerScriptForPhone.isRunning)
        {
            Vector3 offset = new Vector3(-1, 0, 0);


            float probability = Time.deltaTime * frequency;
            if (Random.value < probability)
            {
                AudioSource.PlayClipAtPoint(fireSound, transform.position);
                GameObject obj = Instantiate(firePrefab, transform.position + offset, Quaternion.identity) as GameObject;
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                obj.transform.parent = transform;
            }
        }
        
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerScriptForPhone.isRunning)
        {   if(collision.tag == "heroFire")
            {
                Fire missile = collision.GetComponent<Fire>();
                health -= missile.GetDamage();
                missile.Hit();
                Debug.Log("health of enemy" + health);
                if (health <= 0)
                {
                    StartCoroutine(death());
                }
            }
            
        }
        
    }
    IEnumerator death()
    {
        deathAnimation.SetTrigger("death");
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        yield return new WaitForSeconds(1);
        //nwwd to destroy this particle.
       GameObject obj =  Instantiate(deathParticle, transform.position, Quaternion.identity) ;
        Debug.Log("Destroy of enemy" + health);
        Destroy(obj, 2);
        DestroyVillan();
        
    }
    void DestroyVillan()
    {   
       scoreKeeper2.Score(point);
        Destroy(gameObject);
    }

}
