using UnityEngine;

namespace YetiSnake.MapDraw
{
    public class MapDrawer : MonoBehaviour
    {
        private GameObject _map;
        private SpriteRenderer _mapRenderer;

        [Header("Size of map")]
        [SerializeField] private int _maxHeight = 15;
        [SerializeField] private int _maxWidth = 17;

        [Header("Colors")]
        public Color FirstColor;
        public Color SecondColor;

        private Node[,] _grid;

        private void Start()
        {
            DrawMap();
        }

        private void DrawMap()
        {
            _map = new GameObject("Map");
            _mapRenderer = _map.AddComponent<SpriteRenderer>();

            _grid = new Node[_maxWidth, _maxHeight];

            Texture2D texture = new Texture2D(_maxWidth, _maxHeight);

            for (int x = 0; x < _maxWidth; x++)
            {
                for (int y = 0; y < _maxHeight; y++)
                {
                    Vector3 pos = Vector3.zero;
                    pos.x = x / 2.5f;
                    pos.y = y / 2.5f;

                    Node n = new Node()
                    {
                        X = x,
                        Y = y,
                        WordPosition = pos
                    };

                    _grid[x, y] = n;


                    if (x % 2 == 0)
                    {
                        if (y % 2 == 0)
                        {
                            texture.SetPixel(x, y, FirstColor);
                        }

                        else
                        {
                            texture.SetPixel(x, y, SecondColor);
                        }
                    }

                    else
                    {
                        if (y % 2 == 0)
                        {
                            texture.SetPixel(x, y, SecondColor);
                        }

                        else
                        {
                            texture.SetPixel(x, y, FirstColor);
                        }
                    }
                }
            }

            texture.filterMode = FilterMode.Point;

            texture.Apply();

            Rect rect = new Rect(0, 0, _maxWidth, _maxHeight);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f, 2.5f, 0, SpriteMeshType.FullRect);

            _mapRenderer.sprite = sprite;
            _mapRenderer.sortingOrder = 1;
        }

        public Node GetNode(int x, int y)
        {
            // Preventing from getting negative values or out of bounds values
            if (x < 0 || x > _maxWidth - 1 || y < 0 || y > _maxHeight - 1)
            {
                return null;
            }

            return _grid[x, y];
        }
    }

}