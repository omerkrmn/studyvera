window.showErrorToast = (message, imageUrl) => {
    // Eski toast'ı temizle (varsa)
    const old = document.getElementById("error-toast-container");
    if (old) {
        old.remove();
    }

    const container = document.createElement("div");
    container.id = "error-toast-container";
    container.style.position = "fixed";
    container.style.top = "20px";
    container.style.right = "20px";
    container.style.zIndex = "9999";
    container.style.backgroundColor = "white";
    container.style.border = "1px solid #ccc";
    container.style.borderRadius = "8px";
    container.style.padding = "10px";
    container.style.boxShadow = "0 2px 8px rgba(0,0,0,0.2)";
    container.style.maxWidth = "300px";
    container.style.fontFamily = "sans-serif";

    const img = document.createElement("img");
    img.src = imageUrl;
    img.alt = "Error";
    img.style.maxWidth = "100%";
    img.style.display = "block";
    img.style.marginBottom = "8px";

    const text = document.createElement("div");
    text.textContent = message;

    container.appendChild(img);
    container.appendChild(text);

    document.body.appendChild(container);

    // 5 saniye sonra otomatik kapansın
    setTimeout(() => {
        container.remove();
    }, 5000);
};