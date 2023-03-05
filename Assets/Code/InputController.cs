using Code.components;
using UnityEngine;

namespace Code
{
    public class InputController : MonoBehaviour
    {
        private Camera _camera;
        private TileGrid _tileGrid;

        private void Start(){
            _tileGrid = FindObjectOfType<TileGrid>();
            _camera = Camera.main;
        }

        void Update(){
            if (_camera != null && _tileGrid != null){
                Vector3 mousePos = Input.mousePosition;
                Vector2 worldPos = _camera.ScreenToWorldPoint(mousePos);
                // if any mouse button is pressed
                try{
                    ColorCell(worldPos);
                }
                catch {
                    // ignore
                }
            }
        }

        Vector2 _lastCell = new Vector2(-1, -1);
        void ColorCell(Vector2 worldPos) {
            // when hovering over a cell that did not just get colored, color it red with the left mouse button or green with the right mouse button
            Vector2 cell = _tileGrid.GetCell(worldPos);
            bool leftMouseButton = Input.GetMouseButton(0);
            bool rightMouseButton = Input.GetMouseButton(1);
            if (cell != _lastCell && (leftMouseButton || rightMouseButton)){
                _lastCell = cell;
                _tileGrid.ColorCell(cell, leftMouseButton ? Color.red : Color.green);
            }
            
            // reset last cell when mouse is released
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                _lastCell = new Vector2(-1, -1);
        }
    }
}
