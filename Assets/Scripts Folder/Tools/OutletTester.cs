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
        tester.transform.rotation = currentOutlet.transform.rotation;

        yield return new WaitForSeconds(2);

        testLight.GetComponent<Renderer>().material.color = Color.green;

        yield return new WaitForSeconds(1);

        testLight.GetComponent<Renderer>().material.color = Color.white;

        tester.transform.position = holder.position;
        tester.transform.rotation = holder.rotation;

        PlayerController.Instance.playerControl = true;
        PlayerController.Instance.toolInUse = false;
    }
}
