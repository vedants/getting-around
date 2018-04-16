import os
from flask import Flask, request, redirect, url_for, render_template, jsonify, send_file
from werkzeug.utils import secure_filename

UPLOAD_FOLDER = './audio'
ALLOWED_EXTENSIONS = set(['mp3', 'wav', 'ogg'])
MARKERS = {}

app = Flask(__name__)
app.config['SEND_FILE_MAX_AGE_DEFAULT'] = 0
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

def allowed_file(filename):
    return '.' in filename and \
           filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS

@app.route('/upload_file', methods=['GET', 'POST'])
def upload_file():
    if request.method == 'POST':
        # check if the post request has the file part
        if 'file' not in request.files:
            print('No file part')
            return redirect(request.url)
        file = request.files['file']
        # if user does not select file, browser also
        # submit a empty part without filename
        if file.filename == '':
            print('No selected file')
            return redirect(request.url)
        if file and allowed_file(file.filename):
            filename = secure_filename(file.filename)
            file.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
            coord = request.form['coord']
            MARKERS[coord] = secure_filename(file.filename)
            return redirect(url_for('upload_file',
                                    filename=filename))
    return '''
    <!doctype html>
    <title>Upload new File</title>
    <h1>Upload new File</h1>
    <form method=post enctype=multipart/form-data>
      <p><input type=file name=file>
         <input type=submit value=Upload>
    </form>
    '''
 
@app.route('/get_markers')
def get_markers(): 
 	return jsonify(MARKERS)

@app.route('/get_audio/<filename>')
def send_audio(filename):
    return send_file('./audio/'+filename)


@app.route('/')
def map():
    return app.send_static_file('index.html')

('index.html')

if __name__ == '__main__':
    app.run()
