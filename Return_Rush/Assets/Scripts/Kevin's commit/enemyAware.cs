using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemyAware : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    [SerializeField]
    private float _playerAwareDistance;
    private Transform _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player_Movement>().transform;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToplayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToplayerVector.normalized;

        if (enemyToplayerVector.magnitude <= _playerAwareDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
