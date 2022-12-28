using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerRankingView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberPos;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _outline;

    private string _number;
    private string _name;
    private string _score;

    public void Initialize(string name, string playerName, string score, bool isPlayer)
    {
        _numberPos.text = name;
        _playerName.text = playerName;
        _scoreText.text = score;

        if (isPlayer)
            _outline.gameObject.SetActive(true);
    }

    public void Render()
    {
        _numberPos.SetText(_number);
        _playerName.SetText(_name);
        _scoreText.SetText(_score);
    }
}