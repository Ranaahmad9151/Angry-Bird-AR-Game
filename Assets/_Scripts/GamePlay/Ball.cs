using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ThrowObject
{
    private void OnCollisionEnter(Collision collision)
    {
        //StartCoroutine(EnemyDestroy(3f, this.gameObject));

        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Enemy " + collision.gameObject);
            StartCoroutine(EnemyDestroy(3f, collision.gameObject));
        }
    }
    public IEnumerator EnemyDestroy(float timer, GameObject enemy)
    {
        yield return new WaitForSeconds(timer);
        Destroy(enemy);
    }
    public IEnumerator PlayerDestroy(float timer, GameObject player)
    {
        yield return new WaitForSeconds(timer);
        Destroy(player);
    }

}
