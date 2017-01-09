using UnityEngine;
using System.Collections;

public class WindScript : MonoBehaviour {
    public float windStrength;

    //Degrees of effect of wind zone in terms of it's current forward angle
    private float effRange = 150f;

	void OnTriggerStay(Collider obj)
    {
        if(obj.tag == "Player")
        {
            //Check to see if sails are up
            if (obj.GetComponent<ShipMovement>().GetSailStatus())
            {
                //Calculate the different in the ship angle and the windzone
                float angleDiff = Mathf.Abs(Mathf.Abs(transform.eulerAngles.y - obj.transform.eulerAngles.y + 180) % 360 - 180);
                if(angleDiff < effRange)
                {
                    obj.GetComponent<ShipMovement>().WindMovement(windStrength, angleDiff);
                }
            }
        }
    }
}
