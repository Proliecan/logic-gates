using UnityEngine;

namespace Code.core
{
 public static class Gizmos
 {
     static Material _lineMaterial;
 
     static void CreateLineMaterial(){
         if (!_lineMaterial){
             // Unity has a built-in shader that is useful for drawing
             // simple colored things.
             Shader shader = Shader.Find("Hidden/Internal-Colored");
             _lineMaterial = new Material(shader);
             _lineMaterial.hideFlags = HideFlags.HideAndDontSave;
             // Turn on alpha blending
             _lineMaterial.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
             _lineMaterial.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
             // Turn backface culling off
             _lineMaterial.SetInt("_Cull", (int) UnityEngine.Rendering.CullMode.Off);
             // Turn off depth writes
             _lineMaterial.SetInt("_ZWrite", 0);
         }
     }
     
     public static void DrawLine(Vector3 start, Vector3 end, Color color)
     {
         // Create a material
         Material material = new Material(Shader.Find("Hidden/Internal-Colored"));
         // Set the material color
         material.SetColor("_Color", color);
         // Set the start and end positions
         material.SetPass(0);
         GL.Begin(GL.LINES);
         GL.Vertex(start);
         GL.Vertex(end);
         GL.End();
         // Cleanup
         Object.Destroy(material);
     }
     
     public static void drawGrid(Vector2 origin, Vector2 cellSize, int width, int height, Color color){
         CreateLineMaterial();
         // Apply the line material
         _lineMaterial.SetPass(0);
         GL.Begin(GL.LINES);
         GL.Color(color);
         for (int x = 0; x <= width; x++){
             float startX = origin.x + x * cellSize.x;
             float startY = origin.y;
             float endX = startX;
             float endY = startY + height * cellSize.y;
             GL.Vertex3(startX, startY, 0);
             GL.Vertex3(endX, endY, 0);
         }
         for (int y = 0; y <= height; y++){
             float startX = origin.x;
             float startY = origin.y + y * cellSize.y;
             float endX = startX + width * cellSize.x;
             float endY = startY;
             GL.Vertex3(startX, startY, 0);
             GL.Vertex3(endX, endY, 0);
         }
         GL.End();
     }

 }
   
}
