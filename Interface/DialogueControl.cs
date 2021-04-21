using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public GameObject Npc;
    public Image profile;
    public Text speechText;
    public Text actorName;
    public InformationManager informationPanel;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;
    public Animator ani;

    private void Update()
    {
        if (!informationPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StopTalking();
        }
    }
    public void Speech(Sprite imageS, string[] npcText, string npcName, GameObject npc)
    {
        informationPanel.DeleteText();
        Npc = npc;
        dialogueObj.SetActive(true);
        FindObjectOfType<Mechanics>().isPaused = true;
        FindObjectOfType<Mechanics>()._animator.SetBool("running", false);
        profile.sprite = imageS;
        sentences = npcText;
        actorName.text = npcName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                StopTalking();
            }
        }
        else
        {
            StopAllCoroutines();
            speechText.text = "";
            speechText.text = sentences[index];
        }
    }

    void StopTalking()
    {
        speechText.text = "";
        index = 0;
        dialogueObj.SetActive(false);
        FindObjectOfType<Mechanics>().isPaused = false;
        if (Npc != null)
        {
            if (Npc.CompareTag("Magno"))
            {
                Npc.GetComponent<Animator>().SetTrigger("magic");
            }
            if (Npc.CompareTag("Princess"))
            {
                SceneManager.LoadScene("Final");
            }
        }

    }

    
}
