using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels = new List<LevelData>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SaveManager.saveManager != null) Debug.Log("ada");
        for(int i = 0; i < levels.Count; i++)
        {
            RemainingPathData data = SaveManager.saveManager.remainingPathData.pathsData.Find(x => x.Level == "Lv" + (i + 1));
            if(data != null)
            {
                //int remainPath = data.maxPaths - data.PathsRemain; 
                if(data.PathsRemain > 2)
                {
                    levels[i].Stars[0].SetActive(true);
                    levels[i].Stars[1].SetActive(true);
                    levels[i].Stars[2].SetActive(true);
                    levels[i].Lock.SetActive(false);
                }else if(data.PathsRemain <= 2)
                {
                    levels[i].Stars[0].SetActive(true);
                    levels[i].Stars[1].SetActive(true);
                    levels[i].Stars[2].SetActive(false);
                    levels[i].Lock.SetActive(false);
                }else if(data.PathsRemain <= 1)
                {
                    levels[i].Stars[0].SetActive(true);
                    levels[i].Stars[1].SetActive(false);
                    levels[i].Stars[2].SetActive(false);
                    levels[i].Lock.SetActive(false);
                }
            }else
            {
                RemainingPathData LevelBefore = SaveManager.saveManager.remainingPathData.pathsData.Find(x => x.Level == "Lv" + i);
                if(LevelBefore != null || i == 0)
                {
                    levels[i].button.interactable = true;
                    levels[i].Stars[0].SetActive(false);
                    levels[i].Stars[1].SetActive(false);
                    levels[i].Stars[2].SetActive(false);
                    levels[i].Lock.SetActive(false);
                }else
                {
                    levels[i].button.interactable = false;
                    levels[i].Stars[0].SetActive(false);
                    levels[i].Stars[1].SetActive(false);
                    levels[i].Stars[2].SetActive(false);
                    levels[i].Lock.SetActive(true);
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
