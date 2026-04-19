using System;
using System.Collections;
using UnityEngine;

public class DoorGate : MonoBehaviour
{
    public ButtonPressed buttonPressed;
    public enum DoorType {Button, Timer};
    public DoorType doorType;
    private float time = 0;
    private float duration = 3f;
    public Vector3 startPos;
    public Vector3 endPos;
    public static event Action OnGateOpened;
    public bool isGateOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonPressed != null && doorType == DoorType.Button)
        {
            if(buttonPressed.isPressed)
            {
                isGateOpen = true;
            }
        }else if(doorType == DoorType.Timer && GameManager.instance.timer >= 3f && !isGateOpen)
        {
            isGateOpen = true;
            StartCoroutine(canInvoke());
            
        }

        if(isGateOpen)
        {
            time += Time.deltaTime;
            float t = time / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }
        
    }

    IEnumerator canInvoke()
    {
        //Debug.Log("P");
        yield return new WaitForSeconds(12f);
        if(isGateOpen)
        {
            //Debug.Log("Gate opened!");
            //isGateOpen = false;
            GridManager.Instance.SetObstacles();
            OnGateOpened?.Invoke();
        }
    }


}
