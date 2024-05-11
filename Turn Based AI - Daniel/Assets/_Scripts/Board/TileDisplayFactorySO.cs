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
            Tile tile;
            switch (tileType)
            {
                case TileType.Empty:
                    tile = InitializeAndProvideTile(emptyTilePrefab, x, y, parent);
                    break;
                case TileType.Blocker:
                    tile = InitializeAndProvideTile(prePlacedBlockerPrefab, x, y, parent);
                    break;
                case TileType.Player1Token:
                    tile = CreatePiece(TileType.Player1Token, x, y, parent);
                    break;
                case TileType.Player2Token:
                    tile = CreatePiece(TileType.Player2Token, x, y, parent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
            }
            return tile;
        }

        public Piece CreatePiece(TileType tileType, int x, int y, Transform parent = null)
        {
            Piece piece;
            switch (tileType)
            {
                case TileType.Player1Token:
                    piece = InitializeAndProvideTile(player1PiecePrefab, x, y, parent);
                    break;
                case TileType.Player2Token:
                    piece = InitializeAndProvideTile(player2PiecePrefab, x, y, parent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
            }
            piece.SetCoordinate(x, y);
            return piece;
        }
        
        private Tile InitializeAndProvideTile(Tile tile, int x, int y, Transform parent = null)
        {
            Vector3 position = _gameSetupData.tileCenterPositions[x, y];
            tile = Instantiate(tile, position, Quaternion.identity, parent);
            tile.Place(position, position, _gameSetupData.overallScaleModifier);
            return tile;
        }
        private Piece InitializeAndProvideTile(Piece piece, int x, int y, Transform parent = null)
        {
            Vector3 targetPosition = _gameSetupData.tileCenterPositions[x, y];
            piece = Instantiate(piece, targetPosition, Quaternion.identity, parent);
            piece.Place(GetStartingPosition(x, y), targetPosition, _gameSetupData.overallScaleModifier);
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