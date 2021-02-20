using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeeseManager : MonoBehaviour
{
    [SerializeField] private GameObject _goosePrefab = default;

    [SerializeField] private KeyCode _spawnButton = KeyCode.UpArrow;
    [SerializeField] private KeyCode _killButton = KeyCode.DownArrow;
    [SerializeField] private float _spawnRadius = 20f;

    [Header("Audio Hookups")]
    [SerializeField] private FMODUnity.StudioEventEmitter _emitter = default;
    [SerializeField] private string _parameterName = "Count";

    [Header("UI Display")]
    [SerializeField] private TextMeshProUGUI parameterDisplayText = default;

    public int GeeseCount { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(_spawnButton))
            SpawnGoose();

        if (Input.GetKeyDown(_killButton))
            KillGoose();

        UpdateAudioParameter();
    }

    private void UpdateAudioParameter()
    {
        parameterDisplayText.text = $"{_parameterName}: {transform.childCount}";

        _emitter.SetParameter(_parameterName, transform.childCount);
    }

    private void SpawnGoose()
    {
        Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * _spawnRadius;
        randomOffset.y = 0f;

        Instantiate(_goosePrefab, transform.position + randomOffset, Quaternion.identity, transform);
    }

    private void KillGoose()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }

    // Gizmos

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }

}
