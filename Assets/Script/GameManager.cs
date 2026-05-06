using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public float timer = 0f;
     void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        UIInGame.OnTimerChange += TimerChange;
    }

    void OnDisable()
    {
        UIInGame.OnTimerChange -= TimerChange;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    int TimerChange()
    {
        return Mathf.CeilToInt(30 - timer);
    }
}
