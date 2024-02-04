using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics;

public class CarManager : MonoBehaviour
{

    public VPStandardInput inputValue;
    public Rigidbody rigid;
    public GameObject panel;
    public GameObject left;
    public GameObject right;
    public int turnNum = 0;
    public GameObject otherCar;
    public Text bakeTime;
    public Text bakeValue;
    public Text currentSpeed;
    public Text isBrakeText;
    private float brakeValue = 0;
    private bool finish = false;
    private bool isBrake = false;
    private bool begin = false;
    private float startTime;
    private float finishTime;

    private void Update()
    {
        brakeValue = Input.GetAxis("Vertical");
        if (begin)
        {
            if (brakeValue < -0.1f && !isBrake)
            {
                startTime = Time.time;
                Debug.Log(startTime);
                isBrake = true;
            }
            if (rigid.velocity.magnitude < 0.5f)
            {
                if (finish) return;
                finishTime = Time.time;
                Debug.Log(finishTime);
                if (isBrake)
                {
                    bakeTime.text = (finishTime - startTime).ToString("f2");
                }
                isBrakeText.text = isBrake ? "是" : "否";
                begin = false;
            }
        }
        bakeValue.text = Mathf.Abs((brakeValue < 0 ? brakeValue : 0)).ToString("f2");
        currentSpeed.text = rigid.velocity.magnitude.ToString("f2");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enter"))
        {
            if (other.GetComponent<Trigger>().triggerType == Trigger.TriggerType.Left)
            {
                right.SetActive(false);
                left.SetActive(true);
            }
            else
            {
                left.SetActive(false);
                right.SetActive(true);
            }
            if (turnNum == 9)
                otherCar.SetActive(true);
        }
        else
        {
            turnNum++;
            right.SetActive(false);
            left.SetActive(false);
            if (turnNum == 10)
            {
                begin = true;
                panel.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (finish) return;
        if (turnNum != 10)
            return;
        if (other.collider.CompareTag("OtherCar"))
        {
            finish = true;
        }
    }
}
