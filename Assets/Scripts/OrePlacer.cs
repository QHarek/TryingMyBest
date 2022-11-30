using System;
using System.Linq;
using UnityEngine;

public class OrePlacer : MonoBehaviour
{
    [SerializeField] private GameObject _oreCluster;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private float clusterUnderstanding;
    [SerializeField] private OreClusterData[] possibleClusters;

    void Start()
    {
        GenerateStartClusters();
    }

    private void GenerateStartClusters()
    {
        OreClusterData clusterType = RandomClusterType();
        if (!IsPlaceOccupied(500,503))
            PlaceCluster(500, 503, clusterType);
    }

    public void GenerateNewCluster(OreClusterData.oreTypes oreType)
    {
        OreClusterData clusterType = RandomClusterType();
        if (!IsPlaceOccupied(500, 503))
            PlaceCluster(500, 503, clusterType);
    }

    private OreClusterData RandomClusterType()
    {
        Type type = typeof(OreClusterData.oreTypes);
        Array values = type.GetEnumValues();
        OreClusterData.oreTypes clusterType = (OreClusterData.oreTypes)UnityEngine.Random.Range(0, values.Length);
        Debug.Log(clusterType);
        OreClusterData randomClusterData = possibleClusters.Where(p => p.OreType == clusterType).First();
        return randomClusterData;
    }

    private bool IsPlaceOccupied(float x, float z)
    {
        return false;
    }

    private Vector3 GetClusterPosition(float x, float z)
    {
        float y = _terrain.SampleHeight(new Vector3(x, 0 ,z)) + clusterUnderstanding;
        Vector3 clusterPosition = new Vector3(x, y, z);
        Debug.Log(clusterPosition);
        return clusterPosition;
    }

    private void PlaceCluster(float terrainX, float terrainZ, OreClusterData clusterType)
    {
        var oreCluster = Instantiate(_oreCluster, GetClusterPosition(terrainX, terrainZ), Quaternion.identity, _terrain.transform);
        oreCluster.GetComponent<OreClusterBehavior>().oreClusterData = clusterType;
        oreCluster.name = clusterType.name;
    }
}
