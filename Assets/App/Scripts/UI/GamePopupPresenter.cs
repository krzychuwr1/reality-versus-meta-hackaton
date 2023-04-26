using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePopupPresenter : MonoBehaviour
{
    [SerializeField]
    Text _message;
    [SerializeField]
    Text _subMessage;

    public void UpdateMessage(string message, string subMessage, float deactivateTime)
    {
        if(message != null)
        {
            _message.text = message;
        }

        if(subMessage != null)
        {
            _subMessage.text = subMessage;
        }

        StartCoroutine(DeactivateAfterSeconds(deactivateTime));
    }

    private IEnumerator DeactivateAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
