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
    }
}
