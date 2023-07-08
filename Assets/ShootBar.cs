using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBar : MonoBehaviour
{
   [SerializeField] FootballPlayer footballPlayer;
   [SerializeField] Slider slider;

    public void Update()
    {
        slider.maxValue = footballPlayer.maxShootForce;
        slider.value = footballPlayer.actualShootForce; 
    }

}
