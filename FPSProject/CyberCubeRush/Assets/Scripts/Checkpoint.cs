using System.Collections;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Renderer Model;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.playerSpawnPos.transform.position != transform.position)
        {
            GameManager.Instance.playerSpawnPos.transform.position = transform.position;
            StartCoroutine(DisplayCheckpointPopup());
            if (GameManager.Instance.holdController.hasCube)
            {
                GameManager.Instance.cubeSpawnPos.transform.position = transform.position;
            }
        }
        else if (other.CompareTag("Cube"))
        {
            GameManager.Instance.cubeSpawnPos.transform.position = transform.position + new Vector3(0, 1, 0);
        }
    }

    IEnumerator DisplayCheckpointPopup()
    {
        GameManager.Instance.checkpointMenu.SetActive(true);
        // model.material.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        // model.material.color = baseColor;
        GameManager.Instance.checkpointMenu.SetActive(false);
    }
}
