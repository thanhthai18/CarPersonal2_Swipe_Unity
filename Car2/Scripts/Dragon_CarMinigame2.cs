using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_CarMinigame2 : MonoBehaviour
{
    public Transform gunPoint;
    public Bullet_CarMinigame2 bulletPrefab;
    public bool isFire = false;


    private void Start()
    {
        transform.DOMove(new Vector2(transform.position.x + 2, GameController_CarMinigame2.instance.pos[1].position.y + 1.181364f), 1);
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(2);
        MoveDragon();
        Instantiate(bulletPrefab, new Vector2(gunPoint.transform.position.x, GameController_CarMinigame2.instance.pos[GameController_CarMinigame2.instance.lane].position.y), Quaternion.identity);
        while(GameController_CarMinigame2.instance.iLose == 0 && GameController_CarMinigame2.instance.timeOnMainGame < 45)
        {
            yield return new WaitForSeconds(4);
            Instantiate(bulletPrefab, new Vector2(gunPoint.transform.position.x, GameController_CarMinigame2.instance.pos[GameController_CarMinigame2.instance.lane].position.y), Quaternion.identity);
        }
    }

    public void FireUltil()
    {
        Instantiate(bulletPrefab, new Vector2(gunPoint.transform.position.x, GameController_CarMinigame2.instance.pos[0].position.y), Quaternion.identity);
        Instantiate(bulletPrefab, new Vector2(gunPoint.transform.position.x, GameController_CarMinigame2.instance.pos[1].position.y), Quaternion.identity); 
        Instantiate(bulletPrefab, new Vector2(gunPoint.transform.position.x, GameController_CarMinigame2.instance.pos[2].position.y), Quaternion.identity);
    }

    public void MoveDragon()
    {
        transform.DOMoveY(GameController_CarMinigame2.instance.pos[GameController_CarMinigame2.instance.lane].position.y + 1.181364f, 0.25f);
    }

    

    private void Update()
    {
        if (isFire)
        {
            isFire = false;
            StartCoroutine(Fire());
        }
    }

}
