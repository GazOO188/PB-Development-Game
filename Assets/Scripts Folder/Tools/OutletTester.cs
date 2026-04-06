using UnityEngine;
using System.Collections;

public class OutletTester : MonoBehaviour
{
    [SerializeField] Transform holder;
    [SerializeField] GameObject tester, testLight;
    [SerializeField] float yRot;
    bool inProgress;

    public IEnumerator TestOutlets(GameObject currentOutlet)
    {
        PlayerController.Instance.playerControl = false;
        PlayerController.Instance.toolInUse = true;

        tester.transform.position = currentOutlet.transform.GetChild(0).transform.position;
        Quaternion q = currentOutlet.transform.GetChild(0).transform.rotation;
        tester.transform.rotation = q;

        yield return new WaitForSeconds(2);

        if (currentOutlet.name == "Working Outlet")
            testLight.GetComponent<Renderer>().material.color = Color.green;
        else
            testLight.GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(1);

        testLight.GetComponent<Renderer>().material.color = Color.white;

        tester.transform.position = holder.position;
        tester.transform.rotation = holder.rotation;

        PlayerController.Instance.playerControl = true;
        PlayerController.Instance.toolInUse = false;
    }
}
