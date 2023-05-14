using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Alcohol/New Alcohol", fileName = "NewAlcoholSO", order = 1)]
public class AlcoholSO : ScriptableObject
{

    [Serializable]
    public enum Alcohol
    {
        Beer,
        Vodka,
        Wine,
        Cider,
        Hooch,
        Whiskey,
        Tequilla,
        Rum
    }

    public Alcohol type;
    [Range(-1f, 1f)][SerializeField]private float alcoholFactor;

    public float AlcoholFactor
    {
        get
        {
            if (type is not Alcohol.Beer) return alcoholFactor;
            
            var random = Random.Range(0, 10);
            return random > 3 ? math.abs(alcoholFactor) : -math.abs(alcoholFactor);
        }
    }
}
