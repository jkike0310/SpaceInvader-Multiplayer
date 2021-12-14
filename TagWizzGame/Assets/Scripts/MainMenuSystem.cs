using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject creditsPanel;

    public void HidePanel()
    {
        creditsPanel.SetActive(false);
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
    }
}
