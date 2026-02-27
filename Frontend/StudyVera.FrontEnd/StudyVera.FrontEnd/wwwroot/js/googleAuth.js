
window.googleAuth = {
    initializeGoogleSignIn: (dotNetHelper, clientId) => {
        google.accounts.id.initialize({
            client_id: clientId,
            callback: (response) => {
                dotNetHelper.invokeMethodAsync('HandleCredentialResponse', response.credential);
            }
        });
    },
    renderButton: (elementId) => {
        google.accounts.id.renderButton(
            document.getElementById(elementId),
            { theme: 'filled_black', size: 'large', shape: 'pill', text: 'continue_with' } // Buton görünümü
        );
    },
    prompt: () => {
        google.accounts.id.prompt(); // One Tap deneyimi için
    }
};