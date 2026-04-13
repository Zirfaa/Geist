using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform buttonPos;
    public Transform gravePos;
    [SerializeField] private Transform targetGoal; 
    public float speed = 3f;
    public enum TargetType {Button, Grave};
    public TargetType targetType = TargetType.Button;
    public Coroutine PathCoroutine;
    //private Queue<Vector3Int> pathFind = new Queue<Vector3Int>();
    //private List<Vector3Int> path = new List<Vector3Int>();
    Vector3Int[] directions = new Vector3Int[]
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1)
    };

    void Start()
    {
        //StartCoroutine(PathSearch());
    }
    void Update()
    {
        
    }

    void OnEnable()
    {
        PathPlacement.OnPlayerSearch += CallPathSearch;
        ButtonPressed.OnPlayerSearch += CallPathSearch;
    }

    void OnDisable()
    {
        PathPlacement.OnPlayerSearch -= CallPathSearch;
        ButtonPressed.OnPlayerSearch -= CallPathSearch;
    }

    public void CallPathSearch()
    {
        if(targetType == TargetType.Button)
        {
            targetGoal = buttonPos;
        }else
        {
            targetGoal = gravePos;
        }

        if(PathCoroutine != null)
        {
            StopCoroutine(PathCoroutine);
        }
        PathCoroutine = StartCoroutine(PathSearch(targetGoal));
    }

    IEnumerator PathSearch(Transform targetGoal)
    {
        if(targetType == TargetType.Grave)
        {
            yield return new WaitForSeconds(3.5f);
            //GridManager.Instance.SetObstacles();
        }
        Vector3Int start = GridManager.Instance.WorldToGrid(transform.position);
        Vector3Int targetGrid = GridManager.Instance.WorldToGrid(targetGoal.position);

        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        frontier.Enqueue(start);
        cameFrom[start] = start;

        while(frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            if(current == targetGrid) break;
            foreach(var dir in directions)
            {
                Vector3Int next = current + dir;

                if(!GridManager.Instance.grid.ContainsKey(next)) continue;
                if(GridManager.Instance.grid[next] == GridManager.GridType.Empety || GridManager.Instance.grid[next] == GridManager.GridType.Obstacle) continue;
                if(cameFrom.ContainsKey(next)) continue;
                frontier.Enqueue(next);
                cameFrom[next] = current;
            } 
        }

        if(!cameFrom.ContainsKey(targetGrid))
        {
            Debug.Log("Jalur tdk ada");
            yield break;
        }

        List<Vector3Int> paths = new List<Vector3Int>();
        Vector3Int temp = targetGrid;
        while(temp != start)
        {
            paths.Add(temp);
            temp = cameFrom[temp];
        }

        paths.Reverse();

        foreach(var step in paths)
        {
            Vector3 targetPos = GridManager.Instance.GridToWorld(step);
            targetPos.y -= 0.5f;
            while(Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                Vector3 direction = targetPos - transform.position;
                if(direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }
                
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    
}