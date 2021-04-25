using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNodeHandler : MonoBehaviour
{
    public bool isPowerNodeActive = false;

    public PowerNodeHandler nextPowerNode;

    [Header("Materials")]
    public Material powerNodeActiveMaterial;

    //Colors
    Color color = Color.grey;

    float colorDisabledDesiredAlpha = 0.25f;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PeriodicUpdates());

        Invoke("SetActive", 1.5f);
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
    }

    void SetActive()
    {
        lineRenderer.material = powerNodeActiveMaterial;
    }

    IEnumerator PeriodicUpdates()
    {
        while (true)
        {
            colorDisabledDesiredAlpha = Random.Range(0.05f, 0.5f);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
