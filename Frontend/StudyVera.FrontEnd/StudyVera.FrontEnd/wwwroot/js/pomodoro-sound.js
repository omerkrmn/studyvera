window.playNotificationSound = function (customMessage) {
    // Eğer mesaj boşsa varsayılan bir şey söyle
    const textToSpeak = customMessage || 'Mola bitti, odaklanma zamanı!';
    const utterance = new SpeechSynthesisUtterance(textToSpeak);
    utterance.lang = 'tr-TR';

    utterance.onend = function () {
        const audioCtx = new (window.AudioContext || window.webkitAudioContext)();

        function playAlarmNote(startTime, freq) {
            const osc = audioCtx.createOscillator();
            const gain = audioCtx.createGain();
            osc.connect(gain);
            gain.connect(audioCtx.destination);
            osc.type = 'triangle';
            osc.frequency.setValueAtTime(freq, startTime);
            gain.gain.setValueAtTime(0, startTime);
            gain.gain.linearRampToValueAtTime(0.2, startTime + 0.05);
            gain.gain.exponentialRampToValueAtTime(0.0001, startTime + 0.4);
            osc.start(startTime);
            osc.stop(startTime + 0.4);
        }

        const now = audioCtx.currentTime;
        playAlarmNote(now, 523.25);
        playAlarmNote(now + 0.2, 659.25);
        playAlarmNote(now + 0.4, 783.99);
        playAlarmNote(now + 0.7, 1046.50);
    };

    window.speechSynthesis.speak(utterance);
};