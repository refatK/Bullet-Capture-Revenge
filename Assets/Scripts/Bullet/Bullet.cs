using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mSpeed = 5.0f;

    private string killTag = "";

    public string KillTag { get => killTag; set => killTag = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * mSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("OBJ is:" + other.gameObject.name );
        if(other.gameObject.tag == KillTag)
        {
            // Destroy(other.gameObject); // TODO: Remove
            IHurtable hitObj = other.gameObject.GetComponent<IHurtable>();
            hitObj.OnHitByBullet(this);
            Destroy(this.gameObject);
        }
    }

}
