using UnityEngine;

namespace YetiSnake.Utilities
{
    public class Utils : MonoBehaviour
    {
        public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        public static Sprite CreateSprite(Color color, Sprite image = null)
        {
            if (image == null)
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, color);
                texture.Apply();
                texture.filterMode = FilterMode.Point;

                Rect rect = new Rect(0, 0, 1, 1);
                Sprite sprite = Sprite.Create(texture, rect, Vector2.zero, 1, 0, SpriteMeshType.FullRect);

                return sprite;
            }

            return image;

        }
    }
}
