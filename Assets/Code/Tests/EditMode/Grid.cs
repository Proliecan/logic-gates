using NUnit.Framework;
using UnityEngine;

public class Grid
{
    [Test]
    public void isOnGrid()
    {
        var grid = new GameObject().AddComponent<Code.components.Grid>();
        grid.origin = new Vector2(0, 0);
        grid.cellSize = new Vector2(1, 1);
        grid.size = new Vector2(10, 10);
        grid.color = Color.white;
        Assert.IsTrue(grid.isOnGrid(new Vector2(0, 0)));
        Assert.IsTrue(grid.isOnGrid(new Vector2(5, 5)));
        Assert.IsTrue(grid.isOnGrid(new Vector2(9, 9)));
        Assert.IsTrue(grid.isOnGrid(new Vector2(10, 10)));
        Assert.IsFalse(grid.isOnGrid(new Vector2(-1, -1)));
    }
}
