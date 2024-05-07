from flask import Flask, request, jsonify
import openai

app = Flask(__name__)
openai.api_key = 'xxxxxxxxxxxxxxxxxxxxxxxxxxx'
messages = [ {"role": "system", "content":"You are a intelligent assistant."} ]
mustPrompt = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
global_text = ""

@app.route('/api/receive-text', methods=['POST'])
def receive_text():
    global global_text
    text = request.json.get('text')
    global_text = text
    print("Alınan metin:", text)
    return jsonify({'message': 'Metin başarıyla alındı'})

@app.route('/get_answer', methods=['GET'])
def get_answer():
   global global_text
   message = global_text
   if message:
      messages.append(
         {"role": "user", "content": mustPrompt + message},
      )
      chat = openai.ChatCompletion.create(
         model="gpt-3.5-turbo", messages=messages
      )
   answer = chat.choices[0].message.content
   answer = answer.encode('utf-8').decode('utf-8')
   messages.append({"role": "assistant", "content": answer})
   return jsonify({"answer": answer})



if __name__ == '__main__':
    app.run(debug=True)