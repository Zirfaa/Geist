using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int currentDirection = 0;
    [SerializeField]private int currentStep = 1;
    public int maxStep;
    Vector3Int[] directions = new Vector3Int[]
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 0, -1),
        new Vector3Int(-1, 0, 0),
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CallPatrol());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveNextTile()
    {
        currentStep++;
        if(currentStep >= maxStep)
        {
            currentStep = 1;
            currentDirection++;
            if(currentDirection >= directions.Length)
            {
                currentDirection = 0;
            }
        }
        Vector3Int gridPos = GridManager.Instance.WorldToGrid(transform.position);
        Vector3Int nextGrid = gridPos + directions[currentDirection];
        Vector3 targetPos = GridManager.Instance.GridToWorld(nextGrid);

        while(Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator CallPatrol()
    {
        while(true)
        {
            yield return StartCoroutine(MoveNextTile());
            yield return new WaitForSeconds(1f);
        }
    }
}
