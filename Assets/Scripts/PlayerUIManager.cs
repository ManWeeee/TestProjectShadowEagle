using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _powerAttackVisual;
    [SerializeField]
    private TextMeshProUGUI _waveInfoText;

    private void Start()
    {
        SceneManager.Instance.OnWaveCompleted += UpdateWaveText;
    }

    private void UpdateWaveText()
    {
        _waveInfoText.text = string.Format("Wave {0}/{1}", SceneManager.Instance.CurrentWave, SceneManager.Instance.LevelConfig.Waves.Length);
    }
}