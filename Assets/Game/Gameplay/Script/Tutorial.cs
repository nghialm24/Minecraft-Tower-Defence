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
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject joy;
    [SerializeField] private Transform posPlayer;
    [SerializeField] private GameObject text24;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<Sprite> listIcon;
    private bool quest1;
    private bool quest4;
    private bool quest8;
    //[SerializeField] private GameObject arrow;
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
            panel.SetActive(false);
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
        Debug.Log(Profile.Tutorial);
        countWood = 0;
        img.fillAmount = (float)countWood / 10;
        text.text = countWood+"/10";
        if (Profile.Level == 1)
        {
            if (!Profile.Tutorial)
            {
                DOVirtual.DelayedCall(2f, () => hand.SetActive(true));
                panel.SetActive(true);
                joy.SetActive(false);
                playerController.tutorial = true;
            }
        }else
        {
            playerController.tutorial = false;
            panel.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame

    public void UpItem(CollectedItem.TypeItem type)
    {
        if(type == CollectedItem.TypeItem.wood && quest1)
        {
            if (countWood < 10)
            {
                countWood++;
                img.fillAmount = (float) countWood / 10;
                text.text = countWood+"/10";
                if (countWood == 10)
                {
                    Quest2();
                    quest1 = false;
                }
            }
        }
        if(type == CollectedItem.TypeItem.stone && quest4)
        {
            if (countWood < 10)
            {
                countWood++;
                img.fillAmount = (float) countWood / 10;
                text.text = countWood+"/10";
                if (countWood == 10)
                {
                    Quest5();
                    quest4 = false;
                }
            }
        }
        if(type == CollectedItem.TypeItem.woodVip  && quest8)
        {
            if (countWood < 5)
            {
                countWood++;
                img.fillAmount = (float) countWood / 5;
                text.text = countWood+"/5";
                if (countWood == 5)
                {
                    Quest9();
                    quest8 = false;
                }
            }
        }   
    }

    private void Quest2()
    {
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[1].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[1].position, 3f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 3f).OnComplete(() =>
                {
                    text24.SetActive(true);
                    cam1.gameObject.SetActive(true);
                    cam2.gameObject.SetActive(false);
                    joy.SetActive(true);
                });
            });
        });
        mission.text = QuestText(2);
        Profile.Tutorial = true;
    }
    
    public void Quest1()
    {
        //arrow.SetActive(true);
        //arrow.transform.position = pos[0].position+ new Vector3(0, -8, +15);
        text24.SetActive(false);
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        hand.SetActive(false);
        transform.GetChild(0).DOScale(Vector3.one * 1.5f, 0.5f).OnComplete(() =>
            {
                transform.GetChild(0).DOScale(Vector3.one, 0.5f);
                DOVirtual.DelayedCall(2f,() =>
                {
                    panel.SetActive(false);
                    DOVirtual.DelayedCall(2f,() =>
                    {
                        cam2.transform.DOMove(pos[0].position, 3f).OnComplete(() =>
                            {
                                DOVirtual.DelayedCall(2f,() =>
                                {
                                    cam2.transform.DOMove(posPlayer.position, 3f).OnComplete(() =>
                                    {
                                        text24.SetActive(true);
                                        cam1.gameObject.SetActive(true);
                                        cam2.gameObject.SetActive(false);
                                        joy.SetActive(true);
                                    });
                                });
                            });
                    });
                });
            });
        quest1 = true;
    }

    public void Quest3()
    {
        text24.SetActive(false);
        start.SetActive(true);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[2].position+ new Vector3(0, -8, +25);

        cam2.transform.DOMove(pos[2].position, 3f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 3f).OnComplete(() =>
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
        iconItem.sprite = listIcon[0];
        countWood = 0;
        img.fillAmount = (float) countWood / 10;
        text.text = countWood+"/10";
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[3].position+ new Vector3(0, -8, +25);

        cam2.transform.DOMove(pos[3].position, 3f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 3f).OnComplete(() =>
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
        allTower.SetActive(true);
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[4].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[4].position, 3f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 3f).OnComplete(() =>
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
        //arrow.SetActive(false);
        mission.text = QuestText(6);
    }

    public void Quest7()
    {
        fsh.gameObject.SetActive(true);
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.SetActive(true);
        //arrow.transform.position = pos[5].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[5].position, 3f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f,() =>
            {
                cam2.transform.DOMove(posPlayer.position, 3f).OnComplete(() =>
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
        iconItem.sprite = listIcon[1];
        countWood = 0;
        img.fillAmount = (float) countWood / 5;
        text.text = countWood+"/5";
        mission.text = QuestText(8);
        quest8 = true;
    }

    public void Quest9()
    {
        mission.text = QuestText(9);
        foreach (var u in allUpgrades)
        {
            u.SetActive(true);
        }
    }
    
    public void Quest10()
    {
        bsh.SetActive(true);
        other.SetActive(true);
        img.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        mission.text = QuestText(10);
        mission.transform.position += Vector3.up*35;
    }
    private string QuestText(int index)
    {
        var s = index switch
        {
            1 => "Collect 10 woods",
            2 => "Build your first tower",
            3 => "Start level 1",
            4 => "Collect 10 cobblestones",
            5 => "Build your next tower",
            6 => "Finish level 2",
            7 => "Build your Fusion house",
            8 => "Collect 5 planks",
            9 => "Update your tower",
            10 => "Defend this world",
            _ => null
        };
        return s;
    }
}
