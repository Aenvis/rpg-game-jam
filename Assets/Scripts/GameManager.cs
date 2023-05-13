using DefaultNamespace;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private AlcoholSO testAlcohol;
    [SerializeField]private Player player;
    [SerializeField] [CanBeNull] private TMP_Text perMileValueTxt;
    [SerializeField] private float startPerMileValue;
    [SerializeField] private float factor;
    
    private PerMileMeter _perMileMeter;

    public bool PlayerCanPickup => player.CanPickup;
    public bool PlayerCanPour => player.HasAlcoholInHand;

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
     _perMileMeter = new PerMileMeter(startPerMileValue);
    }
    
    private void Update()
    {
        _perMileMeter.Add(-factor);
        if(perMileValueTxt is not null) perMileValueTxt.text = _perMileMeter.Value.ToString();
        
        if (_perMileMeter.Value <= 0)
            EndGame();        
    }

    private void SeedPlayerData()
    {
        player.Pickup(testAlcohol);
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
        }
    }
    
    public void PickupAlcohol(Alcohol alcohol) => player.Pickup(alcohol.data);

    public void PourAlcohol()
    {
        if (!player.HasAlcoholInHand) return;

        var perMileValue = player.AlcoholFactor * startPerMileValue; 
        _perMileMeter.Add(perMileValue);
        
        Debug.Log($"Added: {player.AlcoholFactor * startPerMileValue}");
        player.Pour();
    }

    private void EndGame()
    {
        Debug.Log("End game");
    }
}
