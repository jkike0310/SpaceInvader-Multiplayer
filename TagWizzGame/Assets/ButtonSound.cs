using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;
 using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip clipHover;
    [SerializeField] private AudioClip clipClic;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>{
            if(clipClic!=null)
            {
                SoundManager.instance.PlayEffect(clipClic);
            }            
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(clipHover!=null)
        {
            SoundManager.instance.PlayEffect(clipHover);
        }
    }



    
}
