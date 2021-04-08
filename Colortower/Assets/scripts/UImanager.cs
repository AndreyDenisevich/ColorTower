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
    [SerializeField] private Animator btn_settings;
    [SerializeField] private Animator btn_shop;
    [SerializeField] private Animator btn_no_ads;

    [SerializeField] private Animator panelHelp;
    [SerializeField] private Animator panelWin;
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
        btn_settings.SetBool("isHidden", isHidden);
        btn_no_ads.SetBool("isHidden", isHidden);
        btn_shop.SetBool("isHidden", isHidden);
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
        obj.GetComponent<GameController>().SetGame(8, 7,this);
    }
    public void StartGame5x8()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(8, 5,this);
    }
    public void StartGame3x8()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(8, 3,this);
    }
    public void StartGame3x3()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(3, 3, this);
    }
    public void StartGame4x4()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(4, 4, this);
    }
    public void StartGame5x5()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(5, 5, this);
    }
    public void StartGame6x6()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(6, 6, this);
    }
    public void StartGame7x7()
    {
        GameObject obj = Instantiate(gameController.gameObject) as GameObject;
        obj.transform.position = Vector3.zero;
        Menu.enabled = false;
        InGameUI.enabled = true;
        obj.GetComponent<GameController>().SetGame(7, 7, this);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Win()
    {
        panelWin.enabled = true;
        btnHelp.SetBool("isHidden", true);
        btnGoToMenu.SetBool("isHidden", true);
    }
}
