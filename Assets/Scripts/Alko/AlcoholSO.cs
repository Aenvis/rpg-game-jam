using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public enum Alcohol
{
    Beer,
    Vodka
}

[CreateAssetMenu(menuName = "Alcohol", fileName = "NewAlcoholSO", order = 1)]
public class AlcoholSO : ScriptableObject
{
    public Alcohol type;
    
    [Range(-1f, 1f)]public float alcoholFactor;
}
