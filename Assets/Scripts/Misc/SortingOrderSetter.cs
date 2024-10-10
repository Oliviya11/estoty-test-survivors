using UnityEngine;

namespace Misc
{
    public class SortingOrderSetter : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] SpriteRenderer underSpriteRenderer;

        void Update()
        {
            // Set the sorting order based on the y-coordinate of the object
            int order = -(int)(transform.position.y * 10);
            spriteRenderer.sortingOrder = order;
            underSpriteRenderer.sortingOrder = order - 1;
        }
    }
}