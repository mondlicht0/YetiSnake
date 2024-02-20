using System;
using UnityEngine;
using YetiSnake.Utilities;
using YetiSnake.YetiNodes;

namespace YetiSnake.PlayerObject
{
    public class Player : MonoBehaviour
    {
        public Color PlayerColor;
        public Node PlayerNode {  get; private set; }

        public event Action OnYetiEated;

        public void SetPlayerNode(Node node)
        {
            PlayerNode = node;
        }

        public void InitPlayer(Node playerNode)
        {
            PlacePlayer();
            SetPlayerNode(playerNode);
            SetStartPosition(playerNode.WordPosition);
        }

        private void PlacePlayer()
        {
            SpriteRenderer playerRenderer = gameObject.AddComponent<SpriteRenderer>();
            playerRenderer.sprite = Utils.CreateSprite(PlayerColor);
            playerRenderer.sortingOrder = 2;
        }

        private void SetStartPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
