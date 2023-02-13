using System;
using UnityEditor;
using UnityEngine;
using Gizmos = Code.core.Gizmos;

namespace Code.components
{
    [ExecuteInEditMode]
    public class TileGrid : MonoBehaviour
    {
        [SerializeField] private bool verbose = false;
        //represents a grid with its origin in the center
        public Vector2 origin = new Vector2(0, 0);
        public Vector2 cellSize = new Vector2(1, 1);
        public Vector2 size = new Vector2(10, 10);
        public Color borderColor = Color.white;
        public class Cell
        {
            public Color Color = Color.black;
        }

        Cell[,] _cells;

        private void Update(){
            // if no cells populate
            if (_cells == null){
                populateGrid();
            }
        }

        private void populateGrid(){
            _cells = new Cell[(int) size.x, (int) size.y];
            for (int x = 0; x < size.x; x++){
                for (int y = 0; y < size.y; y++){
                    _cells[x, y] = new Cell();
                }
            }
            // log success
            if (verbose){
                Debug.Log("Grid populated");
            }
        }

        public void OnRenderObject(){
            Gizmos.DrawGrid(origin, cellSize, (int) size.x, (int) size.y, borderColor);
            if (_cells != null){
                for (int x = 0; x < size.x; x++){
                    for (int y = 0; y < size.y; y++){
                        DrawCell(new Vector2(x, y), borderColor, _cells[x, y].Color);
                    }
                }
            }
        }

        /// <summary>
        /// Draw a cell
        /// </summary>
        /// <param name="cell">The cell to draw</param>
        /// <param name="borderColor">The color of the border</param>
        /// <param name="fillColor">The color to fill the cell with</param>
        /// <exception cref="Exception">Cell is not on the grid</exception>
        private void DrawCell(Vector2 cell, Color borderColor, Color fillColor){
            Gizmos.DrawRect(getCellRect(cell), borderColor, fillColor);
            //log the cell
            if (verbose){
                Debug.Log("Cell " + cell + " colored");
            }
        }
        
        /// <summary>
        /// Color a cell
        /// </summary>
        /// <param name="cell">The cell to color</param>
        /// <param name="fillColor">The color to fill the cell with</param>
        /// <exception cref="Exception">Cell is not on the grid</exception>
        public void ColorCell(Vector2 cell, Color fillColor){
            if (cell.x < 0 || cell.x > size.x || cell.y < 0 || cell.y > size.y){
                throw new System.Exception("Cell is not on the grid");
            }
            _cells[(int) cell.x, (int) cell.y].Color = fillColor;
        }
    
        /// <summary>
        /// Check if a position is on the grid
        /// </summary>
        /// <param name="pos">Position to check</param>
        /// <returns>True if the position is on the grid</returns>
        public bool isOnGrid(Vector2 pos){
            return pos.x >= origin.x && pos.x <= origin.x + size.x * cellSize.x &&
                   pos.y >= origin.y && pos.y <= origin.y + size.y * cellSize.y;
        }
        
        /// <summary>
        /// Get the cell that the position is in
        /// </summary>
        /// <param name="pos">Position to check</param>
        /// <returns>The cell that the position is in</returns>
        public Vector2 GetCell(Vector2 pos){
            if (!isOnGrid(pos)){
                throw new System.Exception("Position is not on the grid");
            }
            return new Vector2(
                Mathf.FloorToInt((pos.x - origin.x) / cellSize.x),
                Mathf.FloorToInt((pos.y - origin.y) / cellSize.y)
            );
        }
        
        /// <summary>
        /// Get the rect of a cell
        /// </summary>
        /// <param name="cell">The cell to get the rect of</param>
        /// <returns>The rect of the cell</returns>
        public Rect getCellRect(Vector2 cell){
            if (cell.x < 0 || cell.x > size.x || cell.y < 0 || cell.y > size.y){
                throw new System.Exception("Cell is not on the grid");
            }
            return new Rect(
                origin.x + cell.x * cellSize.x,
                origin.y + cell.y * cellSize.y,
                cellSize.x,
                cellSize.y
            );
        }
    }
    
    // add button for populating grid to editor
    [CustomEditor(typeof(TileGrid))]
    public class GridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TileGrid tileGrid = (TileGrid) target;
            if (GUILayout.Button("Populate Grid"))
            {
                // trough reflexion call private method
                tileGrid.GetType().GetMethod("populateGrid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.Invoke(tileGrid, null);
            }
        }
    }
}
