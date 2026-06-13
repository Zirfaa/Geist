using System.Collections;
using UnityEngine;

public class DoorUp : MonoBehaviour
{
    public Transform DoorClosed;
    public Vector3 DoorClosedStart;
    public Vector3 DoorClosedTarget;
    private float duration = 3;
    private bool isSkipped = false;
    private float time = 0;
    public AudioClip GateClosed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSkipped)
        {
            time += Time.deltaTime;
            float t = time / duration;
            DoorClosed.transform.position = Vector3.Lerp(DoorClosedStart, DoorClosedTarget, t);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isSkipped = true;
            StartCoroutine(DoorSetObtacle());
        }
    }

    IEnumerator DoorSetObtacle()
    {
        AudioManager.audioManager.PlaySFX(GateClosed);
        yield return new WaitForSeconds(duration);
        GridManager.Instance.SetObstacles();
    }
}
