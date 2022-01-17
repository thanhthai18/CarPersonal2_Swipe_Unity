using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy_CarMinigame2 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            GameController_CarMinigame2.instance.myCar.transform.DOMoveX(GameController_CarMinigame2.instance.myCar.transform.position.x - 1.5f, 1f);
            GameController_CarMinigame2.instance.Lose1();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);
            transform.parent = null;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => { Destroy(gameObject); });

        }
        if (collision.gameObject.CompareTag("Trash"))
        {
            transform.parent = null;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => { Destroy(gameObject); });

        }
    }

   
}
