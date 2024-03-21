using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{   
    [SerializeField] Sprite hasSpeedUpSprite;                   // boost sprite

    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;
    [SerializeField] public float boostSpeed;
    [SerializeField] public float slowSpeed;
    [SerializeField] public Delivery delivery;

    SpriteRenderer spriteRenderer;

    public bool hasSpeedUp;
    bool isSlow;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  
    }
    // Update is called once per frame
    void Update()
    {
        // need to calculate this every frame since it changes frequently based on player control
        float rotateAmount = Input.GetAxis("Horizontal") * rotateSpeed;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed;

        transform.Rotate(0, 0, -rotateAmount * Time.deltaTime);
        transform.Translate(0, moveAmount * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) 
    {
        if (!isSlow)
        {
            moveSpeed = slowSpeed;

            if (delivery.hasPackage)
            {
                spriteRenderer.sprite = delivery.hasPackageSprite;
            }
            
            else if (!delivery.hasPackage)
            {
                spriteRenderer.sprite = delivery.noPackageSprite;
            }

            isSlow = true;
            hasSpeedUp = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo) 
    {
        if (collisionInfo.tag == "SpeedUp" && !hasSpeedUp)
        {
            moveSpeed = boostSpeed;
            spriteRenderer.sprite = hasSpeedUpSprite;

            hasSpeedUp = true;
            isSlow = false;
        }

        
    }
}
