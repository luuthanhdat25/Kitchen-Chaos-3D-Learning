using System;
using KitchenObjects.Counter;
using UnityEngine;

public class ContainerCounterAnimation : RepeatMonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    [SerializeField] private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadContainerCounterComponent();
        LoadAnimatorComponent();
    }

    private void LoadContainerCounterComponent()
    {
        if(this.containerCounter != null) return;
        this.containerCounter = FindComponentInParent<ContainerCounter>();
    }

    private void LoadAnimatorComponent()
    {
        if(this.animator != null) return;
        this.animator = GetComponent<Animator>();
    }

    private void Start() => SubscribeOnPlayerGrabbedObjectEvents();

    private void SubscribeOnPlayerGrabbedObjectEvents()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        this.animator.SetTrigger(OPEN_CLOSE);
    }
}
