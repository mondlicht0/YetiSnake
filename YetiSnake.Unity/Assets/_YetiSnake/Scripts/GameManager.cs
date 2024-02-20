using UnityEngine;
using YetiSnake.PlayerObject;
using YetiSnake.MapDraw;
using YetiSnake.Nodes;
using YetiSnake.Utilities;

namespace YetiSnake
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }

        [field: SerializeField] public MapDrawer MapDrawer { get; private set; }

        [Header("Player Prefab")]
        [SerializeField] private Player _player;

        [SerializeField] private Node _yetiNode;
        [SerializeField] private GameObject _yetiObject;
        [SerializeField] private Sprite _yetiSprite;

        public Node YetiNode { get => _yetiNode; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(Instance);
                Instance = this;
            }
        }

        private void OnEnable()
        {
            _player.OnYetiEated += CreateYeti;
        }

        private void OnDisable()
        {
            _player.OnYetiEated -= CreateYeti;
        }

        private void Start()
        {
            Player player = Instantiate(_player);
            player.InitPlayer(MapDrawer.GetNode(3, 3));

            CreateYeti();
        }

        private void CreateYeti()
        {
            _yetiObject = new GameObject("Yeti");
            SpriteRenderer yetiRenderer = _yetiObject.AddComponent<SpriteRenderer>();
            yetiRenderer.sprite = Utils.CreateSprite(Color.red, _yetiSprite);
            yetiRenderer.sortingOrder = 2;

            RandomSpawnYeti();
        }

        public void RandomSpawnYeti()
        {
            int randPos = Random.Range(0, MapDrawer.AvaliableNodes.Count);
            Node node = MapDrawer.AvaliableNodes[randPos];
            Utils.PlaceObject(_yetiObject, node.WordPosition); 
            //_yetiObject.transform.position = node.WordPosition;
            _yetiNode = node;
        }
    }
}
