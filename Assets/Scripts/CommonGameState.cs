using UnityEngine;

public class CommonGameState : MonoBehaviour
{
    public GameObject CrashedOverlay;

    public bool Crashed { get; private set; }
    public bool Grazing { get; set; }
    public bool Focused { get; set; }

    public delegate void RestartAction();
    public event RestartAction OnRestart;

    private ObstaclesController obstacles;

    void Start()
    {
        obstacles = GetComponentInChildren<ObstaclesController>();
    }

    public void Crash()
    {
        Crashed = true;
        CrashedOverlay.SetActive(true);
    }

    public void Restart()
    {
        Crashed = false;
        Grazing = false;
        Focused = false;
        obstacles.Restart();
        GetComponent<ScoreTracker>().Restart();
        GetComponent<GameTime>().Restart();
        GetComponent<BombController>().Restart();
        GetComponentInChildren<ShipController>().Restart();
        OnRestart();
        CrashedOverlay.SetActive(false);
    }
}
