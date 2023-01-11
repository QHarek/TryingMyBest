using System.Linq;
using UnityEngine;

public sealed class OrePlacer : MonoBehaviour
{
    [SerializeField] private GameObject _oreClusterPrefab;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private int _startClusterCount;
    [SerializeField] private float _clusterDeeping;
    [SerializeField] private int _copperOreSpawnChance;
    [SerializeField] private int _ironOreSpawnChance;
    [SerializeField] private int _goldOreSpawnChance;
    [SerializeField] private OreClusterData[] _possibleClusters;

    private void Start()
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
        OreClusterData clusterType = _possibleClusters.Where(p => p.OreType == oreType).First();
        while (IsPlaceOccupied(clusterPosition))
            clusterPosition = GenerateRandomClusterPosition();
        PlaceCluster(clusterPosition, clusterType);
    }

    private OreClusterData RandomClusterType()
    {
        int chacesSumm = _copperOreSpawnChance + _ironOreSpawnChance + _goldOreSpawnChance;
        int oreRoll = Random.Range(0, chacesSumm);
        OreClusterData.oreTypes oreType;

        if (oreRoll > _copperOreSpawnChance)
            if (oreRoll > _copperOreSpawnChance + _ironOreSpawnChance)
                oreType = OreClusterData.oreTypes.goldOre;
            else
                oreType = OreClusterData.oreTypes.ironOre;
        else
            oreType = OreClusterData.oreTypes.copperOre;

        OreClusterData randomClusterData = _possibleClusters.Where(p => p.OreType == oreType).First();
        return randomClusterData;
    }

    private Vector3 GenerateRandomClusterPosition()
    {
        Vector3 clusterPosition = new Vector3(
            Random.Range(0, _terrain.terrainData.size.x),
            0,
            Random.Range(0, _terrain.terrainData.size.z));
        clusterPosition = GetClusterTerrainPosition(clusterPosition.x, clusterPosition.z);
        return clusterPosition;
    }

    private bool IsPlaceOccupied(Vector3 possiblePosition)
    {
        if (Physics.OverlapSphere(possiblePosition, 10, LayerMask.GetMask("Ore Clusters")).Length == 0)
            return false;
        else
            return true;
    }

    private Vector3 GetClusterTerrainPosition(float x, float z)
    {
        float y = _terrain.SampleHeight(new Vector3(x, 0 ,z));
        Vector3 clusterPosition = new Vector3(x, y, z);
        return clusterPosition;
    }

    private void PlaceCluster(Vector3 clusterPosition, OreClusterData clusterType)
    {
        float normalizedX = clusterPosition.x / _terrain.terrainData.size.x;
        float normalizedZ = clusterPosition.z / _terrain.terrainData.size.z;
        Vector3 terrainNormal = _terrain.terrainData.GetInterpolatedNormal(normalizedX, normalizedZ);
        Quaternion initRotation = Quaternion.LookRotation(terrainNormal) * Quaternion.Euler(90, 0, 0);
        var oreCluster = Instantiate(_oreClusterPrefab, clusterPosition, initRotation, _terrain.transform);
        oreCluster.transform.localPosition += new Vector3(0, -_clusterDeeping, 0);
        oreCluster.GetComponent<OreClusterBehavior>().ClusterData = clusterType;
        oreCluster.name = clusterType.name;
    }
}
