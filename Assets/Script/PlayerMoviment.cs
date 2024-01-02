using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    [SerializeField] float speed, limiteJump, forceJump;
    [SerializeField] WebcamMovement webDetect;

    [SerializeField] float lastY;

    private Rigidbody2D rb;

    bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (webDetect.jumpY < limiteJump && !isJump)
        {
            print("pula");
            rb.velocity = new Vector2(0, forceJump);
            isJump = true;
        }
       
    }

    private void Move()
    {
        transform.Translate(speed*Time.deltaTime, 0, 0);
    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            isJump = false;
        }
    }
}
