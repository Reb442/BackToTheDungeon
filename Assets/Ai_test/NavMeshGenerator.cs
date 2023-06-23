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
        // NavMesh verisini olu�turun ve NavMeshDataInstance al�n
        navMeshData = new NavMeshData();
        navMeshDataInstance = NavMesh.AddNavMeshData(navMeshData);

        // NavMesh verisini hesaplamak i�in BuildNavMeshData y�ntemini �a��r�n
        BuildNavMeshData();
    }

    private void BuildNavMeshData()
    {
        // NavMesh'i hesaplamak i�in NavMeshBuilder.UpdateNavMeshData metodunu kullan�n
        NavMeshData tempNavMeshData = NavMeshBuilder.BuildNavMeshData(GetBuildSettings(), CollectBuildSources(), GetBuildBounds(), transform.position, Quaternion.identity);

        // Hesaplanan NavMesh verisini NavMeshDataInstance'a atay�n
        navMeshDataInstance.Remove();
        navMeshDataInstance = NavMesh.AddNavMeshData(tempNavMeshData);
    }

    private NavMeshBuildSettings GetBuildSettings()
    {
        // NavMesh in�a ayarlar�n� yap�land�r�n
        NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0);
        return buildSettings;
    }

    private List<NavMeshBuildSource> CollectBuildSources()
    {
        // NavMesh in�a kaynaklar�n� toplay�n (genellikle statik MeshColliders veya Terrains)
        // Gerekli kaynaklar� koleksiyona ekleyin ve List<NavMeshBuildSource> listesini d�nd�r�n

        List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();

        // MeshCollider bile�enine sahip t�m nesnelerin build source'lar�n� topla
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

        // Terrain bile�enine sahip t�m nesnelerin build source'lar�n� topla
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
        // NavMesh in�a s�n�rlar�n� belirleyin (NavMesh'in hangi alanlar� kaplayaca��)
        // Gerekli s�n�rlar� d�nd�r�n
        return new Bounds(Vector3.zero, Vector3.one * 100f);
    }
}
