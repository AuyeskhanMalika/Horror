using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isLocked;
    private bool isOpen;
    private float doorOpenAngle = 90f;
    private float doorClosedAngle = 0f;
    private float smooth = 2f;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(isLocked)
        return;

        if(isOpen){
            Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, 
                                targetRotationOpen, smooth * Time.deltaTime);
        }else{
             Quaternion targetRotationClosed = Quaternion.Euler(0, doorClosedAngle, 0);
            transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, 
                                targetRotationClosed, smooth * Time.deltaTime);
        }
    }
    public void Unlock(){
        isLocked = false;
    }
    public void ChangeDoorState(){
        isOpen = !isOpen;
    }
}
