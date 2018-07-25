using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject panelMainMenu;
    public GameObject panelCouchPlayerSelection;

    public GameObject buttonFirstMainMenu;
    public GameObject buttonFirstSelectionMenu;
    public EventSystem es;

    public void OnCouchMultiplayerPress()
    {
        //panelMainMenu.SetActive(false);
        //panelCouchPlayerSelection.SetActive(true);
        //es.firstSelectedGameObject = buttonFirstSelectionMenu;
        SceneManager.LoadScene("Stage v0.1");
    }

    public void OnBackPress()
    {
        panelCouchPlayerSelection.SetActive(false);
        panelMainMenu.SetActive(true);
        es.firstSelectedGameObject = buttonFirstMainMenu;
    }
}
