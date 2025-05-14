using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime.Core.UI
{
    public class SaveGameNotifier : MonoBehaviour
    {
        [SerializeField] private float _notificationTime;
        [SerializeField] private Text _notificationText;

        public void SaveGameNotify()
        {
            StartCoroutine(ProcessNotification());
        }

        private IEnumerator ProcessNotification()
        {
            _notificationText.gameObject.SetActive(true);

            yield return new WaitForSeconds(_notificationTime);
            
            _notificationText.gameObject.SetActive(false);
        }
    }
}