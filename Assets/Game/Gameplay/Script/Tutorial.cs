using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using Funzilla;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private int countWood;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image img;
    [SerializeField] private Image iconItem;

    internal static Tutorial Instance;
    [SerializeField] private TextMeshProUGUI mission;
    //[SerializeField] private GameObject arrow;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject other;
    private bool isDone;
    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;
    [SerializeField] private List<Transform> pos;
    [SerializeField] private GameObject allTower;
    [SerializeField] private GameObject bsh;
    [SerializeField] private GameObject fsh;
    [SerializeField] private List<GameObject> allUpgrades;
    //[SerializeField] private GameObject panel;
    //[SerializeField] private GameObject hand;
    [SerializeField] private GameObject joy;
    [SerializeField] private Transform posPlayer;
    [SerializeField] private GameObject text24;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<Sprite> listIcon;
    private bool quest1;
    private bool quest4;
    private bool quest8;
    //[SerializeField] private GameObject arrow;
    public bool tutorial;
    [SerializeField] private Animator imageAnim;
     private void Awake()
    {
        Instance = this;
    }

    public void Init(GameObject st, GameObject ot, List<Transform> p, GameObject allTower, GameObject bsh, GameObject fsh, List<GameObject> allUpgrades)
    {
        start = st;
        other = ot;
        pos = p;
        this.allTower = allTower;
        this.bsh = bsh;
        this.fsh = fsh;
        this.allUpgrades = allUpgrades;
        if (Profile.Tutorial)
        {
            allTower.SetActive(true);
            bsh.SetActive(true);
            fsh.SetActive(true);
            playerController.tutorial = false;
            //panel.SetActive(false);
            start.SetActive(true);
            other.SetActive(true);
            foreach (var u in allUpgrades)
            {
                u.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        countWood = 0;
        img.fillAmount = (float)countWood / 5;
        text.text = countWood+"/5";
        if (Profile.Level == 1)
        {
            if (!Profile.Tutorial)
            {
                StartCoroutine(WaitStart(2.0f));
                //panel.SetActive(true);
                joy.SetActive(false);
                playerController.tutorial = true;
            }
            else
            {
                quest1 = false;
                quest4 = false;
                quest8 = false;
            }
        }else
        {
            playerController.tutorial = false;
            //panel.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame

    public void UpItem(CollectedItem.TypeItem type)
    {
        if(type == CollectedItem.TypeItem.wood && quest1)
        {
            if (countWood < 5)
            {
                countWood++;
                img.fillAmount = (float) countWood / 5;
                text.text = countWood+"/5";
                if (countWood == 5)
                {
                    Quest2();
                    quest1 = false;
                    SoundManager.PlaySfx("success");
                }
            }
        }
        if(type == CollectedItem.TypeItem.stone && quest4)
        {
            if (countWood < 5)
            {
                countWood++;
                img.fillAmount = (float) countWood / 5;
                text.text = countWood+"/5";
                if (countWood == 5)
                {
                    Quest5();
                    quest4 = false;
                }
            }
        }
        if(type == CollectedItem.TypeItem.woodVip  && quest8)
        {
            if (countWood < 2)
            {
                countWood++;
                img.fillAmount = (float) countWood / 2;
                text.text = countWood+"/2";
                if (countWood == 2)
                {
                    Quest9();
                    quest8 = false;
                }
            }
        }   
    }

    private void Quest2()
    {
        imageAnim.Play("Tutorial_NCL");
        text24.SetActive(false);
        iconItem.sprite = listIcon[1];
        cam2.transform.position = cam1.transform.position;
        playerController.CancelRunAnim();
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[1].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[1].position, 1.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 1.5f).OnComplete(() =>
                {
                    text24.SetActive(true);
                    cam1.gameObject.SetActive(true);
                    cam2.gameObject.SetActive(false);
                    joy.SetActive(true);
                });
            });
        });
        tutorial = true;
        Profile.Tutorial = true;
        mission.text = QuestText(2);
    }
    
    public void Quest1()
    {
        text24.SetActive(false);
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        DOVirtual.DelayedCall(2f,() =>
        {
            cam2.transform.DOMove(pos[0].position, 1.5f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(2f,() =>
                {
                    cam2.transform.DOMove(posPlayer.position, 1.5f).OnComplete(() =>
                    {
                        text24.SetActive(true);
                        cam1.gameObject.SetActive(true);
                        cam2.gameObject.SetActive(false);
                        joy.SetActive(true);
                    });
                });
            });
        });
        quest1 = true;
    }

    public void Quest3()
    {
        imageAnim.Play("Tutorial_NCL");
        iconItem.sprite = listIcon[2];
        text24.SetActive(false);
        start.SetActive(true);
        cam2.transform.position = cam1.transform.position;
        playerController.CancelRunAnim();
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[2].position+ new Vector3(0, -8, +25);

        cam2.transform.DOMove(pos[2].position, 1.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 1.5f).OnComplete(() =>
                {
                    text24.SetActive(true);
                    cam1.gameObject.SetActive(true);
                    cam2.gameObject.SetActive(false);
                    joy.SetActive(true);
                });
            });
        });
        mission.text = QuestText(3);
    }

    public void Quest4()
    {
        imageAnim.Play("Tutorial_CL");
        iconItem.sprite = listIcon[3];
        start.SetActive(false);
        countWood = 0;
        img.fillAmount = (float) countWood / 10;
        text.text = countWood+"/5";
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        playerController.CancelRunAnim();
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[3].position+ new Vector3(0, -8, +25);

        cam2.transform.DOMove(pos[3].position, 1.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 1.5f).OnComplete(() =>
                {
                    text24.SetActive(true);
                    cam1.gameObject.SetActive(true);
                    cam2.gameObject.SetActive(false);
                    joy.SetActive(true);
                });
            });
        });
        mission.text = QuestText(4);
        quest4 = true;
    }

    private void Quest5()
    {
        imageAnim.Play("Tutorial_NCL");
        iconItem.sprite = listIcon[4];
        allTower.SetActive(true);
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        playerController.CancelRunAnim();
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[4].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[4].position, 1.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 1.5f).OnComplete(() =>
                {
                    text24.SetActive(true);
                    cam1.gameObject.SetActive(true);
                    cam2.gameObject.SetActive(false);
                    joy.SetActive(true);
                });
            });
        });
        mission.text = QuestText(5);
    }

    public void Quest6()
    {
        imageAnim.Play("Tutorial_NCL");
        iconItem.sprite = listIcon[5];
        start.SetActive(true);
        mission.text = QuestText(6);
    }

    public void Quest7()
    {
        imageAnim.Play("Tutorial_NCL");
        iconItem.sprite = listIcon[6];
        start.SetActive(false);
        fsh.gameObject.SetActive(true);
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        playerController.CancelRunAnim();
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.SetActive(true);
        //arrow.transform.position = pos[5].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[5].position, 1.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 1.5f).OnComplete(() =>
                {
                    text24.SetActive(true);
                    cam1.gameObject.SetActive(true);
                    cam2.gameObject.SetActive(false);
                    joy.SetActive(true);
                });
            });
        });
        mission.text = QuestText(7);
    }

    public void Quest8()
    {
        imageAnim.Play("Tutorial_CL");
        iconItem.sprite = listIcon[7];
        countWood = 0;
        img.fillAmount = (float) countWood / 5;
        text.text = countWood+"/5";
        mission.text = QuestText(8);
        quest8 = true;
    }

    public void Quest9()
    {
        imageAnim.Play("Tutorial_NCL");
        iconItem.sprite = listIcon[8];
        mission.text = QuestText(9);
        foreach (var u in allUpgrades)
        {
            u.SetActive(true);
        }
    }
    
    public void Quest10()
    {
        imageAnim.Play("Tutorial_NCL");
        start.SetActive(true);
        bsh.SetActive(true);
        other.SetActive(true);
        mission.text = QuestText(10);
    }
    private string QuestText(int index)
    {
        var s = index switch
        {
            1 => "Collect 5 woods",
            2 => "Build your first tower",
            3 => "Start level 1",
            4 => "Collect 5 cobblestones",
            5 => "Build your next tower",
            6 => "Finish level 2",
            7 => "Build your Fusion house",
            8 => "Collect 2 planks",
            9 => "Update your tower",
            10 => "Defend this world",
            _ => null
        };
        return s;
    }
    
    private IEnumerator WaitStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Quest1();
    }
}
