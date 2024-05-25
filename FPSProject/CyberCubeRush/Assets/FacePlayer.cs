using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacePlayer : MonoBehaviour
{
    [SerializeField] float alpha = .8f;
    [SerializeField] Image background;
    [SerializeField] Image border;
    [SerializeField] TMP_Text text;

    private GameObject player;
    private bool insideRange = false;
    private bool fade = false;
    private void Start()
    {
        player = GameManager.Instance.player;
        background.CrossFadeAlpha(0, 0, true);
        border.CrossFadeAlpha(0, 0, true);
        text.CrossFadeAlpha(0, 0, true);
    }

    void Update()
    {
        Vector3 dir = transform.position - player.transform.position;
        transform.forward = dir;

        if (dir.magnitude < 3f)        
            insideRange = true;        
        else 
            insideRange = false;

        if (fade != insideRange && insideRange)
        {
            StartCoroutine(Fade(0, .1f));
            fade = insideRange;
        }
        else if (fade != insideRange && !insideRange)
        {
            StartCoroutine(Fade(alpha, .1f));
            fade = insideRange;
        }
    }

    IEnumerator Fade(float percent, float speed)
    {
        background.CrossFadeAlpha(percent, speed, true);
        border.CrossFadeAlpha(percent, speed, true);
        text.CrossFadeAlpha(percent, speed, true);
        yield return new WaitForSeconds(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Fade(alpha, .5f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Fade(0f, .5f));
        }
    }
}
