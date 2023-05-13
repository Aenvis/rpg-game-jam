using System;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Alcohol", fileName = "NewAlcoholSO", order = 1)]
public class AlcoholSO : ScriptableObject
{

    [Serializable]
    public enum Alcohol
    {
        Beer,
        Vodka
    }

    public Alcohol type;
    [Range(-1f, 1f)]public float alcoholFactor;
}
