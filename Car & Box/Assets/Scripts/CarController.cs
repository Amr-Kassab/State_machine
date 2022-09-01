﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}
public enum Axel{
    Front,
    Back
}
public enum car_type{
    
    _4_4,
    Front_2,
    Back_2,
}


public class CarController : MonoBehaviour
{
    [SerializeField]
    public float max_acceleration = 20.0f;
    [SerializeField]
    public float Default_max_acceleration;
    [SerializeField]
    public float Default_Drift_Acceleration;
    [SerializeField]
    public float turn_Sensetivity = 1.0f;
    [SerializeField]
    public float max_steer_angle = 45.0f;
    [SerializeField]
    public List<Wheel> wheels;

    [SerializeField]
    public Vector3 Center_Of_Mass;
    [SerializeField]
    public float Default_Friction;
    
    private Rigidbody Rb;
    private float inputx, inputy;

    private Rigidbody Rigidbody;

    public car_type car_Type;
    void Start()
    {
        fix_speed();
        Rb = GetComponent<Rigidbody>();
        Rb.centerOfMass = Center_Of_Mass;
       
    }

    // Update is called once per frame
    void Update()
    {
        Get_Input();
        
    }

    private void FixedUpdate() 
    {
        Move();
        Turn();
        Animate_Wheels();
        Drift();
    }

    private void fix_speed(){
        if (car_Type == car_type.Front_2 || car_Type == car_type.Back_2){
            max_acceleration = max_acceleration * 2;
            Default_Drift_Acceleration = Default_Drift_Acceleration * 2;
            Default_max_acceleration = Default_max_acceleration * 2;
        }
    }

    private void Move(){
        foreach(var Wheel in  wheels){
            if (car_Type == car_type._4_4){Wheel.collider.motorTorque = inputy *  max_acceleration  * Time.deltaTime;} 
            if (car_Type == car_type.Front_2){if (Wheel.axel == Axel.Front){Wheel.collider.motorTorque = inputy* max_acceleration  * Time.deltaTime;}}      
            if (car_Type == car_type.Back_2){if (Wheel.axel == Axel.Back){Wheel.collider.motorTorque = inputy * max_acceleration  * Time.deltaTime;}}
        }
    }
    private void Get_Input()
    {
         inputy = Input.GetAxis("Vertical");
         inputx = Input.GetAxis("Horizontal");
    }
    private void Turn(){
        foreach (var Wheel in wheels){
            if (Wheel.axel == Axel.Front){
                var steer_angle = inputx * max_steer_angle * turn_Sensetivity;
                
                Wheel.collider.steerAngle = Mathf.Lerp(Wheel.collider.steerAngle , steer_angle,0.5f);
            }
        }
    }
    private void Animate_Wheels(){
        
        foreach (var Wheel in wheels){

            Quaternion rot_;
            Vector3 pos_;

            Wheel.collider.GetWorldPose(out pos_ , out rot_);

            Wheel.model.transform.position = pos_;
            Wheel.model.transform.rotation = rot_;
        }
        
        
    }
    private void Drift(){
        if (Input.GetKey(KeyCode.Space)){
                foreach(var Wheel in wheels){
                    max_acceleration = Default_Drift_Acceleration;
                    Debug.Log(Wheel.collider.motorTorque);
                    WheelFrictionCurve frictionCurve = Wheel.collider.sidewaysFriction;
                   
                    frictionCurve.stiffness = 0.5f;
                    
                    
                    Wheel.collider.sidewaysFriction= frictionCurve;
                   
                
            
            }
            
        }
        else{
            
            foreach(var Wheel in wheels){
                max_acceleration = Default_max_acceleration;
                WheelFrictionCurve frictionCurve = Wheel.collider.sidewaysFriction;
                
                frictionCurve.stiffness = 2;
                
                
                Wheel.collider.sidewaysFriction= frictionCurve;
                    
                
            }
        }
    }
}