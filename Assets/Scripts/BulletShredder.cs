using UnityEngine;

public class BulletShredder : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      Destroy(collision.gameObject);
   }
}
