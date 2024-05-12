using System;
using UnityEngine;
using UnityEngine.UI;

namespace DannyG
{
    public class BoardButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private Coordinate _coordinate;

        public void Initialize(Action<Coordinate> action)
        {
            button.onClick.AddListener(() => action(_coordinate));

            SetEnableState(false);
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