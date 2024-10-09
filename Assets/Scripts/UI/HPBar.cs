using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] Image ImageCurrent;

        public void SetValue(float current, float max)
        {
            ImageCurrent.fillAmount = current / max;
        }
    }
}