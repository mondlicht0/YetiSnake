using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YetiSnake.Utilities
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Image _timerFill;

        private int _remainingTime;
        private bool _isPause = false;

        public event Action OnEndTimer;

        public void StartTimer(int duration, bool isPause)
        {
            _timerFill.fillAmount = 0;
            _remainingTime = duration;
            StartCoroutine(UpdateTimer(duration, isPause));
        }

        private IEnumerator UpdateTimer(int duration, bool isPause)
        {
            _timerFill.DOFillAmount(1, duration);

            while (_remainingTime > 0)
            {
                if (!isPause)
                {
                    _remainingTime--;
                    yield return new WaitForSeconds(1);
                }

                yield return null;
            }
            OnEndTimer.Invoke();
        }
    }
}
