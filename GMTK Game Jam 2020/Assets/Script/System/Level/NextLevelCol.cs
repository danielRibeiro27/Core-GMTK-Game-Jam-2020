using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class NextLevelCol : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;
    [SerializeField] private string colName;
    private Level02Manager managerLv02;
    private Level01Manager managerLv01;

    private void Start()
    {
        managerLv01 = GetComponentInParent<Level01Manager>();
        managerLv02 = GetComponentInParent<Level02Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (managerLv01 != null)
                managerLv01.NextLevelCol(sceneIndex, collision, colName);
            if (managerLv02 != null)
                managerLv02.NextLevelCol(sceneIndex, collision, colName);
        }

    }
}
