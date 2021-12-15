using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    void Start()
    {
        levelText.text = "Level " + LevelManager.instance.GetLevel().ToString();
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
   
}
