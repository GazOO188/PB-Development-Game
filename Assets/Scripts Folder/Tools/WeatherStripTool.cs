using UnityEngine; 
using UnityEngine.InputSystem; 

public class WeatherStripTool : MonoBehaviour 
{
    // THE STRIP TO SPAWN//
    public GameObject stripPrefab;  

    // REFERENCE TO INPUTHANDLER SCRIPT//
    public InputHandler IH;         

    // THIS VECTOR3 STORES THE STARTING POSITION OR MOUSE POSITION OF WHERE THE DRAG STARTS//
    private Vector3 startPoint;

    // TRACKS WHETHER THE PLAYER IS CURRENTLY DRAGGING / APPLYING A STRIP
    private bool applying = false;

    // STORES THE CURRENT WEATHER STRIP GAMEOBJECT BEING PLACED
    private GameObject currentStrip;

    void Update() 
    {
     
    }

    // THIS FUNCTION HANDLES PLACING, ROTATING, AND SCALING THE WEATHER STRIP
    public void HandleWeatherStrip()
    {
        // SAFETY CHECK: IF INPUT HANDLER OR THE WEATHER STRIP ACTION IS NULL, EXIT THE FUNCTION
        if (IH == null || IH._WeatherStrip == null) return;

        // CONVERTS THE CURRENT MOUSE POSITION TO A 3D RAY//
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // IF THE RAY DOESN’T HIT ANY COLLIDER, EXIT FUNCTION
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        // STORE THE HIT POINT IN 3D SPACE
        Vector3 point = hit.point;

        // READ THE CURRENT VALUE OF THE WEATHER STRIP INPUT (0-1)
        float triggerValue = IH._WeatherStrip.ReadValue<float>();

        // START DRAG: DETECT THE FRAME WHEN THE INPUT IS FIRST PRESSED
        if (IH._WeatherStrip.triggered)
        {
            // RECORD THE STARTING POINT OF THE DRAG
            startPoint = point;   
        }

        // CALCULATE DIRECTION VECTOR FROM START TO CURRENT POINT
        Vector3 dir = point - startPoint;

        // LOCK THE STRIP TO HORIZONTAL SO IT DOSNET SCALE UP//
        dir.y = 0; 

        // CALCULATE THE LENGTH OF THE DRAG (DISTANCE BETWEEN THE START AND END OR CURRENT POINT)
        float length = dir.magnitude;

        // CONTINUE DRAGGING ONLY IF THE INPUT IS HELD AND DRAG DISTANCE IS BIG//
        if (triggerValue > 0 && length > 0.01f)
        {
            // SPAWN THE STRIP ONCE WHEN DRAG STARTS
            if (!applying)
            {
                // NOW DRAGGING//
                applying = true;

                // DEFAULT SPAWN DIRECTION: USE DRAG VECTOR, FALLBACK TO FORWARD IF ZERO
                Vector3 spawnDir = dir;
               
                if (spawnDir == Vector3.zero)
                {
                  spawnDir = Vector3.forward;
                }
                
                // SPAWN THE STRIP PREFAB, ROTATE IT SO X-AXIS POINTS ALONG DRAG DIRECTION
                currentStrip = Instantiate(stripPrefab, startPoint, Quaternion.LookRotation(spawnDir) * Quaternion.Euler(0, -90f, 0));
            }

            // MOVE THE STRIP TO THE MIDPOINT BETWEEN START AND CURRENT POINT
            //** SCALES FROM CENTER (HALF GOES LEF OTHER HALF GOES RIGHT **//
            currentStrip.transform.position = startPoint + dir / 2f;

            // ROTATE THE STRIP EVERY FRAME SO IT FOLLOWS THE DRAG DIRECTION
            currentStrip.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, -90f, 0);

            // SCALE THE STRIP ALONG LOCAL X AXIS TO MATCH DRAG LENGTH//
            // SCALES EVENLY AND SMOOTHLY ON BOTH SIDES//
            currentStrip.transform.localScale = new Vector3(length, currentStrip.transform.localScale.y, currentStrip.transform.localScale.z);
        }

        // STOP DRAGGING: DETECT WHEN INPUT IS RELEASED
        if (applying && IH._WeatherStrip.phase == InputActionPhase.Canceled)
        {
            // MARK THAT DRAGGING HAS ENDED//
            applying = false;

            // SPAWNS A NEW STRIP INSTANCE//
            currentStrip = null;
        }
    }
}