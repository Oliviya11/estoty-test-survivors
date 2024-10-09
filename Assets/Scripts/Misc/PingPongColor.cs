using UnityEngine;

namespace Misc
{
    public class PingPongColor : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Color startColor = Color.white;  // The initial color
        [SerializeField] Color targetColor = Color.red;   // The color to transition to
        [SerializeField] float pingPongDuration = 0.5f; // Duration of one complete ping-pong cycle
        [SerializeField] float duration = 1f; // Duration of effect
        private float elapsedTime = Mathf.Infinity; // Track how long the effect has been playing
        private bool isPlaying = false; // Flag to control the effect
        
        private void Update()
        {
            if (isPlaying)
            {
                // Update the elapsed time
                elapsedTime += Time.deltaTime;

                // Check if the effect duration has been exceeded
                if (elapsedTime > duration)
                {
                    isPlaying = false; // Stop the effect
                    spriteRenderer.color = startColor; // Optionally reset color
                    return; // Exit the Update method
                }
                
                // PingPong returns a value that oscillates between 0 and 1 over time
                float t = Mathf.PingPong(Time.time / pingPongDuration, 1.0f);

                // Interpolate between the startColor and targetColor based on t
                spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
            }
        }

        public void Launch()
        {
            if (isPlaying) return;
            
            elapsedTime = 0;
            isPlaying = true;
        }
    }
}