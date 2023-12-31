using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTransitions : MonoBehaviour
{
    public Transform CurrentSlideContainer;
	public Transform NextSlidesContainer;
	public Transform SlidesContainer;

	public ModelRotation modelRotation;

	public Animator animator;

	List<Transform> slides;
    int currentSlide = 0;

    // Start is called before the first frame update
    void Start()
    {
		slides = new List<Transform>();

		// gets all slides (currently children of slides container
		for (int i = 0; i < SlidesContainer.childCount; i++) {
            Transform child = SlidesContainer.GetChild(i);
			slides.Add(child);
        }

		// places current slides in container
		slides[0].gameObject.SetActive(true);
		slides[0].SetParent(CurrentSlideContainer);

		// set rotation model
		Transform model = slides[0].Find("Model");

		if (!!model)
		{
			modelRotation.model = model;
		}

		// places next slide in container
		slides[1].SetParent(NextSlidesContainer);
	}

    public void Transition()
	{
		// no rotations during transition
		modelRotation.model = null;

		// renable animator for transition
		animator.enabled = true;

		// show next slide
		slides[currentSlide].position = Vector3.zero;

		// make sure next slide isn't out of list bounds
		if (currentSlide + 1 < slides.Count)
		{
			slides[currentSlide + 1].gameObject.SetActive(true);
		}
		else
		{
			// if out of bounds, the next slide is the first slide
			slides[0].gameObject.SetActive(true);
		}

		// tell animator to begin transition animation
		animator.SetTrigger("transition");
	}

	public void OnTransitionFinish()
	{
		// disable animator to stop it from fucking stuff up
		animator.enabled = false;

		// place old slide in regular container and deactivate
		slides[currentSlide].SetParent(SlidesContainer);
		slides[currentSlide].gameObject.SetActive(false);

		// reset position
		CurrentSlideContainer.position = Vector3.zero;
		slides[currentSlide].position = Vector3.zero;

		// increment slide
		currentSlide++;

		// makes sure current slide isn't out of list bounds
		if(currentSlide >= slides.Count)
		{
			currentSlide = 0;
		}

		// place new slide in current container
		slides[currentSlide].SetParent(CurrentSlideContainer);

		// reset position
		CurrentSlideContainer.position = Vector3.zero;
		slides[currentSlide].position = Vector3.zero;

		// make sure next slide isn't out of list bounds
		if (currentSlide + 1 < slides.Count)
		{
			slides[currentSlide + 1].SetParent(NextSlidesContainer);
		}
		else
		{
			// if out of bounds, the next slide is the first slide
			slides[0].SetParent(NextSlidesContainer);
		}

		// set rotation model
		Transform model = slides[currentSlide].Find("Model");

		if (!!model)
		{
			modelRotation.model = model;
		}
	}
}
