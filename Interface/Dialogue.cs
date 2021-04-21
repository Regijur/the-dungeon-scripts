using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;
    public string information;

    public float radius;
    public LayerMask playerLayer;
    private bool onRadius;

    private DialogueControl dialogueContr;
    private void Start()
    {
        dialogueContr = FindObjectOfType<DialogueControl>();
    }

    private void Update()
    {
        if (this.enabled)
        {
            if (!dialogueContr.dialogueObj.activeInHierarchy & Input.GetKeyDown(KeyCode.E) && onRadius)
            {
                dialogueContr.Speech(profile, speechText, actorName, gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Interect();
    }
    public void Interect()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
        {
            if(!onRadius) dialogueContr.informationPanel.GiveInformation(information);
            onRadius = true;
        }
        else
        {
            if (onRadius) dialogueContr.informationPanel.DeleteText();
            onRadius = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void DestroyIt()
    {
        Destroy(gameObject);
    }
}
