using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //[SerializeField] private float _health;
    [SerializeField] private float _healthValue;

    [SerializeField] private Image _healthBar;

    [SerializeField] private bool _isTower;

    private Enemy _enemy;

    private float _firstHealth;

    public int goldValue = 10;
    public GoldManager goldManager;
    private void Start()
    {
        _firstHealth = _healthValue;
        _enemy = GetComponent<Enemy>();
    }

    public float GetHealth()
    {
        return _healthValue;
    }

    public void ReduceHealth(float value)
    {
        _healthValue -= value;

        if(_healthBar != null)
        {
            _healthBar.fillAmount = _healthValue / _firstHealth;
        }

        if(_healthValue <= 0)
        {
            if (_enemy != null)
            {
                //AudioManager.instance.Play("BlastSound");
                GoldManager.Instance.AddGold(goldValue);
                _enemy.PlayDeathParticle();
                EnemyDetector.Instance.GetEnemies().Remove(GetComponent<Enemy>());
            }
            if (_isTower)
            {
                SceneController.Instance.OpenLosePanel();
                return;
            }
            Destroy(gameObject);
        }

    }
}
