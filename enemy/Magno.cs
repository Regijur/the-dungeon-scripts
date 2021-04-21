using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magno : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3[] Positions;
    public float respawnTime;
    private int index;
    private bool run = true;
    
    private void Update()
    {
    
        if (GetComponent<Enemy>().detected && run)
        {
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach(AudioSource audio in audios)
            {
                audio.Stop();
            }
            GetComponent<AudioSource>().Play();
            GameObject[] G = GameObject.FindGameObjectsWithTag("Magno");
            if (G != null)
            {
                foreach(GameObject g in G)
                {
                    Destroy(g);
                }
            }
            GameObject[] E = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject e in E)
            {
                if (e != gameObject) Destroy(e);
            }

            StartCoroutine(CreateMonsters());
        }
        
    }


    IEnumerator CreateMonsters()
    {
        run = false;
        index = Random.Range(0, enemies.Length);
        GameObject tempMonster = enemies[index];
        tempMonster.transform.position = transform.position + CalculePos();
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("hit");
        if (FindObjectsOfType<Enemy>().Length < 3)
        {
            Instantiate(tempMonster);
            FindObjectOfType<UIManager>().SetEnemyHealth();
        }
        yield return new WaitForSeconds(respawnTime);
        StartCoroutine(CreateMonsters()); 
    }

    Vector3 CalculePos()
    {
        int lucky = Random.Range(0, Positions.Length);
        Vector3 Pos = Positions[lucky];
        return Pos;
    }

    IEnumerator Final()
    {
        StopCoroutine(CreateMonsters());
        GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Credits");
    }
}
