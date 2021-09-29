using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_CarMinigame2 : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private void Start()
    {
        speed = 6f;      
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if(gameObject != null)
        {
            speed += Time.deltaTime * 5;
            rb.velocity = transform.right * speed;
        }
    }
}
