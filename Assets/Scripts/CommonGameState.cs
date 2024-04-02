using UnityEngine;

public class CommonGameState : MonoBehaviour
{
    public bool Crashed { get; private set; }

    private ObstaclesController obstacles;

    void Start()
    {
        obstacles = GetComponentInChildren<ObstaclesController>();
    }

    public void Crash()
    {
        Crashed = true;
    }

    public void Bomb()
    {
        if (!Crashed) obstacles.Bomb();
    }
}
