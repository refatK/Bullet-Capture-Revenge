using UnityEngine;

public class InactiveAtStart : MonoBehaviour 
{
    void Start() 
    {
        this.gameObject.SetActive(false);
    }
}