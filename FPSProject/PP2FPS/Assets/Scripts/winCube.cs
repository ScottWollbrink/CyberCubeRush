using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winCube : MonoBehaviour, IWinCube, IDamage
{
    [SerializeField] int HP;
    [SerializeField] Renderer model;

    int currentHP;
    int maxHP;


    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;
        cubeSpawn();
    }

    public void win()
    {
        StartCoroutine(waitWin());
    }

    IEnumerator waitWin()
    {
        yield return new WaitForSeconds(.25f);

        GameManager.Instance.WinGame();
    }

    public void cubeSpawn()
    {
        currentHP = maxHP;
        transform.position = GameManager.Instance.cubeSpawnPos.transform.position;
        GameManager.Instance.holdController.drop();
    }

    public void takeDamage(int amount)
    {
        currentHP -= amount;
        StartCoroutine(redFlash());

        if (currentHP <= 0)
        {
            cubeSpawn();
        }
    }
    IEnumerator redFlash()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        model.material.color = Color.white;
    }
}
