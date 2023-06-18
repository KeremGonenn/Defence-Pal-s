using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private List<SkillData> _skillEvents;

    [SerializeField] private List<Card> _cardList;

    [SerializeField] private GameObject _skillCardPanel;

    private HashSet<int> _idList;
    
    private SkillData _skill;

    public static SkillSystem Instance;

    public SkillEnums skillEnums;

    private int _currentId = -1;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _idList = new HashSet<int>();

        PlaceDataOnCards();
    }

    public void PlaceDataOnCards()
    {
        _idList = new HashSet<int>();
        foreach (var item in _cardList)
        {
            while (true)
            {
                _skill = GetSkillNewData();
                if (!_idList.Contains(_skill.Id))
                {
                    _idList.Add(_skill.Id);
                    SetDataOnCard(item, _skill);
                    break;
                }
            }
        }
    }

    public void PressSkillCard(int order)
    {
        if(order == 1)
        {
            _currentId = _cardList[0].Id;
            Debug.Log("1");
        }
        else if(order == 2)
        {
            _currentId = _cardList[1].Id;
            Debug.Log("2");
        }

        SetEnum(_skillEvents[_currentId].SkillEnums);

        if(skillEnums == SkillEnums.OnPressed)
        {
            ExecuteSkill();
        }

        _skillCardPanel.transform.DOScale(Vector3.zero, .25f).SetEase(Ease.Linear).OnComplete(() => 
        {
            _skillCardPanel.SetActive(false);
        });

    }

   
    //public void OpenSkillCards()
    //{
    //    _skillCardPanel.SetActive(true);
    //    _skillCardPanel.transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack);
    //}

    public void ExecuteSkill()
    {
        _skillEvents[_currentId].Event?.Invoke();
    }


    private void SetEnum(SkillEnums skill)
    {
        skillEnums = skill;
    }


    private SkillData GetSkillNewData()
    {
        SkillData skillData = _skillEvents[Random.Range(0, _skillEvents.Count)];
        return skillData;
    }

    public void SetDataOnCard(Card card, SkillData data)
    {
        card.Id = data.Id;
        card.Header.text = data.Header;
        card.Image.sprite = data.SkillImage;
        card.Description.text = data.Description;
    }

}

public enum SkillEnums
{
    None,
    BallExplosion,
    OnPressed,

}

[Serializable]
public class Card
{
    public int Id;
    public TMP_Text Header;
    public Image Image;
    public TMP_Text Description;
}
