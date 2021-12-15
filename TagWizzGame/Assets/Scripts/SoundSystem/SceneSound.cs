using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSound : MonoBehaviour
{    
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        if(clip !=null)
        {
            SoundManager.instance.PlayMusic(clip);
        }        
    }
}
