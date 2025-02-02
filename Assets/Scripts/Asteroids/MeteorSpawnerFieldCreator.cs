﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates Meteor Spawner Fields
/// </summary>
public class MeteorSpawnerFieldCreator : MonoBehaviour
{
    [Header("FieldCreatorSettings Settings")]
    public Transform parent;

    [SerializeField]
    public Transform[] Meteors;

    private static MeteorSpawnerFieldCreator _instance;

    public Transform MeteorSpawnerField;

    [SerializeField]
    [Range(1, 40)]
    private int _range = 10;

    private List<Transform> _spawnedFields = new List<Transform>();

    private bool[,,] _FieldArray;


    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        SpawnMeteorFields();
        _FieldArray = new bool[_range, _range / 2, _range];

        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range / 2; j++)
            {
                for (int k = 0; k < _range; k++)
                {
                    _FieldArray[i, j, k] = false;
                }
            }
        }
    }

    private void SpawnMeteorFields()
    {
        ClearMeteorFields();
        int size = (int)MeteorSpawnerField.GetComponent<ChunkManager>().ColliderSize;
        bool[,] arr = new bool[_range,_range];

        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range / 2; j++)
            {
                for (int k = 0; k < _range; k++)
                {
                    Vector3 position = new Vector3(
                        i * size - (_range * size / 2) + size / 2 + transform.position.x,
                        j * size - ((_range / 2) * size / 2) + size / 2 + transform.position.y,
                        k * size - (_range * size / 2) + size / 2 + transform.position.z
                        );
                    Transform created = Instantiate(MeteorSpawnerField, position, Quaternion.identity, parent);
                    created.GetComponent<ChunkManager>().CreateCollider();
                    _spawnedFields.Add(created);
                }
            }
        }
    }

    private void ClearMeteorFields()
    {
        foreach (var field in GameObject.FindGameObjectsWithTag("MeteorSpawnerField"))
        {
            //Destroy(field);
            Destroy(field.gameObject);
        }
    }

    /// <summary>
    /// Singleton Instance
    /// </summary>
    public static MeteorSpawnerFieldCreator Instance
    {
        get
        {
            return _instance;
        }
    }

    private void OnDrawGizmosSelected()
    {
        int size = (int)MeteorSpawnerField.GetComponent<ChunkManager>().ColliderSize;

        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range / 2; j++)
            {
                for (int k = 0; k < _range; k++)
                {
                    Vector3 position = new Vector3(
                        i * size - (_range * size / 2) + size / 2 + transform.position.x,
                        j * size - ((_range / 2) * size / 2) + size / 2 + transform.position.y,
                        k * size - (_range * size / 2) + size / 2 + transform.position.z
                        );
                    Gizmos.DrawWireCube(position, size * Vector3.one);
                }
            }
        }
    }
}