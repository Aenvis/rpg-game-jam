using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private AlcoholSO alcoholInHand = null;
        
        public float AlcoholFactor => alcoholInHand?.alcoholFactor ?? 0f;

        public bool HasAlcoholInHand => alcoholInHand is not null;
        public bool CanPickup => !HasAlcoholInHand;

        public void Pickup(AlcoholSO newAlcohol)
        { 
            if (HasAlcoholInHand) return;

            alcoholInHand = newAlcohol;
        }

        public void Pour() => alcoholInHand = null;
    }
}