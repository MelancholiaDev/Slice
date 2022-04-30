using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _winScreen, _loseScreen;

    private CinemachineVirtualCamera cineCam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void ShowWinScreen()
    {
        FindInActiveObjectByName("Win Screen").gameObject.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = null;
    }

    public GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
