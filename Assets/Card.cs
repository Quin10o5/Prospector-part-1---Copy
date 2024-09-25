using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Card : MonoBehaviour
{

    public char suit;
    public int rank;
    public Color color = Color.black;
    public string colS = "Black";
    public GameObject back;
    public JsonCard def;
    
    public List<GameObject> decoGOs = new List<GameObject>();
    public List<GameObject> pipGOs = new List<GameObject>();
    
    // Start is called before the first frame update
    public void Init(char eSuit, int eRank, bool startFaceUp = true)
    {
        gameObject.name = name = eSuit.ToString() + eRank;
        suit = eSuit;
        rank = eRank;
        if (suit == 'D' || suit == 'H')
        {
            colS = "Red";
            color = Color.red;
        }

        def = JsonParseDeck.GET_CARD_DEF(rank);
        AddDecorators();
        AddPips();
        AddFace();
        AddBack();
        faceUp = startFaceUp;
    }

    public bool faceUp
    {
        get {return(!back.activeSelf);}
        set{ back.SetActive(!value);}
    }
    
    public virtual void SetLocalPos(Vector3 v)
    {
        transform.localPosition = v;
    }


    private Sprite _tSprite = null;
    private GameObject _tGO = null;
    private SpriteRenderer _tSRend = null;
    private Quaternion _flipRot = Quaternion.Euler(0, 0, 180);

    private void AddDecorators()
    {
        foreach (JsonPip pip in JsonParseDeck.DECORATORS)
        {
            if (pip.type == "suit")
            {
                _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB,transform);
                _tSRend = _tGO.GetComponent<SpriteRenderer>();
                _tSRend.sprite = CardSpritesSO.SUITS[suit];
            }
            else
            {
                _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB,transform);
                _tSRend = _tGO.GetComponent<SpriteRenderer>();
                _tSRend.sprite = CardSpritesSO.RANKS[rank];
                _tSRend.color = color;
            }

            _tSRend.sortingOrder = 1;
            _tGO.transform.localPosition = pip.loc;
            if (pip.flip) _tGO.transform.localRotation = _flipRot;
            if (pip.scale != 1) _tGO.transform.localScale = Vector3.one * pip.scale;
            _tGO.name = pip.type;
            decoGOs.Add(_tGO);
        }
    }

    private void AddPips()
    {
        int pipNum = 0;
        foreach (JsonPip pip in def.pips)
        {
            _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB,transform);
            _tGO.transform.localPosition = pip.loc;
            
            if (pip.flip) _tGO.transform.localRotation = _flipRot;
            if (pip.scale != 1) _tGO.transform.localScale = Vector3.one * pip.scale;
            _tGO.name = "pip_" + pipNum++;
            _tSRend = _tGO.GetComponent<SpriteRenderer>();
            _tSRend.sprite = CardSpritesSO.SUITS[suit];
            _tSRend.sortingOrder = 1;
            pipGOs.Add(_tGO);

        }
    }

    private void AddFace()
    {
        if (def.face == "")
        {
            return;
        }
        
        string faceName = def.face + suit;

        _tSprite = CardSpritesSO.GET_FACE(faceName);
        if (_tSprite == null)
        {
            Debug.LogError("face sprite  " + faceName + " not found");
            return;
        }
        
        _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB,transform);
        
        _tSRend = _tGO.GetComponent<SpriteRenderer>();
        _tSRend.sprite = _tSprite;
        _tSRend.sortingOrder = 1;
        _tGO.transform.localPosition = Vector3.zero;
        _tGO.name = faceName;

    }

    private void AddBack()
    {
        _tGO = Instantiate(Deck.SPRITE_PREFAB,transform);
        _tSRend = _tGO.GetComponent<SpriteRenderer>();
        _tSRend.sprite = CardSpritesSO.BACK;
        _tGO.transform.localPosition = Vector3.zero;
        _tSRend.sortingOrder = 2;
        _tGO.name = "back";
        back = _tGO;
    }
    
}
