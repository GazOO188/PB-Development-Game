using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SprayFoam : MonoBehaviour 
{
    public GameObject foamPrefab;  
    public InputHandler IH;         

    [Header("Spray Settings")]
    public float spawnRate = 0.03f;
    public float positionJitter = 0.02f;

    [Header("Growth Settings")]
    public float growthSpeed = 1.5f;
    public float maxSize = 1.2f;

    private float nextSpawnTime;
    private bool applying = false;

    // Track all spawned foam blobs
    private List<FoamData> foamBlobs = new List<FoamData>();

    void Update() 
    {
       
    }

    public void HandleFoamSpray()
    {
        if (IH == null || IH._SprayFoam == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        Vector3 point = hit.point;
        Vector3 normal = hit.normal;
        float triggerValue = IH._SprayFoam.ReadValue<float>();

        // START
        if (IH._SprayFoam.triggered)
        {
            applying = true;
        }

        // SPRAYING
        if (applying && triggerValue > 0 && Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;

            Vector3 offset = Random.insideUnitSphere * positionJitter;
            Quaternion rotation = Quaternion.LookRotation(normal);

            GameObject foam = Instantiate(foamPrefab, point + offset, rotation);

            // Create foam data
            FoamData data = new FoamData();
            data.obj = foam;
            data.currentSize = 0.05f;

            //
            data.randomScale = new Vector3(Random.Range(0.5f, 0.5f), Random.Range(0.5f, 0.5f), Random.Range(0.5f, 0.5f));

            foam.transform.localScale = Vector3.one * data.currentSize;

            foamBlobs.Add(data);
        }

        // STOP
        if (applying && IH._SprayFoam.phase == InputActionPhase.Canceled)
        {
            applying = false;
        }
    }

    public void UpdateFoamGrowth()
    {
        for (int i = 0; i < foamBlobs.Count; i++)
        {
            FoamData data = foamBlobs[i];

            if (data.obj == null) continue;

            if (data.currentSize < maxSize)
            {
                // smooth growth over time
                data.currentSize += growthSpeed * Time.deltaTime;

                float size = Mathf.Min(data.currentSize, maxSize);

                // apply random organic scaling
                Vector3 finalScale = Vector3.Scale(Vector3.one * size, data.randomScale);

                data.obj.transform.localScale = finalScale;
            }
        }
    }

    // THIS CLASS TRACKS THE SIZE OF EACH FOAM BLOB//
    class FoamData
    {
        public GameObject obj;
        public float currentSize;
        public Vector3 randomScale;
    }
}