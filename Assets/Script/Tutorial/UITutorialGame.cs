using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITutorialGame : MonoBehaviour
{
    public static UITutorialGame uITutorialGame;
    public GameObject DestroyPathPanel;
    public TextMeshProUGUI PathUnits;
    public static event Func<int> OnPathChanged;
    public GameObject FingerCursorObjt;
    public Animator FingerCursor;
    public Animator HoleHighlight;
    public string[] texts =
    {
        "If you spawn path, path units will decrased by path value",
        "You can rotate the path with the right click",
        "If you destroy the path, path units will increased by value",
        "If you want to back just press R",
        "You have a time to think and find a solution",
        "Enjoy the game"
    };
    public TextMeshProUGUI ExplenationText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(uITutorialGame == null)
        {
            uITutorialGame = this;
        }
    }
    void Start()
    {
        //FingerCursorObjt.SetActive(false);
        DestroyPathPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int pathsValue = OnPathChanged?.Invoke() ?? 0;

        PathUnits.text = "Path Units : " + pathsValue;

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneLoader.instance.ReloadPath();
        }
    }

    void OnEnable()
    {
        TutorialSpawnPath.OnDestroyPanelShow += RemovePathPanel;
        TutorialPathPlacement.OnDestroyPanelHide += RemovePathPanel;
    }

    void OnDisable()
    {
        TutorialSpawnPath.OnDestroyPanelShow -= RemovePathPanel;
        TutorialPathPlacement.OnDestroyPanelHide -= RemovePathPanel;
    }

    public void TutorialDone()
    {
        GameManager.instance.timer = 0;
        PlayerPrefs.SetInt("Tutorial", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Lv1");
    }

    void RemovePathPanel(bool isActive)
    {
        DestroyPathPanel.SetActive(isActive);
    }
}
