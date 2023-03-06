using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Components
{
    public class Initializer : MonoBehaviour
    {
        public TileBase tile;

        // start is called before the first frame update
        public void Start(){
            // initialize tile brush
            TileBrush.Instance.Tile = tile;
            
#if !UNITY_EDITOR
            // destroy the game object in build
            Destroy(gameObject);
#endif
#if UNITY_EDITOR
            // destroy the game object in editor
            DestroyImmediate(gameObject);
#endif
        }
    }
}
