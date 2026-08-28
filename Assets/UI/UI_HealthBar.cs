using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity;
    private CharacterStats mystats;
    private RectTransform myTransform;
    private Slider slider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        mystats = GetComponentInParent<CharacterStats>();

        entity.onFlipped += FlipUI;

        mystats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
       
    }


    private void UpdateHealthUI()
    {
        slider.maxValue = mystats.GetMaxHealthValue();
        slider.value = mystats.currentHealth;
    }

    private void FlipUI() => myTransform.Rotate(0, 180, 0);


    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        mystats.onHealthChanged -= UpdateHealthUI;
    }

    }
