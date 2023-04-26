using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public static GameView Instance { get; private set; }
    [SerializeField] GameObject _gamePopupPanel;

    private void Awake() {
        Instance = this;
    }

    public void ShowGamePopupPanel(string message, string subMessage = "", float selfDestroyTime = 2f)
    {
        _gamePopupPanel.gameObject.SetActive(true);
        var presenter = _gamePopupPanel.GetComponent<GamePopupPresenter>();
        presenter.UpdateMessage(message, subMessage, selfDestroyTime);
    }

}
