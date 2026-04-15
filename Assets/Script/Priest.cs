using UnityEngine;

public class Priest : MonoBehaviour
{
    private bool GateOpened = false; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        DoorGate.OnGateOpened += HandleGateOpened;
    }

    void OnDisable()
    {
        DoorGate.OnGateOpened -= HandleGateOpened;
    }

    void HandleGateOpened()
    {
        if (!GateOpened)
        {
            // Logic to open the gate
            Debug.Log("Gate opened!");
            GateOpened = true; // Ensure this logic runs only once
        }
    }
}
