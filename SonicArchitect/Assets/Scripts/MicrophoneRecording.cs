//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MicrophoneRecording : MonoBehaviour

//{

//    private int _micNdx = 0;
//    private AudioClip _recordedClip;
//    private AudioClip _croppedClip;
//    private bool _isRecordingSpeech = false;

//    void Start()
//    {
//        audioSource.loop = true;
//        audioSource.mute = false;

//        bool micSelected = false;
//        _micNdx = 0;
//        if (Microphone.devices.Length > 1)
//        {
//            for (int i = 0; !micSelected && (i < Microphone.devices.Length); i++)
//            {
//                if (Microphone.devices[i].ToString().Contains("Rift"))
//                {
//                    _micNdx = i;
//                    micSelected = true;
//                }
//            }

//            for (int i = 0; !micSelected && (i < Microphone.devices.Length); i++)
//            {
//                if (Microphone.devices[i].ToString().Contains("Oculus"))
//                {
//                    _micNdx = i;
//                    micSelected = true;
//                }
//            }
//        }
//        InitializeMicrophone();
//    }

//    private void InitializeMicrophone()
//    {
//        if (initialized)
//        {
//            return;
//        }
//        if (Microphone.devices.Length == 0)
//        {
//            return;
//        }

//        selectedDevice = Microphone.devices[0].ToString(); // Oculus' code
//        //selectedDevice = Microphone.devices[_micNdx].ToString(); // My code
//        micSelected = true;
//        GetMicCaps();
//        initialized = true;
//    }

////    public void StartMicrophone() // Oculus' code
//    public void StartMicrophone(int soundLenSec = 1) // My code
//    {
//        if (micSelected == false) return;

//        //Starts recording
//        audioSource.clip = Microphone.Start(selectedDevice, true, 1, micFrequency); // Oculus' code
//        //audioSource.clip = Microphone.Start(selectedDevice, true, soundLenSec, micFrequency); // My code

//        Stopwatch timer = Stopwatch.StartNew();

//        // Wait until the recording has started
//        while (!(Microphone.GetPosition(selectedDevice) > 0) && timer.Elapsed.TotalMilliseconds < 1000)
//        {
//            Thread.Sleep(50);
//        }

//        if (Microphone.GetPosition(selectedDevice) <= 0)
//        {
//            throw new Exception("Timeout initializing microphone " + selectedDevice);
//        }
//        // Play the audio source
//        audioSource.Play();
//    }

//// And I added several methods at the end
//    public void StartMicrophoneRecord(int soundLenSec)
//    {
//        StopMicrophone();
//        StartMicrophone(soundLenSec);
//        _isRecordingSpeech = true;
//    }

//    public void EndMicrophoneRecord()
//    {
//        int recordedSamples = Microphone.GetPosition(null);
//        _recordedClip = (audioSource == null) ? null : audioSource.clip;
//        StopMicrophone();

//        if (_isRecordingSpeech)
//        {
//            if ((audioSource == null) || (audioSource.clip == null))
//            {
//                _recordedClip = null;
//            }
//            else
//            {
//                float[] croppedData = new float[recordedSamples * _recordedClip.channels];
//                _recordedClip.GetData(croppedData, 0);
//                if (_croppedClip != null)
//                {
//                    _croppedClip.UnloadAudioData();
//                    Destroy(_croppedClip);
//                }
//                _croppedClip = AudioClip.Create(_recordedClip.name, recordedSamples, _recordedClip.channels, _recordedClip.frequency, false);
//                _croppedClip.SetData(croppedData, 0);
//            }
//            _isRecordingSpeech = false;
//        }

//        StartMicrophone(1);
//    }

//    public AudioClip GetMicrophoneRecord()
//    {
//        return _croppedClip;
//    }

//    public void Mute()
//    {
//        micInputVolume = 0;
//    }

//    public void Unmute()
//    {
//        micInputVolume = 100;
//    }
//}