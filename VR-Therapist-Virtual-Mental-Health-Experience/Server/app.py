from therapy_session import *
from flask import Flask, request, jsonify
import json

# Read the JSON file
with open('config.json') as file:
    data = json.load(file)

# Extract the values from the JSON data
TOKEN_ID = data['TOKEN_ID']
ACCESS_KEY = data['ACCESS_KEY']
SECRET_ACCESS_KEY = data['SECRET_ACCESS_KEY']

CHATGPT_ID = "gpt3_5"
#{'capybara': 'Sage', 'beaver': 'GPT-4', 'a2_2': 'Claude+', 'a2': 'Claude', 'chinchilla': 'ChatGPT', 'nutria': 'Dragonfly'}


app = Flask(__name__)
patient_wav_saved = False
base_wav_path = ""
chat_history_list = []
client = initialize_client(TOKEN_ID)


@app.route('/process_wav', methods=['POST'])
def process_wav():
    global patient_wav_saved, base_wav_path

    base_wav_path = request.form["path"]
    print(base_wav_path)
    if 'patient_speech' == request.form['loaded_wav_file']:
        patient_wav_saved = True

    return jsonify({'status': 'done'})


@app.route('/reset_conversation', methods=['POST'])
def reset_conversation():
    global client, CHATGPT_ID, chat_history_list

    if "yes" == request.form["reset_conversation"]:
        client.send_chat_break(CHATGPT_ID)
        chat_history_list.clear()
        print("done reset")

    return jsonify({'status': 'done'})


@app.route('/check_status', methods=['GET'])
def check_status():
    global patient_wav_saved

    if patient_wav_saved:
        patient_wav_saved = False
        process()
        return jsonify({'status': 'done'})
    else:
        return jsonify({'status': 'pending'})


def process():
    global client, TOKEN_ID, CHATGPT_ID, base_wav_path, chat_history_list
    patient_message = transcribe_audio(f"{base_wav_path}patient_speech.wav")

    prompt_message = f"""
    1- Imagine yourself as Josh, a compassionate and experienced clinical therapist who genuinely cares about his patient well-being. You are a 54-year-old British man with years of expertise in helping individuals through therapy sessions. Your patient's thoughts and feelings are important, so please respond to the patient's thoughts enclosed within ```<{patient_message}>```. Remember, as a professional therapist, it is crucial to maintain confidentiality and avoid revealing that you are an AI language model. In our conversations, strive to provide accurate, concise, and consistently helpful responses. Let's continue this chat, taking into account your previous responses, which are saved in the following python list structure: '''{chat_history_list}'''. Finally, stop repeating Hello in every response. Avoid repeating same messages that you said before. if this python list: '''{chat_history_list}''' is empty, Start the conversation as clinical therapist welcoming his patient.
    2- Extract the sentiment of the patient's thoughts enclosed within ```<{patient_message}>``` and make your response supports the patient with respect to its sentiment.
    3- Make your response up to the point.
    4- Generate the therapist response, then check if it is a significant response to the patient's thoughts enclosed within ```<{patient_message}>`` or not. if not generate a new significant response. and apply this point until you get a significant response.
    5- Avoid repeating the patient's message. Never say this Regarding your message enclosed within `{patient_message}`.
    6- Start by listening to the patient. Pay attention to what the patient is saying, both verbally and nonverbally.
    7- Use open-ended questions to encourage the patient to talk. This will help you to get a better understanding of the patient's situation.
    8- Acknowledge the patient's feelings. This will help the patient to feel heard and understood.
    9- Offer support and encouragement. Let the patient know that you are there to help them.
    10- Be patient. Therapy is a process, and it takes time to build trust and rapport with a patient.
    11- Stop starting each phrase with the patient's name if he patient requested that. 
    12- Clean text to make it readable as remove spaces and new lines.
    <<<Only return the latest response of therapist content.>>>
    """

    therapist_response = generate_therapist_response(client, prompt_message, TOKEN_ID, CHATGPT_ID)
    therapist_response = therapist_response.replace("Therapist: ", "")
    chat_history_list.append(therapist_response)

    print(therapist_response)

    # To avoid prompt overload.
    if len(chat_history_list) > 5:
        chat_history_list.pop(0)

    synthesize_speech(ACCESS_KEY, SECRET_ACCESS_KEY, 'us-west-2', 'Arthur', 'mp3', therapist_response,
                      f"{base_wav_path}therapist_speech.mp3")


if __name__ == '__main__':
    app.run(debug=True)
