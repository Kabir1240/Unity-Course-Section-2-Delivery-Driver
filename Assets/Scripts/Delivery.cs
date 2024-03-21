using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    // [SerializeField] Color32 hasPackageColor = new Color32 (1, 1, 1, 1);
    // [SerializeField] Color32 noPackageColor = new Color32 (1, 1, 1, 1);

    [SerializeField] public Sprite hasPackageSprite;                    // reference to car sprite if package is being carried
    [SerializeField] public Sprite noPackageSprite;                     // reference to car sprite if package is not being carried

    [SerializeField] float destroyDelay;                                // delay between destroying package

    [SerializeField] Driver driver;                                     // reference to car

    public bool hasPackage = false;                                     // bool top check if driver is carrying a package or not

    SpriteRenderer spriteRenderer;                                      // sprite renderer reference

    void Start() 
    {
        // find sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // method can be used to damage car
    void OnCollisionEnter2D(Collision2D other) 
    {
        // Debug.Log("pain");
    }

    // triggered if car collects package or drops one off
    void OnTriggerEnter2D(Collider2D collisionInfo) 
    {

        // if driver collects package
        if (collisionInfo.tag == "Package" && !hasPackage)
        {
            // Debug.Log("Package picked up");
            hasPackage = true;

            // makes sure boost sprite is not being used
            if (!driver.hasSpeedUp) 
            {
                spriteRenderer.sprite = hasPackageSprite;
            }

            Destroy(collisionInfo.gameObject, destroyDelay);
        }

        // if driver drops package to a customer
        if (collisionInfo.tag == "Customer" && hasPackage)
        {
            // Debug.Log("delivered package");
            hasPackage = false;

            // makes sure boost sprite is not being used
            if (!driver.hasSpeedUp) 
            {
                spriteRenderer.sprite = noPackageSprite;
            }
        }
    }
}
