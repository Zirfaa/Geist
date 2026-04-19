using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public Transform Player;
    private PathFinding pathFinding;
    private bool GateOpened = false; 
    Vector3Int[] directions = new Vector3Int[]
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1)
    };
    private float speed = 5f;
    public static event Action OnGameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathFinding = Player.GetComponent<PathFinding>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //Debug.Log("Priest enabled, subscribing to gate opened event.");
        DoorGate.OnGateOpened += HandleGateOpened;
    }

    void OnDisable()
    {
        DoorGate.OnGateOpened -= HandleGateOpened;
    }

    void HandleGateOpened()
    {
        if (!GateOpened && !pathFinding.getTarget)
        {
            // Logic to open the gate
            Debug.Log("Gate opened!");
            StartCoroutine(PathSearch());
            GateOpened = true; // Ensure this logic runs only once
        }
    }

    IEnumerator PathSearch()
    {
        Vector3Int start = GridManager.Instance.WorldToGrid(transform.position);
        Vector3Int targetPos = GridManager.Instance.WorldToGrid(Player.position);

        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> comeFrom = new Dictionary<Vector3Int, Vector3Int>();

        frontier.Enqueue(start);
        comeFrom[start] = start;

        while(frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            if(current == targetPos) break;
            foreach(var dir in directions)
            {
                Vector3Int next = current + dir;
                if(!GridManager.Instance.grid.ContainsKey(next)) continue;
                if(GridManager.Instance.grid[next] == GridManager.GridType.Empety || GridManager.Instance.grid[next] == GridManager.GridType.Obstacle) continue;
                if(comeFrom.ContainsKey(next)) continue;
                frontier.Enqueue(next);
                comeFrom[next] = current;
            }

        }

        if(!comeFrom.ContainsKey(targetPos))
        {
            Debug.Log("Prist tdk menemukan player");
            yield break;
        }

        List<Vector3Int> paths = new List<Vector3Int>();
        Vector3Int temp = targetPos;
        while(temp != start)
        {
            paths.Add(temp);
            temp = comeFrom[temp];
        }
        paths.Reverse();
        foreach(var step in paths)
        {
            Vector3 target = GridManager.Instance.GridToWorld(step);
            target.y -= 0.5f;
            while(Vector3.Distance(transform.position, target) > 0.05f)
            {
                Vector3 direction = target - transform.position;
                if(direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
        }
        OnGameOver?.Invoke();
    }
}
