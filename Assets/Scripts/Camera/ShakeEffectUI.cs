using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffectUI : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> objects;
    public float force = 20f;
    public float time = 0.25f;

    private Coroutine co;

    public void Shake()
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(c_Shake(time));
    }

    IEnumerator c_Shake(float time)
    {
        List<Vector3> trans = new List<Vector3>();
        for (int i = 0; i < objects.Count; i++)
            trans.Add(objects[i].transform.position);

        float baseTime = 0;

        while (baseTime < time)
        {

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = trans[i];
            }

            Vector3 random = Random.insideUnitSphere;
            baseTime += Time.deltaTime;
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position += new Vector3(random.x * force, random.y * force, 0);
            }
            yield return null;
        }

        for (int i = 0; i < objects.Count; i++)
            objects[i].transform.position = trans[i];

    }

}