using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour
{
    public GameObject[] Stars;
    public GameObject Lock;
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
