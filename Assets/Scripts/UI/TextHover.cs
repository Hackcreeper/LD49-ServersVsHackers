using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextHover : MonoBehaviour
    {
        public float smoothFactor = 5f;
        public float sizeChange = 1.1f;
        public Color colorChange = new Color(250, 183, 246);
        
        private TextMeshProUGUI _text;
        private Color _targetColor = Color.white;
        private Vector3 _targetSize = Vector3.one;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void OnHover()
        {
            _targetColor = colorChange;
            _targetSize = Vector3.one * sizeChange;
        }

        public void OnLeave()
        {
            _targetColor = Color.white;
            _targetSize = Vector3.one;
        }

        private void Update()
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                _targetSize,
                smoothFactor * Time.unscaledDeltaTime
            );

            _text.color = Color.Lerp(
                _text.color,
                _targetColor,
                smoothFactor * Time.unscaledDeltaTime
            );
        }
    }
}