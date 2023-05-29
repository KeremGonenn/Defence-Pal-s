using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private List<SkillData> _skillEvents;

    [SerializeField] private List<Card> _cardList;

    private HashSet<int> _idList;
    
    private SkillData _skill;


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

[Serializable]
public struct Card
{
    public int Id;
    public TMP_Text Header;
    public Image Image;
    public TMP_Text Description;
}
