using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Project.GUI
{
    public sealed class ErrorPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private float displayTime = 5f;
        
        private readonly SerialDisposable timer = new();

        private void Reset() => errorText = GetComponentInChildren<TMP_Text>();

        public void Show(string error)
        {
            errorText.SetText(error);
            gameObject.SetActive(true);
            timer.Disposable = Observable.ReturnUnit()
                .Delay(TimeSpan.FromSeconds(displayTime))
                .Subscribe(_ => gameObject.SetActive(false));
        }

        private void OnDestroy() => timer.Dispose();
    }
}