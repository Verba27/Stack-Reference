using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private MoveObject mover;
    [SerializeField] private PositionChecker positionChecker;
    [SerializeField] private GameObject firstObject;
    [SerializeField] private Camera camera;
    private GameObject objectToSpawn;
    private GameObject fallingPiece;
    private float coordinateY;
    private GameObject movingPlate;
    private Vector3[] spawnPoints;
    private bool isPlaying;

    void Start()
    {
        positionChecker.onGameOver += GameOverInform;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPlaying)
        {
            mover.StopRoutine();
            if (objectToSpawn == null)
            {
                objectToSpawn = firstObject;
            }
            
            SpawnNewPlate(objectToSpawn);
            camera.transform.position += new Vector3(0,1,0);
        }
    }

    public void IsOnPlay(bool play)
    {
        isPlaying = play;
    }

    public void SpawnNewPlate(GameObject obj)
    {
        spawnPoints = new Vector3[4];
        spawnPoints[0] = new Vector3(12, coordinateY, obj.transform.position.z);
        spawnPoints[1] = new Vector3(-12, coordinateY, obj.transform.position.z);
        spawnPoints[2] = new Vector3(obj.transform.position.x, coordinateY, 12);
        spawnPoints[3] = new Vector3(obj.transform.position.x, coordinateY, -12);
        Random rnd = new Random();
        int randomPoint = rnd.Next(0, 4);
        coordinateY += 1;
        
        movingPlate = Instantiate(objectToSpawn, spawnPoints[randomPoint], Quaternion.identity);
        onInstance?.Invoke(true);
        movingPlate.name = coordinateY.ToString();
        mover.StartRoutine(movingPlate);
    }
    private void GameOverInform(bool instance)
    {
        onGameOver?.Invoke(true);
    }
    public void SpawnRemainingPiece(GameObject obj)
    {
        objectToSpawn = obj;
        positionChecker.PreviousPositionSave(objectToSpawn);
    }

    public void SpawnFallingPiece(GameObject obj, Vector3 pos, Vector3 scale)
    {
        fallingPiece = Instantiate(obj, pos, Quaternion.identity);
        fallingPiece.transform.localScale = scale;
        fallingPiece.name = "cuted " + coordinateY;
        fallingPiece.AddComponent<Rigidbody>();
        Handheld.Vibrate();
    }

    public delegate void Instanced(bool instance);
    public event Instanced onInstance;

    public delegate void Finish(bool gamover);
    public event Finish onGameOver;
}
