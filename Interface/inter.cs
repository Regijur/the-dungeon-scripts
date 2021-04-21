using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inter : MonoBehaviour
{
    public List<Weapon> weapons;
    public GameObject listWeaponPrefab;
    public GameObject content;
    private void Start()
    {
        SetWeaponList();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Active(GameObject changeState)
    {
        if (changeState.activeInHierarchy) changeState.SetActive(false);
        else changeState.SetActive(true);
        
    }
   void SetWeaponList()
    {
        foreach(Weapon weapon in weapons)
        {
            GameObject tempObj = listWeaponPrefab;
            tempObj.GetComponent<SetWeaponImage>().weapon = weapon;
            Instantiate(tempObj, content.transform);
        }
    }
}
