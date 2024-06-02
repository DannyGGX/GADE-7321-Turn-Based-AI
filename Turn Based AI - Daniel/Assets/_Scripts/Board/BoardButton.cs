using System;
using UnityEngine;
using UnityEngine.UI;

namespace DannyG
{
    public class BoardButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        private Color _defaultColor;
        private Color _hiddenColor;

        private Coordinate _coordinate;

        public void Initialize(Action<Coordinate> buttonCallback)
        {
            button.onClick.AddListener(() => ButtonClicked(buttonCallback));
            _defaultColor = new Color(255, 255, 255, 255);
            _hiddenColor = new Color(0, 0, 0, 0);
        }

        private void ButtonClicked(Action<Coordinate> buttonCallback)
        {
            buttonCallback.Invoke(_coordinate);
        }
        
        
        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetCoordinate(Coordinate coordinate)
        {
            this._coordinate = coordinate;
        }
        
        public void SetEnableState(bool show)
        {
            // is this more performant than setting enable state of image and button or gameObject.SetActive?
            image.raycastTarget = show;
            image.color = show ? _defaultColor : _hiddenColor;
        }
		
    }
}