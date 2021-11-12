using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject character;
    public Material material;

    public float hookDistance = 10;
    public float timeUntilUnhook = 20;
    public float maxDegreeOffsetUntilUnhook = 120;

    public Camera mainCamera;

    private RaycastHit m_hookAttachment;
    private GameObject m_hitObject = null;
    private float m_timeSinceHook = 0;
    private GameObject m_ropeStart;
    private GameObject m_ropeEnd;
    private bool m_launched = false;


    void Awake()
    {
        m_ropeStart = mainCamera.transform.GetChild(0).gameObject;
        if (m_ropeStart.CompareTag("Rope"))
        {
            m_ropeEnd = m_ropeStart.transform.GetChild(0).gameObject;
        }
        else
        {
            throw new Exception("Wrong child used! Doesnt have Tag 'Rope'");
        }
    }


    /**
	 * Raycast against the scene to see if we can attach the hook to something.
	 */
    private void AttachHook()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Debug.Log("beFORE hit");
        // Raycast to see what we hit:
        if (Physics.Raycast(ray, out m_hookAttachment))
        {
            m_hitObject = m_hookAttachment.transform.gameObject;
            if (m_hitObject.CompareTag("enemy") 
                // &&
                // Vector3.Distance(m_hitObject.transform.position, this.gameObject.transform.position) <=
                // hookDistance)
                )
            {
                // an object was hit!
                Debug.Log("HIT: " + m_hookAttachment.transform.gameObject.name);
                m_timeSinceHook = 0;
            }
            else
            {
                Debug.Log("NOT HIT: Object tag: " + m_hitObject.tag + ", dist: " + Vector3.Distance(m_hitObject.transform.position, this.gameObject.transform.position));
                ResetHook();
            }
        }
        else
        {
            Debug.Log("Raycast says no.");
        }
    }


    private void ResetHook()
    {
        m_ropeEnd.transform.DetachChildren();
        m_hitObject = null;
        m_timeSinceHook = 0;
        m_launched = false;
    }


    void FixedUpdate()
    {
        m_timeSinceHook += Time.deltaTime;


        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("IN BUTTON DOWN");
            if (!m_hitObject)
            {
                Debug.Log("IN ATTACH");
                AttachHook();
            }
            else
            {
                
                Debug.Log("IN RESET");
                ResetHook();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            
            Debug.Log("IN BUTTON UP");

        }
        else if ((bool) m_hitObject && (m_timeSinceHook >= timeUntilUnhook || maxDegreeOffsetUntilUnhook <=
            Vector3.Angle(mainCamera.ScreenToWorldPoint(Input.mousePosition), m_hitObject.transform.position)))
        {
            ResetHook();
        }
    }
}