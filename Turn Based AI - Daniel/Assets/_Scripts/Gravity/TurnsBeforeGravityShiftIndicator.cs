using System;
using TMPro;
using UnityEngine;

namespace DannyG
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TurnsBeforeGravityShiftIndicator : MonoBehaviour
    {
        private TextMeshProUGUI _Text;
        private int _turnsAmountBeforeGravityShift;
        private int _currentTurnCountBeforeGravityShift;
        private void OnEnable()
        {
            EventManager.onTurnStart.Subscribe(UpdateTurnsCountdown);
            EventManager.onGravityShift.Subscribe(GravityShift);
            _turnsAmountBeforeGravityShift = TurnManager.Instance.TurnsBeforeGravityShift;
            _currentTurnCountBeforeGravityShift = _turnsAmountBeforeGravityShift;
            _Text = GetComponent<TextMeshProUGUI>();
        }
        private void OnDisable()
        {
            EventManager.onTurnStart.Unsubscribe(UpdateTurnsCountdown);
            EventManager.onGravityShift.Unsubscribe(GravityShift);
        }

        private void UpdateTurnsCountdown(PlayerId playerId)
        {
            if (_currentTurnCountBeforeGravityShift > 1)
            {
                _Text.text = $"{_currentTurnCountBeforeGravityShift} turns before Gravity Shift";
            }
            else
            {
                _Text.text = "Last turn before Gravity Shift!";
            }
            _currentTurnCountBeforeGravityShift--;
        }
        private void GravityShift()
        {
            //_Text.text = "Gravity shifting...";
            _currentTurnCountBeforeGravityShift = _turnsAmountBeforeGravityShift;
        }
    }
}