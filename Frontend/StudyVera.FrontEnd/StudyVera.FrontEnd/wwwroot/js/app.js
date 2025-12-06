window.initApp = function () {


    const themeToggle = document.getElementById('themeToggle');
    const htmlElement = document.documentElement;

    if (themeToggle) {

        let currentTheme = localStorage.getItem('theme') || 'light';
        htmlElement.setAttribute('data-bs-theme', currentTheme);

        if (currentTheme === 'dark') {
            themeToggle.innerHTML = '🌙 Gece Modu';
        } else {
            themeToggle.innerHTML = '☀️ Gündüz Modu';
        }

        function toggleTheme() {
            if (htmlElement.getAttribute('data-bs-theme') === 'light') {
                currentTheme = 'dark';
                themeToggle.innerHTML = '🌙 Gece Modu';
            } else {
                currentTheme = 'light';
                themeToggle.innerHTML = '☀️ Gündüz Modu';
            }

            htmlElement.setAttribute('data-bs-theme', currentTheme);
            localStorage.setItem('theme', currentTheme);
        }

        themeToggle.addEventListener('click', toggleTheme);
    } else {
        console.warn("Tema düğmesi (ID: themeToggle) bulunamadı. Tema mantığı başlatılmadı.");
    }

    const reveals = document.querySelectorAll(".reveal");

    const observer = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                entry.target.classList.add("active");
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.2 });

    if (reveals.length > 0) {
        reveals.forEach((el) => observer.observe(el));
    }
};