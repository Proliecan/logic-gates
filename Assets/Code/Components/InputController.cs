using UnityEngine;
using UnityEngine.TestTools;

namespace Code.Components
{
    // HAS TO BE MANUALLY TESTED!!!
    [ExcludeFromCoverage]
    public class InputController : MonoBehaviour
    {
        private Camera _mainCam;
        private bool _mainCamNotNull;

        private GizmosDrawer _gizmosDrawer;

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
            _gizmosDrawer = FindObjectOfType<GizmosDrawer>();
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


                // draw tile rect
                Vector3Int cellPosition = TileBrush.Instance.Tilemap.WorldToCell(mousePosition);
                if (_gizmosDrawer != null){
                    _gizmosDrawer.CellPosition = (Vector2Int) cellPosition;
                }
            }
        }
    }
}
