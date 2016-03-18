using UnityEngine;
using System.Collections;

public class GameTimerForScene0 : GameTimer {

    [SerializeField] SinkingWaterScript sink_water;    
    [SerializeField] float timeForTriggerSinking;

    bool sinkingTriggered = false;

    // Inside the Update function of the parent class
    // Treat this function as an extention of the parent update function
    public override void CustomizedSettingsForScene()
    {
        base.CustomizedSettingsForScene();  
        if( Time.time > timeForTriggerSinking && !sinkingTriggered )
        {
            sink_water.TriggerSinkingWater();
            sinkingTriggered = true;
        }

    }
}
