using UnityEngine;

namespace Beginner2D
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class SpeedBoostPickup : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float boostFactor = 1.6f;
        [SerializeField, Min(0.1f)] private float boostTime = 4f;
        [SerializeField] private AudioClip collectedClip;
        [SerializeField] private ParticleSystem pickUpParticleEffect;
        private bool consumed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (consumed || !isActiveAndEnabled || !other.TryGetComponent(out PlayerController player))
                return;

            consumed = true;
            player.ApplySpeedBoost(boostFactor, boostTime);
            if (collectedClip != null) player.PlaySound(collectedClip);
            if (pickUpParticleEffect != null)
                Instantiate(pickUpParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
