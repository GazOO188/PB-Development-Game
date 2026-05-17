using UnityEngine;
using System.Collections;

public class OutletTester : MonoBehaviour
{
    [SerializeField] Transform holder;
    [SerializeField] GameObject tester, testLight;
    [SerializeField] float yRot;
    bool inProgress;
    Outlet outlet;

    void Start()
    {
        outlet = GetComponent<Outlet>();
    }

    public IEnumerator TestOutlets(GameObject currentOutlet)
    {
        PlayerController.Instance.playerControl = false;
        PlayerController.Instance.toolInUse = true;

        Vector3 target = currentOutlet.transform.GetChild(0).transform.position;
        tester.transform.position = target + currentOutlet.transform.GetChild(0).transform.up * 0.5f;

        Quaternion q = Quaternion.Euler(270f, 0f, 0f);//currentOutlet.transform.GetChild(0).transform.rotation;
        tester.transform.rotation = q;

        yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            float time = elapsed / duration;
            tester.transform.position = Vector3.Lerp(tester.transform.position, target, time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        tester.transform.position = target;

        yield return new WaitForSeconds(1.5f);

        if (currentOutlet.name == "Working Outlet")
            testLight.GetComponent<Renderer>().material.color = Color.green;
        else
            testLight.GetComponent<Renderer>().material.color = Color.red;

        outlet.outletTested = true;

        yield return new WaitForSeconds(0.7f);

        testLight.GetComponent<Renderer>().material.color = Color.white;

        float elapsed2 = 0f;
        float duration2 = 0.6f;

        while (elapsed2 < duration2)
        {
            float time = elapsed2 / duration2;
            tester.transform.position = Vector3.Lerp(tester.transform.position, target + currentOutlet.transform.GetChild(0).transform.up * 0.5f, time);
            elapsed2 += Time.deltaTime;
            yield return null;
        }

        tester.transform.position = holder.position;
        tester.transform.rotation = holder.rotation;

        PlayerController.Instance.playerControl = true;
        PlayerController.Instance.toolInUse = false;
    }
}
