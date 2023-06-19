using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ability.Testspace;
using Unity.VisualScripting;

public class Controller : MonoBehaviour
{
  private float horizontalInput;
  private float verticalInput;
  private float steerAngle;
  private bool isBreaking;

  public WheelCollider frontLeftWheelCollider;
  public WheelCollider frontRightWheelCollider;
  public WheelCollider rearLeftWheelCollider;
  public WheelCollider rearRightWheelCollider;
  public Transform frontLeftWheelTransform;
  public Transform frontRightWheelTransform;
  public Transform rearLeftWheelTransform;
  public Transform rearRightWheelTransform;

  public float maxSteeringAngle = 30f;
  public float motorForce = 50f;
  public float brakeForce = 0f;
  public float rotationSpeed = 5f;

  [SerializeField] private ParticleSystem m_FlameThrower0;
  [SerializeField] private ParticleSystem m_FlameThrower1;
  [SerializeField] private ParticleSystem m_Smoke;
  [SerializeField] private ParticleSystem m_Expo;
  private void Start()
  {
    m_FlameThrower0.Stop();
    m_FlameThrower1.Stop();
    m_Smoke.Stop();
    m_Expo.Stop();
  }
  private void FixedUpdate()
  {
    GetInput();
    HandleMotor();
    HandleSteering();
    UpdateWheels();
    
  }

  private void GetInput()
  {
    horizontalInput = Input.GetAxis("Horizontal");
    verticalInput = Input.GetAxis("Vertical");
    isBreaking = Input.GetKey(KeyCode.Z);
  
    
  }

  private void HandleSteering()
  {
    steerAngle = maxSteeringAngle * horizontalInput;
    frontLeftWheelCollider.steerAngle = steerAngle;
    frontRightWheelCollider.steerAngle = steerAngle;
  }

  private void HandleMotor()
  {
    frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
    frontRightWheelCollider.motorTorque = verticalInput * motorForce;

    brakeForce = isBreaking ? 3000f : 0f;
    frontLeftWheelCollider.brakeTorque = brakeForce;
    frontRightWheelCollider.brakeTorque = brakeForce;
    rearLeftWheelCollider.brakeTorque = brakeForce;
    rearRightWheelCollider.brakeTorque = brakeForce;
  }

  private void UpdateWheels()
  {
    UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
    UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
    UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
    UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
  }

  private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
  {
    Vector3 pos;
    Quaternion rot;
    wheelCollider.GetWorldPose(out pos, out rot);
    trans.rotation = rot;
    trans.position = pos;
  }

  private void Update()
  {

  

   

    if (Input.GetMouseButtonDown(0))
    {
      m_FlameThrower0.Play();
      m_FlameThrower1.Play();
    }

    if (Input.GetMouseButtonUp(0))
    {
      m_FlameThrower0.Stop();
      m_FlameThrower1.Stop();
    }
    if (Input.GetMouseButtonDown(1))
    {
      m_Smoke.Play();
      
    }
    if (Input.GetKeyDown(KeyCode.Q))
    {
      m_Expo.Play();

    }
    if (Input.GetKeyUp(KeyCode.Q))
    {
      m_Expo.Stop();

    }
  }
}
