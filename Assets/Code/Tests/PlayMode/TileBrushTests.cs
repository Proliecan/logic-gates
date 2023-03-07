using System.Collections;
using Code.Components;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

namespace Code.Tests.PlayMode
{
    public class TileBrushTests
    {
        [UnityTest]
        public IEnumerator TileBrush_Getter_Setter(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            var tile = ScriptableObject.CreateInstance<Tile>();

            // Act
            TileBrush.Instance.Tilemap = tilemap;
            TileBrush.Instance.Tile = tile;

            // Assert
            Assert.AreEqual(tile, TileBrush.Instance.Tile);
            Assert.AreEqual(tilemap, TileBrush.Instance.Tilemap);

            // Cleanup

            // return null to please the compiler
            yield return null;
        }

        [UnityTest]
        public IEnumerator TileBrush_Reset(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            var tile = ScriptableObject.CreateInstance<Tile>();

            // Act
            TileBrush.Instance.Tilemap = tilemap;
            TileBrush.Instance.Tile = tile;
            TileBrush.Instance.Reset();

            // Assert
            Assert.IsNull(TileBrush.Instance.Tile);
            Assert.AreEqual(tilemap, TileBrush.Instance.Tilemap);

            // Cleanup

            // return null to please the compiler
            yield return null;
        }

        [UnityTest]
        public IEnumerator TileBrush_GetTileIndex(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            var tile = ScriptableObject.CreateInstance<Tile>();
            TileBrush.Instance.Tilemap = tilemap;
            TileBrush.Instance.Tile = tile;

            // Act
            // var index = TileBrush.Instance.GetTileIndex(new Vector2(1, 1)); but through reflection
            var index = typeof(TileBrush).GetMethod("GetTileIndex",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(TileBrush.Instance, new object[]{new Vector2(1, 1)});

            // Assert
            Assert.AreEqual(new Vector2Int(1, 1), index);

            // Cleanup

            // return null to please the compiler
            yield return null;
        }

        [UnityTest]
        public IEnumerator TileBrush_Paint(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            var tile = ScriptableObject.CreateInstance<Tile>();
            TileBrush.Instance.Tilemap = tilemap;
            TileBrush.Instance.Tile = tile;

            // Act
            TileBrush.Instance.Paint(new Vector2(1, 1));

            // Assert
            Assert.AreEqual(tile, tilemap.GetTile(new Vector3Int(1, 1, 0)));

            // Cleanup

            // return null to please the compiler
            yield return null;
        }

        [UnityTest]
        public IEnumerator TileBrush_Paint_WithTile(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            var tile = ScriptableObject.CreateInstance<Tile>();
            TileBrush.Instance.Tilemap = tilemap;

            // Act
            TileBrush.Instance.Paint(new Vector2(1, 1), tile);

            // Assert
            Assert.AreEqual(tile, tilemap.GetTile(new Vector3Int(1, 1, 0)));

            // Cleanup

            // return null to please the compiler
            yield return null;
        }

        [UnityTest]
        public IEnumerator TileBrush_Erase(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            var tile = ScriptableObject.CreateInstance<Tile>();
            TileBrush.Instance.Tilemap = tilemap;
            TileBrush.Instance.Tile = tile;

            // Act
            TileBrush.Instance.Paint(new Vector2(1, 1));
            TileBrush.Instance.Erase(new Vector2(1, 1));

            // Assert
            Assert.IsNull(tilemap.GetTile(new Vector3Int(1, 1, 0)));

            // Cleanup

            // return null to please the compiler
            yield return null;
        }

        [UnityTest]
        public IEnumerator TileBrush_GetTileRect(){
            // Arrange
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);
            
            var tile = ScriptableObject.CreateInstance<Tile>();
            TileBrush.Instance.Tilemap = tilemap;
            TileBrush.Instance.Tile = tile;
            
            // Act
            var rect = TileBrush.Instance.GetTileRect(new Vector2(1, 1));
            
            // Assert
            Assert.AreEqual(new Rect(1, 1, 1, 1), rect);
            
            // Cleanup
            
            // return null to please the compiler
            yield return null;
        }
    }
}
