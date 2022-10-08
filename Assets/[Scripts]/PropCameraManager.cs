using UnityEngine;
using Cinemachine;

public class PropCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject virtualCam;
    [SerializeField] private CinemachineVirtualCamera cm;

    [Header("Camera Priority When Active")]
    [Range(0, 25)]
    [SerializeField] 
    private int camPriorityActive;
    
    [Header("Camera Priority When Inactive")]
    [Range(0, 25)]
    [SerializeField] 
    private int camPriorityInactive;
    
    
    /*
     *  When game starts, find camera child object named "Signed Cam" and assign it to virtualCam variable,
     *  then find the CinemachineVirtualCamera component and set the Camera priority to an integer less
     *  than the priority of the main camera,
     *  then set the virtualCam gameobject to inactive to wait for trigger activation.
     */
    private void Start()
    {
        virtualCam = transform.GetChild(0).gameObject;
        cm = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        cm.Priority = camPriorityInactive;
        virtualCam.SetActive(false);
    }
    
    /*
     *  When collider with the "Player" tag enters the trigger zone and the other collider is not a trigger,
     *  set the camera priority to an integer higher than the main camera,
     *  then set the camera to active.
     */
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            cm.Priority = camPriorityActive;
            virtualCam.SetActive(true);
        }
    }
    
    /*
     *  When collider with the "Player" tag exits the trigger zone and the other collider is not a trigger,
     *  set the camera priority to an integer less than the main camera,
     * then set the camera to inactive to wait for trigger again.
     */
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            cm.Priority = camPriorityInactive;
            virtualCam.SetActive(false);
        }
    }
}