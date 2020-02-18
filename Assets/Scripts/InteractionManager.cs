using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionManager : MonoBehaviour{

    [SerializeField]
    private RigidbodyFirstPersonController playerController;
    [SerializeField]
    private Image handImage;

    [SerializeField]
    private GameObject paperPanel;

    [SerializeField]
    private float interactDistance;

    [SerializeField]
    private LayerMask layerMask;
    private bool hasKey = false;

    [SerializeField]
    private FlashLight flashLight;

    private void Start(){
        // отключаем руку
        handImage.gameObject.SetActive(false);
        paperPanel.SetActive(false);
    }


    private void Update(){
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit raycastHit;
        if(Physics.Raycast(ray, out raycastHit, interactDistance, layerMask)){
            // если рука не отображается
            if(!handImage.gameObject.activeSelf){
                // показать картинку
                handImage.gameObject.SetActive(true);
            }
            Debug.Log("Ray hit object: " + raycastHit.transform.name);
            // если нажата клавиша Е
            if(Input.GetKeyDown(KeyCode.E)){
                // если смотрю на батарейки
                if(raycastHit.transform.tag == "Battery"){
                    // пополнить заряд фонарика
                    flashLight.AddEnergy();
                    // уничтожить батарейки
                    Destroy(raycastHit.transform.gameObject);
                }
                 if(raycastHit.transform.tag == "Paper"){
                    paperPanel.SetActive(true);
                    playerController.enabled = false;
                }
                if(raycastHit.transform.tag == "Key"){
                   Key key = raycastHit.transform.GetComponent<Key>();
                   if(key != null){
                            key.PickUp();
                        }
                }
                if(raycastHit.transform.tag == "Door"){
                        Door door = raycastHit.transform.GetComponent<Door>();
                        if(door != null){
                            door.ChangeDoorState();
                        }
            }
            if (Input.GetKeyDown(KeyCode.Escape)){
                paperPanel.SetActive(false);
                playerController.enabled = true;
            }
        }else{
            handImage.gameObject.SetActive(false);
        }
    }
}
}
