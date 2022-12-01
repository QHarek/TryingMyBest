using System;
using System.Linq;
using UnityEngine;

public class OrePlacer : MonoBehaviour
{
    [SerializeField] private GameObject _oreClusterPrefab;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private int _startClusterCount;
    [SerializeField] private float clusterUnderstanding;
    [SerializeField] private OreClusterData[] possibleClusters;

    void Start()
    {
        GenerateStartClusters();
    }

    private void GenerateStartClusters()
    {
        for (int i = 0; i < _startClusterCount; i++)
        {
            Vector3 clusterPosition = GenerateRandomClusterPosition();
            OreClusterData clusterType = RandomClusterType();
            while (IsPlaceOccupied(clusterPosition))
                clusterPosition = GenerateRandomClusterPosition();
            PlaceCluster(clusterPosition, clusterType);
        }
    }

    public void GenerateNewCluster(OreClusterData.oreTypes oreType)
    {
        Vector3 clusterPosition = GenerateRandomClusterPosition();
        OreClusterData clusterType = possibleClusters.Where(p => p.OreType == oreType).First();
        while (IsPlaceOccupied(clusterPosition))
            clusterPosition = GenerateRandomClusterPosition();
        Debug.Log(clusterPosition);
        Debug.Log(oreType);
        PlaceCluster(clusterPosition, clusterType);
    }

    private OreClusterData RandomClusterType()
    {
        Type type = typeof(OreClusterData.oreTypes);
        Array values = type.GetEnumValues();
        OreClusterData.oreTypes oreType = (OreClusterData.oreTypes)UnityEngine.Random.Range(0, values.Length);
        OreClusterData randomClusterData = possibleClusters.Where(p => p.OreType == oreType).First();
        return randomClusterData;
    }

    private Vector3 GenerateRandomClusterPosition()
    {
        Vector3 clusterPosition = new Vector3(
            UnityEngine.Random.Range(0, _terrain.terrainData.size.x),
            0,
            UnityEngine.Random.Range(0, _terrain.terrainData.size.z));
        clusterPosition = GetClusterTerrainPosition(clusterPosition.x, clusterPosition.z);
        return clusterPosition;
    }

    private bool IsPlaceOccupied(Vector3 possiblePosition)
    {
        return false;
    }

    private Vector3 GetClusterTerrainPosition(float x, float z)
    {
        float y = _terrain.SampleHeight(new Vector3(x, 0 ,z)) + clusterUnderstanding;
        Vector3 clusterPosition = new Vector3(x, y, z);
        return clusterPosition;
    }

    private void PlaceCluster(Vector3 clusterPosition, OreClusterData clusterType)
    {
        var oreCluster = Instantiate(_oreClusterPrefab, clusterPosition, Quaternion.identity, _terrain.transform);
        oreCluster.GetComponent<OreClusterBehavior>().oreClusterData = clusterType;
        oreCluster.name = clusterType.name;
    }
}
