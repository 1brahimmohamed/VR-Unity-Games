import speech_recognition as sr
from poe_api_wrapper import PoeApi
import boto3


def initialize_client(token_id):
    client = PoeApi(token_id)
    return client


def transcribe_audio(input_path):
    recognizer = sr.Recognizer()
    with sr.AudioFile(input_path) as source:
        audio = recognizer.record(source)
        try:
            patient_text = recognizer.recognize_google(audio)
            return patient_text
        except sr.UnknownValueError:
            return "Speech recognition could not understand audio"
        except sr.RequestError as e:
            return f"Error occurred during speech recognition: {e}"


def generate_therapist_response(client, prompt_message, TOKEN_ID, CHATGPT_ID):
    try:
        for chunk in client.send_message(CHATGPT_ID, prompt_message):
            pass
        therapist_response = chunk["text"]
    except Exception as e:
        client = initialize_client(TOKEN_ID)
        for chunk in client.send_message(CHATGPT_ID, prompt_message):
            pass
        therapist_response = chunk["text"]

    return therapist_response


def synthesize_speech(access_key, secret_access_key, region_name, voice_id, output_format, text, output_path):
    session = boto3.Session(
        aws_access_key_id=access_key,
        aws_secret_access_key=secret_access_key,
        region_name=region_name
    )
    polly_client = session.client('polly')

    response = polly_client.synthesize_speech(
        VoiceId=voice_id,
        OutputFormat=output_format,
        Text=text,
        Engine='neural'
    )

    with open(output_path, 'wb') as file:
        file.write(response['AudioStream'].read())

