using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_CarMinigame2 : MonoBehaviour
{

    public static GameController_CarMinigame2 instance;

    public GameObject backGround;
    public int lane;
    public MyCar_CarMinigame2 myCar;
    public Dragon_CarMinigame2 dragon;
    public Camera mainCamera;
    public List<Transform> posSetUpMap = new List<Transform>();
    public List<GameObject> groupSpawn = new List<GameObject>();
    public List<Transform> pos = new List<Transform>();
    public bool isIntro, isWin;
    public int timeOnMainGame = 2;
    private int speed = 5;
    private int timeGrowSpeed = 0;
    public int iLose = 0;
    public bool isTut = false;
    public GameObject tutorial;
    private bool isLockStage;
    public Button btnNitro;
    public GameObject nitroObj, houseObj;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    private void Start()
    {
        nitroObj.SetActive(false);
        btnNitro.enabled = false;
        btnNitro.gameObject.SetActive(false);
        btnNitro.onClick.AddListener(NitroMode);
        isLockStage = false;
        isIntro = true;
        isWin = false;
        tutorial.SetActive(false);
        lane = 1;
        SetSizeCamera();
        SetUpMap();
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        if (f1 > f2)
        {
            mainCamera.orthographicSize *= f1 / f2;
            mainCamera.orthographicSize = Mathf.Max(7, mainCamera.orthographicSize);
        }
    }

    void SpawnCarEnemy(int indexPos)
    {
        GameObject carE = Instantiate(groupSpawn[Random.Range(0, groupSpawn.Count)]);
        carE.transform.parent = backGround.transform;
        carE.transform.position = posSetUpMap[indexPos].transform.position;
    }

    void SetUpMap()
    {
        for (int i = 0; i < posSetUpMap.Count; i++)
        {
            SpawnCarEnemy(i);

        }
    }

    public void Lose1()
    {
        Debug.Log("Thua 1");
        iLose = 1;
        isIntro = true;
        dragon.StopAllCoroutines();
        StopAllCoroutines();

    }
    public void Lose2()
    {
        Debug.Log("Thua 2");
        iLose = 2;
        isIntro = true;
        if(btnNitro.enabled == true)
        {
            btnNitro.enabled = false;
        }
        dragon.StopAllCoroutines();
        StopAllCoroutines();
    }

    public void EnableTutorial()
    {
        if (tutorial.activeSelf == true)
        {
            tutorial.transform.DOMoveY(tutorial.transform.position.y + 5, 1).OnComplete(() =>
            {
                tutorial.transform.DOMoveY(tutorial.transform.position.y - 5, 1).OnComplete(() =>
                {
                    EnableTutorial();
                });
            });
        }

    }
    public void DestroyTutorial()
    {
        if (tutorial.activeSelf == true)
        {
            tutorial.SetActive(false);
            tutorial.transform.DOKill();
            Destroy(tutorial);
            StartCoroutine(CountingTime());
            iLose = 0;
        }
    }

    IEnumerator CountingTime()
    {
        while (timeOnMainGame <= 50)
        {
            timeOnMainGame++;
            timeGrowSpeed++;
            yield return new WaitForSeconds(1);
            if (timeGrowSpeed == 10)
            {
                speed++;
                timeGrowSpeed = 0;
            }
        }
    }

    void NitroMode()
    {
        btnNitro.enabled = false;
        btnNitro.gameObject.SetActive(false);
        nitroObj.SetActive(false);
        nitroObj.transform.DOKill();       
        isWin = true;
        mainCamera.transform.DOMoveX(mainCamera.transform.position.x + 50, 2);
        myCar.transform.DOMoveX(myCar.transform.position.x + 50, 2).OnComplete(() =>
        {
            var t = FindObjectOfType<Bullet_CarMinigame2>();
            Destroy(t);
            speed -= 4;
            for (int i = 0; i < backGround.transform.GetChild(0).childCount; i++)
            {
                backGround.transform.GetChild(0).GetChild(i).GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
            }
            Invoke(nameof(Win), 2);
            
        });
    }

    void Win()
    {
        Debug.Log("Win");
        Debug.Log(houseObj.transform.position.x);
        Debug.Log(backGround.transform.position.x);
        iLose = -1;
        isWin = true;
        myCar.transform.DOMove(new Vector2(houseObj.transform.position.x , houseObj.transform.position.y + backGround.transform.position.y - 3), 2);
    }

    private void Update()
    {
        if (iLose == 0)
        {
            backGround.transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);

            if (timeOnMainGame == 50 && !isLockStage)
            {
                isLockStage = true;
                speed++;
                for (int i = 0; i < backGround.transform.GetChild(0).childCount; i++)
                {
                    backGround.transform.GetChild(0).GetChild(i).GetChild(3).GetComponent<SpriteRenderer>().color = Color.gray;                 
                }
                dragon.transform.DOShakePosition(1).OnComplete(() =>
                {
                    dragon.StopAllCoroutines();
                    dragon.FireUltil();
                    nitroObj.gameObject.SetActive(true);
                    nitroObj.transform.DOShakeScale(1).SetLoops(-1);
                    btnNitro.gameObject.SetActive(true);
                    btnNitro.enabled = true;
                });
            }
        }

    }
}

