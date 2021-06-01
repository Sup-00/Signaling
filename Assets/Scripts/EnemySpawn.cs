using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _cooldown;

    private Transform[] _spawnPoints;
    private float _timer;
    private int _currectSpawnPosition;

    private void Start()
    {
        _currectSpawnPosition = 0;
        _timer = _cooldown;
        _spawnPoints = new Transform[gameObject.transform.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = gameObject.transform.GetChild(i);
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            if (_currectSpawnPosition >= _spawnPoints.Length)
            {
                _currectSpawnPosition = 0;
            }

            SpawnEnemy(_spawnPoints[_currectSpawnPosition].transform.position);
            _timer = _cooldown;
        }
    }

    private void SpawnEnemy(Vector3 EnemyPosition)
    {
        Instantiate(_enemy, EnemyPosition, Quaternion.identity);
        _currectSpawnPosition++;
    }
}
