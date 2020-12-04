using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tank_drive_controller : MonoBehaviour
{
    private bool swerve_mode = false;

    private string[] dataText;

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

            transform.position += new Vector3((float)Math.Sin((double)currentAngle) * forward * 0.125f, 0, 
                                            (float)Math.Cos((double)currentAngle) * forward * 0.125f);
            transform.Rotate(0, rotation * 3, 0, Space.Self);
        }else if(swerve_mode){
            float forward = motorValues[0];
            float strafe = motorValues[1];
            float rotation = motorValues[2];

            transform.position += new Vector3(strafe * 0.125f, 0, forward * 0.125f);
            transform.Rotate(0, rotation * 5, 0, Space.Self);
        }

        foreach(float num in motorValues){
            Debug.Log(num);
        }

        // transform.position = new Vector3(robotPosition[1], 0.8f, robotPosition[0]);
        // transform.eulerAngles = new Vector3(0 , robotPosition[2], 0);
        // float inputY = deadBand(Input.GetAxis("Vertical"));
        // float inputX = deadBand(Input.GetAxis("Horizontal"));
        // float inputStrafe = deadBand(Input.GetAxis("Strafe"));

        // Debug.Log("Rotation: " + inputX);
        // Debug.Log("Forward: " +inputY);

        // float currentAngle = (float)Math.PI * transform.rotation.eulerAngles.y/180;  

        // if(swerve_mode){
        //     transform.position += new Vector3(inputStrafe * Time.deltaTime * SPEED, 0, 
        //                                       inputY * Time.deltaTime * SPEED);
        // }else{

        // }
        // // transform.position += new Vector3((float)Math.Sin((double)currentAngle) * inputY * Time.deltaTime * SPEED, 0, 
        // //                                   (float)Math.Cos((double)currentAngle) * inputY * Time.deltaTime * SPEED);
        // transform.Rotate(0, inputX * Time.deltaTime * ROTATE_SPEED, 0, Space.World);


    }

    string calculateDriveType(string driveType){
        if(driveType.Equals("S")){
            return "Swerve";
        }
        return "Tank";
    }
    float deadBand(float input){
        if(Math.Abs(input) > 0.15){
            return input;
        }
        return 0;
    }
}
