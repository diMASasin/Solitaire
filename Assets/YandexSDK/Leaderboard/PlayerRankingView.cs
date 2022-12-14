using UnityEngine;
using TMPro;

public class PlayerRankingView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberPos;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private string _number;
    private string _name;
    private string _score;

    public void Initialize(string name, string playerName, string score)
    {
        _numberPos.text = name;
        _playerName.text = playerName;
        _scoreText.text = score;
    }

    public void Render()
    {
        _numberPos.SetText(_number);
        _playerName.SetText(_name);
        _scoreText.SetText(_score);
    }
}