using System;
using NUnit.Framework;
using UnityEngine;

public class Grid
{
    [Test]
    public void isOnGrid(){
        var grid = new GameObject().AddComponent<Code.components.TileGrid>();
        grid.origin = new Vector2(0, 0);
        grid.cellSize = new Vector2(1, 2);
        grid.size = new Vector2(10, 10);
        grid.borderColor = Color.white;
        Assert.IsTrue(grid.isOnGrid(new Vector2(0, 0)));
        Assert.IsTrue(grid.isOnGrid(new Vector2(1, 2)));
        Assert.IsTrue(grid.isOnGrid(new Vector2(9, 18)));
        Assert.IsTrue(grid.isOnGrid(new Vector2(10, 20)));
        Assert.IsFalse(grid.isOnGrid(new Vector2(-1, -2)));
    }

    [Test]
    public void getCell(){
        var grid = new GameObject().AddComponent<Code.components.TileGrid>();
        grid.origin = new Vector2(0, 0);
        grid.cellSize = new Vector2(1, 2);
        grid.size = new Vector2(10, 10);
        grid.borderColor = Color.white;
        // Assert.AreEqual(new Vector2(0, 0), grid.getCell(new Vector2(.5f, .5f)));
        // Assert.AreEqual(new Vector2(1,1), grid.getCell(new Vector2(1.5f, 2.5f)));
        // Assert.AreEqual(new Vector2(9, 9), grid.getCell(new Vector2(9.5f, 18.5f)));
        // Assert.AreEqual(new Vector2(10, 10), grid.getCell(new Vector2(10.5f, 20.5f)));
        // try{
        // grid.getCell(-.5f * Vector2.one);
        //     Assert.Fail("Should have thrown an exception");
        // }
        // catch (Exception e){
        //     Assert.AreEqual("Position is not on the grid", e.Message);
        // }
        
        // assert the center of cell 0,0 is in cell 0,0
        Assert.AreEqual(new Vector2(0, 0), grid.GetCell(new Vector2(.5f, 1f)));
        // assert the center of cell 1,1 is in cell 1,1
        Assert.AreEqual(new Vector2(1, 1), grid.GetCell(new Vector2(1.5f, 3f)));
        // assert the center of cell 9,9 is in cell 9,9
        Assert.AreEqual(new Vector2(9, 9), grid.GetCell(new Vector2(9.5f, 19f)));
        
        // assert asking for a position outside the grid throws an exception
        try{
            grid.GetCell(grid.origin - .5f * grid.cellSize);
            Assert.Fail("Should have thrown an exception");
        }
        catch (Exception e){
            Assert.AreEqual("Position is not on the grid", e.Message);
        }
    }

    [Test]
    public void getCellRect(){
        var grid = new GameObject().AddComponent<Code.components.TileGrid>();
        grid.origin = new Vector2(0, 0);
        grid.cellSize = new Vector2(1, 2);
        grid.size = new Vector2(10, 10);
        grid.borderColor = Color.white;
        
        Assert.AreEqual(new Rect(0f, 0f, 1, 2), grid.getCellRect(new Vector2(0, 0)));
        Assert.AreEqual(new Rect(1f, 2f, 1, 2), grid.getCellRect(new Vector2(1, 1)));
        Assert.AreEqual(new Rect(9f, 18f, 1, 2), grid.getCellRect(new Vector2(9, 9)));
        
        try{
            grid.getCellRect(new Vector2(-1, -1));
            Assert.Fail("Should have thrown an exception");
        }
        catch (Exception e){
            Assert.AreEqual("Cell is not on the grid", e.Message);
        }
    }
}
