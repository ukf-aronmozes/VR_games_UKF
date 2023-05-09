using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DrumController : MonoBehaviour {

    public AudioClip drumSound;
    public float hapticIntensity = 0.1f;
    public float hapticDuration = 0.1f;

    private void OnTriggerEnter(Collider other) {

        AudioSource.PlayClipAtPoint(drumSound, transform.position);

        if(other.gameObject.name == "Collider1"){
            StartCoroutine(HapticFeedback(XRNode.LeftHand));
        }
        else {
            StartCoroutine(HapticFeedback(XRNode.RightHand));
        }

    }

    private IEnumerator HapticFeedback(XRNode controllerNode) {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device != null && device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
            device.SendHapticImpulse(0, hapticIntensity, hapticDuration);
        }
        yield return null;
    }
}
