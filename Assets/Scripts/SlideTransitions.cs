using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTransitions : MonoBehaviour
{
    List<Transform> slides;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < transform.childCount; i++) {
            slides.Add(transform.GetChild(i));
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
