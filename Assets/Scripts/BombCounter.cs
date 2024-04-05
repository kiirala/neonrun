using UnityEngine;

public class BombCounter : MonoBehaviour
{
    public Sprite AvailableBomb;
    public Sprite UsedBomb;

    private SpriteRenderer[] icons;
    private BombController controller;
    private CommonGameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        icons = GetComponentsInChildren<SpriteRenderer>();
        controller = GetComponentInParent<BombController>();
        gameState = GetComponentInParent<CommonGameState>();
        UpdateBombCounter();
    }

    void OnEnable()
    {
        GetComponentInParent<BombController>().OnBombActivated += UpdateBombCounter;
        GetComponentInParent<CommonGameState>().OnRestart += UpdateBombCounter;
    }

    void OnDisable()
    {
        controller.OnBombActivated -= UpdateBombCounter;
        gameState.OnRestart -= UpdateBombCounter;
    }

    void UpdateBombCounter()
    {
        for (int i = 0; i < icons.Length; ++i)
        {
            if (i + 1 <= controller.Bombs)
            {
                icons[i].enabled = true;
                icons[i].sprite = AvailableBomb;
            }
            else
            {
                if (i < controller.DefaultBombCount) icons[i].sprite = UsedBomb;
                else icons[i].enabled = false;
            }
        }
    }
}
