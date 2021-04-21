using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow : MonoBehaviour
{
    public Transform enemy;
    public Vector3 Distance;
    private void Update()
    {
        if (enemy != null)
        {
            GetComponent<RectTransform>().position = enemy.position + Distance;
            GetComponent<Text>().text = enemy.GetComponent<Enemy>().health.ToString();
        }
        else Destroy(gameObject);
    }
}
