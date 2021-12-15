using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySoundData
{
    
    public string id;
    public AudioClip clip;
}

public class EnemySound : MonoBehaviour
{
    public static EnemySound instance;    
    [SerializeField] private List<EnemySoundData> soundClipsList = new List<EnemySoundData>();

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string idClip)
    {
        if(soundClipsList.Exists(s => s.id == idClip))
        {
            SoundManager.instance.PlayEffect(soundClipsList.Find(sound => sound.id == idClip).clip);
        }
        
    }

}
