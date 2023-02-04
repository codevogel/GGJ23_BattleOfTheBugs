using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject tree;

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Acorn;
  
    [SerializeField]
    private GameObject spawnPoint;

    private AcornBehaviour acornBehaviour;
    private Vector2 inputVector;

    public float shootingCoolDown = 1.5f;

    private float time;

    private void Awake()
    {
        EventManager.OnPlayer1AimPerformed += AimTowards;
        EventManager.OnPlayer1Attack += SpawnAcorn;
       
    }

    private void FixedUpdate()
    {
        float rads = Mathf.Atan2(inputVector.x, inputVector.y);
        float degrees = rads * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Euler(new Vector3(0,0, -degrees));
        transform.position = new Vector3(tree.transform.position.x, tree.transform.position.y +2 , tree.transform.position.z);
    }

    public void AimTowards(Vector2 input)
    {
        inputVector = input.normalized;
    }

    private void SpawnAcorn()
    {
        if (time > 0)
            return;

        if (inputVector == Vector2.zero.normalized)
            inputVector = Vector2.up.normalized;

        GameObject newAcorn = Instantiate(Acorn);
        newAcorn.transform.position = spawnPoint.transform.position;
        acornBehaviour = newAcorn.GetComponent<AcornBehaviour>();
        acornBehaviour.moveVector = inputVector;
        time = shootingCoolDown;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayer1AimPerformed -= AimTowards;
        EventManager.OnPlayer1Attack -= SpawnAcorn;
    }

    private void Update()
    {
        time -= Time.deltaTime;
    }

}
