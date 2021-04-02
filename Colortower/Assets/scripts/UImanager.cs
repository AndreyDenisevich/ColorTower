using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameController gameController;

    [SerializeField] private Canvas Menu;
    [SerializeField] private Canvas InGameUI;

    [SerializeField] private Animator panelModes;
    [SerializeField] private Animator btn_start;

    [SerializeField] private Animator panelHelp;
    [SerializeField] private Animator btnHelp;
    [SerializeField] private Animator btnGoToMenu;
    void Start()
    {
        InGameUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TogglePanel()
    {
        panelModes.enabled = true;
        bool isHidden = panelModes.GetBool("isHidden");
        panelModes.SetBool("isHidden", !isHidden);
        btn_start.SetBool("isHidden", isHidden);
    }
    public void TogglePanelHelp()
    {
        panelHelp.enabled = true;
        bool isHidden = panelHelp.GetBool("isHidden");
        panelHelp.SetBool("isHidden", !isHidden);
        btnHelp.SetBool("isHidden", isHidden);
        btnGoToMenu.SetBool("isHidden", isHidden);
    }
    public void StartGame7x8()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
