using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tank_drive_controller : MonoBehaviour
{
    private bool swerve_mode = false;

    private string[] dataText;

    private const float ROBOT_SPEED_MULTIPLIER = 0.125f;
    private const float ROBOT_TANK_ROTATION_MULTIPLIER = 3f;
    private const float ROBOT_SWERVE_ROTATION_MULTIPLIER = 5f;

    void Start()
    {
        dataText = System.IO.File.ReadAllText(@"C:\Users\ediso\vs-workspace\SimulatorTest\data.txt").Split(' ');

        if(calculateDriveType(dataText[dataText.Length - 1]).Equals("Swerve")){
            swerve_mode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        dataText = System.IO.File.ReadAllText(@"C:\Users\ediso\vs-workspace\SimulatorTest\data.txt").Split(' ');
        float[] motorValues = {0, 0, 0};
        
        for(int i = 0; i < dataText.Length - 1; i++){
            motorValues[i] = float.Parse(dataText[i]);
        }

        if(!swerve_mode){
            float forward = (float)Math.Sign(motorValues[0] - motorValues[1]);
            float rotation = (float)Math.Sign(motorValues[0] + motorValues[1]);

            float currentAngle = (float)Math.PI * transform.rotation.eulerAngles.y/180;  

            transform.position += new Vector3((float)Math.Sin((double)currentAngle) * forward * ROBOT_SPEED_MULTIPLIER, 0, 
                                            (float)Math.Cos((double)currentAngle) * forward * ROBOT_SPEED_MULTIPLIER);
            transform.Rotate(0, rotation * ROBOT_TANK_ROTATION_MULTIPLIER, 0, Space.Self);
        }else if(swerve_mode){
            float forward = motorValues[0];
            float strafe = motorValues[1];
            float rotation = motorValues[2];

            transform.position += new Vector3(strafe * ROBOT_SPEED_MULTIPLIER, 0, forward * ROBOT_SPEED_MULTIPLIER);
            transform.Rotate(0, rotation * ROBOT_SWERVE_ROTATION_MULTIPLIER, 0, Space.Self);
        }

        foreach(float num in motorValues){
            Debug.Log(num);
        }
    }

    string calculateDriveType(string driveType){
        if(driveType.Equals("S")){
            return "Swerve";
        }
        return "Tank";
    }
}
