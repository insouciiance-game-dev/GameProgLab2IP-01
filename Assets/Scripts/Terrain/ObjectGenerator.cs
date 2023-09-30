using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class ObjectGenerator : MonoBehaviour
{
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public GameObject PrefabStorage { get; private set; }

    [field: SerializeField]
    public int ObjectCount { get; private set; }

    [field: SerializeField]
    public float MaxHeight { get; private set; }

    public Terrain Terrain { get; private set; }

    private void Start()
    {
        Terrain = GetComponent<Terrain>();

        for (int i = 0; i < ObjectCount; i++)
            GenerateObject();
    }

    private void GenerateObject()
    {
        var position = GetRandomPositionOnTerrain();
        Instantiate(Prefab, position, Quaternion.identity, PrefabStorage.transform);
        
        Vector3 GetRandomPositionOnTerrain()
        {
            while (true)
            {
                var terrainPosition = Terrain.GetPosition();
                float randomX = Random.Range(terrainPosition.x, terrainPosition.x + Terrain.terrainData.size.x);
                float randomZ = Random.Range(terrainPosition.z, terrainPosition.z + Terrain.terrainData.size.z);

                float y = Terrain.SampleHeight(new((int)randomX, 0, (int)randomZ));

                if (y < MaxHeight)
                    return new Vector3(randomX, y, randomZ);
            }
        }
    }
}
