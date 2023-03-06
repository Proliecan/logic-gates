using UnityEngine;

namespace Code.Components
{
    // HAS TO BE MANUALLY TESTED!!!
    public class InputController : MonoBehaviour
    {
        private Camera _mainCam;
        private bool _mainCamNotNull;

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
                    TileBrush.Instance.Paint(mousePosition);

#if UNITY_EDITOR
                    if (debugMode) Debug.Log($"Painted tile at {mousePosition}");
#endif
                }

                if (Input.GetMouseButton(1)){
                    TileBrush.Instance.Erase(mousePosition);

#if UNITY_EDITOR
                    if (debugMode) Debug.Log($"Erased tile at {mousePosition}");
#endif
                }
            }
        }
    }
}
