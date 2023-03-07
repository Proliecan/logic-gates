using System;
using UnityEngine;
using UnityEngine.TestTools;
using Gizmos = Code.Core.gizmos.Gizmos;

namespace Code.Components
{
    // HAS TO BE MANUALLY TESTED!!!
    [ExcludeFromCoverage]
    public class InputController : MonoBehaviour
    {
        private Camera _mainCam;
        private bool _mainCamNotNull;

        public enum BrushMode
        {
            Paint,
            Erase
        }

        public BrushMode currentBrushMode = BrushMode.Paint;

#if UNITY_EDITOR
        public bool debugMode = false;
#endif

        private void Start(){
            _mainCam = Camera.main;
            _mainCamNotNull = _mainCam != null;
        }

        private void Update(){
            // mouse input
            bool leftClick = Input.GetMouseButton(0);
            bool rightClick = Input.GetMouseButtonDown(1);

            if (_mainCamNotNull){                             
                // paint on left click
                Vector2 mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                if (leftClick){
                    switch (currentBrushMode){
                        case BrushMode.Paint:
                            TileBrush.Instance.Paint(mousePosition);
                            break;
                        case BrushMode.Erase:
                            TileBrush.Instance.Erase(mousePosition);
                            break;
                    }

#if UNITY_EDITOR
                    if (debugMode) Debug.Log($"{currentBrushMode} tile at {mousePosition}");
#endif
                }

                // change mode on right click
                if (rightClick){
                    currentBrushMode = currentBrushMode == BrushMode.Paint ? BrushMode.Erase : BrushMode.Paint;
#if UNITY_EDITOR
                    if (debugMode) Debug.Log($"Changed mode to {currentBrushMode}");
#endif
                }
            }
        }
        
        // playmode gizmos
        private void OnRenderObject(){
            if (_mainCamNotNull){
                // draw tile rect
                Vector2 mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                Rect tileRect = TileBrush.Instance.GetTileRect(mousePosition);
                Gizmos.DrawRect(tileRect, Color.grey, Color.clear);
            }
        }
    }
}
