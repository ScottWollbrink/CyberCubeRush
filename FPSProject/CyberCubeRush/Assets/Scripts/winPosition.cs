using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        IWinCube cube = other.GetComponent<IWinCube>();
        
        if (cube != null)
        {
            cube.win();
        }
        else if (GameManager.Instance.holdController.hasCube)
        {
            IWinCube winCube = GameManager.Instance.holdController.heldObj.GetComponent<IWinCube>();
            if (winCube != null)
            {
                winCube.win();
            }
        }
    }
}
