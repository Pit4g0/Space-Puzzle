using UnityEditor;
using UnityEngine;

//// Original script by nappin <3
//// https://www.youtube.com/@nappin_zz

//[ExecuteInEditMode]
//public class LockToGrid : MonoBehaviour
//{
//    public int tileSize = 1;
//    public Vector2 tileOffset = Vector2.zero;


//    void Update()
//    {
//        if (EditorApplication.isPlaying)
//        {
//            Vector2 currentPosition = transform.position;

//            float snappedX = Mathf.Round(currentPosition.x / tileSize) * tileSize + tileOffset.x;
//            float snappedY = Mathf.Round(currentPosition.y / tileSize) * tileSize + tileOffset.y;

//            Vector2 snappedPosition = new Vector2(snappedX, snappedY);
//            transform.position = snappedPosition;
//        }
//    }
//}