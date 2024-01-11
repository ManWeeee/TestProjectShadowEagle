using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerAttackVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject _blurImage;
    [SerializeField]
    private TextMeshProUGUI _cooldownText;
    [SerializeField]
    private Player _player;

    private void Start()
    {
        _player = SceneManager.Instance.Player;
}

    // Update is called once per frame
    void Update()
    {
        SetVisibility();
    }

    private void SetVisibility()
    {
        if (_player.IsEnemyClose && _player.TimeFromLastHit > _player.PowerAttackCooldown)
        {
            _blurImage.SetActive(false);
            _cooldownText.enabled = true;
        }
        else
        {
            _blurImage.SetActive(true);
            if (_player.PowerAttackCooldown - _player.TimeFromLastHit < 0)
                _cooldownText.enabled = false;
            _cooldownText.text = (_player.PowerAttackCooldown - _player.TimeFromLastHit).ToString("0.0");
        }
    }
}
