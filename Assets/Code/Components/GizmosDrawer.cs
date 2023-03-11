using UnityEngine;
using UnityEngine.TestTools;
using Gizmos = Code.Core.gizmos.Gizmos;

namespace Code.Components
{
    [ExecuteAlways]
    // HAS TO BE MANUALLY TESTED!!!
    [ExcludeFromCoverage]
    public class GizmosDrawer : MonoBehaviour
    {
        public Color cellBorderColor = Color.gray;
        public float cellBorderThickness = 0.05f;

        public Color gridColor = new(88, 88, 88);
        public Vector2Int gridMargin = Vector2Int.one;

        public Vector2Int CellPosition{ private get; set; }

        private Camera _mainCam;
        private bool _mainCamNotNull;

        private void Start(){
            _mainCam = Camera.main;
            _mainCamNotNull = _mainCam != null;
        }

        // playmode gizmos
        private void OnRenderObject(){
            if (_mainCamNotNull){
                // draw grid
                Vector2Int gridDimensions = (Vector2Int) TileBrush.Instance.Tilemap.size + gridMargin * 2;
                Vector3Int gridOriginCell = TileBrush.Instance.Tilemap.origin - (Vector3Int) gridMargin;

                Grid grid = TileBrush.Instance.Tilemap.layoutGrid;
                Vector2 cellSize = grid.cellSize;
                Vector2 gridOrigin = grid.CellToWorld(gridOriginCell);

                Gizmos.DrawGrid(gridOrigin, cellSize, gridDimensions.x, gridDimensions.y, gridColor);


                // draw tile rect
                Rect tileRect = TileBrush.Instance.GetTileRect(CellPosition);
                Gizmos.DrawRect(tileRect, cellBorderThickness,  cellBorderColor, Color.clear);
            }
        }
    }
}
