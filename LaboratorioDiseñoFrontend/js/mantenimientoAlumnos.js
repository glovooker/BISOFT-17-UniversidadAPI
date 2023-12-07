function StudentsView() {

    this.ViewName = "StudentsView";
    this.ApiService = "Alumnos";
    this.CarrersApiService = "Carreras";

    var self = this;
    var carreras = [];

    this.InitView = function () {

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();

        $("#btnCreate").click(function () {
            var view = new StudentsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new StudentsView();
            view.Update();
        });

        $("#btnDelete").click(function () {
            var view = new StudentsView();
            view.Delete();
        });

        $("#btnCancel").click(function () {
            var view = new StudentsView();
            view.CleanForm();
        });

        this.LoadStudentsTable();
    }

    this.Create = function () {
        const formValidation = this.InputsValidation($("#cedula").val(), $("#nombre").val(), $("#telefono").val(), $("#email").val(), $("#fechaNacimiento").val(), $("#carrera").val());
        if (formValidation) {
            var student = {};
            student.nombre = $("#nombre").val();
            student.cedula = $("#cedula").val();
            student.telefono = $("#telefono").val();
            student.email = $("#email").val();
            student.clave = $("#contrasenaTemporal").val();
            student.fechaNacimiento = $("#fechaNacimiento").val();
            student.carrera = $("#carrera").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPI(serviceCreate, student, function () {
                self.LoadStudentsTable();
                Swal.fire({
                    title: 'Alumno creado',
                    text: '¡El Alumno se ha creado exitosamente!',
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
            });
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }

    this.Update = function () {
        const formValidation = this.InputsValidation($("#codigo").val(), $("#nombre").val(), $("#creditos").val(), $("#horasSemanales").val(), $("#ciclo").val());
        if (formValidation) {
            var student = {};

            student.id = $("#id").val();
            student.nombre = $("#nombre").val();
            student.cedula = $("#cedula").val();
            student.telefono = $("#telefono").val();
            student.email = $("#email").val();
            student.clave = $("#contrasenaTemporal").val();
            student.fechaNacimiento = $("#fechaNacimiento").val();
            student.carrera = $("#carrera").val();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, student, function () {
                self.LoadStudentsTable();
                Swal.fire({
                    title: 'Alumno actualizado',
                    text: '¡El Alumno se ha actualizado exitosamente!',
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
            });
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }

    this.LoadStudentsTable = function () {
        self.CleanForm();
        self.LoadCarrersDropdown();

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla
        if ($.fn.DataTable.isDataTable('#tblStudents')) {
            // Destruir la instancia existente
            $('#tblStudents').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'cedula' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'telefono' }
        arrayColumnsData[3] = { 'data': 'email' }
        arrayColumnsData[4] = {
            'data': 'fechaNacimiento',
            'render': function (data) {
                return formatDate(data);
            }
        }
        // arrayColumnsData[4] = { 'data': 'fechaNacimiento' }
        // Load cycle name from the array in which the cycle id is the same as the student cycle id
        arrayColumnsData[5] = {
            'data': null,
            'render': function (data) {
                var carrera = carreras.find(carrera => carrera.codigo === data.carrera);
                return carrera.codigo + ' - ' + carrera.nombre;
            }
        }

        // Crear la instancia de la tabla con los datos vacíos
        var table = $('#tblStudents').DataTable({
            "data": [],
            "columns": arrayColumnsData
        });

        // Actualizar la tabla con los nuevos datos
        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                data.map(function (currentValue, index, arr) {
                    console.log(currentValue.fechaNacimiento);
                });
                table.rows.add(data).draw();
            },
            error: function (error) {
                console.log(error);
            }
        });

        $('#tblStudents tbody').on('click', 'tr', function () {
            var tr = $(this).closest('tr');
            var data = $('#tblStudents').DataTable().row(tr).data();

            $("#id").val(data.id);
            $("#nombre").val(data.nombre);
            $("#cedula").val(data.cedula);
            $("#telefono").val(data.telefono);
            $("#email").val(data.email);
            $("#contrasenaTemporal").val(data.clave);
            $("#fechaNacimiento").val(toLocalDateISOString(data.fechaNacimiento));
            $("#carrera").val(data.carrera);
            $("#divContrasenaTemporal").hide();

            $("#btnCreate").hide();
            $("#btnUpdate").show();
            $("#btnDelete").show();
        });
    }

    this.InputsValidation = (cedula, nombre, telefono, email, contrasenaTemporal, fechaNacimiento, carrera) => {
        return !(cedula === "" || nombre === "" || telefono === "" || email === "" || contrasenaTemporal === "" || fechaNacimiento === "" || carrera === "");
    }

    this.Delete = function () {
        Swal.fire({
            title: 'Confirmar eliminación',
            text: '¿Está seguro de que desea eliminar este Alumno?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sí',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                var ctrlActions = new ControlActions();
                var serviceDelete = this.ApiService + "/Delete?id=" + $("#id").val();

                ctrlActions.DeleteToAPI(serviceDelete, null, function () {
                    self.LoadStudentsTable();
                    Swal.fire({
                        title: 'Alumno eliminado',
                        text: '¡El Alumno se ha eliminado exitosamente!',
                        icon: 'success',
                        confirmButtonText: 'Entendido'
                    });
                });
            } else {
                self.CleanForm();
            }
        });
    }

    this.LoadCarrersDropdown = function () {
        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.CarrersApiService + "/RetrieveAll");

        const select = document.getElementById("carrera");

        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                carreras = data;
                select.innerHTML = "";

                carreras.forEach((dato) => {
                    const option = document.createElement("option");
                    option.text = dato.codigo + ' - ' + dato.nombre;
                    option.value = dato.codigo;
                    select.add(option);
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    this.CleanForm = function () {
        $("#id").val("");
        $("#nombre").val("");
        $("#cedula").val("");
        $("#telefono").val("");
        $("#email").val("");
        $("#contrasenaTemporal").val("");
        $("#fechaNacimiento").val("");
        $("#carrera").val("");

        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#btnDelete").hide();
        $("#divContrasenaTemporal").show();
    }

    function formatDate(dateString) {
        const date = new Date(dateString);
        const day = date.getDate().toString().padStart(2, '0');
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    }

    function toLocalDateISOString(dateString) {
        const date = new Date(dateString);
        const localOffset = date.getTimezoneOffset() * 60000;
        const localTime = new Date(date.getTime() - localOffset);
        return localTime.toISOString().split('T')[0];
    }
}

$(document).ready(function () {
    var view = new StudentsView();
    view.InitView();
})