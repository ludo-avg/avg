using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionList : MonoBehaviour
{
    public static InteractionList singleton = null;
    public List<object> list;

    //setting
    [SerializeField] GameObject teacher = null;
    [SerializeField] GameObject laoMuGen = null;
    [SerializeField] GameObject Farmer = null;
    [SerializeField] Interactions.ChangeBackGround backGroundSchoolGate = null;
    [SerializeField] Interactions.ChangeBackGround backGroundPeopleMarket = null;
    [SerializeField] Interactions.ChangeBackGround backGroundRentFatherCorner = null;
    [SerializeField] Interactions.TimeChoice timeChoice = null;
    [SerializeField] Interactions.Choice choice = null;
    [SerializeField] Interactions.CustomInteraction custom = null;

    void Awake()
    {
        singleton = this;
        InitList(); //也可以不在Awake初始，在第一次获取list时初始化。
    }

    void InitList()
    {
        list = new List<object>();

        //--------------------------------------------------------
        list.Add(new Interactions.Idle(0.35f));
        //教室内
        {
            string[] texts = new string[]
            {
                "期末考试终于结束了，今天是这个学期最后一次登校，下午开完家长会，愉快的暑假就要到来了。",
                "阿雯正在幻想着如何度过这越快的假期，丝毫没有注意老师在讲台上讲什么。",
                "这时阿雯顿感一股杀气袭来，迎面飞来一个粉笔头！",
            };
            foreach (var text in texts)
            {
                Interactions.Dialogue d = new Interactions.Dialogue(text);
                list.Add(d);
            }
            //
            list.Add(timeChoice);
            //
            Interactions.Character teacherShow = new Interactions.Character(teacher, true);
            list.Add(teacherShow);
            ;
            string text2 = "阿雯，你看你考这个成绩，三科合起来考了24分，让狗踩出来的答题卡，都得比你分高，一会儿下午家长会，我非得让你爹打断你的腿。";
            Interactions.Dialogue d2 = new Interactions.Dialogue(text2, "老师");
            list.Add(d2);
            ;
            Interactions.Character teacherHide = new Interactions.Character(teacher, false);
            list.Add(teacherHide);
        }
        //校门口
        {
            list.Add(backGroundSchoolGate);
            //
            string text3 = "阿雯愁眉苦脸的拿着成绩单，站在学校门口不知所措，这时老穆棍悄咪咪的走了过来。";
            Interactions.Dialogue d3 = new Interactions.Dialogue(text3);
            list.Add(d3);
            //
            Interactions.Character laoMuGenShow = new Interactions.Character(laoMuGen, true);
            list.Add(laoMuGenShow);
            ;
            string text4 = "阿雯，走，我带你去个好地方。";
            Interactions.Dialogue d4 = new Interactions.Dialogue(text4, "老穆根");
            list.Add(d4);
            ;
            Interactions.Character laoMuGenHide = new Interactions.Character(laoMuGen, false);
            list.Add(laoMuGenHide);
            //
            string text5 = "说着老穆棍拉着阿雯快速离开了学校。";
            Interactions.Dialogue d5 = new Interactions.Dialogue(text5);
            list.Add(d5);
        }
        //人才市场大门口
        {
            list.Add(backGroundPeopleMarket);
            //
            string text = "来到了一个地方，门上写着人才市场。";
            Interactions.Dialogue d = new Interactions.Dialogue(text);
            list.Add(d);
            //
            Interactions.Character laoMuGenShow = new Interactions.Character(laoMuGen, true);
            list.Add(laoMuGenShow);
            ;
            string text2 = "像我们这种人才，就应该来这种人才市场。";
            Interactions.Dialogue d2 = new Interactions.Dialogue(text2, "老穆根");
            list.Add(d2);
            ;
            Interactions.Character laoMuGenHide = new Interactions.Character(laoMuGen, false);
            list.Add(laoMuGenHide);
            //
            string text3 = "我们来干啥啊？";
            Interactions.Dialogue d3 = new Interactions.Dialogue(text3);
            list.Add(d3);
            //
            list.Add(laoMuGenShow);
            ;
            string text4 = "我们来给你租个爹开家长会啊。";
            Interactions.Dialogue d4 = new Interactions.Dialogue(text4, "老穆根");
            list.Add(d4);
            ;
            list.Add(laoMuGenHide);
            //
            string text5 = "但是我只有10块钱啊。";
            Interactions.Dialogue d5 = new Interactions.Dialogue(text5);
            list.Add(d5);
            //
            list.Add(laoMuGenShow);
            ;
            string text6 = "先看看再说。";
            Interactions.Dialogue d6 = new Interactions.Dialogue(text6, "老穆根");
            list.Add(d6);
            ;
            list.Add(laoMuGenHide);
        }
        //租爹专区
        {
            Interactions.Character laoMuGenShow = new Interactions.Character(laoMuGen, true);
            Interactions.Character laoMuGenHide = new Interactions.Character(laoMuGen, false);
            Interactions.Character FarmerShow = new Interactions.Character(Farmer, true);
            Interactions.Character FarmerHide = new Interactions.Character(Farmer, false);

            list.Add(backGroundRentFatherCorner);
            //
            string text = "老穆棍领着阿雯来到了租爹专区。";
            Interactions.Dialogue d = new Interactions.Dialogue(text);
            list.Add(d);
            //
            string text2 = "阿雯看着这个场景惊讶不已。";
            Interactions.Dialogue d2 = new Interactions.Dialogue(text2, "老穆根");
            list.Add(d2);
            //
            string text3 = "现在这么流行租爹吗？";
            Interactions.Dialogue d3 = new Interactions.Dialogue(text3);
            list.Add(d3);
            //
            list.Add(laoMuGenShow);
            ;
            string text4 = "哪里有需求哪里就有市场啊。";
            Interactions.Dialogue d4 = new Interactions.Dialogue(text4, "老穆根");
            list.Add(d4);
            //
            string text5 = "我先走了，你在这选吧。";
            Interactions.Dialogue d5 = new Interactions.Dialogue(text5, "老穆根");
            list.Add(d5);
            ;
            list.Add(laoMuGenHide);
            //
            list.Add(choice);
            //
            string text6 = "阿雯看到500的牌子脑袋有点转不过弯，没想明白为啥要选500的，说话都开始磕巴了。";
            Interactions.Dialogue d6 = new Interactions.Dialogue(text6);
            list.Add(d6);
            //
            string text7 = "5.。。5.。。";
            Interactions.Dialogue d7 = new Interactions.Dialogue(text7);
            list.Add(d7);
            //
            list.Add(FarmerShow);
            string text8 = "5块钱成交！";
            Interactions.Dialogue d8 = new Interactions.Dialogue(text8);
            list.Add(d8);
            list.Add(FarmerHide);
            //
            string[] texts = new string[]
            {
                "这价格波动让阿雯始料未及，现在抹零都是这么抹的吗？",
                "于是阿雯颤颤巍巍的递出了自己仅有的10块钱。",
                "邋遢老头拿过钱说：",
            };
            foreach (var oneText in texts)
            {
                Interactions.Dialogue dia = new Interactions.Dialogue(oneText);
                list.Add(dia);
            }
            //
            list.Add(FarmerShow);
            string text9 = "小朋友，我这手里没零钱，我买几个橘子去。你就在此地，不要走动。";
            Interactions.Dialogue d9 = new Interactions.Dialogue(text9);
            list.Add(d9);
            list.Add(FarmerHide);
            //
            string[] texts2 = new string[]
            {
                "看着老头离去的背影，阿雯感觉好像哪里不对，却又说不出，就在这里一直等。",
                "一个小时过去了，老头也没有回来，阿雯确认自己被骗了。",
                "身无分文的阿雯没办法只得回家，回去就和老爹说这次家长会取消了，应该能行",
            };
            foreach (var oneText in texts2)
            {
                Interactions.Dialogue dia = new Interactions.Dialogue(oneText);
                list.Add(dia);
            }
        }
        //Home
        {
            list.Add(custom);
        }
        //游戏结束
        {
            list.Add(new Interactions.GameEnd());
        }
    }

    public void Insert(int position, object item)
    {
        list.Insert(position, item);
    }
}
