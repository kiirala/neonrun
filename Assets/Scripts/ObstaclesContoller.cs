using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public SingleObstacleController ObstaclePrefab;

    private CommonGameState state;

    private readonly List<SingleObstacleController> activeObstacles = new();
    private readonly List<SingleObstacleController> inactiveObstacles = new();

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponentInParent<CommonGameState>();
        inactiveObstacles.AddRange(GetComponentsInChildren<SingleObstacleController>());
    }

    // Update is called once per frame
    void Update()
    {
        if (state.Crashed) return;

        if (activeObstacles.Count == 0)
        {
            SpawnObstacle(1, 1);
            SpawnObstacle(2, 3);
            SpawnObstacle(3, 2);
            SpawnObstacle(4, 3);
            SpawnObstacle(5, 1);
        }

        activeObstacles.ForEach(o => { o.UpdatePosition(Time.time); });
        int i = 0;
        while (i < activeObstacles.Count)
        {
            if (!activeObstacles[i].IsInView)
            {
                activeObstacles[i].gameObject.SetActive(false);
                inactiveObstacles.Add(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public IEnumerable<SingleObstacleController> GetHitboxNearZero(int lane, float range)
        => activeObstacles.Where(
            o => o.Lane == lane &&
            o.BoardYPosition + o.HitboxRadius >= -range &&
            o.BoardYPosition - o.HitboxRadius <= range);

    public IEnumerable<SingleObstacleController> GetVisibleNearZero(int lane, float range)
        => activeObstacles.Where(
            o => o.Lane == lane &&
            o.BoardYPosition + o.VisibleRadius >= -range &&
            o.BoardYPosition - o.VisibleRadius <= range);

    public void Bomb()
    {
        activeObstacles.ForEach(o => o.gameObject.SetActive(false));
        inactiveObstacles.AddRange(activeObstacles);
        activeObstacles.Clear();
    }

    private void SpawnObstacle(int lane, int height)
    {
        SingleObstacleController obstacle;
        if (inactiveObstacles.Count > 0)
        {
            obstacle = inactiveObstacles[^1];
            inactiveObstacles.RemoveAt(inactiveObstacles.Count - 1);
        }
        else
        {
            obstacle = Instantiate(ObstaclePrefab, transform);
        }
        obstacle.gameObject.SetActive(true);
        obstacle.Initialize(lane, height, Time.time);
        activeObstacles.Add(obstacle);
    }

}
