using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMENUManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopAllAudios();
        AudioManager.instance.PlayByName("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
