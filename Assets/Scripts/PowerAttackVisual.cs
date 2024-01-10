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
    private float _powerAttackCooldown;
    private void Start()
    {
        _player = SceneManager.Instance.Player;
        _powerAttackCooldown = _player.PowerAttackCooldown;
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
            _powerAttackCooldown = _player.PowerAttackCooldown;
        }
        else
        {
            _blurImage.SetActive(true);
            _powerAttackCooldown -= Time.deltaTime;
            if (_powerAttackCooldown < 0)
                _cooldownText.enabled = false;
            _cooldownText.text = (_powerAttackCooldown).ToString("0.0");
        }
    }
}
