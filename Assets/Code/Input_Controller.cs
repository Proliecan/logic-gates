using UnityEngine;
using Grid = Code.components.Grid;

namespace Code
{
    public class Input_Controller : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // is left mouse button pressed?
            if (Input.GetMouseButtonDown(0))
            {
                // get mouse pos
                Vector3 mousePos = Input.mousePosition;
                // convert to world pos
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                // get grid
                Grid grid = FindObjectOfType<Grid>();
                // is on grid?
                Debug.Log(worldPos);
                Debug.Log(grid.getCell(worldPos));
                Debug.Log(grid.getCellRect(grid.getCell(worldPos)));
            }
            
        }
    }
}
