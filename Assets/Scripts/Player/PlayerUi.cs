using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Text mLives;
    private Player player;

    void Start() {
        player = GetComponent<Player>();
        SetLivesUi(player.NumLives);
    }
    
    public void SetLivesUi(int numLives)
    {
        if (numLives < 0)
        {
            return;
        }

        mLives.text = numLives.ToString();
    }
}
