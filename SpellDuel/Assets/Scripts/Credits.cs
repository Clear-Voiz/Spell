using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private float speed;
    private float duration;
    private float advancement;
    //private float elapsedTime;

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x,20.43f,transform.position.z);
        speed = 1.5f;
        duration = (endPos.y - startPos.y) / speed;
        StartCoroutine(Rolling());
        StartCoroutine(BackToTheTrack(duration+2f));
    }

    IEnumerator Rolling()
    {
        float elapsedTime = 0f;
        float completeness = 0f;
        while (completeness<1f)
        {
            elapsedTime = Mathf.Clamp(elapsedTime, 0, duration);
            completeness = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, endPos, completeness);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator BackToTheTrack(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(0);
    }
}
