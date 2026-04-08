using System;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    private float time = 0;
    private float duration = 2f;
    public bool isPressed = false;
    public static event Action OnPlayerSearch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            time += Time.deltaTime;
            float t = time / duration;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPressed = true;
            PathFinding pathFinding = other.GetComponent<PathFinding>();
            pathFinding.targetType = PathFinding.TargetType.Grave;
            OnPlayerSearch?.Invoke();
        }
    }
}
