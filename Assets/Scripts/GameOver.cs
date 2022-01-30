using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject mMenuPanel;
    public Text mMenuTitle;

    public string mMenuTitleText;
    
    public void EndGame() 
    {
        if (!mMenuTitle)
        {
            return;
        }

        mMenuTitle.text = mMenuTitleText;
        mMenuPanel.SetActive(true);
    }

    private void OnDestroy() {
        EndGame();
    }
}
