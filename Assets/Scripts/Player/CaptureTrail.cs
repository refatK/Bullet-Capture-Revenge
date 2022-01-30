using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTrail : MonoBehaviour
{
    public int mTrailNodeCount = 3;
    public float mDistanceTillUpdate = 2.0f;
    public float mTimeTillUpdate = 1.0f;
    public bool mUseTimeForUpdate = false;

    private GameObject mPlayerObj;
    private Player mPlayer;
    private EdgeCollider2D mTrail;
    private LineRenderer mTrailVisual;
    private Vector2[] mPlayerPosArr;
    private Vector2 mPrevPlayerPos;
    private float mPlayerDistance = 0.0f; // TODO: maybe base off time instead
    private float mPlayerTime = 0.0f; 


    // Start is called before the first frame update
    void Start()
    {
        mPlayerPosArr = new Vector2[mTrailNodeCount];
        mPlayerObj = transform.parent.gameObject;
        mPlayer = transform.parent.GetComponent<Player>();
        mPrevPlayerPos = (Vector2) mPlayerObj.transform.position;
        mTrail = gameObject.GetComponent<EdgeCollider2D>();
        mTrailVisual = gameObject.GetComponent<LineRenderer>();
        InitizalizePlayerPosArr();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currPlayerPos = (Vector2) mPlayerObj.transform.position;
        UpdatePlayerDistance(currPlayerPos);
        UpdatePlayerTime();

        if (HasReachedUpdateThreshold())
        {
            UpdatePlayerPosArr(currPlayerPos);
            UpdateTrailPoints(currPlayerPos);
            mPlayerDistance = 0.0f;
            mPlayerTime = 0.0f;
        }

        mPrevPlayerPos = currPlayerPos;
    }

    private bool HasReachedUpdateThreshold()
    {
        if (mUseTimeForUpdate)
        {
            return HasReachedTimeThreshold();
        }
        else
        {
            return HasReachedDistanceThreshold();
        }
    }

    bool HasReachedDistanceThreshold()
    {
        return mPlayerDistance >= mDistanceTillUpdate;
    }

    bool HasReachedTimeThreshold()
    {
        return mPlayerTime >= mTimeTillUpdate;
    }


    private void UpdatePlayerDistance(Vector2 currPos) 
    {
        mPlayerDistance += Vector2.Distance(currPos, mPrevPlayerPos);
    }

    private void UpdatePlayerTime() 
    {
        mPlayerTime += Time.deltaTime;
    }
    
    private void UpdatePlayerPosArr(Vector2 currPos) 
    {
        // shift values 1 to the right
        for (int i = mTrailNodeCount - 1; i > 0; i--)
        {
            mPlayerPosArr[i] = mPlayerPosArr[i-1];
        }

        // current postion stored at 0 index
        mPlayerPosArr[0] = currPos;   
    }

    void UpdateTrailPoints(Vector2 currPos)
    {
        Vector2[] newTrailPoints = new Vector2[mTrailNodeCount];
        Vector3[] newTrailVisualPoints = new Vector3[mTrailNodeCount];

        for (int i = 0; i < mTrailNodeCount; i++)
        {
            newTrailPoints[i] = mPlayerPosArr[i] - currPos;

            Vector3 newTrailVisualPoint = newTrailPoints[i];
            newTrailVisualPoint.z = transform.position.z;
            newTrailVisualPoints[i] = newTrailVisualPoint;
        }

        mTrail.points = newTrailPoints;
        mTrailVisual.SetPositions(newTrailVisualPoints);
        mTrailVisual.startColor = Color.black;
        mTrailVisual.endColor = Color.white;
    }

    private void InitizalizePlayerPosArr()
    {
        Vector2[] initalTrailPoints = new Vector2[mTrailNodeCount];

        for (int i = 0; i < mTrailNodeCount; i++)
        {
            mPlayerPosArr[i] = (Vector2) mPlayerObj.transform.position;
            initalTrailPoints[i] = Vector2.zero;
        }

        mTrail.points = initalTrailPoints;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            if(other.gameObject.GetComponent<Bullet>().KillTag == "Enemy")
            {
                return;
            }

            Destroy(other.gameObject);
            mPlayer.IncrementAmmo();
        }
    }
}
