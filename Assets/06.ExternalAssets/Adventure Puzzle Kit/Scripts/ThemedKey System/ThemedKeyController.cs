using UnityEngine;
using AdventurePuzzleKit;

namespace ThemedKeySystem
{
    public class ThemedKeyController : MonoBehaviour
    {
        [Header("Type of Key")]
        [SerializeField] private Key keyType = Key.None;

        [Header("Key Pickup Sound")]
        [SerializeField] private string keySound = "ThemedKeyPickup";

        public enum Key { None, UnderStore, Store, Cafe, Drug }

        public void KeyPickup()
        {
            switch (keyType)
            {
                case Key.UnderStore:
                    DataManager.instance.nowPlayer.underStoreKey = true;
                    break;
                case Key.Store:
                    DataManager.instance.nowPlayer.storeKey = true;
                    break;
                case Key.Cafe:
                    DataManager.instance.nowPlayer.cafeKey = true;
                    break;
                case Key.Drug:
                    DataManager.instance.nowPlayer.drugKey = true;
                    break;
            }
            PickupSound();
            gameObject.SetActive(false);
        }

        void PickupSound()
        {
            AKAudioManager.instance.Play(keySound);
        }
    }
}
