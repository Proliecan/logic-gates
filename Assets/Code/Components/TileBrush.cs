using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Components
{
    public class TileBrush
    {
        // singleton
        private static TileBrush _instance;
        public static TileBrush Instance => _instance ??= new TileBrush();

        public TileBase Tile{ get; set; }
        public Tilemap Tilemap{ get; set; }

#if UNITY_EDITOR
        public bool debugMode = false;
#endif

        // initialize
        private TileBrush(){
            Tile = null;
            // find tilemap in scene without assuming the name
            Tilemap = Object.FindObjectOfType<Tilemap>();
        }

        // reset instance
        public void Reset(){
            _instance = null;
        }

        // get tile index from mouse position
        public Vector2Int GetTileIndex(Vector2 worldPosition){
            Vector3Int tileIndex = Tilemap.WorldToCell(worldPosition);
            return new Vector2Int(tileIndex.x, tileIndex.y);
        }

        // paint tile from mouse position
        public void Paint(Vector2 index){
            Paint(index, Tile);
        }

        public void Paint(Vector2 index, TileBase tile){
            Paint(GetTileIndex(index), tile);
        }

        // paint tile
        private Vector2Int _lastPaintedTile = new Vector2Int(-1, -1);
        private TileBase _lastPaintedTileBase = null;

        [ExcludeFromCoverage]
        private void Paint(Vector2Int position, TileBase tile = null){
            if (Tile != null && Tilemap != null && (_lastPaintedTile != position || _lastPaintedTileBase != tile)){
                Tilemap.SetTile(new Vector3Int(position.x, position.y, 0), tile);
#if UNITY_EDITOR
                if (debugMode) Debug.Log($"Painted {tile} at {position}");
#endif
                // save last painted tile
                _lastPaintedTile = position;
                _lastPaintedTileBase = tile;
            }
        }

        // erase tile from mouse position
        public void Erase(Vector2 mousePosition){
            Paint(mousePosition, null);
        }
    }

#if UNITY_EDITOR
    // custom plugin window
    [ExcludeFromCoverage]
    class TileBrushWindow : EditorWindow
    {
        [MenuItem("Window/2D/Tile Brush")]
        public static void ShowWindow(){
            GetWindow<TileBrushWindow>("Tile Brush");
        }

        private void OnGUI(){
            // Heading
            // EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tile Brush",
                new GUIStyle(GUI.skin.label)
                    {fontSize = 20, alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold},
                GUILayout.Height(30));
            // EditorGUILayout.Space();

            if (GUILayout.Button("Reset")){
                TileBrush.Instance.Reset();
            }

            EditorGUILayout.Space();

            TileBrush.Instance.Tile =
                (TileBase) EditorGUILayout.ObjectField("Tile", TileBrush.Instance.Tile, typeof(TileBase), false);
            TileBrush.Instance.Tilemap = (Tilemap) EditorGUILayout.ObjectField("Tilemap", TileBrush.Instance.Tilemap,
                typeof(Tilemap), true);

            EditorGUILayout.Space();

            TileBrush.Instance.debugMode = EditorGUILayout.Toggle("Debug Mode", TileBrush.Instance.debugMode);
        }
    }
#endif
}
