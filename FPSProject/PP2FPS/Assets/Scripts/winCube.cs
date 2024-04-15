using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winCube : MonoBehaviour, IWinCube
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void win()
    {
        StartCoroutine(waitWin());
    }

    IEnumerator waitWin()
    {
        yield return new WaitForSeconds(1);

        GameManager.Instance.WinGame();
    }
}
