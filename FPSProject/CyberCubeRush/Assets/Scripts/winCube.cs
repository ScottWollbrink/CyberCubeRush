using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCube : MonoBehaviour, IWinCube, IDamage
{
    [SerializeField] float HP;
    [SerializeField] Renderer model;
    [SerializeField] Rigidbody rb;

    float currentHP;
    float maxHP;
    [Header("Audio")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] audImpact;
    [SerializeField, Range(0, 1f)] float audImpactVol;


    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;
        cubeSpawn();
    }

    public void win()
    {
        GameManager.Instance.HandleEnding();
        //StartCoroutine(waitWin());
    }

    IEnumerator waitWin()
    {
        yield return new WaitForSeconds(.1f);

        GameManager.Instance.WinGame();
    }

    public void cubeSpawn()
    {
        rb.velocity = Vector3.zero;
        currentHP = maxHP;
        transform.position = GameManager.Instance.cubeSpawnPos.transform.position;
        GameManager.Instance.holdController.drop();
    }

    public void takeDamage(float amount)
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
    private void OnTriggerEnter(Collider other)
    {
        
    }
    public void ApplyVelocity(Vector3 velocity)
    {
        rb.velocity += velocity;
    }
}
