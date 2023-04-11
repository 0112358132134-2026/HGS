function LogOut() {
    Swal.fire({
        title: '¿Cerrar Sesión?',
        icon: 'question',
        confirmButtonText: 'Sí',
        cancelButtonText: 'No',
        allowOutsideClick: true,
        showCancelButton: true
    }
    ).then((result) => {
        if (result.value) {
            var url = 'https://localhost:7112/Home/Login';
            window.location = url;
        }
    });
}