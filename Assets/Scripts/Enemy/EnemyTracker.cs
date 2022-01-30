using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : Enemy
{
    public float mRotSpeed = 1.0f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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
        else
        {
            Track();
        }

        if (mStateShoot)
        {
            Shoot();
        }

    }

    protected override void Move(Vector3 dir)
    {
        base.Move(dir);
        RotateTowardsDirection(Vector3.down);
    }

    private void Track()
    {
        Vector3 lookDir = Vector3.down;
        if (player)
        {
            lookDir = (player.transform.position - this.transform.position).normalized;
        }

        RotateTowardsDirection(lookDir);
    }

    private void RotateTowardsDirection(Vector3 lookDir)
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion newRot = Quaternion.AngleAxis(angle, transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * mRotSpeed);
    }
}
