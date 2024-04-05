using UnityEngine;

public class BombController : MonoBehaviour
{
    public int DefaultBombCount;

    public int Bombs { get; private set; }

    public delegate void BombAction();
    public event BombAction OnBombActivated;


    private ObstaclesController obstaclesController;
    private CommonGameState gameState;

    void Start()
    {
        obstaclesController = GetComponentInChildren<ObstaclesController>();
        gameState = GetComponent<CommonGameState>();
        Bombs = DefaultBombCount;
    }

    public void TriggerBomb()
    {
        if (Bombs <= 0 || gameState.Crashed) return;
        Bombs--;
        obstaclesController.Bomb();
        OnBombActivated();
    }

    public void Restart()
    {
        Bombs = DefaultBombCount;
    }
}