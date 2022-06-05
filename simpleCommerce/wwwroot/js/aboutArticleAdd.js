function showPreview(event,displayRow) {
    if (event.target.files.length > 0) {
        for (i = 0; i < event.target.files.length; i++) {
            var src = URL.createObjectURL(event.target.files[i]);
            var block = document.getElementById(displayRow);

            var preview = document.createElement('img');
            preview.src = src;
            preview.className = "previewImage";
            preview.style.display = "block";
            block.appendChild(preview);
        }
    }
}

function onSave() {
    $('#createAboutArticleForm').submit();
}