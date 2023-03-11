using UnityEngine;
using UnityEngine.TestTools;

namespace Code.Core.gizmos
{
    //HAS TO BE MANUALLY TESTED!!!
    [ExcludeFromCoverage]
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
            DrawRect(rect, 1, borderColor, fillColor);
        }
        
        public static void DrawRect(Rect rect, float borderThickness, Color borderColor, Color fillColor){
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
            // draw border with centered on the perimiter
            Vector3[] vertices = new Vector3[8];
            vertices[0] = new Vector3(rect.xMin+borderThickness/2, rect.yMin+borderThickness/2, 0);
            vertices[1] = new Vector3(rect.xMax-borderThickness/2, rect.yMin+borderThickness/2, 0);
            vertices[2] = new Vector3(rect.xMax-borderThickness/2, rect.yMax-borderThickness/2, 0);
            vertices[3] = new Vector3(rect.xMin+borderThickness/2, rect.yMax-borderThickness/2, 0);
            vertices[4] = new Vector3(rect.xMin-borderThickness/2, rect.yMin-borderThickness/2, 0);
            vertices[5] = new Vector3(rect.xMax+borderThickness/2, rect.yMin-borderThickness/2, 0);
            vertices[6] = new Vector3(rect.xMax+borderThickness/2, rect.yMax+borderThickness/2, 0);
            vertices[7] = new Vector3(rect.xMin-borderThickness/2, rect.yMax+borderThickness/2, 0);
            int[] triangles = new int[24];
            triangles[0] = 0;
            triangles[1] = 4;
            triangles[2] = 1;
            triangles[3] = 1;
            triangles[4] = 4;
            triangles[5] = 5;
            triangles[6] = 1;
            triangles[7] = 5;
            triangles[8] = 2;
            triangles[9] = 2;
            triangles[10] = 5;
            triangles[11] = 6;
            triangles[12] = 2;
            triangles[13] = 6;
            triangles[14] = 3;
            triangles[15] = 3;
            triangles[16] = 6;
            triangles[17] = 7;
            triangles[18] = 3;
            triangles[19] = 7;
            triangles[20] = 0;
            triangles[21] = 0;
            triangles[22] = 7;
            triangles[23] = 4;
            GL.Begin(GL.TRIANGLES);
            GL.Color(borderColor);
            foreach (var t in triangles){
                GL.Vertex(vertices[t]);
            }
            GL.End();
        }
    }
}
