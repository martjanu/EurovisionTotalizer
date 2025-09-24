document.getElementById("entity-select").addEventListener("change", function () {
    const selectedName = this.value;
    const type = this.options[this.selectedIndex].dataset.type;

    if (!selectedName) {
        document.getElementById("update-form").style.display = "none";
        return;
    }

    document.getElementById("entity-type").value = type;

    if (type === "country") {
        document.getElementById("country-fields").style.display = "block";
        document.getElementById("update-form").style.display = "block";

        fetch(`/DataEntry/GetCountry?name=${encodeURIComponent(selectedName)}`)
            .then(r => r.json())
            .then(data => {
                document.getElementById("country-name").value = data.name;
                document.getElementById("country-infinal").checked = data.isInFinal;
                document.getElementById("country-semifinal").value = data.semiFinal;
                document.getElementById("country-place").value = data.placeInFinal ?? "";
            });
    } else {
        document.getElementById("country-fields").style.display = "none";
    }
});
