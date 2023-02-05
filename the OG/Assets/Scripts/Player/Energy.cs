using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Energy : MonoBehaviour
{

    [SerializeField]
    private float _currentEnergy;
    [SerializeField]
    private float _maxEnergy;


    [SerializeField]
    private float _decrementAmount;
    [SerializeField]
    private float _decrementWaitTime;

    [SerializeField]
    private EnergyBarRenderer _energyBarRenderer;

    private bool m_InLight = false;

    private void Awake()
    {
	    EventManager.OnStartGame += OnStartGame;
	    this.enabled = false;
    }

    private void OnStartGame()
    {
	    this.enabled = true;
    }

	private void Update()
    {
        var amount = _decrementAmount * Time.deltaTime;

        if (m_InLight)
		{
			IncreaseEnergy(amount * 3);
		}
		else
		{
			DecreaseEnergy(amount * 10);
		}
    }

    public void IncreaseEnergy(float amount)
    {
        if (amount <= 0)
            throw new NotSupportedException("Tried increasing energy by 0 or a negative value. This is not supported!");
        AlterHealth(amount);
    }

    public void DecreaseEnergy(float amount)
    {
        if (amount <= 0)
            throw new NotSupportedException("Tried increasing energy by 0 or a negative value. This is not supported!");
        AlterHealth(-amount);
    }

    private void AlterHealth(float health)
    {
	    if (_currentEnergy + health <= 0)
	    {
            //TODO CHANGE TO BUILDINDEX OR SOMETHING FIX ME SENPAPI
            if(SceneManager.GetActiveScene().name == "Level1 Tut")
            {
                SceneManager.LoadScene("Level1 Tut");
            }else
            {
                GameStateManager.LoadScene("LossScene");
            }
            
	    }
        _currentEnergy = Mathf.Clamp(_currentEnergy + health, 0, _maxEnergy);
        _energyBarRenderer.RenderHealth(_currentEnergy, _maxEnergy);
    }

    public void SetInLight(bool inLight)
    {
	    m_InLight = inLight;
    }

    private void OnDestroy()
    {
	    EventManager.OnStartGame -= OnStartGame;
    }
}
