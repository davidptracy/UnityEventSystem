using UnityEngine;
using System.Collections;

// Based on Type-Safe Event System by Will Miller http://www.willrmiller.com/?p=87

// Attach this script to a GameObject that will respond to GameEvents
// The class methods are project specific, but this will outline how to use a delegate funciton with parameters

// The events can be called from another object by calling:
// EventManagerTypeSafe.instance.TriggerEvent(new ReticleEndEvent());

public class EventRegister : MonoBehaviour {

	Animation mAnimation;
	int playCount;

	void Start(){
		mAnimation = this.GetComponent<Animation> ();
		int playCount = 0;
	}

	void OnEnable()
	{
		//Points to the instance of the EventManager and Adds the EventType and function to the dictionary
		EventManagerTypeSafe.instance.AddListener<ReticleStartEvent> (ReticleStart);
		EventManagerTypeSafe.instance.AddListener<ReticleHitEvent> (ReticleHit);
		EventManagerTypeSafe.instance.AddListener<ReticleEndEvent> (ReticleOff);
	}

	void OnDisable()
	{
		//When the object is disabled, tell the Event Manager to remove it to the Event Dictionary
		EventManagerTypeSafe.instance.RemoveListener<ReticleStartEvent> (ReticleStart);
		EventManagerTypeSafe.instance.RemoveListener<ReticleHitEvent> (ReticleHit);
		EventManagerTypeSafe.instance.RemoveListener<ReticleEndEvent> (ReticleOff);
	}

	void ReticleStart(ReticleStartEvent _e){
		if (_e.id == this.gameObject.GetInstanceID ()) {
			Debug.Log ("Reticle Start Event Triggered");
			if (mAnimation) {
				if (!mAnimation.isPlaying && playCount == 0) {
					mAnimation.Play ();
					playCount++;
				} 
			} 
		}
	}

	void ReticleHit(ReticleHitEvent _e)
	{
		if (_e.id == this.gameObject.GetInstanceID ()) {
			Debug.Log ("Reticle Hit Event Triggered");
//			if (mAnimation) {
//				if (!mAnimation.isPlaying && playCount == 0) {
//					mAnimation.Play ();
//					playCount++;
//				} 
//			} 
		}
	}

	void ReticleOff(ReticleEndEvent _e){
		playCount = 0;
	}
}