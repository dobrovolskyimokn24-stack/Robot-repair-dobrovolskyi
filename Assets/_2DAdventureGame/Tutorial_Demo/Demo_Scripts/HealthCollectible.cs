using UnityEngine;


namespace Beginner2D
{
    /// <summary>
    /// Will handle giving health to the character when they enter the trigger.
    /// </summary>
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField, Min(1)] private int healing = 1;
        public AudioClip collectedClip;
        public ParticleSystem pickUpParticleEffect;
        void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController controller = other.GetComponent<PlayerController>();

            if (controller != null && controller.health< controller.maxHealth)
            {
                Instantiate(pickUpParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                controller.PlaySound(collectedClip);
                controller.ChangeHealth(healing);
                Destroy(gameObject);
            }
        }
    }

}
