using DefaultNamespace;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private AlcoholSO testAlcohol;
    [SerializeField] private Player player;
    [SerializeField] private DynamicInventory inventory;
    [SerializeField] [CanBeNull] private TMP_Text perMileValueTxt;
    [SerializeField] private float startPerMileValue;
    [SerializeField] private float factor;
    [SerializeField] private StaryController stary;
    
    private PerMileMeter _perMileMeter;
    private bool gameHasEnded = false;
    private ItemData lastAdded = null;
    public UnityEvent EndGameEvent;

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

        if (_perMileMeter.Value <= 0 && !gameHasEnded)
        {
            gameHasEnded = true;
            EndGame();
        }
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

    public void PickupAlcohol(ItemData item)
    {
        player.Pickup(item.alcoholData);
        lastAdded = item;
        inventory.AddItem(item);
    }

    public void PourAlcohol()
    {
        if (!player.HasAlcoholInHand) return;

        var perMileValue = player.AlcoholFactor * startPerMileValue; 
        _perMileMeter.Add(perMileValue);
        
        Debug.Log($"Added: {player.AlcoholFactor * startPerMileValue}");
        player.Pour();
        if(lastAdded != null) inventory.DeleteItem(lastAdded);

    }

    private void EndGame()
    {

        // Position stary in front of the player
        stary.gameObject.transform.position = player.transform.position + player.transform.forward * 1.0f;

        // Rotate stary to face the opposite direction of the player
        stary.gameObject.transform.rotation = player.transform.rotation * Quaternion.Euler(0, 180, 0);

        EndGameEvent.Invoke();
    }
}
