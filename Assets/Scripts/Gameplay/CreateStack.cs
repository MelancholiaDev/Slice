using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStack : MonoBehaviour
{
    [SerializeField] private GameObject[] _multiplierBanners;
    [SerializeField] private List<GameObject> finalMultiplierOrder = new List<GameObject>();
    [SerializeField] private float height;
    [SerializeField] List<GameObject> tempList = new List<GameObject>();



    private void Start()
    {
        Stack();
    }

    public void Stack()
    {

        foreach (var item in _multiplierBanners)
        {
            tempList.Add(item);
        }

        for (int i = 0; i < _multiplierBanners.Length; i++)
        {
            int r = Random.Range(0, tempList.Count - 1);

            finalMultiplierOrder.Add(tempList[r]);

            tempList.Remove(tempList[r]);
        }

        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + height, this.transform.position.z);
        Instantiate(finalMultiplierOrder[0], pos, finalMultiplierOrder[0].transform.rotation, this.gameObject.transform);
        height += 2;

        for (int i = 1; i < finalMultiplierOrder.Count; i++)
        {
            pos = new Vector3(this.transform.position.x, this.transform.position.y + height, this.transform.position.z);
            Instantiate(finalMultiplierOrder[i], pos, finalMultiplierOrder[i].transform.rotation, this.gameObject.transform);
            height += 2;

        }
    }
}
