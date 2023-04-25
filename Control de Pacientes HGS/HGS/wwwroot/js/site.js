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
            
            $.ajax({
                url: '/Home/Logout',
                type: 'POST',
                success: function (response) {
                    
                    if (response.success) {
                        
                        window.location = 'https://localhost:7112/Home/Login';
                    } else {
                        
                        Swal.fire({
                            title: 'Error',
                            text: 'No se pudo cerrar la sesión. Inténtalo de nuevo.',
                            icon: 'error'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Error',
                        text: 'Error al comunicarse con el servidor. Inténtalo de nuevo.',
                        icon: 'error'
                    });
                }
            })
        }
    });
}