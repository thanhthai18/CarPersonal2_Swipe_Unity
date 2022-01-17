using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCar_CarMinigame2 : MonoBehaviour
{
    public Camera mainCamera;
    public Vector2 currentMousePos;
    public Vector2 preMousePos;
    public bool isLock;
    public bool isWin = false;

    private void Start()
    {
        isLock = true;
        transform.DOMove(GameController_CarMinigame2.instance.pos[1].transform.position, 1).OnComplete(() =>
        {
            GameController_CarMinigame2.instance.isIntro = false;
            GameController_CarMinigame2.instance.isTut = true;
            GameController_CarMinigame2.instance.tutorial.SetActive(true);
            GameController_CarMinigame2.instance.EnableTutorial();
            GameController_CarMinigame2.instance.iLose = -1;
        });
    }


    private void Update()
    {
        if (!GameController_CarMinigame2.instance.isIntro && !GameController_CarMinigame2.instance.isWin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                preMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                isLock = false;
            }

            if (currentMousePos.y - preMousePos.y > 0 && !isLock)
            {
                if (GameController_CarMinigame2.instance.isTut)
                {
                    GameController_CarMinigame2.instance.isTut = false;
                    GameController_CarMinigame2.instance.DestroyTutorial();
                    GameController_CarMinigame2.instance.iLose = 0;
                    GameController_CarMinigame2.instance.dragon.isFire = true;
                }

                if (GameController_CarMinigame2.instance.lane > 0)
                {
                    GameController_CarMinigame2.instance.lane--;
                    transform.DOMove(GameController_CarMinigame2.instance.pos[GameController_CarMinigame2.instance.lane].transform.position, 0.2f);
                    GameController_CarMinigame2.instance.dragon.MoveDragon();
                    currentMousePos.y = 0;
                    preMousePos.y = 0;
                    isLock = true;
                }


            }
            if (currentMousePos.y - preMousePos.y < 0 && !isLock)
            {
                if (GameController_CarMinigame2.instance.isTut)
                {
                    GameController_CarMinigame2.instance.isTut = false;
                    GameController_CarMinigame2.instance.DestroyTutorial();
                    GameController_CarMinigame2.instance.iLose = 0;
                    GameController_CarMinigame2.instance.dragon.isFire = true;
                }

                if (GameController_CarMinigame2.instance.lane < 2)
                {
                    GameController_CarMinigame2.instance.lane++;
                    transform.DOMove(GameController_CarMinigame2.instance.pos[GameController_CarMinigame2.instance.lane].transform.position, 0.2f);
                    GameController_CarMinigame2.instance.dragon.MoveDragon();
                    currentMousePos.y = 0;
                    preMousePos.y = 0;
                    isLock = true;
                }
            }

            if (currentMousePos.y == preMousePos.y && !isLock)
            {
                isLock = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameController_CarMinigame2.instance.Lose2();
            Destroy(collision.gameObject);
            GetComponent<Collider2D>().enabled = false;
            // anim boc hoi
        }
    }
}
