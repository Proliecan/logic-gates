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

        public static void DrawLine(Vector3 start, Vector3 end, Color color){
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
            // Cleanup the material in editor mode and in play mode
            if (Application.isEditor){
                GameObject.DestroyImmediate(material);
            }
            else{
                GameObject.Destroy(material);
            }
        }

        public static void DrawGrid(Vector2 origin, Vector2 cellSize, int width, int height, Color color){
            // using draw line
            for (int x = 0; x <= width; x++){
                Vector3 start = new Vector3(origin.x + x * cellSize.x, origin.y, 0);
                Vector3 end = new Vector3(origin.x + x * cellSize.x, origin.y + height * cellSize.y, 0);
                DrawLine(start, end, color);
            }

            for (int y = 0; y <= height; y++){
                Vector3 start = new Vector3(origin.x, origin.y + y * cellSize.y, 0);
                Vector3 end = new Vector3(origin.x + width * cellSize.x, origin.y + y * cellSize.y, 0);
                DrawLine(start, end, color);
            }
        }

        public static void DrawRect(Rect rect, Color borderColor, Color fillColor){
            CreateLineMaterial();
            // Apply the line material
            _lineMaterial.SetPass(0);
            GL.Begin(GL.QUADS);
            GL.Color(fillColor);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.End();
            GL.Begin(GL.LINES);
            GL.Color(borderColor);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.End();
        }
    }
}
