using Fungus;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SpawnRoom : MonoBehaviour
{
    [Title("Decor")]
    private List<Tile> numberLabel;

    [SerializeField]
    private GameObject report;

    //[SerializeField]
    //private GameObject bossRoom;

    [SerializeField]
    private List<GameObject> computerPartsList;

    [SerializeField]
    private GameObject clothes;

    [SerializeField]
    private List<Tile> numberTile;

    [Title("Room Elements")]
    [SerializeField]
    private List<GameObject> rooms;
    [SerializeField]
    private List<GameObject> bossRooms;


    [Title("Number of Levels")]
    public int numberOfLevels = 5;
    public int currentLevel = 1;

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
    private List<GameObject> enemy;
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

        
       
        StartGeneration();
  
    }

    public void StartGeneration()
    {
        topTileCheck = tileList.Count - 1;
        Vector3 vec = new Vector3(0, 0, 0);
        player.transform.position = vec;
        InstantiateRoom();
        TileTest2();
        //groundMap = GameObject.Get
        //TileTest2();
        // SpawnWeapons();

    }

    public void InstantiateRoom()
    {
        int rand = Random.Range(0, rooms.Count);
        Instantiate(rooms[rand]);
    }


    public void TileTest2()
    {
        groundMap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        pitMap = GameObject.FindGameObjectWithTag("Pit").GetComponent<Tilemap>();
        wallMap = GameObject.FindGameObjectWithTag("Walls").GetComponent<Tilemap>();
        stairsMap = GameObject.FindGameObjectWithTag("Stairs").GetComponent<Tilemap>();
        //groundMap = transform.GetComponentInParent<Tilemap>();
        List<WeaponStats> newWeapons = new List<WeaponStats>(weapons);
        List<GameObject> weaponsOccurance = new List<GameObject>();
        List<Vector3> availablePlaces = new List<Vector3>();
        List<Vector3> wallAvailable = new List<Vector3>();
        int count;
        Vector3Int vec;
       
        for (int n = groundMap.cellBounds.xMin; n < groundMap.cellBounds.xMax; n++)
        {
            for (int p = groundMap.cellBounds.yMin; p < groundMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)groundMap.transform.position.y));
                Vector3 place = groundMap.CellToWorld(localPlace);
                if (groundMap.HasTile(localPlace) && !pitMap.HasTile(localPlace) && !wallMap.HasTile(localPlace) && !stairsMap.HasTile(localPlace))
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
        for (int n = wallMap.cellBounds.xMin; n < wallMap.cellBounds.xMax; n++)
        {
            for (int p = wallMap.cellBounds.yMin; p < wallMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)wallMap.transform.position.y));
                Vector3 place = groundMap.CellToWorld(localPlace);
                if (wallMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    wallAvailable.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        int check = Random.Range(1, 5);
        
        while(check != 0)
        {
            count = Random.Range(0, wallAvailable.Count - 1);
            vec = new Vector3Int((int)wallAvailable[count].x, (int)wallAvailable[count].y, 0);
            if(GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().level <= 15)
            {
                stairsMap.SetTile(vec, numberTile[GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().level]);
            }
            
            check--;
        }

        int level = currentLevel;
        Flowchart flow = GameObject.FindGameObjectWithTag("Game Flowchart").GetComponent<Flowchart>();
        if ((level == 1 && !flow.GetBooleanVariable("ComputerPart1Found")) || (level == 2 && !flow.GetBooleanVariable("ComputerPart2Found")) || (level == 5 && !flow.GetBooleanVariable("ComputerPart4Found")))
        {
            if (level == 1)
            {
                player.GetComponent<ReportContact>().mission.text = "Find Report 0.2";
            }
            if (level == 2)
            {
                player.GetComponent<ReportContact>().mission.text = "Find Report 0.3";
            }
            if (level == 5)
            {
                player.GetComponent<ReportContact>().mission.text = "Find Report 0.4";
            }
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(report, vec, Quaternion.identity).transform.parent = ItemParent.transform;

        }
        else if (level == 3 && !flow.GetBooleanVariable("ComputerPart3Found"))
        {
            player.GetComponent<ReportContact>().mission.text = "Find Computer Part 3";
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(computerPartsList[2], vec, Quaternion.identity).transform.parent = ItemParent.transform;
        }
        else if (level == 4 && !flow.GetBooleanVariable("ClothesFound"))
        {
            player.GetComponent<ReportContact>().mission.text = "Find Clothes";
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(clothes, vec, Quaternion.identity).transform.parent = ItemParent.transform;
        }

        for (int i = 0; i <= numberOfEnemies; i++)
        {
            //tileLocList.RemoveAt(count);
            count = Random.Range(0, availablePlaces.Count - 1);
            int enemySelect = Random.Range(0, enemy.Count);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(enemy[enemySelect], vec, Quaternion.identity).transform.parent = ItemParent.transform;
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
            healthCount = Random.Range(0, 1);
            specialCount = Random.Range(0, 1);

        }
        else
        {
            healthCount = Random.Range(0, 1);
            specialCount = Random.Range(0, 1);
        }
        for (int i = 0; i <= healthCount; i++)
        {
            //tileLocList.RemoveAt(count);
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(health, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        for (int i = 0; i <= specialCount; i++)
        {
            availablePlaces.RemoveAt(count);
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(special, vec, Quaternion.identity).transform.parent = ItemParent.transform;
            //numberOfEnemies--;
        }
        availablePlaces.RemoveAt(count);
    }

    public void DeleteRoom()
    {
        pitMap.ClearAllTiles();
        groundMap.ClearAllTiles();
        stairsMap.ClearAllTiles();
        wallMap.ClearAllTiles();
        groundMap.CompressBounds();
        stairsPut = false;
        instantiatedWeapon = false;
        foreach (Transform child in ItemParent.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(GameObject.FindGameObjectWithTag("Grid"));
    }


    public void AddComputerPart()
    {
        groundMap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        pitMap = GameObject.FindGameObjectWithTag("Pit").GetComponent<Tilemap>();
        wallMap = GameObject.FindGameObjectWithTag("Walls").GetComponent<Tilemap>();
        stairsMap = GameObject.FindGameObjectWithTag("Stairs").GetComponent<Tilemap>();
        
        List<Vector3> availablePlaces = new List<Vector3>();

        int count;
        Vector3Int vec;
        int level = currentLevel;

        for (int n = groundMap.cellBounds.xMin; n < groundMap.cellBounds.xMax; n++)
        {
            for (int p = groundMap.cellBounds.yMin; p < groundMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)groundMap.transform.position.y));
                Vector3 place = groundMap.CellToWorld(localPlace);
                if (groundMap.HasTile(localPlace) && !pitMap.HasTile(localPlace) && !wallMap.HasTile(localPlace) && !stairsMap.HasTile(localPlace))
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
        if (level == 1)
        {
            player.GetComponent<ReportContact>().mission.text = "Find Computer Part 1";
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(computerPartsList[0], vec, Quaternion.identity).transform.parent = ItemParent.transform;
        }
        else if (level == 2)
        {
            player.GetComponent<ReportContact>().mission.text = "Find Computer Part 2";
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(computerPartsList[1], vec, Quaternion.identity).transform.parent = ItemParent.transform;
        }
        else if (level == 5)
        {
            player.GetComponent<ReportContact>().mission.text = "Find Computer Part 4";
            count = Random.Range(0, availablePlaces.Count - 1);
            vec = new Vector3Int((int)availablePlaces[count].x, (int)availablePlaces[count].y, 0);
            Instantiate(computerPartsList[3], vec, Quaternion.identity).transform.parent = ItemParent.transform;
        }
       
    
    }

    //public void Reset()
    //{


     

    //}
}
