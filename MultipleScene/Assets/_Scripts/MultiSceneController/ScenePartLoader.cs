using UnityEngine;
using UnityEngine.SceneManagement;

public enum CheckMethod
{
    Distance,
    Trigger
}
public class ScenePartLoader : MonoBehaviour
{
    [Header("General Settings")]
    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;

    [Header("Local Physics Settings")]
    [SerializeField] bool localPhysics;
    [SerializeField] float physicsSceneTimeScale = 1;

    //Scene state
    private bool isLoaded;
    private bool shouldLoad;
    private bool loadedPhysicsScene;
    private PhysicsScene physicsScene;

    void Start()
    {
        //verify if the scene is already open to avoid opening a scene twice
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(loadedPhysicsScene)
        {
            physicsScene.Simulate(Time.fixedDeltaTime * physicsSceneTimeScale);
        }
    }

    void Update()
    {
        //Checking which method to use
        if (checkMethod == CheckMethod.Distance)
        {
            DistanceCheck();
        }
        else if (checkMethod == CheckMethod.Trigger)
        {
            TriggerCheck();
        }
    }

    void DistanceCheck()
    {
        //Checking if the player is within the range
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }

    void LoadScene()
    {
        if (!isLoaded)
        {
            //Loading the scene, using the gameobject name as it's the same as the name of the scene to load
            if (localPhysics)
            {
                LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
                SceneManager.LoadSceneAsync(gameObject.name, param).completed += handle =>
                {
                    Scene scene = SceneManager.GetSceneByName(gameObject.name);
                    physicsScene = scene.GetPhysicsScene();
                    loadedPhysicsScene = true;
                };
            }
            else
            {
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            }

            //We set it to true to avoid loading the scene twice
            isLoaded = true;
        }
    }

    void UnLoadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            if (loadedPhysicsScene)
            {
                loadedPhysicsScene = false;
            }
            isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }

    void TriggerCheck()
    {
        //shouldLoad is set from the Trigger methods
        if (shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }
}

