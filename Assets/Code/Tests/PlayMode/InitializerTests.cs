using System.Collections;
using Code.Components;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;

namespace Code.Tests.PlayMode
{
    [ExcludeFromCoverage]
    public class InitializerTests
    {
        [UnityTest]
        public IEnumerator Initializer_Start()
        {
            // Arrange
            var initializer = new GameObject().AddComponent<Initializer>();
            var tile = ScriptableObject.CreateInstance<Tile>();
            initializer.tile = tile;
            // generate grid with tilemap child
            var grid = new GameObject().AddComponent<Grid>();
            var tilemap = new GameObject().AddComponent<Tilemap>();
            tilemap.transform.SetParent(grid.transform);

            // Act
            initializer.Start();
            
            // Assert
            Assert.AreEqual(tile, TileBrush.Instance.Tile);
            Assert.AreEqual(tilemap, TileBrush.Instance.Tilemap);
            Assert.IsTrue(initializer == null);  // != Assert.IsNull(initializer);
            
            // Cleanup
            
            // return null to please the compiler
            yield return null;
        }
    }
}
