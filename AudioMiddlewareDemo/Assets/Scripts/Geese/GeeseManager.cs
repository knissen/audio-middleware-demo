using TMPro;
using UnityEngine;

public class GeeseManager : MonoBehaviour
{
    [SerializeField] private GameObject _goosePrefab = default;

    [SerializeField] private KeyCode _spawnButton = KeyCode.UpArrow;
    [SerializeField] private KeyCode _killButton = KeyCode.DownArrow;
    [SerializeField] private float _spawnRadius = 20f;
    [SerializeField] private float _spawnHeight = 4f;

    [Header("Audio Hookups")]
    [SerializeField] private FMODUnity.StudioEventEmitter _emitter = default;
    [SerializeField] private string _parameterName = "Count";

    [Header("UI Display")]
    [SerializeField] private TextMeshProUGUI parameterDisplayText = default;

    public int GeeseCount { get; private set; }

    private float _spamTimer;

    private void Update()
    {
        _spamTimer -= Time.deltaTime;

        if (_spamTimer > 0f) return;

        if (Input.GetKey(_spawnButton))
        {
            SpawnGoose();
            _spamTimer = 0.1f;
        }

        if (Input.GetKey(_killButton))
        {
            KillGoose();
            _spamTimer = 0.1f;
        }

        UpdateAudioParameter();
    }

    private void UpdateAudioParameter()
    {
        parameterDisplayText.text = $"FMOD Parameter Value: {transform.childCount}";

        _emitter.SetParameter(_parameterName, transform.childCount);
    }

    private void SpawnGoose()
    {
        Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * _spawnRadius;
        randomOffset.y = _spawnHeight;

        Instantiate(_goosePrefab, transform.position + randomOffset, Quaternion.Euler(0f, Random.Range(0,360), 0f), transform);
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
