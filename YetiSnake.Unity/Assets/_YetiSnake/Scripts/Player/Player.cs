using System;
using System.Collections.Generic;
using UnityEngine;
using YetiSnake.MapDraw;
using YetiSnake.Nodes;
using YetiSnake.Utilities;

namespace YetiSnake.PlayerObject
{
    public class Player : MonoBehaviour
    {
        public Color PlayerColor;

        private GameObject _tailParent;
        private Sprite _playerSprite;

        #region Properties
        public Node PlayerNode { get; private set; }
        public Node PreviousPlayerNode { get; private set; }
        public List<TailNode> Tail { get; private set; } = new List<TailNode>();
        public GameObject TailParent { get => _tailParent; }
        #endregion
        

        public void SetPlayerNode(Node node)
        {
            PlayerNode = node;
        }

        public void InitPlayer(Node playerNode)
        {
            PlacePlayer();
            SetPlayerNode(playerNode);
            SetStartPosition(playerNode.WordPosition);
            Utils.PlaceObject(gameObject, PlayerNode.WordPosition);
        }

        public TailNode CreateTailNode(int x, int y)
        {
            TailNode tail = new TailNode();
            tail.Node = MapDrawer.Instance.GetNode(x, y);
            tail.Object = new GameObject();
            tail.Object.transform.parent = _tailParent.transform;
            tail.Object.transform.position = tail.Node.WordPosition;
            tail.Object.transform.localScale = Vector3.one * 0.95f;

            SpriteRenderer tailRenderer = tail.Object.AddComponent<SpriteRenderer>();
            tailRenderer.sprite = _playerSprite;
            tailRenderer.sortingOrder = 2;

            return tail;
        }

        private void PlacePlayer()
        {
            SpriteRenderer playerRenderer = gameObject.AddComponent<SpriteRenderer>();
            _playerSprite = Utils.CreateSprite(PlayerColor);
            playerRenderer.sprite = _playerSprite;
            playerRenderer.sortingOrder = 2;

            gameObject.transform.localScale = Vector3.one * 1.2f;

            _tailParent = new GameObject("TailParent");
        }

        public void ClearTail()
        {
            foreach (var tail in Tail)
            {
                Destroy(tail.Object);
            }

            Tail.Clear();
        }

        private void SetStartPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
