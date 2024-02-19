using UnityEngine;

namespace YetiSnake.PlayerObject
{
    public class Player : MonoBehaviour
    {
        public Color PlayerColor;

        public void InitPlayer(Vector3 startPosition)
        {
            PlacePlayer();
            SetStartPosition(startPosition);
        }

        private void PlacePlayer()
        {
            SpriteRenderer playerRenderer = gameObject.AddComponent<SpriteRenderer>();
            playerRenderer.sprite = CreatePlayerSprite(PlayerColor);
            playerRenderer.sortingOrder = 2;
        }

        private void SetStartPosition(Vector3 position)
        {
            transform.position = position;
        }

        private Sprite CreatePlayerSprite(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            texture.filterMode = FilterMode.Point;

            Rect rect = new Rect(0, 0, 1, 1);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f, 2.5f, 0, SpriteMeshType.FullRect);

            return sprite;
        }
    }
}
