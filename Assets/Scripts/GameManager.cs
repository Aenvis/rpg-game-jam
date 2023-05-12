using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]private Player player;
    [SerializeField] private float startPerMileValue;
    [SerializeField] private float factor;

    [SerializeField] private AlcoholSO testAlcohol;

    private float _perMileMeter;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
     SeedPlayerData();
     _perMileMeter = startPerMileValue;
    }
    
    private void Update()
    {
        _perMileMeter -= factor;
        if (_perMileMeter <= 0)
            EndGame();
        
        Debug.Log(_perMileMeter / 100f);

        Input();
    }

    private void SeedPlayerData()
    {
        player.Pickup(testAlcohol);
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
            GiveAlcohol();
        }
    }
    
    private void GiveAlcohol()
    {
        if (!player.HasAlcohol) return;

        _perMileMeter += player.AlcoholFactor * startPerMileValue;
        Debug.Log($"Added: {player.AlcoholFactor * startPerMileValue}");
    }

    private void EndGame()
    {
        Debug.Log("End game");
    }
}
