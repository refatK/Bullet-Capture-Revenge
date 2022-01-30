using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHurtable
{
    [SerializeField] private int mNumLives;
    [SerializeField] private int mAmmo;
    [SerializeField] private GameObject mBulletType;

    public int NumLives
   {
       get { return mNumLives; }
       set {
          mNumLives = value;
          playerUi.SetLivesUi(value);
       }
   }

    private PlayerUi playerUi;

    // Start is called before the first frame update
    void Start()
    {
        playerUi = GetComponent<PlayerUi>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShooting() && mAmmo > 0)
        {
            Shoot();
        }    
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(mBulletType, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().KillTag = "Enemy";
        --mAmmo; 
    }

    private bool IsShooting() {
        // Mouse Input
        if (Input.GetMouseButtonDown(1)) 
        {
            return true;
        }

        // Touch Input
        if (Input.touchCount > 1)
        {
            Touch secondTouch = Input.GetTouch(1);
            return secondTouch.phase == TouchPhase.Began;
        }

        return false;
    }

    public void IncrementAmmo() {
        ++mAmmo;
        Debug.Log("Ammo is: " + mAmmo);
    }

    public void OnHitByBullet(Bullet bullet)
    {
        Debug.Log("Ouch, I got hit");
        NumLives -= 1;
        if (NumLives < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
