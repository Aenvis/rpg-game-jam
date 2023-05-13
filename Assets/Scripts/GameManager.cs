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

    private PerMileMeter _perMileMeter;
    
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
     _perMileMeter = new PerMileMeter(startPerMileValue);
    }
    
    private void Update()
    {
        _perMileMeter.Add(-factor);
        
        if (_perMileMeter.Value <= 0)
            EndGame();
        
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

        var perMileValue = player.AlcoholFactor * startPerMileValue; 
        _perMileMeter.Add(perMileValue);
        
        Debug.Log($"Added: {player.AlcoholFactor * startPerMileValue}");
    }

    private void EndGame()
    {
        Debug.Log("End game");
    }
}
