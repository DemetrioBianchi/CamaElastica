using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;


public class WebcamMovement : MonoBehaviour
{
    [SerializeField] Text cam;

    WebCamTexture webcam;
    CascadeClassifier cascade;
    OpenCvSharp.Rect myFace;

    public float jumpY;
    bool faceFound = false;

    Mat frame;

    [SerializeField] Size minSize, maxSize;
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        webcam = new WebCamTexture(devices[0].name);
        webcam.Play();

       //GetComponent<Renderer>().material.mainTexture = webcam;

        if (webcam.isPlaying)
        {
            cam.text = "Cam ativa";
        }
        else
        {
            cam.text = "Cam não encontrada";
        }

        //cascade = new CascadeClassifier(Application.dataPath + @"haarcascade_frontalface_default.xml");
        //cascade = new CascadeClassifier("Assets/haarcascade_upperbody.xml");
        cascade = new CascadeClassifier("Assets/haarcascade_frontalface_default.xml");
       


    }

    void Update()
    {
        //GetComponent<Renderer>().material.mainTexture = webcam;
        frame = OpenCvSharp.Unity.TextureToMat(webcam);

        findNewFace(frame);

        //desenha a detecção de movimento
        display(frame);
    }
    
    void findNewFace(Mat frame)
    {
            
        var faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage, minSize, maxSize);
        cam.text = "Fim Find";
        //limitador de reconhecimentos
        if (faces.Length >= 1)
        {

                //Debug.Log(faces[0].Location);
                myFace = faces[0];
                jumpY = faces[0].Y;

                faceFound = true;
        }
        
        
        
    }

    void display(Mat frame)
    {
        if (myFace != null)
        {
            frame.Rectangle(myFace, new Scalar(0, 0, 250), 2);
        }

        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newtexture;
    }
}
