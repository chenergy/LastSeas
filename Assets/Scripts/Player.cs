﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Airship controlled by the player in the scene.
/// </summary>
public class Player : MonoBehaviour
{
    public AimTarget m_aimTarget;

    /// <summary>
    /// Reference to the base model object contained in the GameObject.
    /// </summary>
//    public GameObject m_model;
    public Transform m_bow;
    public Transform m_port;
    public Transform m_starboard;

    /// <summary>
    /// Mode showing how the flight should behave.
    /// </summary>
//    public enum FlightMode { ALL_RANGE, ON_RAILS };

    /// <summary>
    /// The mode the airship is currently in.
    /// </summary>
//    public FlightMode m_mode = FlightMode.ALL_RANGE;

    public float m_maxMoveDelta = 0.1f;
//    public float m_maxRadiansDelta = 0.5f;
//    public float m_maxMagnitudeDelta = 0.0f;

    /// <summary>
    /// Check whether player can be accessed.
    /// </summary>
    //public bool m_movementEnabled = false;

    /// <summary>
    /// Multiplier of horizontal rotation in degrees.
    /// </summary>
//    public float m_horizontalRotationMultiplier = 30f;
//    public float m_horizontalPositionMultiplier = 10f;

    /// <summary>
    /// Multiplier of vertical rotation in degrees.
    /// </summary>
//    public float m_verticalRotationMultiplier = 15f;
//    public float m_verticalPositionMultiplier = 10f;
//    public Transform t;

    public void Update()
    {
        transform.LookAt(m_aimTarget.m_farTarget);

//        float stepRotation = m_maxRadiansDelta * Time.deltaTime;
//        m_model.transform.forward = Vector3.RotateTowards(m_model.transform.forward, (m_aimTarget.m_farTarget.position - m_model.transform.position), stepRotation, m_maxMagnitudeDelta);

//        transform.localPosition = new Vector3(Input.GetAxis("Horizontal") * m_horizontalPositionMultiplier, 
//            Input.GetAxis("Vertical") * m_verticalPositionMultiplier, 0.0f);
//        transform.localRotation = Quaternion.Euler(Input.GetAxis("Vertical") * m_verticalRotationMultiplier,
//            0f, -Input.GetAxis("Horizontal") * m_horizontalRotationMultiplier);

//        Vector3 viewportPoint = new Vector3(m_aimTarget.TargetViewportPosition.x, 
//                                    m_aimTarget.TargetViewportPosition.y,
//                                    AimTarget.FAR_TARGET_MAX_DISTANCE);
//        Vector3 aimTargetPosition = Camera.main.ViewportToWorldPoint(viewportPoint);
//        Vector3 localAimPosition = m_model.transform.InverseTransformPoint(aimTargetPosition);
//        m_model.transform.localPosition = Vector3.MoveTowards(m_model.transform.localPosition,
//            localAimPosition,
//            m_maxMoveDelta);

//        Vector3 localAimPosition = m_model.transform.InverseTransformPoint(m_aimTarget.m_farTarget.position);
//        m_model.transform.localPosition = Vector3.MoveTowards(m_model.transform.localPosition,
//            new Vector3(localAimPosition.x, localAimPosition.y, 0),
//            m_maxMoveDelta);

        float step = m_maxMoveDelta * Time.deltaTime;
        Vector3 screenTargetPosition = Camera.main.WorldToScreenPoint(m_aimTarget.m_farTarget.position);
        Vector3 localTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenTargetPosition.x, screenTargetPosition.y, -Camera.main.transform.localPosition.z));
//        localTargetPosition = transform.InverseTransformPoint(localTargetPosition);
//        t.localPosition = localTargetPosition;
//        transform.localPosition = Vector3.MoveTowards(transform.localPosition,
//            new Vector3(localTargetPosition.x, localTargetPosition.y, 0),
//            step);
//        Debug.Log(localTargetPosition);
        transform.position = Vector3.Lerp(transform.position, localTargetPosition, step);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
//        transform.localPosition = Vector3.Lerp(transform.localPosition,
//            new Vector3(m_aimTarget.m_farTarget.localPosition.x, m_aimTarget.m_farTarget.localPosition.y, 0),
//            step);

        if (Input.GetButtonDown("Jump"))
        {
            GameObject gobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gobj.transform.position = m_bow.transform.position;
            gobj.GetComponent<Collider>().isTrigger = true;
            Rigidbody rb = gobj.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = m_bow.transform.forward * 50;
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
//    public void FixedUpdate()
//    {
//        
//        m_curVelocity =
//        float displacement = m_curSpeed * Time.deltaTime + (0.5f * m_acceleration * Time.deltaTime * Time.deltaTime);
//        transform.position += transform.forward * displacement;
//        m_curSpeed = Mathf.Min(m_topSpeed, m_curSpeed + m_acceleration * Time.deltaTime);

        //m_curSpeed = Mathf.Min(m_topSpeed, m_curSpeed + m_acceleration * Time.deltaTime);
        //if (m_mode == FlightMode.ALL_RANGE)
        //{
        //    float displacement = m_curVelocity * Time.deltaTime + (0.5f * m_acceleration * Time.deltaTime * Time.deltaTime);
        //    transform.position += transform.forward * displacement;
        //    m_curVelocity = Mathf.Min(m_topSpeed, m_curVelocity + m_acceleration * Time.deltaTime);

        //    Vector3 curRotation = transform.rotation.eulerAngles;
        //    transform.rotation = Quaternion.Euler(-Input.GetAxis("Vertical") * m_verticalRotationMultiplier,
        //                                          curRotation.y + Input.GetAxis("Horizontal") * m_horizontalRotationMultiplier, 0);
        //    m_model.transform.localRotation = Quaternion.Euler(Input.GetAxis("Horizontal") * m_horizontalRotationMultiplier * 20,
        //        90f, 0f);
        //}
        //else if (m_mode == FlightMode.ON_RAILS)
        //{
//        transform.localPosition = new Vector3(Input.GetAxis("Horizontal") * m_horizontalPositionMultiplier, 
//            Input.GetAxis("Vertical") * m_verticalPositionMultiplier, 0.0f);
//        transform.localRotation = Quaternion.Euler(Input.GetAxis("Vertical") * m_verticalRotationMultiplier,
//            0f, -Input.GetAxis("Horizontal") * m_horizontalRotationMultiplier);
        //}

        //if (Input.GetAxis("Jump") > 0)
        //{
        //    GameObject gobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    gobj.transform.position = m_bow.transform.position;
        //    gobj.GetComponent<Collider>().isTrigger = true;
        //    Rigidbody rb = gobj.AddComponent<Rigidbody>();
        //    rb.useGravity = false;
        //    rb.velocity = m_bow.transform.forward * m_curVelocity * 50;
        //}
//    }
}

