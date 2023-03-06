using UnityEngine;

namespace Code.Components
{
    // HAS TO BE MANUALLY TESTED!!!
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
            // get mouse position
            if (_mainCamNotNull){
                Vector2 mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                if (Input.GetMouseButton(0)){
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
                if (Input.GetMouseButtonDown(1)){
                    currentBrushMode = currentBrushMode == BrushMode.Paint ? BrushMode.Erase : BrushMode.Paint;
#if UNITY_EDITOR
                    if (debugMode) Debug.Log($"Changed mode to {currentBrushMode}");
#endif
                }
            }
        }
    }
}
