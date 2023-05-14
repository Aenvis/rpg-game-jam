using DefaultNamespace;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private AlcoholSO testAlcohol;
    [SerializeField] private Player player;
    [SerializeField] private DynamicInventory inventory;
    [SerializeField] [CanBeNull] private TMP_Text perMileValueTxt;
    [SerializeField] private GameObject deathScreen;
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
        if (gameHasEnded) return;
        _perMileMeter.Add(-factor);
        if(perMileValueTxt is not null) perMileValueTxt.text = _perMileMeter.Value.ToString();

        if (_perMileMeter.Value <= 0 && !gameHasEnded)
        {
            _perMileMeter.Value = 0;
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
        EndGameEvent.Invoke();
        // Position stary in front of the player
        var playerTransform = player.GetComponentInChildren<PlayerController>().gameObject.transform;
        stary.gameObject.transform.position = playerTransform.position + playerTransform.forward * 1.0f;

        // Rotate stary to face the opposite direction of the player
        stary.gameObject.transform.rotation = playerTransform.rotation * Quaternion.Euler(0, 180, 0);

    }

    public void ShowDeathScreen() => deathScreen.SetActive(true);
}
