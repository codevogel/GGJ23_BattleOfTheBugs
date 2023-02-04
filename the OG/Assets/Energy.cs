using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{

    [SerializeField]
    private int _currentEnergy;
    [SerializeField]
    private int _maxEnergy;


    [SerializeField]
    private int _decrementAmount;
    [SerializeField]
    private float _decrementWaitTime;

    [SerializeField]
    private EnergyBarRenderer _energyBarRenderer;

    private void Start()
    {
        StartCoroutine(DecrementEnergyOverTime());
    }

    public IEnumerator DecrementEnergyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_decrementWaitTime);
            DecreaseEnergy(_decrementAmount);
        }
    }

    public void IncreaseEnergy(int amount)
    {
        if (amount <= 0)
            throw new NotSupportedException("Tried increasing energy by 0 or a negative value. This is not supported!");
        AlterHealth(amount);
    }

    public void DecreaseEnergy(int amount)
    {
        if (amount <= 0)
            throw new NotSupportedException("Tried increasing energy by 0 or a negative value. This is not supported!");
        AlterHealth(-amount);
    }

    private void AlterHealth(int health)
    {
        _currentEnergy = Mathf.Clamp(_currentEnergy + health, 0, _maxEnergy);
        _energyBarRenderer.RenderHealth(_currentEnergy, _maxEnergy);
    }
}
