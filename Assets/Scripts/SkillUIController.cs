using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillUIController : MonoBehaviour
{
    [SerializeField] private GameObject _skillCardPanel;

    [SerializeField] private GameObject _skillIndicator;

    [SerializeField]

    public static SkillUIController Instance;
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

    private void OnEnable()
    {
        _skillIndicator.transform.DOScale(Vector3.one * .35f, .5f).SetLoops(-1,LoopType.Yoyo);
    }

    public void OpenSpecialPowerUI()
    {
        _skillIndicator.SetActive(true);
    }

    public void OpenSkillCards()
    {
        SkillSystem.Instance.PlaceDataOnCards();
        _skillIndicator.SetActive(false);
        _skillCardPanel.SetActive(true);
        _skillCardPanel.transform.DOScale(new Vector3(0.81f,.61f,.61f), .25f).SetEase(Ease.OutBack);
    }
}
