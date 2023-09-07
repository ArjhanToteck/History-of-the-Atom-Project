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
            child.name = "Slide";
			slides.Add(child);
        }

		// places current and next slides in containers
		slides[0].gameObject.SetActive(true);
		modelRotation.target = slides[0];
		slides[0].SetParent(CurrentSlideContainer);

		slides[1].SetParent(NextSlidesContainer);
	}

    public void Transition()
    {
        // tell animator to begin transition animation
        animator.SetTrigger("transition");
    }

	public void OnTransitionFinish()
	{
		// place old slide in regular container
		slides[currentSlide].SetParent(SlidesContainer);
		slides[currentSlide].gameObject.SetActive(false);

		// increment slide
		currentSlide++;

		// makes sure current slide isn't out of list bounds
		if(currentSlide >= slides.Count)
		{
			currentSlide = 0;
		}

		// place new slide in current container
		slides[currentSlide].SetParent(CurrentSlideContainer);

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
	}
}
