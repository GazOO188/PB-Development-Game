using System.Collections.Generic;
using UnityEngine;

public class CaulkGun : MonoBehaviour
{
    // REFERENCE TO THE CAMERA, WHERE THE PLAYER IS LOOKING. USED FOR CASTING A RAY FROM THE CAMERA
    public Camera playerCamera;

    // THE CAULK PREFAB TO SPAWN
    public GameObject caulkPrefab;

    // MAX DISTANCE FOR CAULKING
    public float maxDistance = 5f;

    // TIME BETWEEN SPAWNS
    public float sprayRate = 0.05f;

    // LAST TIME CAULK WAS APPLIED
    public float lastUsedTime = 0f;

    // PREVIOUS POINT ON SURFACE
    private Vector3 lastPoint;

    // TRACK IF WE HAVE A PREVIOUS POINT
    public bool hasLastPoint = false;

    // TRACK SEGMENTS OF THE CURRENT LINE
    private List<GameObject> currentLineSegments = new List<GameObject>();

    void Update()
    {
        
    }

    public void ShootCaulk()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            //GETS THE CURRENT HIT POINT//
            Vector3 currentPoint = hit.point;

            // STARTS A NEW LINE, DELETES PREVIOUS LINE//
            if (!hasLastPoint)
            {
                ClearCurrentLine();
                lastPoint = currentPoint;
                hasLastPoint = true;
                return;
            }

            // CREATE CAULK LINE SEGMENT BETWEEN LAST AND CURRENT POINT
            //THIS LINE SERVES AS THE CALLER -> ASKS THE METHOD/FUNCTION TO MAKE A SEGMENT//
            GameObject segment = CreateCaulkSegment(lastPoint, currentPoint, hit.normal);

            //ADDS THE SEGMENT TO THE LIST FOR CLEARING//
            if (segment != null)
                currentLineSegments.Add(segment);

            // UPDATE LAST POINT
            lastPoint = currentPoint;
        }
    }

    // CREATES A CAULK SEGMENT AND RETURNS THE GAMEOBJECT
    GameObject CreateCaulkSegment(Vector3 start, Vector3 end, Vector3 normal)
    {
        //GETS THE DIRECTION FROM THE STARTING POINT ON THE WALL TO THE END//
        Vector3 direction = end - start;

        //GETS THE LENGTH OR DISTANCE OF THE DIRECTION//
        float distance = direction.magnitude;

        // IGNORES TINY MOVEMENT
        if (distance < 0.01f)
            return null;

        //GETS THE MIDPOINT BETWEEN THE START AND END POINTS//
        Vector3 middle = (start + end) / 2f;


        //SPAWNS THE CAULK AT THE MIDDLE//
        GameObject segment = Instantiate(caulkPrefab, middle, Quaternion.identity);

        // SCALES THE SEGMENT, UNITY SCALES FROM THE CENTER//

        //IF YOU PUT DISTANCE IN X -> THE CAULK WOULD STRETCH SIDEWAYS//
        //IF YOU PUT DISTANCE IN Y -> THE CAULK WOULD STRETCH VERTICALLY//
        //IF YOU PUT DISTANCE IN Z -> THE CAULK WOULD STRETCH FROM START TO END//
        segment.transform.localScale = new Vector3(0.04f, 0.04f,distance);

        // ROTATE SEGMENT TO MATCH THE SURFACE
        segment.transform.rotation = Quaternion.LookRotation(direction.normalized, normal);

        //THIS LINE RETURNS THE SEGMENT BACK TO THE FUNCTION THAT CALLED THIS FUNCTION THE: GameObject segment LINE//
        return segment;
    }

    // DELETE ALL SEGMENTS OF THE CURRENT LINE
    void ClearCurrentLine()
    {
        foreach (GameObject segment in currentLineSegments)
        {
            if (segment != null)
                Destroy(segment);
        }
        currentLineSegments.Clear();
    }
}