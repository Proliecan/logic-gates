using UnityEngine;
using Gizmos = Code.core.Gizmos;

namespace Code.components
{
    [ExecuteInEditMode]
    public class Grid : MonoBehaviour
    {
        //represents a grid with its origin in the center
        public Vector2 origin = new Vector2(0, 0);
        public Vector2 cellSize = new Vector2(1, 1);
        public Vector2 size = new Vector2(10, 10);
        public Color color = Color.white;

        public void OnRenderObject(){
            Gizmos.drawGrid(origin, cellSize, (int) size.x, (int) size.y, color);
        }
    
        public bool isOnGrid(Vector2 pos){
            return pos.x >= origin.x && pos.x <= origin.x + size.x * cellSize.x &&
                   pos.y >= origin.y && pos.y <= origin.y + size.y * cellSize.y;
        }
        
        public Vector2 getCell(Vector2 pos){
            if (!isOnGrid(pos)){
                throw new System.Exception("Position is not on the grid");
            }
            return new Vector2(
                Mathf.FloorToInt((pos.x - origin.x) / cellSize.x),
                Mathf.FloorToInt((pos.y - origin.y) / cellSize.y)
            );
        }
        
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
}
