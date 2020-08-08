using UnityEngine;

public class DynamicTerrain : MonoBehaviour
{
    public Texture2D HeightMapTexture;

	private void Start ()
    {
        BuildTerrain();
	}
	
    private void BuildTerrain()
    {
        Terrain terrain = gameObject.AddComponent<Terrain>();
        TerrainCollider terrainCollider = gameObject.AddComponent<TerrainCollider>();
        TerrainData terrainData = new TerrainData();
        terrainCollider.terrainData = terrainData;
       

        terrain.terrainData = terrainData;
        BuildTerrainData(terrainData);
    }

    private void BuildTerrainData(TerrainData terrainData)
    {
        terrainData.heightmapResolution = 512 + 1;
        terrainData.baseMapResolution = 512 + 1;
        terrainData.SetDetailResolution(1024, 32);

        // Set the terrain size after resolution
        terrainData.size = new Vector3(1024, 512, 1024);

        float [,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        Color[] heightMapPixels = HeightMapTexture.GetPixels();

        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int y = 0; y < terrainData.heightmapHeight; y++)
            {
                float xPosition = x / (float)x/terrainData.heightmapWidth;
                float yPosition = y / (float)y/terrainData.heightmapHeight;

                Color pixels = heightMapPixels[
                    Mathf.FloorToInt(xPosition * HeightMapTexture.width) +
                    Mathf.FloorToInt(yPosition * HeightMapTexture.height) * HeightMapTexture.width
                    ];

                heights[x, y] = pixels.grayscale;
            }

            terrainData.SetHeights(0, 0, heights);
        }
    }
}
