using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHurtable
{
    public GameObject mBulletType;
    public float mMoveSpeed = 5.0f;
    public int mNumShots = 2;
    public float mShotsPerSecond = 1.0f;

    // states
    protected bool mStateMoving;
    protected bool mStateLeaving;

    protected bool mStateShoot;

    protected int mShotsTaken = 0;
    protected float mShotTime = 0.0f;
    protected Vector3 mMoveDir;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Approach", 0.0f);
        Invoke("StartShoot", 2.0f);
        Invoke("Leave", 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (mStateMoving)
        {
            Move(mMoveDir);
        }

        if (mStateShoot)
        {
            Shoot();
        }
    }

    protected virtual void Move(Vector3 dir) {
        Vector3 newPos = transform.position + (mMoveSpeed * dir);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }

    protected void Shoot()
    {
        if (mShotsTaken < mNumShots && mShotTime == 0.0f)
        {
            GenerateBullets();
        }

        mShotTime += Time.deltaTime;
        if (mShotTime >= mShotsPerSecond) {
            mShotTime = 0.0f;
        }

        if (mShotsTaken >= mNumShots)
        {
            StopShoot();
        }
    }

    protected virtual void GenerateBullets()
    {
        GameObject bullet = Instantiate(mBulletType, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().KillTag = "Player";
        mShotsTaken += 1;
    }

    protected void StopMove()
    {
        mStateMoving = false;
    }

    protected void StopShoot()
    {
        mStateShoot = false;
    }

    void Approach() 
    {
        mMoveDir = Vector3.down;
        mStateMoving = true;
        Invoke("StopMove", 0.8f); // this shouldnt be hardcoded
    }

    void Leave()
    {
        mMoveDir = Vector3.down;
        mStateMoving = true;
        mStateLeaving = true;
    }

    void StartShoot()
    {
        mStateShoot = true;
    }

    float ShootDuration() {
        return mNumShots * mShotsPerSecond;
    }

    public void OnHitByBullet(Bullet bullet)
    {
        Destroy(this.gameObject);
    }
}
