using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : Enemy
{
    public float mSpreadMaxAngle = 20;
    public int mSpreadNumBullets = 3;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Approach", 0.0f);
        Invoke("StartShoot", 2.0f);
        Invoke("Leave", 7.0f);
    }

    protected override void GenerateBullets()
    {
        GenerateSpreadBullets();
        mShotsTaken += 1;
    }

    private void GenerateSpreadBullets() {
        Vector3 lookDir = (transform.up).normalized;
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90.0f;
        float radiusMin = lookAngle - mSpreadMaxAngle;

        float radiusIncrement = (mSpreadMaxAngle*2 / (mSpreadNumBullets - 1.0f));

        for (int i = 0; i < mSpreadNumBullets; i++)
        {
            float bulletAngle = radiusMin + (radiusIncrement * i);
            Quaternion bulletQuat = Quaternion.AngleAxis(bulletAngle, transform.forward);
            GameObject bullet = Instantiate(mBulletType, transform.position, bulletQuat);
            bullet.GetComponent<Bullet>().KillTag = "Player";
        }
    }
}
