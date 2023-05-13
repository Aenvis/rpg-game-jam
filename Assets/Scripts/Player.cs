using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private AlcoholSO alcoholInHand = null;
        
        public float AlcoholFactor => alcoholInHand?.alcoholFactor ?? 0f;

        public bool HasAlcohol => alcoholInHand is not null;

        public void Pickup(AlcoholSO newAlcohol)
        { 
            if (HasAlcohol) return;

            alcoholInHand = newAlcohol;
        }

        public void GiveAlcohol() => alcoholInHand = null;
    }
}