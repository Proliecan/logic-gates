using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.components
{
    public class TileBrush
    {
        // singleton
        private static TileBrush _instance;
        public static TileBrush Instance => _instance ??= new TileBrush();

        public TileBase Tile{ get; private set; }
        public Tilemap Tilemap{ get; private set; }

        public void SetTile(TileBase tile){
            Tile = tile;
        }

        public void SetTilemap(Tilemap tilemap){
            Tilemap = tilemap;
        }

#if UNITY_EDITOR
        public bool debugMode = false;
#endif

        // initialize
        private TileBrush(){
            Tile = null;
            // find tilemap in scene
            Tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        }

        // reset instance
        public void Reset(){
            _instance = null;
        }

        // get tile index from mouse position
        public Vector2Int GetTileIndex(Vector2 mousePosition){
            Vector3Int tileIndex = Tilemap.WorldToCell(mousePosition);
            return new Vector2Int(tileIndex.x, tileIndex.y);
        }

        // paint tile from mouse position
        public void Paint(Vector2 mousePosition){
            Paint(mousePosition, Tile);
        }

        public void Paint(Vector2 mousePosition, TileBase tile){
            Paint(GetTileIndex(mousePosition), tile);
        }

        // paint tile
        private void Paint(Vector2Int position, TileBase tile = null){
            if (Tile != null && Tilemap != null){
                Tilemap.SetTile(new Vector3Int(position.x, position.y, 0), tile);
#if UNITY_EDITOR
                if (debugMode) Debug.Log($"Painted {tile} at {position}");
#endif
            }
        }

        // erase tile from mouse position
        public void Erase(Vector2 mousePosition){
            Paint(mousePosition, null);
        }
    }

#if UNITY_EDITOR
    // custom plugin window
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

            TileBrush.Instance.SetTile(
                (TileBase) EditorGUILayout.ObjectField("Tile", TileBrush.Instance.Tile, typeof(TileBase), false));
            TileBrush.Instance.SetTilemap((Tilemap) EditorGUILayout.ObjectField("Tilemap", TileBrush.Instance.Tilemap,
                typeof(Tilemap), true));

            EditorGUILayout.Space();

            TileBrush.Instance.debugMode = EditorGUILayout.Toggle("Debug Mode", TileBrush.Instance.debugMode);
        }
    }
#endif
}
