using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Transform VillanPos;
    Vector2 distanceBetween;
    Player playerObj;
    // Use this for initialization
    void Start () {
        playerObj = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        distanceBetween = transform.position - playerObj.transform.position;
        for (int i = 0; i < 1; i++)
        {   
            Instantiate(this, transform.position, Quaternion.identity);
        }
    }
}
