function loadPredictions(participantName) {
    fetch(`/Predictions/GetPredictions?participantName=${encodeURIComponent(participantName)}`)
        .then(response => response.json())
        .then(data => {
            if (data.success === false) return;

            // Reset visi selectai į "none"
            document.querySelectorAll("select[name^='SemiPredictions']").forEach(s => s.value = "none");
            document.querySelectorAll("select[name^='FinalPredictions']").forEach(s => s.value = "none");

            // Užpildyti pusfinalių spėjimus
            for (const pred of data.semi) {
                const selectEl = document.querySelector(`select[name="SemiPredictions[${pred.country}]"]`);
                if (selectEl) selectEl.value = pred.type;
            }

            // Užpildyti finalo spėjimus
            for (const pred of data.final) {
                const selectEl = document.querySelector(`select[name="FinalPredictions[${pred.country}]"]`);
                if (selectEl) {
                    if (pred.type === "ExactPlaceInFinal" && pred.placeInFinal > 0) {
                        selectEl.value = pred.placeInFinal;
                    } else if (pred.type === "Last3InFinal") {
                        selectEl.value = "Bottom3";
                    } else {
                        selectEl.value = "none";
                    }
                }
            }
        })
        .catch(err => console.error("Klaida užkraunant spėjimus:", err));
}

function updateTableVisibility() {
    const value = document.getElementById("tableSelector").value;
    const tables = ["table-semi1", "table-semi2", "table-final"];

    tables.forEach(id => {
        document.getElementById(id).style.display = "none";
    });

    if (value === "all") {
        tables.forEach(id => {
            document.getElementById(id).style.display = "table";
        });
    } else {
        document.getElementById("table-" + value).style.display = "table";
    }
}

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("participant").addEventListener("change", function () {
        loadPredictions(this.value);
    });

    document.getElementById("tableSelector").addEventListener("change", updateTableVisibility);

    var participantSelect = document.getElementById("participant");
    if (participantSelect.value) {
        loadPredictions(participantSelect.value);
    }
    document.getElementById("tableSelector").value = "all";
    updateTableVisibility();
});
