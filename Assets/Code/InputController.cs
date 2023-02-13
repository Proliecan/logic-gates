using System;
using Code.components;
using UnityEngine;

namespace Code
{
    public class InputController : MonoBehaviour
    {
        Vector2 _lastCell = new Vector2(-1, -1);

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
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) 
                    colorCell(worldPos);
            }
        }

        void colorCell(Vector2 worldPos){
            try{
                Vector2 cell = _tileGrid.GetCell(worldPos);
                if (cell != _lastCell){
                    if (Input.GetMouseButton(0))
                        _tileGrid.ColorCell(cell, Color.red);
                    else if (Input.GetMouseButton(1))
                        _tileGrid.ColorCell(cell, Color.black);
                    _lastCell = cell;
                }
            }
            catch {
                //ignore
            }
        }
    }
}
