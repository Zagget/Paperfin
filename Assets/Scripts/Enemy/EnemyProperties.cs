using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public float mouthWidth;
    public bool isFollowing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && !target.GetComponent<EnvironmentEffects>().isVisible)
        {
            target = null;
            isFollowing = false;
        }
    }
}
