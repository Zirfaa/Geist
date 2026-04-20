using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject settingPanel;

    public void OpenSetting()
    {
        
        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
}