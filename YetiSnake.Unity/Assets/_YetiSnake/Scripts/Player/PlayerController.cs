using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using YetiSnake.Input;
using YetiSnake.MapDraw;
using YetiSnake.Nodes;
using YetiSnake.Utilities;

namespace YetiSnake.PlayerObject
{
    public class PlayerController : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private MobileInput _swipeInput;
        private InputHandler _inputHandler;

        [SerializeField] private float _moveRate = 0.25f;
        private float _timer;

        private MoveDirection _targetMoveDirection;
        private MoveDirection _currentMoveDirection;
        private bool _up, _down, _left, _right;

        private List<TailNode> _tail => _player.Tail;

        public event Action OnYetiEated;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            InputHandler.Instance.OnEndTouch += SetPlayerDirection;
        }

        private void Start()
        {
            InputHandler.Instance.OnEndTouch += SetPlayerDirection;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _moveRate)
            {
                _timer = 0;
                _currentMoveDirection = _targetMoveDirection;
                Move();
            }
        }

        private void SetPlayerDirection(Vector2 position, float time)
        {

            if (_swipeInput.ToUp && !IsOppositeDirection(MoveDirection.Up))
            {
                SetDirection(MoveDirection.Up);
            }

            else if (_swipeInput.ToDown && !IsOppositeDirection(MoveDirection.Down))
            {
                SetDirection(MoveDirection.Down);
            }

            else if (_swipeInput.ToLeft && !IsOppositeDirection(MoveDirection.Left))
            {
                SetDirection(MoveDirection.Left);
            }

            else if (_swipeInput.ToRight && !IsOppositeDirection(MoveDirection.Right))
            {
                SetDirection(MoveDirection.Right);
            }
        }

        private void SetDirection(MoveDirection direction)
        {
            if (!IsOppositeDirection(direction))
            {
                _targetMoveDirection = direction;
            }
        }

        private void Move()
        {
            int x = 0;
            int y = 0;

            switch (_currentMoveDirection)
            {
                case MoveDirection.Up:
                    y = 1;
                    break;
                case MoveDirection.Down:
                    y = -1;
                    break;
                case MoveDirection.Left:
                    x = -1;
                    break;
                case MoveDirection.Right:
                    x = 1;
                    break;
            }

            Node targetNode = MapDrawer.Instance.GetNode(_player.PlayerNode.X + x, _player.PlayerNode.Y + y);

            if ( targetNode == null )
            {
                GameManager.Instance.GameOver();
            }

            else
            {
                if (isTailNode(targetNode))
                {
                    GameManager.Instance.GameOver();
                }

                else
                {
                    bool isYeti = false;

                    if (targetNode == GameManager.Instance.YetiNode)
                    {
                        isYeti = true;
                    }

                    Node previousNode = _player.PlayerNode;
                    MapDrawer.Instance.AvaliableNodes.Add(_player.PlayerNode);


                    if (isYeti)
                    {
                        OnYetiEated?.Invoke();

                        _player.Tail.Add(_player.CreateTailNode(previousNode.X, previousNode.Y));
                        MapDrawer.Instance.AvaliableNodes.Remove(previousNode);
                        GameManager.Instance.RandomSpawnYeti();
                    }

                    MoveTail();
                    Utils.PlaceObject(gameObject, targetNode.WordPosition);
                    //transform.position = targetNode.WordPosition;
                    _player.SetPlayerNode(targetNode);
                    MapDrawer.Instance.AvaliableNodes.Remove(_player.PlayerNode);
                }
            }

        }

        private void MoveTail()
        {
            Node previousNode = null;

            for (int i = 0; i < _player.Tail.Count; i++)
            {
                TailNode tail = _player.Tail[i];
                MapDrawer.Instance.AvaliableNodes.Add(tail.Node);

                if (i == 0)
                {
                    previousNode = tail.Node;
                    tail.Node = _player.PlayerNode;
                }

                else
                {
                    Node prevNode = tail.Node;
                    tail.Node = previousNode;
                    previousNode = prevNode;
                }

                MapDrawer.Instance.AvaliableNodes.Remove(tail.Node);
                Utils.PlaceObject(tail.Object, tail.Node.WordPosition);
                //tail.Object.transform.position = tail.Node.WordPosition;
            }
        }
    
        private bool IsOppositeDirection(MoveDirection direction)
        {
            switch (direction) 
            {
                default:

                case MoveDirection.Up:
                    if (_currentMoveDirection == MoveDirection.Down)
                        return true;
                    else
                        return false;

                case MoveDirection.Down:
                    if (_currentMoveDirection == MoveDirection.Up)
                        return true;
                    else
                        return false;

                case MoveDirection.Left:
                    if (_currentMoveDirection == MoveDirection.Right)
                        return true;
                    else
                        return false;

                case MoveDirection.Right:
                    if (_currentMoveDirection == MoveDirection.Left)
                        return true;
                    else
                        return false;
            }
        }
    
        private bool isTailNode(Node node)
        {
            for (int i = 0; i < _tail.Count; i++)
            {
                if (_tail[i].Node == node)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
