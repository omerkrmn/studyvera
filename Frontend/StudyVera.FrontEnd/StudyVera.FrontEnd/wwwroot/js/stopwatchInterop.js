window.stopwatchInterop = {
    initializeKeyboardControl: function (dotNetHelper) {
        if (window.stopwatchListenerAdded) return;

        document.addEventListener('keydown', function (event) {
            if (event.target.tagName === 'INPUT' || event.target.tagName === 'TEXTAREA') return;

            switch (event.key) {
                case 'a':
                case 'A':
                    dotNetHelper.invokeMethodAsync('StartTimer');
                    break;
                case 's':
                case 'S':
                    dotNetHelper.invokeMethodAsync('StopTimer');
                    break;
                case 'Backspace':
                    dotNetHelper.invokeMethodAsync('ResetTimer');
                    break;
                case 'Enter':
                    if (event.shiftKey) {
                        dotNetHelper.invokeMethodAsync('LapClear');
                    } else {
                        dotNetHelper.invokeMethodAsync('Lap');
                    }
                    event.preventDefault();
                    break;
            }
        });
        window.stopwatchListenerAdded = true;
    }
};