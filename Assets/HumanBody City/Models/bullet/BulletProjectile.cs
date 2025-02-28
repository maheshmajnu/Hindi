using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private GameObject VfxHitGreen;
    [SerializeField]
    private GameObject VfxHitRed;
 
    private Image image_HealthBar;
    public LayerMask ignoreLayer;          


    void Awake()
    {
        

        bulletRigidbody = GetComponent<Rigidbody>();
        DestroyObjectDelayed();

        //get heathbar 
        GameObject healthBar = GameObject.Find("HealthBar");
        if(healthBar != null )
        {
            image_HealthBar = healthBar.GetComponent<Image>();

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(9, 7);
        bulletRigidbody.velocity = transform.forward*speed;
 
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hir road");
        if (other.gameObject.tag == "BulletTarget" && image_HealthBar != null)
        {
            Instantiate(VfxHitGreen, transform.position, Quaternion.identity);
            //healthbar increment  recovery 
            Vector2 size = image_HealthBar.rectTransform.sizeDelta;
            image_HealthBar.rectTransform.sizeDelta = new Vector2(size.x+2f, 5f);   //2f rate of heal
            if(size.x > 98f)
            {
                //objective complete
                GameObject.Find("TPP_Player").GetComponent<Sfx_HumanBodyGame>().LastObjective();
                Destroy(other.gameObject);
                
            }  

        }
        else
        {
            Instantiate(VfxHitRed, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    void DestroyObjectDelayed()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, 3);
    }
}
