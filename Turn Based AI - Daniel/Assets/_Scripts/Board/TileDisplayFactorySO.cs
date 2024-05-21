using System;
using UnityEngine;

namespace DannyG
{
    [CreateAssetMenu(fileName = "Display Tile Factory", menuName = "Scriptable Object/Display Tile Factory", order = 1)]
    public class TileDisplayFactorySO : ScriptableObject
    {
        [SerializeField] private Tile emptyTilePrefab;
        [SerializeField] private Tile prePlacedBlockerPrefab;
        [SerializeField] private Piece player1PiecePrefab;
        [SerializeField] private Piece player2PiecePrefab;
        
        private GameSetupDataSO _gameSetupData;

        public void Init(GameSetupDataSO gameSetupData)
        {
            _gameSetupData = gameSetupData;
        }
        
        public Tile CreateTile(TileType tileType, int x, int y, Transform parent = null)
        {
            Tile tile = tileType switch
            {
                TileType.Empty => InitializeAndProvideTile(emptyTilePrefab, x, y, parent),
                TileType.Blocker => InitializeAndProvideTile(prePlacedBlockerPrefab, x, y, parent),
                TileType.Player1Token => CreatePiece(TileType.Player1Token, x, y, parent),
                TileType.Player2Token => CreatePiece(TileType.Player2Token, x, y, parent),
                _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
            };
            return tile;
        }

        public Piece CreatePiece(TileType tileType, int x, int y, Transform parent = null)
        {
            Piece piece = tileType switch
            {
                TileType.Player1Token => InitializeAndProvideTile(player1PiecePrefab, x, y, parent),
                TileType.Player2Token => InitializeAndProvideTile(player2PiecePrefab, x, y, parent),
                _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
            };
            return piece;
        }
        
        private Tile InitializeAndProvideTile(Tile tile, int x, int y, Transform parent = null)
        {
            Vector3 position = _gameSetupData.tileCenterPositions[x, y];
            tile = Instantiate(tile, position, Quaternion.identity, parent);
            tile.ApplyScale(_gameSetupData.overallScaleModifier);
            return tile;
        }
        private Piece InitializeAndProvideTile(Piece piece, int x, int y, Transform parent = null)
        {
            piece = Instantiate(piece, GetStartingPosition(x, y), Quaternion.identity, parent);
            piece.ApplyScale(_gameSetupData.overallScaleModifier);
            piece.SetCoordinate(x, y);
            return piece;
        }

        private Vector3 GetStartingPosition(int x, int y)
        {
            GravityStates currentGravityState = GravityManager.currentGravityState;
            Vector3[] startingPositions = _gameSetupData.startingPositions[currentGravityState];
            if (currentGravityState is GravityStates.Down or GravityStates.Up)
                return startingPositions[x];
            else
                return startingPositions[y];
        }
        
    }
}