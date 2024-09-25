using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "CardSprite", menuName = "ScriptableObjects/CardSpriteSO")]
public class CardSpritesSO : ScriptableObject
{

    public Sprite cardBack;
    public Sprite cardBackGold;
    public Sprite cardFront;
    public Sprite cardFrontGold;


    public Sprite suitClub;
    public Sprite suitDiamond;
    public Sprite suitHeart;
    public Sprite suitSpade;

    public Sprite[] faceSprites;
    public Sprite[] rankSprites;

    private static CardSpritesSO S; 
    public static Dictionary<char,Sprite> SUITS { get; private set; }
    // Start is called before the first frame update
    public void Init()
    {
        INIT_STATICS(this);
    }

    // Update is called once per frame
    static void INIT_STATICS(CardSpritesSO cSSO)
    {
        if (S != null)
        {
            Debug.LogError("CardSpriteSO is already instantiated");
            return;
        }

        S = cSSO;

        SUITS = new Dictionary<char, Sprite>()
        {
            { 'C', S.suitClub },
            { 'D', S.suitDiamond },
            { 'H', S.suitHeart },
            { 'S', S.suitSpade },
        };
    }

    public static Sprite[] RANKS
    {
        get { return S.rankSprites; }
    }

    public static Sprite GET_FACE(string name)
    {
        foreach (Sprite spr in S.faceSprites)
        {
            if(spr.name == name) return spr;
        }

        return null;
    }

    public static Sprite BACK
    {
        get{return S.cardBack;}
    }

    public static void RESET()
    {
        S = null;
    }
    
}
