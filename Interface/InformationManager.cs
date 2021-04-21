using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationManager : MonoBehaviour
{
    private readonly float messageTime = 2.0f;
    public Text informationText;
    public void GiveInformation(string information)
    {
        information = information.ToUpper();
        informationText.text = information;
        gameObject.SetActive(true);
    }
    public void DeleteText()
    {
        informationText.text = "";
        gameObject.SetActive(false);
    }
    
    public void TakeInformation(string message)
    {
        GiveInformation(message);
        StartCoroutine(ShowMessage());
    }
    IEnumerator ShowMessage()
    {
        yield return new WaitForSecondsRealtime(messageTime);
        DeleteText();
    }

}
