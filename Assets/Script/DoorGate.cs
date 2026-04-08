using UnityEngine;

public class DoorGate : MonoBehaviour
{
    public ButtonPressed buttonPressed;
    private float time = 0;
    private float duration = 3f;
    public Vector3 startPos;
    public Vector3 endPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonPressed.isPressed)
        {
            time += Time.deltaTime;
            float t = time / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }
        
    }


}
