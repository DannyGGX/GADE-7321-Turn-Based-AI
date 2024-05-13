using System;
using UnityEngine;
using UnityEngine.UI;

namespace DannyG
{
    public class BoardButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private Coordinate _coordinate;

        public void Initialize(Action<Coordinate> buttonCallback)
        {
            button.onClick.AddListener(() => ButtonClicked(buttonCallback));

            SetEnableState(false);
        }

        private void ButtonClicked(Action<Coordinate> buttonCallback)
        {
            buttonCallback.Invoke(_coordinate);
            this.Log("Button Clicked");
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
            button.enabled = show;
        }
		
    }
}