using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFunctions : MonoBehaviour
{
    public Vector3 SkillPosition;

    public SkillFunctions skillPrefab;

    public bool IsThreeBall;

    public static SkillFunctions Instance;

    private List<ParticleSystem> _particles = new List<ParticleSystem>();

    [Header("Map Range")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
    [SerializeField] private float _height;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }



    public void ElectricSkill(SkillData skillData)
    {
        var skill = Instantiate(skillData.Particles, SkillPosition, skillData.Particles.transform.rotation);
        Debug.Log(skill.transform.position);

    }

    public void FireSkill(SkillData skillData)
    {
        var skill = Instantiate(skillData.Particles, SkillPosition, skillData.Particles.transform.rotation);
        Debug.Log(skill.transform.position);

    }

    public void MagnetSkill(SkillData skillData)
    {
        var skill = Instantiate(skillData.Particles, SkillPosition, skillData.Particles.transform.rotation);
        Debug.Log(skill.transform.position);
    }

    public void ThreeBallSkill(SkillData skillData)
    {
        SkillFunctions.Instance.IsThreeBall = true;
        Debug.Log("x");
    }

    public void SpawnSoldiers()
    {
        for (int i = 0; i < 2; i++)
        {
            Allys.Instance.SpawnSoldiers();
        }
    }

    public void HealthSkill(SkillData skillData)
    {
        foreach (var item in Allys.Instance.Soldiers)
        {
            if (item != null)
            {
                item.Health.ReduceHealth(-20);
                var healthSkill = Instantiate(skillData.Particles, item.transform.position, item.transform.rotation);
                Destroy(healthSkill, 2.5f);
            }
        }
    }

    public void SoulBomb(SkillData skillData)
    {
        for (int i = 0; i < 7; i++)
        {
            Vector3 pos = CalculateRandomPoint();
            var skill = Instantiate(skillData.Particles, pos, skillData.Particles.transform.rotation);
        }
    }

    private Vector3 CalculateRandomPoint()
    {
        float x = Random.Range(_minX, _maxX);
        float y = Random.Range(_height - 2, _height + 2);
        float z = Random.Range(_minZ, _maxZ);

        Vector3 point = new Vector3(x, y, z);

        return point;
    }

    private void SetParticleList(SkillData skillData)
    {
        ParticleSystem temp = skillData.Particles.GetComponent<ParticleSystem>();
        if (temp != null)
        {
            _particles.Add(temp);
        }

        foreach (var item in skillData.Particles.GetComponentsInChildren<ParticleSystem>())
        {
            if (item.GetComponent<ParticleSystem>() != null)
            {
                _particles.Add(item);
            }
        }
    }
}

