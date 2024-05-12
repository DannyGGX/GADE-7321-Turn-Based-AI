using System.Collections;
using System.Collections.Generic;
using DannyG;
using UnityEngine;

/// <summary>
/// Used for storing events in one place.
/// Source: https://youtu.be/RPhTEJw6KbI?si=-_n_Dyt1jjGXp6BY
/// Pro: This event system is less resource intensive than a scriptable object event system that uses UnityEvents.
/// Con: not as good for team collaboration, because of potential conflicts from working in this class.
///
/// Tip: use { get; } to expose where the event is used
/// </summary>
public static class EventManager
{
    public static Event<bool> onGamePaused { get; } = new (); // bool: true if game is paused
    
    public static Event<PlayerId> onTurnStart { get; } = new ();
    public static Event<ValidMovesData> onDeterminedValidMoves { get; } = new ();
    public static Event<MoveData> onPlacePiece { get; } = new ();
    public static Event onBoardDisplayFinishedUpdating { get; } = new ();
    
    public static Event onGravityShift { get; } = new ();
    public static Event<ShiftedTilesData> onApplyGravityShiftToDisplay { get; } = new ();
    
    public static Event onDrawGame { get; } = new ();
    public static Event<PlayerId> onPlayerWin { get; } = new ();
}
