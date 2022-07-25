using TMPro;
using UnityEngine;

public class Speller : MonoBehaviour
{
    public bool visible;
    public TextMeshProUGUI _renderer;
    public float secs = 0f;
    private float maxsecs = 2f;
    private Timers tim;
    //private bool ready;

    private void Awake()
    {
        _renderer = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _renderer.enabled = visible;

        if (visible)
        {
            Cronological();
        }
    }

    private void Cronological()
    {
        if (secs < maxsecs)
        {
            Mathf.Clamp(secs, 0f, 10f);
            secs += 1f * Time.deltaTime;
            //ready = true;
        }
        else
        {
            visible = false;
            secs = 0;
        }
    }
}
