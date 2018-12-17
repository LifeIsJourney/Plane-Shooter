using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    float damage = 10;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if(transform.position.x<-12 || transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }
}
