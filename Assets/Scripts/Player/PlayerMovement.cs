using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            mOffset = transform.position - GetMousePos();
        }

        // TODO: kind of works, should have better fix
        if (!Input.GetMouseButton(0) || Input.touchCount > 1) { return; }
        transform.position = GetMousePos() + mOffset;
    }

    private Vector3 GetMousePos() 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        return mousePos;
    }
}
