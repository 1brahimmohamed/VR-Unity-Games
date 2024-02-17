using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class WavProcessing : MonoBehaviour
{
    private string baseUri = "http://localhost:5000";
    private const string BaseWavPath = "Wav Files/";

    private AudioSource audioSource;
    private Coroutine recordAudioCoroutine;
    private Coroutine sendWavFileCoroutine;

    // private float recordBtnValue = 0f;
    private Image imageLoader;
    private Button recordBtn;
    private Button resetBtn;
    private Button stopBtn;

    private bool isRecording;

    // The transform that the prefab or animator should look at
    private Transform lookAtTarget;


    // Start is called before the first frame update
    void Start()
    {
        recordBtn = GameObject.Find("Record_Btn").GetComponent<Button>();
        resetBtn = GameObject.Find("Reset_Btn").GetComponent<Button>();
        stopBtn = GameObject.Find("Stop_Btn").GetComponent<Button>();


        stopBtn.onClick.AddListener(EndRecording);
        recordBtn.onClick.AddListener(RecordAndProcess);
        resetBtn.onClick.AddListener(Reset);

        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, BaseWavPath)))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, BaseWavPath));
        }

        // Get the AudioSource component from the prefab
        audioSource = GetComponent<AudioSource>();

        // Get the Image component for image loader
        imageLoader = GameObject.Find("ImageLoader").GetComponent<Image>();

        // Set the lookAtTarget to Patient
        lookAtTarget = GameObject.Find("Patient").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the prefab or animator always look at the player's head
        transform.LookAt(lookAtTarget.position);
    }

    void RecordAndProcess()
    {
        // Record audio for 120 seconds

        isRecording = true;
        if (recordAudioCoroutine != null)
            StopCoroutine(RecordAudioCoroutine());

        recordAudioCoroutine = StartCoroutine(RecordAudioCoroutine());
    }

    void EndRecording()
    {
        isRecording = false;
    }

    private void Reset()
    {
        isRecording = true;
        StartCoroutine(ResetProcess());
    }

    private IEnumerator ResetProcess()
    {
        string uri = $"{baseUri}/reset_conversation";
        WWWForm form = new WWWForm();
        form.AddField("reset_conversation", "yes");

        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(request.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator RecordAudioCoroutine()
    {
        string wavFilePath = Path.Combine(Application.persistentDataPath, $"{BaseWavPath}patient_speech.wav");
        int recordingTime = 120;
        float fillSpeed = 1f / recordingTime;
        float fillAmount = 0f;
        imageLoader.fillAmount = fillAmount;

        AudioClip recordedClip = Microphone.Start(null, false, recordingTime, 44100);

        while ((fillAmount < 1f) && (isRecording))
        {
            fillAmount += fillSpeed * Time.deltaTime;
            imageLoader.fillAmount = fillAmount;
            yield return null;
        }

        if (isRecording)
        {
            // Wait for the recording time to elapse
            yield return new WaitForSeconds(recordingTime);
        }

        imageLoader.fillAmount = 1f;

        Microphone.End(null);

        // Save the recorded audio as a wav file
        SavWav.Save(wavFilePath, recordedClip);

        // Disable all buttons
        recordBtn.interactable = false;
        resetBtn.interactable = false;
        stopBtn.interactable = false;

        // Send a POST request to the Flask server with "patient_speech.wav" string
        if (sendWavFileCoroutine == null)
            sendWavFileCoroutine = StartCoroutine(SendPatientWav());

        // Check status periodically until it's "done"
        yield return StartCoroutine(CheckStatus());
    }

    IEnumerator SendPatientWav()
    {
        string uri = $"{baseUri}/process_wav";
        WWWForm form = new WWWForm();
        form.AddField("loaded_wav_file", "patient_speech");
        form.AddField("path", Path.Combine(Application.persistentDataPath, BaseWavPath));


        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(request.downloadHandler.text);
                    break;
            }
        }

        sendWavFileCoroutine = null;
    }

    IEnumerator CheckStatus()
    {
        string uri = $"{baseUri}/check_status";

        while (true)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                yield return request.SendWebRequest();

                switch (request.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(request.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        string response = request.downloadHandler.text;

                        if (response.Contains("done"))
                        {
                            // Start playing therapist_speech.wav
                            StartCoroutine(PlayTherapistWav());
                            yield break;
                        }

                        break;
                }
            }

            yield return new WaitForSeconds(1f); // Wait for 1 seconds before checking status again
        }
    }

    IEnumerator PlayTherapistWav()
    {
        string therapistWavPath = Path.Combine(Application.persistentDataPath, $"{BaseWavPath}therapist_speech.mp3");

        // Load the audio clip from therapistWavPath
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(therapistWavPath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(www.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Assign the loaded audio clip to the AudioSource component
                    audioSource.clip = DownloadHandlerAudioClip.GetContent(www);

                    if (audioSource.clip == null)
                    {
                        Debug.Log("Audio clip is empty");
                    }

                    // Play the audio clip
                    audioSource.Play();

                    // Wait for the audio clip to finish playing
                    yield return new WaitForSeconds(audioSource.clip.length);

                    // Activate all buttons
                    recordBtn.interactable = true;
                    resetBtn.interactable = true;
                    stopBtn.interactable = true;
                    break;
            }
        }
    }
}