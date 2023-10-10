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
    }
    private void Start()
    {
        countWood = 0;
        img.fillAmount = (float)countWood / 10;
        text.text = countWood+"/10";
        if (Profile.Level == 1)
        {
            DOVirtual.DelayedCall(2f, ()=>hand.SetActive(true));
            panel.SetActive(true);
            joy.SetActive(false);
            playerController.tutorial = true;
        }else
        {
            playerController.tutorial = false;
            panel.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // if(isDone)
        // {
        //     arrow.transform.LookAt(target);
        // }
        // else
        // {
        //     if (tree == null)
        //         return;
        //     if (tree.gameObject.activeSelf)
        //     {
        //         arrow.transform.LookAt(tree);
        //     } 
        //     else if (tree2.gameObject.activeSelf)
        //     {
        //         arrow.transform.LookAt(tree2);
        //     }
        //     else if (tree3.gameObject.activeSelf)
        //     {
        //         arrow.transform.LookAt(tree3);
        //     }
        // }
    }
    
    public void UpItem(CollectedItem.TypeItem type)
    {
        if(type == CollectedItem.TypeItem.wood)
        {
            if (countWood < 10)
            {
                countWood++;
                img.fillAmount = (float) countWood / 10;
                text.text = countWood+"/10";
                if (countWood == 10)
                {
                    Quest2();
                }
            }
        }
        if(type == CollectedItem.TypeItem.stone)
        {
            if (countWood < 10)
            {
                countWood++;
                img.fillAmount = (float) countWood / 10;
                text.text = countWood+"/10";
                if (countWood == 10)
                {
                    Quest5();
                }
            }
        }        
        if(type == CollectedItem.TypeItem.skin)
        {
            if (countWood < 8)
            {
                countWood++;
                img.fillAmount = (float) countWood / 8;
                text.text = countWood+"/8";
                if (countWood == 8)
                {
                    Quest9();
                }
            }
        }        
        if(type == CollectedItem.TypeItem.skinArmor)
        {
            if (countWood < 1)
            {
                countWood++;
                img.fillAmount = (float) countWood / 1;
                text.text = countWood+"/1";
                if (countWood == 1)
                {
                    Quest10();
                }
            }
        }
        if(type == CollectedItem.TypeItem.woodVip)
        {
            if (countWood < 10)
            {
                countWood++;
                img.fillAmount = (float) countWood / 10;
                text.text = countWood+"/10";
                if (countWood == 10)
                {
                    Quest12();
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
        bsh.gameObject.SetActive(true);
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
        countWood = 0;
        img.fillAmount = (float) countWood / 8;
        text.text = countWood+"/8";
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        //arrow.transform.position = pos[6].position+ new Vector3(0, -8, +25);
        cam2.transform.DOMove(pos[6].position, 3f).OnComplete(() =>
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
        mission.text = QuestText(8);
    }

    private void Quest9()
    {
        text24.SetActive(false);
        cam2.transform.position = cam1.transform.position;
        joy.SetActive(false);
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
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
        countWood = 0;
        img.fillAmount = (float) countWood / 1;
        text.text = countWood+"/1";
        mission.text = QuestText(9);
    }

    public void Quest10()
    {
        fsh.SetActive(true);
        countWood = 10;
        img.fillAmount = 1;
        text.text = "";
        mission.text = QuestText(10);
    }

    public void Quest11()
    {
        countWood = 0;
        img.fillAmount = (float) countWood / 10;
        text.text = countWood+"/10";
        mission.text = QuestText(11);
    }
    
    public void Quest12()
    {
        mission.text = QuestText(12);
        foreach (var u in allUpgrades)
        {
            u.SetActive(true);
        }
    }

    public void Quest13()
    {
        other.SetActive(true);
        img.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        mission.text = QuestText(13);
        mission.transform.position += Vector3.up*3;
    }
    private string QuestText(int index)
    {
        var s = index switch
        {
            1 => "Collect 10 woods",
            2 => "Build your first tower",
            3 => "Start level 1",
            4 => "Collect 10 stones",
            5 => "Build your next tower",
            6 => "Finish level 2",
            7 => "Build your blacksmith",
            8 => "Collect 8 leathers",
            9 => "Craft 1 leather armour",
            10 => "Build your fusion house",
            11 => "Collect 10 planks",
            12 => "Update your tower",
            13 => "Defend this world",
            _ => null
        };
        return s;
    }
}
