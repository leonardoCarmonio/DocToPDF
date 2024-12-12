const fileInput = document.querySelector('.file-input');
const form = document.querySelector('.form');

fileInput.addEventListener('change', () => {    
    if (fileInput.files.length > 0) {
        const fileName = document.querySelector('.file-name');
        fileName.textContent = fileInput.files[0].name;
    }
});

form.addEventListener('submit', (event) => {
    event.preventDefault();
    hideMessage();
    
    const formData = new FormData();
    const file = fileInput.files[0];
    formData.append('file', file, fileInput.files[0].name);
    
    const url = 'http://localhost:5075/upload'
    fetch(url, {
        method: 'POST',
        body: formData
    }).then(response => {
        if (response.ok) 
            return response.json();
        else 
            console.error('Falha no upload');
    }).then(data => {
        const message = 'Upload realizado com sucesso !';
        const typeMessage = 'is-success';
        showMessage(message, typeMessage);
    }).catch(error => {
        console.error(error);
        const message = 'Erro ao realizar o upload, tente novamente !'
        const typeMessage = 'is-danger';
        showMessage(message, typeMessage);
    }); 
});

showMessage = (message, type) => {
    let div = document.createElement('div');
    div.classList.add('notification', type);
    div.textContent = message;
    
    let button = document.createElement('button');
    button.classList.add('delete');
    button.addEventListener('click', hideMessage);
    
    div.appendChild(button);
    document.querySelector('#message').appendChild(div);
}

hideMessage = () => 
    document.querySelector('.notification')?.remove();
