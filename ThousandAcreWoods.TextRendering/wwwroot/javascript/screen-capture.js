const displayMediaOptions = {
    video: {
        displaySurface: "browser",
    },
    audio: false,
    preferCurrentTab: true,
    selfBrowserSurface: "include",
    systemAudio: "exclude",
    surfaceSwitching: "exclude",
    monitorTypeSurfaces: "exclude",
};

let captureStreams = {};
let recorders = {};

async function startCapture() {

    try {
        captureStreams.openStream =
            await navigator.mediaDevices.getDisplayMedia(displayMediaOptions)
    } catch (err) {
        console.error(`Error: ${err}`)
    }
    return captureStreams.openStream
}

async function stopCapture() {
    try {
        let tracks = captureStreams.openStream.getTracks()
        tracks.forEach((track) => track.stop());
        captureStreams.openStream = null;
    } catch (err) {
        console.error(`Error: ${err}`)
    }
}

async function startRecording() {
    // Optional frames per second argument.
    let stream = await navigator.mediaDevices.getDisplayMedia(displayMediaOptions)
    let options = { mimeType: "video/webm; codecs=vp9" };
    let mediaRecorder = new MediaRecorder(stream, options);
    mediaRecorder.ondataavailable = async function (ev) {
        if (ev.data.size > 0) {
            let reader = new FileReader()
            reader.onloadend = function () {
                console.log(reader.result)
                let resultData = reader.result


                let filename = "screen-capture-" + Date.now() + ".webm"
                if (window.navigator.msSaveOrOpenBlob && 1 == 2) {
                    window.navigator.msSaveBlob(resultData, filename)
                }
                else {
                    var elem = window.document.createElement('a')
                    elem.href = resultData
                    elem.download = filename
                    document.body.appendChild(elem)
                    elem.click();
                    document.body.removeChild(elem)
                }
            };
            reader.readAsDataURL(ev.data)

        }
    }

    mediaRecorder.start();
    recorders.currentRecorder = mediaRecorder;
}

async function stopRecording() {
    let recorder = recorders.currentRecorder
    recorder.stop()
    recorder.stream.getTracks()[0].stop();
}




export { startCapture, stopCapture, startRecording, stopRecording }
