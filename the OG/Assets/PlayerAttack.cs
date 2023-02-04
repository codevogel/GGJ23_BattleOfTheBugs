using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Acorn;
  
    [SerializeField]
    private GameObject spawnPoint;

    private AcornBehaviour acornBehaviour;
    private Vector2 _inputVector;
    private float distance = 1f;

    public float shootingCoolDown = 1.5f;

    private void Awake()
    {
        EventManager.OnPlayerAimPerformed += AimTowards;
        EventManager.OnPlayerAttack += SpawnAcorn;
    }

    private void FixedUpdate()
    {
        float rads = Mathf.Atan2(_inputVector.x, _inputVector.y);
        float degrees = rads * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Euler(new Vector3(0,0, -degrees));
    }

    public void AimTowards(Vector2 input)
    {
        _inputVector = input.normalized;
    }

    private void SpawnAcorn()
    {

        GameObject newAcorn = Instantiate(Acorn);
        newAcorn.transform.position = spawnPoint.transform.position;
        acornBehaviour = newAcorn.GetComponent<AcornBehaviour>();
        acornBehaviour.moveVector = _inputVector;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerAimPerformed -= AimTowards;
        EventManager.OnPlayerAttack -= SpawnAcorn;
    }

}
