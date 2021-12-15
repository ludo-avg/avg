using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Extensions;
using Modules;

public class ConfirmBoxButton : UIInSceneInteract
{
    private SpriteRenderer background;
    private float onHoldingCurrentTime = -1f;



    private void Awake()
    {
        background = transform.LudoFind("Sprite", includeInactive:true).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.time - onHoldingCurrentTime < 0.05f)
        {
            background.color = new Color(0.5f,0.5f,0.5f, 1f);
        }
        else
        {
            background.color = new Color(1f, 1f, 1f, 1f);
        }

    }

    public override void OnClick()
    {
        if(gameObject.name == "ButtonYes")
        {
            transform.GetComponentInParent<ConfirmBox>().Yes();
        }
        else if (gameObject.name == "ButtonNo")
        {
            transform.GetComponentInParent<ConfirmBox>().No();
        }
    }

    public override void OnHolding()
    {
        onHoldingCurrentTime = Time.time;
    }
}
