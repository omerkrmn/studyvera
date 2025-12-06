window.stopwatchInterop = {
    initializeKeyboardControl: function (dotNetHelper) {
        document.addEventListener('keydown', function (event) {
            switch (event.key) {
                case 'a':
                    dotNetHelper.invokeMethodAsync('StartTimer');
                    break;
                case 's':
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
                default:
                    break;
            }
        });
    }
};