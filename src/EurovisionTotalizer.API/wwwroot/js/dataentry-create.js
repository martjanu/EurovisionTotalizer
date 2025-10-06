const createType = document.getElementById('create-type');
const additionalField = document.getElementById('create-additional');

function toggleAdditionalField() {
    if (createType.value === 'country') {
        additionalField.style.display = 'block';
    } else {
        additionalField.style.display = 'none';
        additionalField.value = '';
    }
}

toggleAdditionalField();
createType.addEventListener('change', toggleAdditionalField);
