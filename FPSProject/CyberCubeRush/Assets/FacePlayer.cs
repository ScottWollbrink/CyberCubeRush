using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        transform.forward = player.transform.position - transform.position;
    }
}
