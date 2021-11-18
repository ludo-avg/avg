//C#
using System;
using System.Collections;
using System.Collections.Generic;
//Unity
using UnityEngine;
using UnityEngine.Events;
//Plugin
using NaughtyAttributes;
//Project
using Interactions;
using Tools.Extensions;
//Editor
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class InteractionList : MonoBehaviour
{
    #region Singleton
    public static InteractionList singleton = null;
    void Awake()
    {
        singleton = this;
    }
    #endregion

    //setting
    [SerializeField] UserData userData = null;
    [SerializeField] GameObject characterTeacher = null;
    [SerializeField] GameObject characterLaoMuGen = null;
    [SerializeField] GameObject characterFarmer = null;
    [SerializeField] GameObject characterDirector = null;
    [SerializeField] GameObject backGroundSchoolGate = null;
    [SerializeField] GameObject backGroundPeopleMarket = null;
    [SerializeField] GameObject backGroundRentFatherCorner = null;
    [SerializeField] GameObject backGroundHome = null;
    [SerializeField] TimeChoice timeChoice = null;
    [SerializeField] Choice choice = null;
    [SerializeField] Custom custom = null;

    private void PutLast(GameObject child)
    {
        child.transform.parent = gameObject.transform;
        child.transform.SetAsLastSibling();
    }

    private void PutLastAndRename(GameObject child, string name = null)
    {
        child.transform.parent = gameObject.transform;
        child.transform.SetAsLastSibling();

        if (name != null)
        {
            child.name = name;
        }
        else
        {
            if (child.GetComponent<Idle>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Idle>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Idle" + num;
            }

            if (child.GetComponent<GameEnd>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<GameEnd>() != null)
                    {
                        num++;
                    }
                }
                child.name = "GameEnd" + num;
            }

            if (child.GetComponent<Dialogue>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Dialogue>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Dialogue" + num;
            }

            if (child.GetComponent<Character>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Character>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Character" + num;
            }

            if (child.GetComponent<ChangeBackground>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<ChangeBackground>() != null)
                    {
                        num++;
                    }
                }
                child.name = "ChangeBackground" + num;
            }

            if (child.GetComponent<Choice>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Choice>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Choice" + num;
            }

            if (child.GetComponent<TimeChoice>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<TimeChoice>() != null)
                    {
                        num++;
                    }
                }
                child.name = "TimeChoice" + num;
            }

            if (child.GetComponent<Custom>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Custom>() != null)
                    {
                        num++;
                    }
                }
                child.name = "CustomInteraction" + num;
            }
        }
    }

#if UNITY_EDITOR
    [Button("CreateList")]
    private void CreateList()
    {
        PutLastAndRename(InteractionCreator.Idle.Create(0.35f));

        //教室内
        {
            {
                string[] texts = new string[]
                {
                    "期末考试终于结束了，今天是这个学期最后一次登校，下午开完家长会，愉快的暑假就要到来了。",
                    "阿雯正在幻想着如何度过这越快的假期，丝毫没有注意老师在讲台上讲什么。",
                    "这时阿雯顿感一股杀气袭来，迎面飞来一个粉笔头！",
                };
                foreach (var text in texts)
                {
                    PutLastAndRename(InteractionCreator.Dialogue.Create(text));
                }
            }

            //TimeChoice
            var tc = InteractionCreator.TimeChoice.Copy(timeChoice.gameObject);
            tc.GetComponent<TimeChoice>().text = "阿雯反应极快，瞬间就做出了判断：";
            PutLastAndRename(tc); 
            var b11 = InteractionCreator.Dialogue.Create("只见老师使出了一招左右幻影投射技巧，粉笔头正中阿雯眉心。");
            var b12 = InteractionCreator.Dialogue.Create("老师得意的吹了手上的粉笔灰。");
            var b21 = InteractionCreator.Dialogue.Create("只见老师使出了一招左右幻影投射技巧，粉笔头同时在左右出现了一道幻影，阿雯丝毫来不及反应，但是粉笔头奇异的从阿雯的耳边飞过。");
            var b22 = InteractionCreator.Dialogue.Create("老师生气的瞪了阿雯一眼。");
            PutLast(b11);
            PutLast(b12);
            PutLast(b21);
            PutLast(b22);
            b11.name = "S1-B11";
            b12.name = "S1-B12";
            b21.name = "S1-B21";
            b22.name = "S1-B22";
            ;
            var afterb = InteractionCreator.Character.Create(characterTeacher, true);
            PutLast(afterb);
            afterb.name = "S1-AfterB";
            ;
            PutLastAndRename(InteractionCreator.Dialogue.Create(
                "阿雯，你看你考这个成绩，三科合起来考了24分，让狗踩出来的答题卡，都得比你分高，一会儿下午家长会，我非得让你爹打断你的腿。",
                "老师"
                ));
            ;
            PutLastAndRename(InteractionCreator.Character.Create(characterTeacher, false));

            //
            {
                tc.GetComponent<TimeChoice>().leftCall = new UnityEvent();
                tc.GetComponent<TimeChoice>().rightCall = new UnityEvent();
                tc.GetComponent<TimeChoice>().overTimeCall = new UnityEvent();
                UnityEventTools.AddPersistentListener(tc.GetComponent<TimeChoice>().leftCall, userData.ChalkChooseLeft);
                UnityEventTools.AddPersistentListener(tc.GetComponent<TimeChoice>().rightCall, userData.ChalkChooseRight);
                UnityEventTools.AddPersistentListener(tc.GetComponent<TimeChoice>().overTimeCall, userData.ChalkOverTime);

                var tcgoto = tc.GetComponent<TimeChoice>().@goto = new InteractionBase.ConditionalGoto[2];
                tcgoto[0] = new InteractionBase.ConditionalGoto();
                tcgoto[1] = new InteractionBase.ConditionalGoto();
                tcgoto[0].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(tcgoto[0].condition, userData.Condition_IsChalkNotOverTime);
                tcgoto[0].interaction = b11.GetComponent<InteractionBase>();
                tcgoto[1].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(tcgoto[1].condition, userData.Condition_IsChalkOverTime);
                tcgoto[1].interaction = b21.GetComponent<InteractionBase>();

                var b12goto = b12.GetComponent<InteractionBase>().@goto = new InteractionBase.ConditionalGoto[1];
                b12goto[0] = new InteractionBase.ConditionalGoto();
                b12goto[0].interaction = afterb.GetComponent<InteractionBase>();
                var b22goto = b22.GetComponent<InteractionBase>().@goto = new InteractionBase.ConditionalGoto[1];
                b22goto[0] = new InteractionBase.ConditionalGoto();
                b22goto[0].interaction = afterb.GetComponent<InteractionBase>();
                
            }
        }

        //校门口
        {

            PutLastAndRename(InteractionCreator.ChangeBackground.Create(backGroundSchoolGate));
            ;
            PutLastAndRename(InteractionCreator.Dialogue.Create("阿雯愁眉苦脸的拿着成绩单，站在学校门口不知所措，这时老穆棍悄咪咪的走了过来。"));
            ;
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, true));
            PutLastAndRename(InteractionCreator.Dialogue.Create("阿雯，走，我带你去个好地方。", "老穆根"));
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, false));
            ;
            PutLastAndRename(InteractionCreator.Dialogue.Create("说着老穆棍拉着阿雯快速离开了学校。", "老穆根"));
        }

        //人才市场大门口
        {
            PutLastAndRename(InteractionCreator.ChangeBackground.Create(backGroundPeopleMarket));
            ;
            PutLastAndRename(InteractionCreator.Dialogue.Create("来到了一个地方，门上写着人才市场。"));
            ;
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, true));
            PutLastAndRename(InteractionCreator.Dialogue.Create("像我们这种人才，就应该来这种人才市场。", "老穆根"));
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, false));
            ;
            PutLastAndRename(InteractionCreator.Dialogue.Create("我们来干啥啊？"));
            ;
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, true));
            PutLastAndRename(InteractionCreator.Dialogue.Create("我们来给你租个爹开家长会啊。", "老穆根"));
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, false));
            ;
            PutLastAndRename(InteractionCreator.Dialogue.Create("但是我只有10块钱啊。"));
            ;
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, true));
            PutLastAndRename(InteractionCreator.Dialogue.Create("先看看再说。", "老穆根"));
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, false));
        }

        //租爹专区
        {
            PutLastAndRename(InteractionCreator.ChangeBackground.Create(backGroundRentFatherCorner));
            ;
            var returnb = InteractionCreator.Dialogue.Create("老穆棍领着阿雯来到了租爹专区。");
            PutLast(returnb);
            returnb.name = "S3-ReturnB";
            PutLastAndRename(InteractionCreator.Dialogue.Create("阿雯看着这个场景惊讶不已。"));
            PutLastAndRename(InteractionCreator.Dialogue.Create("现在这么流行租爹吗？"));
            ;
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, true));
            PutLastAndRename(InteractionCreator.Dialogue.Create("哪里有需求哪里就有市场啊。", "老穆根"));
            PutLastAndRename(InteractionCreator.Dialogue.Create("我先走了，你在这选吧。", "老穆根"));
            PutLastAndRename(InteractionCreator.Character.Create(characterLaoMuGen, false));
            ;
            //Choice
            var ch = InteractionCreator.Choice.Copy(choice.gameObject);
            ch.GetComponent<Choice>().text = "阿雯选择了：";
            PutLastAndRename(ch);
            ;
            var b11 = InteractionCreator.Dialogue.Create("我正好有10块钱，就选这个吧。");
            var b12 = InteractionCreator.Dialogue.Create("此时旁边跳出来一个身影，正是阿雯学校的教导主任。");
            var b13 = InteractionCreator.Character.Create(characterDirector, true);
            var b14 = InteractionCreator.Dialogue.Create("我就知道开家长会一定会有人来租爹，给我压回学校。", "教导主任");
            var b15 = InteractionCreator.Character.Create(characterDirector, false);
            var b16 = InteractionCreator.Dialogue.Create("阿雯就这样结束了愉快的暑假。");
            var b21 = InteractionCreator.Dialogue.Create("阿雯看到500的牌子脑袋有点转不过弯，没想明白为啥要选500的，说话都开始磕巴了。");
            var b22 = InteractionCreator.Dialogue.Create("5。。。5。。。");
            var b23 = InteractionCreator.Character.Create(characterFarmer, true);
            var b24 = InteractionCreator.Dialogue.Create("5块钱成交！", "邋遢老头");
            var b25 = InteractionCreator.Character.Create(characterFarmer, false);
            b11.name = "S3-B11";
            b12.name = "S3-B12";
            b13.name = "S3-B13";
            b14.name = "S3-B14";
            b15.name = "S3-B15";
            b16.name = "S3-B16";
            b21.name = "S3-B21";
            b22.name = "S3-B22";
            b23.name = "S3-B23";
            b24.name = "S3-B24";
            b25.name = "S3-B25";
            PutLast(b11);
            PutLast(b12);
            PutLast(b13);
            PutLast(b14);
            PutLast(b15);
            PutLast(b16);
            PutLast(b21);
            PutLast(b22);
            PutLast(b23);
            PutLast(b24);
            PutLast(b25);
            ;
            var afterb = InteractionCreator.Dialogue.Create("这价格波动让阿雯始料未及，现在抹零都是这么抹的吗？");
            afterb.name = "S3-AfterB";
            PutLast(afterb);
            string[] texts = new string[]
            {
                "于是阿雯颤颤巍巍的递出了自己仅有的10块钱。",
                "邋遢老头拿过钱说：",
            };
            foreach (var text in texts)
            {
                PutLastAndRename(InteractionCreator.Dialogue.Create(text));
            }
            //
            PutLastAndRename(InteractionCreator.Character.Create(characterFarmer, true));
            PutLastAndRename(InteractionCreator.Dialogue.Create("小朋友，我这手里没零钱，我买几个橘子去。你就在此地，不要走动。", "邋遢老头"));
            PutLastAndRename(InteractionCreator.Character.Create(characterFarmer, false));
            //
            string[] texts2 = new string[]
            {
                "看着老头离去的背影，阿雯感觉好像哪里不对，却又说不出，就在这里一直等。",
                "一个小时过去了，老头也没有回来，阿雯确认自己被骗了。",
                "身无分文的阿雯没办法只得回家，回去就和老爹说这次家长会取消了，应该能行。",
            };
            foreach (var text in texts2)
            {
                PutLastAndRename(InteractionCreator.Dialogue.Create(text));
            }

            {
                ch.GetComponent<Choice>().initCall = new UnityEvent();
                ch.GetComponent<Choice>().leftCall = new UnityEvent();
                ch.GetComponent<Choice>().rightCall = new UnityEvent();
                UnityEventTools.AddPersistentListener(ch.GetComponent<Choice>().initCall, userData.RentInit);
                UnityEventTools.AddPersistentListener(ch.GetComponent<Choice>().leftCall, userData.RentChooseLeft);
                UnityEventTools.AddPersistentListener(ch.GetComponent<Choice>().rightCall, userData.RentChooseRight);

                var chgoto = ch.GetComponent<Choice>().@goto = new InteractionBase.ConditionalGoto[2];
                chgoto[0] = new InteractionBase.ConditionalGoto();
                chgoto[1] = new InteractionBase.ConditionalGoto();
                chgoto[0].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(chgoto[0].condition, userData.Condition_IsRentLeftChosen);
                chgoto[0].interaction = b11.GetComponent<InteractionBase>();
                chgoto[1].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(chgoto[1].condition, userData.Condition_IsRentRightChosen);
                chgoto[1].interaction = b21.GetComponent<InteractionBase>();

                var b16goto = b16.GetComponent<InteractionBase>().@goto;
                b16goto[0].interaction = returnb.GetComponent<InteractionBase>();
                var b25goto = b25.GetComponent<InteractionBase>().@goto;
                b25goto[0].interaction = afterb.GetComponent<InteractionBase>();
            }

        }
        //Home
        {
            PutLastAndRename(InteractionCreator.ChangeBackground.Create(backGroundHome));
            PutLastAndRename(InteractionCreator.Dialogue.Create("回到家却发现老爹没在家，桌上留着一张字条。"));
            PutLastAndRename(InteractionCreator.Dialogue.Create("阿雯心一下凉了下来。"));
            PutLastAndRename(InteractionCreator.Dialogue.Create("（得赶快行动，为即将到来的暴风骤雨做好准备我得赶快找点东西保护好自己。）"));

            //Choice
            var cus = InteractionCreator.CustomInteraction.Copy(custom.gameObject);
            cus.name = "Custom";
            PutLast(cus);

            PutLastAndRename(InteractionCreator.Dialogue.Create("门外已经响起了开锁的声音。"));
            var beforeb = InteractionCreator.Dialogue.Create("阿雯老爸老妈一起气势汹汹进到屋内。");
            PutLastAndRename(beforeb);

            var b11 = InteractionCreator.Dialogue.Create("阿雯爸看到阿雯手拿菜刀站在屋内，气的一飞脚把阿雯踹飞在地，使出了36式擒拿手，成功的掰断了阿雯的胳膊。");
            var b12 = InteractionCreator.Dialogue.Create("于是阿雯愉快的在医院度过了愉快的假期。");
            var b21 = InteractionCreator.Dialogue.Create("阿雯爸看到阿雯竟然把鸡毛掸子藏了起来，气不打一处来，抽出腰带，刷出来雯家祖传九节鞭，成功把阿雯抽到医院。");
            var b22 = InteractionCreator.Dialogue.Create("于是阿雯愉快的在医院度过了愉快的假期。");
            var b31 = InteractionCreator.Dialogue.Create("老爸拿起鸡毛掸子对着阿雯屁股使劲一抽，咣的一声响彻屋内。");
            var b32 = InteractionCreator.Dialogue.Create("你还会防御了哈。");
            var b33 = InteractionCreator.Dialogue.Create("阿雯爸把阿雯扒了精光，用鸡毛掸子使出了一套少林棍法。");
            var b34 = InteractionCreator.Dialogue.Create("于是阿雯愉快的在医院度过了愉快的假期。");
            var b41 = InteractionCreator.Dialogue.Create("老爸拿起鸡毛掸子就要抽阿雯，阿雯眼疾手快，急忙跑进了自己的房间，老爸也跟着追了进去。");
            var b42 = InteractionCreator.Dialogue.Create("趁着老妈没进来，阿雯对老爸说。");
            var b43 = InteractionCreator.Dialogue.Create("你要是真打我，你就再也看不到你抽屉夹层里的私房钱了。");
            var b44 = InteractionCreator.Dialogue.Create("阿雯爸听到这里一愣。");
            var b45 = InteractionCreator.Dialogue.Create("于是配合阿雯在屋里演出了一场好戏。");
            var b46 = InteractionCreator.Dialogue.Create("阿雯因此躲过一劫，真正的过来一个愉快的假期。");
            var b51 = InteractionCreator.Dialogue.Create("阿雯爸抄起鸡毛掸子，对阿雯使出了鸡毛掸子三连击。");
            var b52 = InteractionCreator.Function.Create();
            var b521 = InteractionCreator.Dialogue.Create("于是阿雯愉快的在医院度过了愉快的假期。");
            var b522 = InteractionCreator.Dialogue.Create("阿雯在阿雯爸的暴打下坚持了下来，终于渡过了这完美的一天。");
            b11 .name = "S4-B11";
            b12 .name = "S4-B12";
            b21 .name = "S4-B21";
            b22 .name = "S4-B22";
            b31 .name = "S4-B31";
            b32 .name = "S4-B32";
            b33 .name = "S4-B33";
            b34 .name = "S4-B34";
            b41 .name = "S4-B41";
            b42 .name = "S4-B42";
            b43 .name = "S4-B43";
            b44 .name = "S4-B44";
            b45 .name = "S4-B45";
            b46 .name = "S4-B46";
            b51 .name = "S4-B51";
            b52 .name = "S4-B52";
            b521.name = "S4-B521";
            b522.name = "S4-B522";
            PutLast(b11  );
            PutLast(b12  );
            PutLast(b21  );
            PutLast(b22  );
            PutLast(b31  );
            PutLast(b32  );
            PutLast(b33  );
            PutLast(b34  );
            PutLast(b41  );
            PutLast(b42  );
            PutLast(b43  );
            PutLast(b44  );
            PutLast(b45  );
            PutLast(b46  );
            PutLast(b51  );
            PutLast(b52  );
            PutLast(b521 );
            PutLast(b522);
            var afterb = InteractionCreator.Idle.Create(0.35f);
            PutLastAndRename(afterb);

            {

                var beforeBGoto = beforeb.GetComponent<InteractionBase>().@goto = new InteractionBase.ConditionalGoto[5];
                for(int i=0; i < 5;i++)
                {
                    beforeBGoto[i] = new InteractionBase.ConditionalGoto();
                }
                beforeBGoto[0].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(beforeBGoto[0].condition, userData.Condition_IsKnife);
                beforeBGoto[0].interaction = b11.GetComponent<InteractionBase>();
                beforeBGoto[1].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(beforeBGoto[1].condition, userData.Condition_IsChicken);
                beforeBGoto[1].interaction = b21.GetComponent<InteractionBase>();
                beforeBGoto[2].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(beforeBGoto[2].condition, userData.Condition_IsCover);
                beforeBGoto[2].interaction = b31.GetComponent<InteractionBase>();
                beforeBGoto[3].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(beforeBGoto[3].condition, userData.Condition_IsMoney);
                beforeBGoto[3].interaction = b41.GetComponent<InteractionBase>();
                beforeBGoto[4].condition = null;
                beforeBGoto[4].interaction = b51.GetComponent<InteractionBase>();
                ;
                b12.GetComponent<InteractionBase>().@goto[0].interaction 
                    = afterb.GetComponent<InteractionBase>();
                b22.GetComponent<InteractionBase>().@goto[0].interaction
                    = afterb.GetComponent<InteractionBase>();
                b34.GetComponent<InteractionBase>().@goto[0].interaction
                    = afterb.GetComponent<InteractionBase>();
                b46.GetComponent<InteractionBase>().@goto[0].interaction
                    = afterb.GetComponent<InteractionBase>();


                b52.GetComponent<Function>().function = new UnityEvent();
                UnityEventTools.AddPersistentListener(b52.GetComponent<Function>().function, userData.Beat);
                var b52Goto = b52.GetComponent<InteractionBase>().@goto = new InteractionBase.ConditionalGoto[2];
                for (int i = 0; i < 2; i++)
                {
                    b52Goto[i] = new InteractionBase.ConditionalGoto();
                }
                b52Goto[0].condition = new UnityEvent();
                UnityEventTools.AddPersistentListener(b52Goto[0].condition, userData.Condition_IsNotHealth);
                b52Goto[0].interaction = b521.GetComponent<InteractionBase>();
                b52Goto[1].condition = null;
                b52Goto[1].interaction = b522.GetComponent<InteractionBase>();
                ;
                b521.GetComponent<InteractionBase>().@goto[0].interaction
                    = afterb.GetComponent<InteractionBase>();
                b522.GetComponent<InteractionBase>().@goto[0].interaction
                    = afterb.GetComponent<InteractionBase>();
            }




        }

        #region 游戏结束
        {
            PutLastAndRename(InteractionCreator.GameEnd.Create());
        }
        #endregion
    }
#endif

    [Button("DeleteAllChildren")]
    private void DeleteAllChildren()
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform child in transform)
        {
            list.Add(child);
        }
        foreach (Transform child in list)
        {
            DestroyImmediate(child.gameObject);
        }


    }
}
