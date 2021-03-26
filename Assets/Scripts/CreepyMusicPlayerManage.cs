using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepyMusicPlayerManage : MonoBehaviour
{
    void Awake()
    {
        GameObject[] musicObjectMenu = GameObject.FindGameObjectsWithTag("CreepyMusicPlayer");

        if (musicObjectMenu.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
