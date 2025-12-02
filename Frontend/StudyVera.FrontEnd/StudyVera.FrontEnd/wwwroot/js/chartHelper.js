// Zoom plugin'ini Chart.js'e kaydet
Chart.register(window.ChartZoom);

window.chartInstances = {};

window.drawLineChart = (canvasId, labels, data, chartLabel) => {

    if (window.chartInstances[canvasId]) {
        try { window.chartInstances[canvasId].destroy(); } catch { }
    }

    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");

    const total = labels.length;    // 365
    const visible = 10;             // ilk etapta görünen

    window.chartInstances[canvasId] = new Chart(ctx, {
        type: "line",
        data: {
            labels,
            datasets: [{
                label: chartLabel,
                data,
                borderColor: "rgba(75, 192, 192, 1)",
                backgroundColor: "rgba(75, 192, 192, 0.3)",
                borderWidth: 2,
                tension: 0.25,
                pointRadius: 3
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,

            scales: {
                x: {
                    min: total - visible,
                    max: total - 1
                }
            },
            plugins: {
                zoom: {
                    pan: {
                        enabled: true,
                        mode: "x"
                    },
                    zoom: {
                        wheel: { enabled: true },
                        pinch: { enabled: true },
                        mode: "x"
                    }
                }
            }
        }
    });
};
