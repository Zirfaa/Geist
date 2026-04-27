using System;
using UnityEngine;

public class SettingManagers : MonoBehaviour
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