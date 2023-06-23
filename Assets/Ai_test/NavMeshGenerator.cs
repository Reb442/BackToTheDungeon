using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NavMeshGenerator : MonoBehaviour
{
    private NavMeshData navMeshData;
    private NavMeshDataInstance navMeshDataInstance;

    private void Start()
    {
        // NavMesh verisini oluþturun ve NavMeshDataInstance alýn
        navMeshData = new NavMeshData();
        navMeshDataInstance = NavMesh.AddNavMeshData(navMeshData);

        // NavMesh verisini hesaplamak için BuildNavMeshData yöntemini çaðýrýn
        BuildNavMeshData();
    }

    private void BuildNavMeshData()
    {
        // NavMesh'i hesaplamak için NavMeshBuilder.UpdateNavMeshData metodunu kullanýn
        NavMeshData tempNavMeshData = NavMeshBuilder.BuildNavMeshData(GetBuildSettings(), CollectBuildSources(), GetBuildBounds(), transform.position, Quaternion.identity);

        // Hesaplanan NavMesh verisini NavMeshDataInstance'a atayýn
        navMeshDataInstance.Remove();
        navMeshDataInstance = NavMesh.AddNavMeshData(tempNavMeshData);
    }

    private NavMeshBuildSettings GetBuildSettings()
    {
        // NavMesh inþa ayarlarýný yapýlandýrýn
        NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0);
        return buildSettings;
    }

    private List<NavMeshBuildSource> CollectBuildSources()
    {
        // NavMesh inþa kaynaklarýný toplayýn (genellikle statik MeshColliders veya Terrains)
        // Gerekli kaynaklarý koleksiyona ekleyin ve List<NavMeshBuildSource> listesini döndürün

        List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();

        // MeshCollider bileþenine sahip tüm nesnelerin build source'larýný topla
        MeshCollider[] meshColliders = FindObjectsOfType<MeshCollider>();
        foreach (MeshCollider meshCollider in meshColliders)
        {
            if (meshCollider.sharedMesh == null)
                continue;

            NavMeshBuildSource buildSource = new NavMeshBuildSource();
            buildSource.shape = NavMeshBuildSourceShape.Mesh;
            buildSource.sourceObject = meshCollider.sharedMesh;
            buildSource.transform = meshCollider.transform.localToWorldMatrix;
            buildSources.Add(buildSource);
        }

        // Terrain bileþenine sahip tüm nesnelerin build source'larýný topla
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
        {
            NavMeshBuildSource buildSource = new NavMeshBuildSource();
            buildSource.shape = NavMeshBuildSourceShape.Terrain;
            buildSource.sourceObject = terrain.terrainData;
            buildSource.transform = terrain.transform.localToWorldMatrix;
            buildSources.Add(buildSource);
        }

        return buildSources;
    }

    private Bounds GetBuildBounds()
    {
        // NavMesh inþa sýnýrlarýný belirleyin (NavMesh'in hangi alanlarý kaplayacaðý)
        // Gerekli sýnýrlarý döndürün
        return new Bounds(Vector3.zero, Vector3.one * 100f);
    }
}
