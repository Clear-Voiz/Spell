using FishNet.Connection;
using TMPro;
using UnityEngine;

public class LocalPoints : MonoBehaviour
{
   public float duration;
   private TextMeshPro tmp;
   public int points;
   private Vector3 startPos;
   private Vector3 endPos;
   private float elapsedTime;
   private float completePercent;
   public Transform cam;
   public NetworkConnection conn;

   private void Awake()
   {
      tmp = GetComponent<TextMeshPro>();
      cam = Cine_Shake.Instance._virtualCamera.transform;
      //cam = GameObject.FindGameObjectWithTag("Virtual Camera").transform;
   }

   private void Start()
   {
      duration = 1f;
      points = 2;
      Destroy(gameObject,2f);
      if (conn != null && GameManager.Instance.LocalConnection == conn)
      {
         tmp.text = $"tech <size=26><i>{points}</size></i>p";
      }
      else
      {
         tmp.text = $"<size=12><color=#641e16>Dmg</size> <size=26> <i>x2</size></i></color>";
      }
      
      Globs.Xp += Mathf.RoundToInt(points * Globs.xpGain.Value);
      print("exp: " + Globs.Xp);
      startPos = transform.position;
      endPos = startPos + new Vector3(0f, 1f, 0f);
      if (cam != null) transform.forward = cam.forward;
   }

   private void Update()
   {
      elapsedTime += Time.deltaTime;
      completePercent = elapsedTime / duration;
      transform.position = Vector3.Lerp(startPos, endPos, completePercent);
      
      //if it doesn't always look at the camera set the transform.forward to the camera forward. Thanks for the tip buddie
   }
}
