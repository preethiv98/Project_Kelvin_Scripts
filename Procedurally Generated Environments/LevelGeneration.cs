using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[System.Serializable]
public class TileCoordinates
{
    public int x;
    public int y;

    public TileCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class TopTileList
{
    public Tile topTile;
    public Tile bottomTile;
}

public class LevelGeneration : MonoBehaviour
{
    [Title("Number of Levels")]
    public int numberOfLevels = 5;

    [Title("Top Tiles")]
    [SerializeField]
    private List<TopTileList> tileList;

    int check = 6;
    [Title("Tiles")]
    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile botWallTile;
    [SerializeField]
    private Tile stairsTile;
    [Title("Tilemap")]
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private Tilemap stairsMap;
    [Title("Room")]

    public GameObject player;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;
    [Title("Spawning Enemies")]
    [SerializeField]
    public GameObject enemy;
    [SerializeField]
    private int numberOfEnemies = 5;
    [Title("Item Spawning")]
    [SerializeField]
    private GameObject health;
    [SerializeField]
    private GameObject special;
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private List<WeaponStats> weapons;
    [SerializeField]
    private List<GameObject> lights;


    [SerializeField]
    private List<Tile> graffiti;

    int healthCount;
    int specialCount;

    int numberofInstantiations = 0;

    int topTileCheck;
    bool instantiatedWeapon = false;
    bool stairsPut = false;
    private int routeCount = 0;

    int lightCount = 4;
    private void Start()
    {
        topTileCheck = tileList.Count-1;
        //TileTest2();
        StartGeneration();
    }

    public void StartGeneration()
    {
        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);
       
        FillWalls();
        TileTest2();
        // SpawnWeapons();

    }

    public void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBelow != null)
                    {
                        int rand = Random.Range(0, 4);
                        int randIndex = Random.Range(0, graffiti.Count);
                        Vector3Int posTopTwo = new Vector3Int(xMap, yMap + 1, 0);
                        if(rand == 3)
                        {
                            stairsMap.SetTile(pos, graffiti[randIndex]);
                        }
                        wallMap.SetTile(pos, tileList[topTileCheck].topTile);
                        wallMap.SetTile(posTopTwo, tileList[topTileCheck].bottomTile);
                        topTileCheck--;
                    }
                    else if (tileAbove != null)
                    {
                        wallMap.SetTile(pos, botWallTile);
                    }
                    if (topTileCheck == 0)
                    {
                        topTileCheck = 5;
                    }
                }
            }
        }
    }

    public void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3ove
                int roomSize = 1; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(3, 6);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }
    }

    public void GenerateSquare(int x, int y, int radius)
    {
        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                player.transform.position = tilePos;
                //if (Random.value <= weapons[3].occuranceRate && !instantiatedWeapon)
                //{
                //    Instantiate(weapons[3].weaponPrefab, tilePos, Quaternion.identity);
                //    instantiatedWeapon = true;
                //}
                //if (!stairsPut)
                //{
                //    stairsMap.SetTile(tilePos, stairsTile);
                //    stairsPut = true;
                //}
               
                groundMap.SetTile(tilePos, groundTile);
                
            }
           
           
        }
    }

    public void TileTest()
    {
        BoundsInt bounds = groundMap.cellBounds;
        TileBase[] allTiles = groundMap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
        //for (int i = 0; i < numberOfEnemies; i++)
        //{
        //    //tileLocList.RemoveAt(count);
        //    int count = Random.Range(0, allTiles.Length - 1);
        //    Vector3Int vec = new Vector3Int(allTiles[count].tran, tileLocList[count].y, 0);
        //    Instantiate(enemy, vec, Quaternion.identity).transform.parent = ItemParent.transform;
        //    //numberOfEnemies--;
        //}
        //Debug.Log(allTiles.Length);
    }

    public void TileTest2()
    {
        //groundMap = transform.GetComponentInParent<Tilemap>();
        List<WeaponStats> newWeapons = new List<WeaponStats>(weapons);
        List<GameObject> weaponsOccurance = new List<GameObject>();
        List<Vector3> availablePlaces = new List<Vector3>();
        int count;
        Vector3Int vec;
        for (int n = groundMap.cellBounds.xMin; n < groundMap.cellBounds.xMax; n++)
        {
            for (int p = groundMap.cellBounds.yMin; p < groundMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)groundMap.transform.position.y));
                Vector3 place = groundMap.CellToWorld(localPlace);
                if (groundMap.HasTile(localPlace) && !pitMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //tileLocList.RemoveAt(count);
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x+1, (int)availablePlaces[count].y+1, 0);
            Instantiate(enemy, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        count = Random.Range(0, availablePlaces.Count - 1);
        vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
        stairsMap.SetTile(vec, stairsTile);
        //stairsMap.gameObject.SetActive(false);
        //stairsMap.gameObject.SetActive(true);
        stairsMap.GetComponent<BoxCollider2D>().offset = new Vector2(vec.x + 0.5f, vec.y + 0.5f);
        //Debug.Log(groundMap.Count);
        numberofInstantiations = Random.Range(0, 2);
        count = Random.Range(0, availablePlaces.Count - 1);
        vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
        Debug.Log(weapons.Count);
        if (Random.value <= weapons[0].occuranceRate)
        {
            weaponsOccurance.Add(weapons[0].weaponPrefab);
            //Instantiate(weapons[0].weaponPrefab, vec, Quaternion.identity);
        }
        //tileLocList.RemoveAt(count);
        //count = Random.Range(0, tileLocList.Count - 1);
        //vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        if (Random.value <= weapons[2].occuranceRate)
        {
            weaponsOccurance.Add(weapons[2].weaponPrefab);
            //Instantiate(weapons[2].weaponPrefab, vec, Quaternion.identity);
        }
        //tileLocList.RemoveAt(count);
        //count = Random.Range(0, tileLocList.Count - 1);
        //vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        if (Random.value <= weapons[3].occuranceRate)
        {
            weaponsOccurance.Add(weapons[3].weaponPrefab);
            //Instantiate(weapons[3].weaponPrefab, vec, Quaternion.identity);
        }
        //tileLocList.RemoveAt(count);
        //count = Random.Range(0, tileLocList.Count - 1);
        //vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        if (Random.value <= weapons[1].occuranceRate)
        {
            weaponsOccurance.Add(weapons[1].weaponPrefab);
            //Instantiate(weapons[1].weaponPrefab, vec, Quaternion.identity);
        }
        for (int i = numberofInstantiations; i > 0; i--)
        {
            int randomValue = Random.Range(0, weapons.Count - 1);
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(newWeapons[randomValue].weaponPrefab, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            newWeapons.RemoveAt(randomValue);
        }

        if (SceneManager.GetActiveScene().name == "Level_One")
        {
            healthCount = Random.Range(0, 2);
            specialCount = Random.Range(0, 1);

        }
        else
        {
            healthCount = Random.Range(0, 1);
            specialCount = Random.Range(0, 1);
        }
        for (int i = 0; i < healthCount; i++)
        {
            //tileLocList.RemoveAt(count);
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(health, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        for (int i = 0; i < specialCount; i++)
        {
            availablePlaces.RemoveAt(count);
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(special, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        availablePlaces.RemoveAt(count);
    }

    public void SpawnWeapons()
    {
        Vector3 tilePosition;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);
        List<WeaponStats> newWeapons = new List<WeaponStats>(weapons);
        List<TileCoordinates> tileLocList = new List<TileCoordinates>();
        List<GameObject> weaponsOccurance = new List<GameObject>();
      
        for (int i = 0; i < groundMap.size.x; i++)
        {
            for (int j = 0; j < groundMap.size.y; j++)
            {
                if(groundMap.HasTile(new Vector3Int(i, j, 0)))
                {
                    coordinate.x = i; coordinate.y = j;

                    tilePosition = groundMap.CellToWorld(coordinate);
                    tileLocList.Add(new TileCoordinates((int)tilePosition.x, (int)tilePosition.y));
                    //Debug.Log(string.Format("Position of tile [{0}, {1}] = ({2}, {3})", coordinate.x, coordinate.y, tilePosition.x, tilePosition.y));
                }
               
            }
        }
        Debug.Log(tileLocList.Count);
        numberofInstantiations = Random.Range(0, 2);
        int count = Random.Range(0, tileLocList.Count-1);
        Vector3Int vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        Debug.Log(weapons.Count);
        if (Random.value <= weapons[0].occuranceRate)
        {
            weaponsOccurance.Add(weapons[0].weaponPrefab);
            //Instantiate(weapons[0].weaponPrefab, vec, Quaternion.identity);
        }
        //tileLocList.RemoveAt(count);
        //count = Random.Range(0, tileLocList.Count - 1);
        //vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        if (Random.value <= weapons[2].occuranceRate)
        {
            weaponsOccurance.Add(weapons[2].weaponPrefab);
            //Instantiate(weapons[2].weaponPrefab, vec, Quaternion.identity);
        }
        //tileLocList.RemoveAt(count);
        //count = Random.Range(0, tileLocList.Count - 1);
        //vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        if (Random.value <= weapons[3].occuranceRate)
        {
            weaponsOccurance.Add(weapons[3].weaponPrefab);
            //Instantiate(weapons[3].weaponPrefab, vec, Quaternion.identity);
        }
        //tileLocList.RemoveAt(count);
        //count = Random.Range(0, tileLocList.Count - 1);
        //vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        if (Random.value <= weapons[1].occuranceRate)
        {
            weaponsOccurance.Add(weapons[1].weaponPrefab);
            //Instantiate(weapons[1].weaponPrefab, vec, Quaternion.identity);
        }
        for(int i = numberofInstantiations; i > 0; i--)
        {
            int randomValue = Random.Range(0, weapons.Count-1);
            count = Random.Range(0, tileLocList.Count-1);
            vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
            Instantiate(newWeapons[randomValue].weaponPrefab, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            newWeapons.RemoveAt(randomValue);
        }

        if(SceneManager.GetActiveScene().name == "Level_One")
        {
            healthCount = Random.Range(0, 2);
            specialCount = Random.Range(0, 1);

        }
        else
        {
            healthCount = Random.Range(0, 1);
            specialCount = Random.Range(0, 1);
        }

        tileLocList.RemoveAt(count);
        count = Random.Range(0, tileLocList.Count-1);
        vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        stairsMap.SetTile(vec, stairsTile);
        //stairsMap.gameObject.SetActive(false);
        //stairsMap.gameObject.SetActive(true);
        stairsMap.GetComponent<BoxCollider2D>().offset = new Vector2(vec.x+0.5f, vec.y + 0.5f);
        //for (int i = 0; i < lightCount; i++)
        //{
        //    tileLocList.RemoveAt(count);
        //    count = Random.Range(0, tileLocList.Count - 1);
        //    vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
        //    Instantiate(lights[0], vec, Quaternion.identity).transform.parent = ItemParent.transform;
        //    //numberOfEnemies--;
        //}
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //tileLocList.RemoveAt(count);
            count = Random.Range(0, tileLocList.Count-1);
            vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
            Instantiate(enemy, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        for (int i = 0; i < healthCount; i++)
        {
            //tileLocList.RemoveAt(count);
            count = Random.Range(0, tileLocList.Count - 1);
            vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
            Instantiate(health, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        for (int i = 0; i < specialCount; i++)
        {
            tileLocList.RemoveAt(count);
            count = Random.Range(0, tileLocList.Count - 1);
            vec = new Vector3Int(tileLocList[count].x, tileLocList[count].y, 0);
            Instantiate(special, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
    }

    public void Reset()
    {
        
    
        pitMap.ClearAllTiles();
        groundMap.ClearAllTiles();
        stairsMap.ClearAllTiles();
        wallMap.ClearAllTiles();
        stairsPut = false;
        instantiatedWeapon = false;
        foreach (Transform child in ItemParent.transform)
        {
            Destroy(child.gameObject);
        }

    }

    //[MenuItem("Level Generation/Generate Level")]
    //public void GenerateLevel()
    //{
    //    //int check = 6;
    //    topTileCheck = tileList.Count - 1;
    //    StartGeneration();
    //    GameObject grid = GameObject.FindGameObjectWithTag("Grid");


    //    // Keep track of the currently selected GameObject(s)
    //    //GameObject[] objectArray = Selection.gameObjects;

    //    // Loop through every GameObject in the array above
    //    //foreach (GameObject gameObject in objectArray)
    //    //{
    //        // Set the path as within the Assets folder,
    //        // and name it as the GameObject's name with the .Prefab format
    //        string localPath = "Assets/Prefabs/FloorPrefabs" + "Floor" + "_" + check + ".prefab";

    //        // Make sure the file name is unique, in case an existing Prefab has the same name.
    //        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

    //        // Create the new Prefab.
    //        PrefabUtility.SaveAsPrefabAssetAndConnect(grid, localPath, InteractionMode.UserAction);
    //    check++;
    //    //}
    //}


    //public void SpawnWeapons()
    //{

    //}
}