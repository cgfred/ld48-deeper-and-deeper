using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNodeHandler : MonoBehaviour
{
    public bool isPowerNodeActive = false;

    public PowerNodeHandler nextPowerNode;
 
    public Renderer powerSourceRenderer;

    [Header("LeechShip")]
    public Transform leechShip;
    public LineRenderer leechShiplineRenderer;

    [Header("Materials")]
    public Material powerNodeActiveMaterial;
    public Material powerSourceActiveMaterial;
    public Material powerSourceDisabledMaterial;

    //Colors
    Color color = CGUtils.HexToColor("006110");

    float colorDisabledDesiredAlpha = 0.25f;

    //Info about Leach ship
    bool wasLeachShipAttachedOnStart = false;

    LineRenderer lineRenderer;
    MaterialPropertyBlock materialPropertyBlock;
    Renderer renderer_;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        renderer_ = lineRenderer.GetComponent<Renderer>();

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        color.a = 0.25f;

        if (powerSourceRenderer != null)
            powerSourceRenderer.material = powerSourceDisabledMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (leechShip != null)
            wasLeachShipAttachedOnStart = true;

        StartCoroutine(PeriodicUpdates());
    }

    // Update is called once per frame
    void Update()
    {
        color.a = Mathf.Lerp(color.a, colorDisabledDesiredAlpha, Time.deltaTime * 2);

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        if (nextPowerNode != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, nextPowerNode.transform.position);
        }
        else lineRenderer.enabled = false;

        if(leechShip !=null)
        {
            leechShiplineRenderer.SetPosition(0, transform.position);
            leechShiplineRenderer.SetPosition(2, leechShip.transform.position);

            //Set a random position for the leech line
            Vector3 leechLinePosition = Vector3.Lerp(leechShip.transform.position, transform.position, 0.5f); //Get half way vector
            leechLinePosition += Vector3.one * Random.Range(-5, 5);

            leechShiplineRenderer.SetPosition(1, leechLinePosition);
        }
        else leechShiplineRenderer.enabled = false;

        //Check if the player destroyed the attached leech ship.
        if (wasLeachShipAttachedOnStart && leechShip == null)
        {
            leechShiplineRenderer.enabled = false;
            Invoke("SetActive",0.5f);
        }
    }

    public void SetActive()
    {
        lineRenderer.material = powerNodeActiveMaterial;

        if (powerSourceRenderer != null)
            powerSourceRenderer.material = powerSourceActiveMaterial;

        if (nextPowerNode != null)
            Invoke("SetNextActive", 0.5f);
    }

    void SetNextActive()
    {
        if (nextPowerNode != null)
            nextPowerNode.SetActive();
    }

    IEnumerator PeriodicUpdates()
    {
        while (true)
        {
            colorDisabledDesiredAlpha = Random.Range(0.8f, 1.0f);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
