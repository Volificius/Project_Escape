using UnityEngine;

namespace DefaultNamespace
{
    public class CollisionInteractable : MonoBehaviour
    {
        [SerializeField]
        private GameEvents gameEvents;
    
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                gameEvents.onItemPickedUp.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}