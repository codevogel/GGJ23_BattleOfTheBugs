using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject tree;
    public float shootingCoolDown = 1.5f;

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Acorn;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject birb;

    private AcornBehaviour acornBehaviour;
    private Vector2 inputVector;
    private float time;
    private bool noAim = true;
    

    private void Awake()
    {
        EventManager.OnPlayerAimPerformed += AimTowards;
        EventManager.OnPlayerAimCanceled += FadePointer;
        EventManager.OnPlayerAttack += SpawnAcorn;   
    }

    private void FixedUpdate()
    {
        float rads = Mathf.Atan2(inputVector.x, inputVector.y);
        float degrees = rads * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Euler(new Vector3(0,0, -degrees));
        transform.position = new Vector3(tree.transform.position.x, tree.transform.position.y +2 , tree.transform.position.z);
    }

    private void AimTowards(Vector2 input)
    {
        StopCoroutine(FadeTo(0.0f, 0.1f));
        StartCoroutine(FadeTo(1.0f, 0.1f));
        inputVector = input.normalized;
        noAim = false;
    }

    private void FadePointer()
    {
        StopCoroutine(FadeTo(1.0f, 0.1f));
        StartCoroutine(FadeTo(0.0f, 0.1f));
        noAim = true;
        
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        SpriteRenderer[] spriterenders = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in spriterenders)
        {
            float alpha = sprite.material.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
                sprite.material.color = newColor;
                yield return null;
            }
            Color finaleColor = new Color(1, 1, 1, aValue);
            sprite.material.color = finaleColor;
        }
    }

    private void SpawnAcorn()
    {
        if (time > 0)
            return;

        if (noAim)
            return;

        birb.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        GameObject newAcorn = Instantiate(Acorn);
        newAcorn.transform.position = spawnPoint.transform.position;
        acornBehaviour = newAcorn.GetComponent<AcornBehaviour>();
        acornBehaviour.moveVector = inputVector;
        time = shootingCoolDown;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerAimPerformed -= AimTowards;
        EventManager.OnPlayerAimCanceled -= FadePointer;
        EventManager.OnPlayerAttack -= SpawnAcorn;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if(time < 0)
            birb.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

}
